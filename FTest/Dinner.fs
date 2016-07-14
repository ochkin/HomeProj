module Dinner

open Swensen.Unquote.Assertions
open Xunit
open Classes
open System 

let private fork = new Fork()

[<Fact>]
let testInterlocked () =
    let dude1 = new Philosopher("Dude1", fork, fork, TimeSpan.FromSeconds(10.0))
    let dude2 = new Philosopher("Dude2", fork, fork, TimeSpan.FromSeconds(10.0))

    test <@ System.Object.ReferenceEquals(None, None) @>

    test <@ System.Object.ReferenceEquals(dude1, dude1) @>

    test <@ not (System.Object.ReferenceEquals(dude1, dude2)) @>

    test <@ not <| System.Object.ReferenceEquals(Some dude1, Some dude1) @>

    let tmp = Some dude1
    test <@ System.Object.ReferenceEquals(tmp, tmp) @>


[<Fact>]
let testFork () =
    test <@ fork.Available @>

    let dude = new Philosopher("Dude", fork, fork, TimeSpan.FromSeconds(1.0)) |> Some
    test <@ fork.Take dude @>
    test <@ not fork.Available @>

    let gal = new Philosopher("Gal", fork, fork, TimeSpan.FromHours(1.0)) |> Some
    test <@ not <| fork.Take gal @>

    fork.Release gal 
    test <@ not fork.Available @>
    
    fork.Release dude
    test <@ fork.Available @>