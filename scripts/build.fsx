#load ".fake/build.fsx/intellisense.fsx"
open Fake.Docker
open Fake.Core
open Fake.DotNet
open Fake.IO
open Fake.IO.FileSystemOperators
open Fake.IO.Globbing.Operators
open Fake.Core.TargetOperators
open Fake.Tools.Git
open Fake.Api

type Branch =
    | Develop of sha: string
    | Feature of name: string * sha: string
    | Release of version: string
    | Master of SemVerInfo
    | Other of name: string * sha: string

type AzureDevOpsData = { branch: Branch; buildId: string; buildNumber: string; repoName: string}
type BuildType = 
    | AzureDevOps of data: AzureDevOpsData
    | Default of branch: Branch

type Tag = string * bool
type Tags = Tag seq

let dockerUsername = lazy(Environment.environVarOrFail "DOCKER_USERNAME")
let dockerOrganization = lazy(Environment.environVarOrFail "DOCKER_ORGANIZATIOM")
let dockerPassword = lazy(Environment.environVarOrFail "DOCKER_PASSWORD")

type DockerFile = { path: string; imageName: string }

let files = [{ path = "../Dockerfile"; imageName = "catalog-api" }]

let getBranch (sha: string)(branchName: string) =    
    match branchName with
    | "master" -> 
        let releaseNotes = "../RELEASE_NOTES.md" |> ReleaseNotes.load
        Master(releaseNotes.SemVer)
    | "develop" -> 
        Develop(sha)
    | branch when String.startsWith("feature") branch -> 
        let featureName = branch.Replace("feature/", "")
        Feature(featureName, sha)
    | branch when String.startsWith("release") branch -> 
        let version = branch.Replace("release/", "")
        Release(version)
    | branch ->  
        Other(branch, sha)


let buildType = lazy ( 
    match Environment.environVarOrNone "BUILD_BUILDNUMBER" with 
    | Some bId -> 
        let branch = getBranch (Information.getCurrentSHA1(".")) (Information.getBranchName("."))
        let buildNumber = Environment.environVarOrDefault "BUILD_NUMBER" "1"
        let repoName = Environment.environVarOrDefault "REPOSITORY_NAME" "Unknown_Repo"
        AzureDevOps({ branch = branch; buildId = bId; buildNumber = buildNumber; repoName = repoName})
    | None -> 
        let branch = getBranch (Information.getCurrentSHA1(".")) (Information.getBranchName("."))
        Default(branch)
)

let tags: Lazy<Tags> = lazy (
    let tag branch id =
        match branch with
        | Master version -> 
            seq [("latest", true); (sprintf "%u" version.Major, true); (sprintf "%u.%u" version.Major version.Minor, true); (version.AsString, false);]
        | Release version -> 
            seq [(version + (sprintf "-beta%s" (if String.isNullOrEmpty(id) then "" else "-" + id )), false)]
        | Develop sha -> 
            seq [("develop", true); (sprintf "develop.%s" sha, false)]
        | Feature(name, sha) -> 
            seq [ (sprintf "feature.%s.%s" name sha, false)]
        | Other(branch, sha) -> 
            seq [ (sprintf "%s.%s" branch sha, false) ]  

    match buildType.Value with
    | AzureDevOps({ branch = branch; buildId = buildId}) ->
        tag branch buildId    
    | Default(branch) ->    
        tag branch "" 
)


Target.create "PrintInfo" (fun _ ->
    printfn "Use branch %A" (buildType.Value)
    printfn "Tags %A" (tags.Value)
)

Target.create "Docker:Build" (fun _ ->
    for image in files do
        Docker.build(fun x -> { x with File = image.path; ImageName = image.imageName; Context = "../" }) |> ignore
)

Target.create "Docker:Tag" (fun _ ->
    for image in files do
        for (tag, _) in tags.Value do
            Docker.tag(fun x -> { x with Organization = dockerOrganization.Value; ImageName = image.imageName; ImageTag = "latest"; Tag = tag }) |> ignore
)

Target.create "Docker:Login" (fun _ ->
    Docker.login(fun x ->{ x with Username = dockerUsername.Value; Password = dockerPassword.Value }) |> ignore
)

Target.create "Docker:Push" (fun _ ->
    for image in files do
        let dockerHubTags = DockerHub.loginAndGetTagList dockerUsername.Value dockerPassword.Value image.imageName |> Async.RunSynchronously
        for (tag, shouldOverwrite) in tags.Value |> Seq.sortBy(fun (_, overwrite) -> overwrite) do
            let canPush = shouldOverwrite ||
                        (match dockerHubTags with
                         | Ok(t) -> 
                             match t |> Seq.tryFind(fun x -> x.name = tag) with
                             | Some _ -> false
                             | None -> true
                         | _ -> true)
            if canPush then
                Docker.push(fun x -> { x with UserName = dockerUsername.Value; Password = dockerPassword.Value; Organization = dockerOrganization.Value; ImageName = image.imageName; Tag = tag; }) |> ignore
            else
                failwith (sprintf "Can't overwrite image [%s] tag [%s] on docker hub!!!!!!!!!!!!!!!!!!!!!!!!!" image.imageName tag)
)

Target.create "Docker:Clean" (fun _ ->
    let images = Docker.images(id).Result
    for image in files do
        for (tagName, _) in tags.Value  do
            match images |> Seq.tryFind (fun x -> x.Repository.EndsWith(image.imageName) && x.Tag = tagName) with
            | Some image -> 
                Docker.rmi(fun x -> { x with Force = true; ImageId = image.ID; }) |> ignore
            | None -> 
                Trace.traceErrorfn "Image Not Found for i: %A and tag %A" image tagName 
)

Target.create "All" ignore

"PrintInfo"
    ==> "Docker:Build"
    ==> "Docker:Tag"
    ==> "Docker:Login"
    ==> "Docker:Push"
    ==> "Docker:Clean"
    ==> "All"


Target.runOrDefault "All"

