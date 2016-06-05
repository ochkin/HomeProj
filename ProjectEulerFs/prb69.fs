module prb69

open FSharp.Collections.ParallelSeq
open System.Collections.Generic

/// assume a < b
let rec coprime a b =
    match a with
        | 0 -> false
        | 1 -> true
        | _ -> coprime (b % a) a
let phi n =
    seq { 1..n-1 } |> Seq.where (fun i -> coprime i n) |> Seq.length

//let maxN = 1000000
//let solveNaive () =
//    seq { 2 .. maxN } |> PSeq.maxBy (fun n -> float n / float (phi n))


let findMax coprimeCount = coprimeCount
                            |> Seq.mapi (fun n phi -> n, float n / float phi)
                            |> Seq.skip 2
                            |> Seq.maxBy snd
                            |> fst

let getAllPhi maxN =
    let coprimeCount = Array.zeroCreate <| maxN + 1
    let primes = Prime.ListAtkinTPL maxN |> Set.ofArray
    for p in primes do
        Array.set coprimeCount p (p-1)
    let nonPrimes = coprimeCount |> Seq.mapi (fun i cc -> i, cc) |> Seq.where (fun (i, cc) -> cc = 0) |> Seq.skip 2 |> Seq.map fst |> Array.ofSeq
    for nonPrime in nonPrimes do
        let rec getDivs number productPhi =
            match number with
                | 0 | 1 -> productPhi
                | _ ->
                    let findDiv = primes
                                    |> Seq.takeWhile (fun pr -> pr <= number)
                                    |> Seq.where (fun pr -> number % pr = 0)
                                    |> Seq.head
                    let generator state = if 0 <state && state % findDiv = 0 then Some(state, state / findDiv) else None
                    let power = number
                                |> Seq.unfold (generator)
                                |> Seq.length
                    getDivs (number / (pown findDiv power)) ((pown findDiv (power - 1))*(findDiv - 1) * productPhi)
        Array.set coprimeCount nonPrime <| getDivs nonPrime 1
    coprimeCount

let solve () =
    findMax <| getAllPhi 1000000

//510510