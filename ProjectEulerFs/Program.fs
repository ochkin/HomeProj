namespace Ochkin.ProjectEuler
module ProjectEulerMain =

    [<EntryPoint>]
    let main argv = 
        printfn "%A" (prb60dp5.solve 1)
        //System.Console.WriteLine(prb60dp.test |> Seq.length)
//        prb60dp.halfSurface
//            |> Seq.take 20
//            |> Seq.iter (fun i -> System.Console.WriteLine(i.ToString()))
            // |> ignore
        System.Console.ReadKey() |> ignore
        0 // return an integer exit code
