namespace Classes

open System.Threading
open System.Collections.Concurrent

type FifoSemaphore<'T when 'T: equality>(initAvailable, maxAllowed) =
    let sema = new SemaphoreSlim(initAvailable, maxAllowed)
    let q = new ConcurrentQueue<'T>()
    let qCha2 = new ManualResetEvent(false)
    let waitInQueue (id: 'T) = async {
        while not (match q.TryPeek () with success, threadId -> success && (threadId = id)) do
            do! Async.AwaitWaitHandle qCha2 |> Async.Ignore
        }

    member this.WaitAsync (id: 'T) = async {
        q.Enqueue id
        do! waitInQueue id

        let success, threadId = q.TryDequeue () 
        if (not success) || threadId <> id then
            failwith "Error in FifoSemaphore."

        do! Async.AwaitTask (sema.WaitAsync())
        qCha2.Set() |> ignore
        }

    member this.Release () =
        sema.Release () |> ignore
        qCha2.Set() |> ignore

    interface System.IDisposable with 
        member this.Dispose() = 
            sema.Dispose()


