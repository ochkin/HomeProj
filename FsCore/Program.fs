open System

[<EntryPoint>]
let main argv = 
    let start = DateTime.Now
    printfn "%A" <| prb75.solve ()
    let finish = DateTime.Now
    printfn "%A" <| finish - start

    Console.Beep ()
    Console.ReadKey() |> ignore
    0 // return an integer exit code


