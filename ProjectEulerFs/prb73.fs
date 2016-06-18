module prb73

open Util
let left = { n=1UL; d=3UL}
let right = { n=1UL; d=2UL}

let solve () =
    let addFraction lookup fraction =
        let myHash = Util.hash fraction
        if Map.containsKey myHash lookup then
            let have = lookup.[myHash]
            if List.map (fun f -> Util.equal f fraction) have |> List.isEmpty then
                Map.add myHash (fraction::have) lookup
            else
                lookup
        else
            Map.add myHash [fraction] lookup

    seq {2UL .. 12000UL }
//        |> Seq.map (fun d -> { n=threshold.n * d / threshold.d; d=d })
        |> Seq.collect (fun d -> seq { 1UL + d/3UL .. (d-1UL)/2UL } |> Seq.map (fun n -> { n=n; d=d }))
        |> Seq.where (fun f -> compare left f < 0 && compare f right < 0)
        |> Seq.map Simplify

        |> Seq.fold addFraction Map.empty
        |> Seq.collect (fun kv -> kv.Value)

        |> Seq.length

//292150
//00:00:19.5981098
//290143
//00:00:23.8167755
