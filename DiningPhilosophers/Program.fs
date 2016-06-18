open Classes
open System
open System.Threading

[<EntryPoint>]
let main argv =
    let N = 5
    let forks = List.init N (fun i -> new Fork())
    let philosophers =
        List.pairwise (List.last forks :: forks)
            |> List.mapi (fun i pairOfForks -> new Philosopher("P"+i.ToString(),
                                                fst pairOfForks,
                                                snd pairOfForks,
                                                TimeSpan.FromSeconds(3.0)))
    printfn "%i philosophers dining." (List.length philosophers)
    use semaphor = new System.Threading.SemaphoreSlim(2, 2)
    let runPhilosopher (person: Philosopher) = async  {
        while true do
            do! (semaphor.WaitAsync() |> Async.AwaitTask)
            do! (Async.Ignore (person.Eat()))
            semaphor.Release() |> ignore
        }
//    let token = new CancellationToken()
    philosophers |> Seq.map runPhilosopher |> Async.Parallel |> Async.Ignore |> Async.Start
//    Async.Start(run, token)
    Console.ReadKey () |> ignore
    Async.CancelDefaultToken ()

    0 // return an integer exit code
