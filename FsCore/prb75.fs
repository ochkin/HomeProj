module prb75

[<Literal>]
let Limit = 1500000UL
let natural = Seq.initInfinite uint64 |> Seq.skip 1
let primitiveTriples =
    natural
    |> Seq.map (fun n -> natural |> Seq.map (fun i -> n, n + i)
                                 |> Seq.takeWhile (fun (n,m) -> 2UL*m*(m+n) <= Limit)
                                 |> List.ofSeq)
    |> Seq.takeWhile (List.isEmpty >> not)
    |> Seq.collect id

let multiply (n, m) =
    let generator (step1, step2, step3) (a,b,c) =
        if a + b + c <= Limit then
            Some ((a,b,c), (a+step1, b+step2, c+step3))
        else
            None
    let triple = m*m - n*n, 2UL*n*m, m*m+n*n
    Seq.unfold (generator triple) triple
// let allTriangles (n: int64) = seq {
//     let third = n / 3L
//     for i = 3L to third do
//         let part = 2L*n*i - n*n
//         let sumSqLessThan2 x = part + 2L*x*(n - i)
//         match Util.binarySearch (i + 1L) ((n-i) / 2L) sumSqLessThan2 with
//         | Some j -> yield i, j, n-i-j
//         | None -> ()
//     }

// let allTriples =
//   let primitiveTriples2 =
//     natural
//     |> Seq.map (fun n -> natural
//                          |> Seq.map (fun i -> n, n + i)
//                          |> Seq.takeWhile (fun (n, m) -> 2UL * m * (n + m) <= Limit)
//                          |> List.ofSeq)
//     |> Seq.takeWhile (List.isEmpty >> not)
//     |> Seq.collect id

//   let multiply (n, m) =
//     let generator (step1,step2,step3) (a,b,c) =
//         if a + b + c <= Limit then
//             Some ((a,b,c), (a+step1,b+step2,c+step3))
//         else
//             None
//     let triple = m*m-n*n, 2UL*n*m, m*m+n*n
//     Seq.unfold (generator triple) triple
//   primitiveTriples2 // 1u 2u
//   |> Seq.collect multiply
//   |> Seq.distinct
//   |> Seq.groupBy (fun (a,b,c) -> a+b+c)
//   |> Seq.map (snd >> Seq.length)
//   |> Seq.filter (fun l -> l = 1)
//   |> Seq.length

let solve () =
    let sortTuple (a,b,c) =
        match List.sort [a;b;c] with
        | [x;y;z] -> x, y, z
        | _ -> failwith "Invalid array"
    primitiveTriples
    |> Seq.collect multiply
    |> Seq.map sortTuple
    |> Seq.distinct
    |> Seq.groupBy (fun (a,b,c) -> a+b+c)
    |> Seq.filter (fun l -> l |> snd |> Seq.length = 1)
    |> Seq.length

// no collect:
// 145179
// 00:00:00.757797
// with collect:
// 107718
// 00:00:04.4615180
// with tuples
// 1232834
// 00:00:13.3593758

// 112486
// 00:00:13.0105811

// 161667
