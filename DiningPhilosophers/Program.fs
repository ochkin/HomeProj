open Classes
open System
open System.Threading

let private TryEat (x: Philosopher option) =
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
                    //printfn "%s is done eating." philosopher.Name
                    return true
                else
                    //printfn "%s didn't get a fork :-(" philosopher.Name
                    return false
            finally
                l.Release x
                r.Release x
        }
      | None ->
        failwith "Invlid operation. Empty philosopher."

let runWithSema philosophers =
    use semaphor = new FifoSemaphore<string>(2, 2)
    let runPhilosopher (person: Philosopher option) = async  {
        while true do
            do! semaphor.WaitAsync person.Value.Name
            do! TryEat person |> Async.Ignore
            semaphor.Release()
        }
    philosophers |> Seq.map runPhilosopher |> Async.Parallel |> Async.Ignore |> Async.Start

type Message = Go

let runWithMailboxProcessor philosophers =
    let guys = philosophers |> Seq.map Option.get |> Array.ofSeq
    let runner (another: Lazy<MailboxProcessor<Message>>) startAt (me: MailboxProcessor<Message>) =
        let rec loop pos = async {
            let! msg = me.Receive ()
            match msg with
            | Go ->
                do! Array.get guys pos |> Some |> TryEat |> Async.Ignore
                another.Value.Post Go
                do! loop ((pos + 1) % 5)
            }
        loop startAt
    let rec first : Lazy<MailboxProcessor<Message>> =
        lazy MailboxProcessor<Message>.Start (runner second 0)
    and second : Lazy<MailboxProcessor<Message>> =
        lazy MailboxProcessor<Message>.Start (runner first 2)
    first.Value.Post Go
    second.Value.Post Go
    second.Value.Post Go

[<EntryPoint>]
let main argv =
    let N = 5
    let forks = List.init N (fun i -> new Fork())
    let philosophers =
        List.pairwise (List.last forks :: forks)
            |> List.mapi (fun i pairOfForks -> new Philosopher("P"+i.ToString(),
                                                fst pairOfForks,
                                                snd pairOfForks,
                                                TimeSpan.FromSeconds(2.0)))
            |> List.map Some
    printfn "%i philosophers dining." (List.length philosophers)
    
    runWithMailboxProcessor philosophers

    Console.ReadKey () |> ignore
    Async.CancelDefaultToken ()
    0