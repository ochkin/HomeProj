module prb70

let phi = prb69.getAllPhi 10000000

let arePerm a b =
    let aStr = a.ToString()
    let bStr = b.ToString()
    aStr.Length = bStr.Length && 0 = Seq.compareWith (fun (x:char) (y:char) -> x.CompareTo y) (Seq.sort aStr) (Seq.sort bStr)

let solve () =
    phi |> Seq.mapi (fun i ph -> i, ph)
        |> Seq.skip 2
        |> Seq.where (fun (n, ph) -> arePerm n ph)
        |> Seq.minBy (fun (n, ph) -> float n / float ph)
        |> fst
//  8319823
//00:00:50.1215556
