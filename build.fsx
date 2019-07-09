#load ".fake/build.fsx/intellisense.fsx"
open Fake.Core
open Fake.DotNet
open Fake.IO
open Fake.IO.FileSystemOperators
open Fake.IO.Globbing.Operators
open Fake.Core.TargetOperators
open Fake.Tools.Git


type Branch =
    | Develop of sha: string
    | Feature of name: string * sha: string
    | Release of version: string
    | Master of version: string
    | Other of name: string * sha: string

type BuildType = 
    | AzureDevOps of branch: Branch * buildId: string
    | Default of branch: Branch

type Tag = string
type Tags = Tag seq

let dockerUsername = lazy(Environment.environVarOrFail "DOCKER_USERNAME")
let dockerPassword = lazy(Environment.environVarOrFail "DOCKER_PASSWORD")

type DockerFile = { path: string; imageName: string }

let files = [{ path = "./src/Catalog.Api/Dockerfile"; imageName = "catalog" }]

let getBranch (sha: string)(branchName: string) =    
    match branchName with
    | "master" -> 
        let releaseNotes = "./RELEASE_NOTES.md" |> ReleaseNotes.load
        Master(releaseNotes.SemVer.AsString)
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
        AzureDevOps(branch, bId)
    | None -> 
        let branch = getBranch (Information.getCurrentSHA1(".")) (Information.getBranchName("."))
        Default(branch)
)

let tags: Lazy<Tags> = lazy (
    match buildType.Value with
    | AzureDevOps(branch, buidId) ->
        match branch with
        | Master version -> 
            seq ["latest"; version]
        | Release version -> 
            seq [version + sprintf "-beta.%s" buidId]
        | Develop sha -> 
            seq ["develop"; sprintf "develop.%s" sha]
        | Feature(name, sha) -> 
            seq [ sprintf "feature.%s.%s" name sha]
        | Other(branch, sha) -> 
            seq [ sprintf "%s.%s" branch sha ]
    | Default(branch) ->
        match branch with
        | Master version -> 
            seq [version]
        | Release version -> 
            seq [version + "-beta"]
        | Develop sha -> 
            seq [sprintf "develop.%s" sha]
        | Feature(name, sha) -> 
            seq [ sprintf "feature.%s.%s" name sha]
        | Other(branch, sha) -> 
            seq [ sprintf "%s.%s" branch sha ]     
)


Target.create "PrintInfo" (fun _ ->
    printfn "Use branch %A" (buildType.Value)
    printfn "Tags %A" (tags.Value)
)

Target.create "Docker:Build" (fun _ ->
    for image in files do
        Fake.Docker.Docker.build(fun x -> { x with File = image.path; ImageName = image.imageName; Context = "./" }) |> ignore
)

Target.create "Docker:Tag" (fun _ ->
    for image in files do
        for tag in tags.Value do
            Fake.Docker.Docker.tag(fun x -> { x with Organization = dockerUsername.Value; ImageName = image.imageName; ImageTag = "latest"; Tag = tag }) |> ignore
)

Target.create "Clean" (fun _ ->
    !! "src/**/bin"
    ++ "src/**/obj"
    |> Shell.cleanDirs 
)

Target.create "Build" (fun _ ->
    !! "src/**/*.*proj"
    |> Seq.iter (DotNet.build id)
)

Target.create "All" ignore

"Clean"
  ==> "Build"
  ==> "All"

"PrintInfo"
    ==> "Docker:Build"
    ==> "Docker:Tag"

Target.runOrDefault "Docker:Tag"
