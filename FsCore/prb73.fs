module prb73

open Util
let left = { n=1UL; d=3UL}
let right = { n=1UL; d=2UL}

let solve () =
    seq {2UL .. 12000UL }
        |> Seq.collect (fun d -> seq { 1UL + d/3UL .. (d-1UL)/2UL } |> Seq.map (fun n -> { n=n; d=d }))
        |> Seq.where (fun f -> compare left f < 0 && compare f right < 0)
        |> Seq.map Simplify
        |> Seq.distinct
        |> Seq.length

//292150
//00:00:19.5981098
//290143
//00:00:23.8167755

//7295372