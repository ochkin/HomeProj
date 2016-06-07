module prb72

open Util
let solve () =
    prb69.getAllPhi 1000000 |> Seq.map uint64 |> Seq.sum

//    let addFraction lookup fraction =
//        let myHash = Util.hash fraction
//        if Map.containsKey myHash lookup then
//            let have = lookup.[myHash]
//            if List.map (fun f -> Util.equal f fraction) have |> List.isEmpty then
//                Map.add myHash (fraction::have) lookup
//            else
//                lookup
//        else
//            Map.add myHash [fraction] lookup
//
//    seq { 2UL .. 100000UL }
//        |> Seq.collect (fun d -> seq { 1UL .. (d-1UL) } |> Seq.map (fun n -> {numerator=n; denominator=d}))
//        |> Seq.map Util.Simplify
//        |> Seq.fold addFraction Map.empty
//        |> Seq.collect (fun keyValue -> keyValue.Value)
//        |> Seq.length

//303963552392UL - 1UL
//00:00:04.1736890
//
//
