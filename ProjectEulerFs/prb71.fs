module prb71

open Util
let threshold = { numerator=3UL; denominator=7UL}

let solve () =
    seq {2UL .. 1000000UL }
        |> Seq.map (fun d -> { numerator=threshold.numerator * d / threshold.denominator; denominator=d })
        |> Seq.map Simplify
        |> Seq.where (fun f -> compare f threshold < 0)
        |> Seq.fold (fun state t -> match state with Some f -> Some (max f t) | None -> Some t) None

//Some {numerator = 428570UL;
//      denominator = 999997UL;}
//00:00:00.7060195
//
//
