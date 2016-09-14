open System
open prb74

[<EntryPoint>]
let main argv = 
    printfn "%A" (solve())
    Console.ReadKey() |> ignore
    0 // return an integer exit code
