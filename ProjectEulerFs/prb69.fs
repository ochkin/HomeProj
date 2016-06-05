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
    // 1
    Array.set coprimeCount 1 1
    // primes
    let primes = Prime.ListAtkinTPL maxN |> Set.ofArray
    for p in primes do
        Array.set coprimeCount p (p-1)
    // non primes
    let composeAndSetPhi n =
        let rec getPhiFromDivs number productPhi =
            match number with
                | 0 | 1 -> productPhi
                | prime when Set.contains prime primes -> (prime - 1) * productPhi
                | _ ->                    
                    let smallestDiv = primes
                                    |> Seq.where (fun pr -> number % pr = 0)
                                    |> Seq.head
                    let divide state = if 0 <state && state % smallestDiv = 0 then Some(state, state / smallestDiv) else None
                    let power = number
                                |> Seq.unfold divide
                                |> Seq.length
                    let dPowerM1 = pown smallestDiv (power - 1)
                    let dPower = dPowerM1 * smallestDiv
                    getPhiFromDivs (number / dPower) ((dPower - dPowerM1) * productPhi)
        Array.set coprimeCount n <| getPhiFromDivs n 1
    let nonPrimes = coprimeCount
                    |> Seq.mapi (fun i cc -> i, cc)
                    |> Seq.skip 2
                    |> Seq.where (fun (i, cc) -> cc = 0)
                    |> Seq.map fst
    nonPrimes
        |> PSeq.iter composeAndSetPhi
    coprimeCount

let solve () =
    findMax <| getAllPhi 1000000

//510510