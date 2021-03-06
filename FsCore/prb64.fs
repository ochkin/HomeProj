﻿module prb64

open Util
type Irrational =
    { r: Fraction<int>; i: Fraction<int> }
    override x.ToString() = sprintf "%A + sqrt * %A" x.r x.i

let simplifyI (ir: Irrational) =
    { r = Simplify ir.r; i = Simplify ir.i}
let IntegralPart lowerRange x =
    (x.r.N * x.i.D + lowerRange * x.i.N * x.r.D) / 
    (x.r.D * x.i.D)

let x0 = { r={N=0; D=1}; i={N=1; D=1}}

let invert rootBase ir =
    let (a, b, c, d) = (ir.r.N, ir.r.D, ir.i.N, ir.i.D)
    let den = rootBase * c * c * b * b - a * a * d * d
    { r = {N = -a*b*d*d; D=den}; i = {N = b*b*c*d; D=den} }

let getSequence sq =
    let lowerRange = float sq |> sqrt |> int
    let rec findSequ4nce xs ints =
        let x = List.head xs
        let a = IntegralPart lowerRange x
        let xMinusA = { r = {N = x.r.N - x.r.D * a; D = x.r.D} ; i = x.i}
        let inverted = xMinusA |> invert sq
        let xn =
            inverted
                |> simplifyI
        if List.contains xn xs then
            xs, a::ints
        else
            findSequ4nce (xn :: xs) (a :: ints)
    findSequ4nce [x0] []

let seqNonSquares limit = seq {
    let mutable n = 2
    for sq in Seq.init limit (fun i -> i * i) |> Seq.skipWhile (fun i -> i < n) do
        yield! seq { n .. (sq-1) } 
        n <- sq + 1
    }

let solve () =
    seqNonSquares 101
        |> Seq.map (fun nSq -> nSq |> getSequence |> fst)
        |> Seq.map List.length
        |> Seq.where (fun l -> 0 = l % 2)
        |> Seq.length