open System

[<EntryPoint>]
let main argv = 
    printfn "%A" (prb74.solve())
    Console.ReadKey() |> ignore
    0 // return an integer exit code

(*

    let start = System.DateTime.Now
    printfn "%A" (prb73.solve ())
    let finish = System.DateTime.Now
    printfn "%A" (finish - start)

    System.Console.Beep ()

*)