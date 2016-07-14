namespace Classes

open System.Threading
open System.Collections.Concurrent

type FifoSemaphore<'T when 'T: equality>(initAvailable, maxAllowed) =
    let sema = new SemaphoreSlim(initAvailable, maxAllowed)
    let q = new ConcurrentQueue<'T>()
    //let qChange = new Event<_>()
    let qCha2 = new ManualResetEvent(false)
    let waitQ (id: 'T) = async {
        //let! x = Async.StartChild <| Async.AwaitEvent (qChange.Publish)
        //let mutable wait = x
        while not (match q.TryPeek () with success, threadId -> success && (threadId = id)) do
            printfn "%A is waiting for event..." id
//            do! wait
//            let! temporary = Async.AwaitEvent (qChange.Publish) |> Async.StartChild
//           c wait <- temporary
            do! Async.AwaitWaitHandle qCha2 |> Async.Ignore
        }
    member this.WaitAsync (id: 'T) = async {
        q.Enqueue id
        do! waitQ id

        let success, threadId = q.TryDequeue () 
        if (not success || threadId <> id) then
            failwith "Error in FifoSemaphore."

        do! Async.AwaitTask (sema.WaitAsync())
//        qChange.Trigger ()
        qCha2.Set() |> ignore
        }
    member this.Release () =
        sema.Release () |> ignore
        qCha2.Set() |> ignore
    interface System.IDisposable with 
        member this.Dispose() = 
            sema.Dispose()


