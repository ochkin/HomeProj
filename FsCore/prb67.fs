module prb67

let solve () =
    let parseLine (line: System.String) = line.Split() |> Seq.map int
    match System.IO.File.ReadLines "p067_triangle.txt" |> Seq.rev |> Seq.map parseLine |> List.ofSeq with
        | bottom :: lines ->
            let folder state t =
                Seq.pairwise state
                    |> Seq.map (fun (a, b) -> max a b)
                    |> Seq.map2 (fun t1 t2 -> t1 + t2) t
            List.fold folder bottom lines
        | [] -> invalidOp "Empty file"