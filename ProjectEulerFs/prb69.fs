module prb69

open FSharp.Collections.ParallelSeq
open System.Collections.Generic

//let cache = new System.Collections.Concurrent.ConcurrentDictionary<int*int, bool>(10, 1000000)
/// assume a < b
let rec coprime a b =
    match a with
        | 0 -> false
        | 1 -> true
        | _ -> coprime (b % a) a
            //cache.GetOrAdd((a,b), fun (x,y) -> coprime (y % x) x) 
let phi n =
    seq { 1..n-1 } |> Seq.where (fun i -> coprime i n) |> Seq.length

let maxN = 1000000
let solveNaive () =
    seq { 2 .. maxN } |> PSeq.maxBy (fun n -> float n / float (phi n))

let coprimeCount = Array.zeroCreate <| maxN + 1
//let rec traverseTreeRec n m =
//    if m <= maxN then
//        Array.set coprimeCount m <| 1 + Array.get coprimeCount m
//        traverseTreeRec m (2*m - n)
//        traverseTreeRec m (2*m + n)
//        traverseTreeRec n (m + 2*n)

//let q = new Queue<int * int>()
//let traverseTreeQ n m =
//    q.Clear()
//    Array.set coprimeCount m <| 1 + Array.get coprimeCount m
//    q.Enqueue(n, m)
//
//    while 0 < q.Count do
//        let a, b = q.Dequeue ()
//        if 2*b - a <=maxN then
//            Array.set coprimeCount b <| 1 + Array.get coprimeCount b
//            q.Enqueue (b, 2*b - a)
//        Array.set coprimeCount b <| 1 + Array.get coprimeCount b
//        Array.set coprimeCount b <| 1 + Array.get coprimeCount b
//
//        if 2*b + a <= maxN then q.Enqueue (b, 2*b + a)
//        if b + 2*a <= maxN then q.Enqueue (a, b + 2*a)
//traverseTreeQ 1 2
//traverseTreeQ 1 3
//let phi2 n =
//    Array.get coprimeCount n
let findMax () =
    coprimeCount |> Seq.mapi (fun n phi -> n, float n / float phi) |> Seq.skip 2 |> Seq.maxBy snd |> fst

//let pow a n =
//    Seq.replicate n a |> Seq.fold (fun (product, item) -> product * item) 1
let solve () =
    let primes = Prime.ListAtkinTPL maxN |> Set.ofArray
    for p in primes do
        Array.set coprimeCount p (p-1)
    let nonPrimes = coprimeCount |> Seq.mapi (fun i cc -> i, cc) |> Seq.where (fun (i, cc) -> cc = 0) |> Seq.skip 2 |> Seq.map fst |> Array.ofSeq
    for nonPrime in nonPrimes do
        //printfn "Processing nonPrime %i" nonPrime
        let rec getDivs number productPhi =
            match number with
                | 0 | 1 -> productPhi
                | _ ->
//            if Set.contains number primes then
//                productPhi * (number - 1)
//            else
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
    printfn "%A" coprimeCount
    findMax ()

510510