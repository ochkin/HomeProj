
let char2byte (c:char) =
    let raw = byte c
    raw - 48uy

let bytes = 567 |> sprintf "%i" |> Seq.map byte |> Array.ofSeq

printfn "%A" bytes


[0 .. 9]
    |> List.map (sprintf "%i")
    |> List.map (fun str -> str.[0])
    |> List.map char2byte