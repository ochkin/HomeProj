namespace Ochkin.ProjectEuler

module prb59 =
    open System.IO
    open System.Linq
    let solve59 =
        let test = File.ReadAllText("p059_cipher.txt").Split(',') |> Seq.map byte |> Seq.toArray

        let first = test |> Seq.mapi (fun i x -> (i,x)) |> Seq.choose (fun (i,x) -> if (i+2) % 3 = 0 then Some(x) else None)
        let second = test |> Seq.mapi (fun i x -> (i,x)) |> Seq.choose (fun (i,x) -> if (i+1) % 3 = 0 then Some(x) else None)
        let third = test |> Seq.mapi (fun i x -> (i,x)) |> Seq.choose (fun (i,x) -> if i % 3 = 0 then Some(x) else None)
        let statistics sequence =
            System.Console.WriteLine("************************************************************************")
            System.Console.WriteLine(sequence |> Seq.map string |> String.concat ",")
            System.Console.WriteLine()
            let temp = sequence |> Seq.countBy (fun x -> x) |> Seq.sortBy snd
            for item in temp do
                System.Console.Write("{0}({1}) ", fst item, (snd item))
            System.Console.WriteLine()
            let cnt= temp |> Seq.map snd |> Seq.sum |> string
            System.Console.WriteLine((["Total ",(cnt)," items.*************************************************"]) |> System.String.Concat)
            System.Console.WriteLine()
        statistics first
        statistics second
        statistics third

        let code = [|103uy; 111uy; 100uy|]
        let decoded = test |> Seq.mapi (fun i x -> x ^^^ (Array.get code (i % 3)))
        let mutable i = 0
        for symbol in decoded do
            System.Console.Write(char symbol)
            i <- i + 1
        System.Console.WriteLine()
        decoded |> Seq.map int |> Seq.sum
