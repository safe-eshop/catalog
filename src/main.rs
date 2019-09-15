use cpuprofiler::PROFILER;

#[derive(Debug)]
struct Product {
    id: String,
    q: i32
}
fn main() {
    PROFILER.lock().unwrap().start("./my-prof.profile");
    let mut a = Vec::new();
    a.push(Product{ id: String::from("hasdhkjdsa"), q: 3});
    a.push(Product{ id: String::from("hasdhkjdsa"), q: 4});
    let b = a.iter().map(|x| x.q).filter(|&x| x == 3).collect::<Vec<_>>();
    PROFILER.lock().unwrap().stop();
    println!("Hello, world! {:?}", b);
}
