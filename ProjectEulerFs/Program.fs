
module ProjectEulerMain

[<EntryPoint>]
let main argv =
    let start = System.DateTime.Now
    printfn "%A" (prb72.solve ())
    let finish = System.DateTime.Now
    printfn "%A" (finish - start)

    System.Console.Beep ()
    System.Console.ReadKey () |> ignore
    0
