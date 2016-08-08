module testUtil

open Util
open Swensen.Unquote.Assertions
open Xunit

[<Fact>]
let testSimplify () =
    //test <@ {n=1; d=3} = Simplify {n=6098; d=3*6098} @>
    Assert.Equal({n=1; d=3}, Simplify {n=6098; d=3*6098})
    //test <@ {n=1; d=3} <> Simplify {n=6098; d=3*6098-1} @>
    Assert.NotEqual({n=1; d=3}, Simplify {n=6098; d=3*6098-1})
//    test <@ {n=1; d=3} <> Simplify {n=6098; d=2*6098} @>
    Assert.NotEqual({n=1; d=3}, Simplify {n=6098; d=2*6098})

[<Fact>]
let testfactorial () =
    test <@ 120 = factorial 5 @>
