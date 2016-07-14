namespace Classes

open System.Threading
open System

type Fork () =
    let mutable takenBy = None
    member x.Available with get() = Option.isNone takenBy
    member x.Take (person) =
        ignore <| Interlocked.CompareExchange<Philosopher option>(&takenBy, person, None)
        takenBy = person
    member x.Release (person) =
        ignore <| Interlocked.CompareExchange<Philosopher option>(&takenBy, None, person)
        //x.Available
        
and Philosopher(name: string, leftFork: Fork, rightFork: Fork, eatingTime: TimeSpan) =
    member x.Name = name
    //member x.CanEat = leftFork.Available && rightFork.Available
    member x.Left = leftFork
    member x.Right = rightFork
    member x.EatingTime = eatingTime
