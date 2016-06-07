module prb66

let natural = Seq.initInfinite (fun y -> y)

let squares = Seq.initInfinite (fun x -> x * x)

let findMinX (D: int) = // naive solution
    let xSolution = seq {
        let mutable yLast = 0UL
        for x in natural |> Seq.skip 3 do
            let x2m1 = -1I + pown (bigint x) 2
            let tries =
                Seq.initInfinite (fun i -> yLast + 1UL + uint64 i)
                    |> Seq.takeWhile (fun y -> x2m1 > (bigint D) * (pown (bigint y) 2))
            if Seq.isEmpty tries |> not then
                yLast <- Seq.last tries
            if x2m1 = (bigint D) * pown (bigint (yLast + 1UL)) 2 then
                yLast <- yLast + 1UL
                yield x
        }
    Seq.head xSolution

open Util
let findMinX2 D = //right solution
    let infLoop arr = seq { while true do yield! arr }
    let solutions = seq {
        match prb64.getSequence D |> snd |> List.rev with
            | a0 :: otherA ->                
                let mutable prev = {numerator = 1I; denominator= 0I}
                let mutable cur = {numerator = bigint a0; denominator = 1I}
                for an in infLoop otherA do
                    let cn = {
                        numerator= (bigint an) * cur.numerator + prev.numerator;
                        denominator= (bigint an) *cur.denominator + prev.denominator } |> Simplify
                    if (pown cn.numerator 2) - bigint D * (pown cn.denominator 2) = 1I then
                        yield cn.numerator
                    prev <- cur
                    cur <- cn
            | [] -> invalidArg "D" "No solutions found."
        }
    solutions |> Seq.head

let seqNonSquares limit = seq {
    let squares_ = squares |> Seq.skip 2 |> Seq.takeWhile (fun j -> j < limit)
    let mutable n = 2
    for sq in squares_ do
        yield! seq { n .. (sq-1) } 
        n <- sq + 1
    yield! seq { n .. limit }
    }

let solve () =
    seqNonSquares 1000
        |> Seq.skip 100
        |> Seq.map (fun D -> D, findMinX2 D)
        |> Seq.maxBy snd
        |> fst
