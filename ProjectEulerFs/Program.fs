namespace Ochkin.ProjectEuler
module ProjectEulerMain =

    [<EntryPoint>]
    let main argv =
        let start = System.DateTime.Now
        printfn "%A" (prb60dp4.solve 1)
        let finish = System.DateTime.Now
        printfn "%A" (finish - start)
        //System.Console.WriteLine(prb60dp.test |> Seq.length)
//        prb60dp.halfSurface
//            |> Seq.take 20
//            |> Seq.iter (fun i -> System.Console.WriteLine(i.ToString()))
            // |> ignore
        System.Console.ReadKey() |> ignore
        0 // return an integer exit code
