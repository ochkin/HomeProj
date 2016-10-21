module prb71

open Util
let threshold = { N=3UL; D=7UL}

let solve () =
    seq {2UL .. 1000000UL }
        |> Seq.map (fun d -> { N=threshold.N * d / threshold.D; D=d })
        |> Seq.map Simplify
        |> Seq.where (fun f -> compare f threshold < 0)
        |> Seq.fold (fun state t -> match state with Some f -> Some (max f t) | None -> Some t) None

//Some {numerator = 428570UL;
//      denominator = 999997UL;}
//00:00:00.7060195
//
//
