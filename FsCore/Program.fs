open System

[<EntryPoint>]
let main argv = 
    printfn "%A" (prb74.solve())
    Console.ReadKey() |> ignore
    0 // return an integer exit code
