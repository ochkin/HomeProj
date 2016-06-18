namespace Classes

open System.Threading
open System

type Fork () =
    let mutable takenBy = None
    member x.Available with get() = Option.isNone takenBy
    member x.Take (person) =
        ignore <| Interlocked.CompareExchange<Philosopher option>(&takenBy, Some person, None)
        takenBy = Some person
    member x.Release (person) =
        ignore <| Interlocked.CompareExchange<Philosopher option>(&takenBy, None, Some person)
        //x.Available
        
and Philosopher(name: string, leftFork: Fork, rightFork: Fork, eatingTime: TimeSpan) =
    member x.Name = name
    member x.CanEat = leftFork.Available && rightFork.Available
    member x.Eat () = async {
        if leftFork.Take x && rightFork.Take x then
            printfn "%s is eating for %A..." name eatingTime
            do! Async.Sleep (int eatingTime.TotalMilliseconds)
            printfn "%s is done eating." name
            leftFork.Release x
            rightFork.Release x
            return true
        else
            leftFork.Release x
            rightFork.Release x
            return false
        }