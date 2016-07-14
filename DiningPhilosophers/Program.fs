open Classes
open System
open System.Threading

let private Eat (x: Philosopher option) =
    match x with
      | Some philosopher ->
        let l = philosopher.Left
        let r = philosopher.Right
        async {
            try
                if l.Take x && r.Take x then
                    let time = int philosopher.EatingTime.TotalMilliseconds
                    printfn "%s is eating for %A..." philosopher.Name time
                    do! Async.Sleep time
                    printfn "%s is done eating." philosopher.Name
                    return true
                else
                    printfn "%s didn't get a fork :-(" philosopher.Name
                    return false
            finally
                l.Release x
                r.Release x
        }
      | None ->
        failwith "Invlid operation. Empty philosopher."

[<EntryPoint>]
let main argv =
    let N = 5
    let forks = List.init N (fun i -> new Fork())
    let philosophers =
        List.pairwise (List.last forks :: forks)
            |> List.mapi (fun i pairOfForks -> new Philosopher("P"+i.ToString(),
                                                fst pairOfForks,
                                                snd pairOfForks,
                                                TimeSpan.FromSeconds(10.0)))
            |> List.map Some
    printfn "%i philosophers dining." (List.length philosophers)
    use semaphor = new FifoSemaphore<string>(2, 2)
    let runPhilosopher (person: Philosopher option) = async  {
        while true do
            do! semaphor.WaitAsync(person.Value.Name)
            do! Eat person |> Async.Ignore
            semaphor.Release() |> ignore
        }
    philosophers |> Seq.map runPhilosopher |> Async.Parallel |> Async.Ignore |> Async.Start

    Console.ReadKey () |> ignore
    Async.CancelDefaultToken ()
    0