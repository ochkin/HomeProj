﻿module prb65

let convergents n =
    seq {
        let k = (n - 1)/3
        if 1 + 3 *k + 2 <= n then
            yield 2 * (k+1)
        if 1 + 3 *k + 1 <= n then
            yield 1
        for i = k downto 1 do
            yield 1
            yield 2 * i
            yield 1
        yield 2
    }

let simplify (fraction : Util.Fraction<bigint>) =
    let rec gcd a b =
        if a = 0I then b else gcd (b % a) a
    let div = gcd (min fraction.numerator fraction.denominator) (max fraction.numerator fraction.denominator)
    if 1I < div then
        {
            Util.Fraction.numerator = fraction.numerator / div;
            Util.Fraction.denominator = fraction.denominator / div
        }
    else
        fraction

// 1. invert the fraction
// 2. add one
// 3. simplify fraction
let step (state: Util.Fraction<bigint>) (convergent: int) =
    { Util.Fraction.numerator = state.numerator * (bigint convergent) + state.denominator; Util.Fraction.denominator = state.numerator}
        |> simplify

let solve () =
    let c = convergents 100
    for temp in c do
        printfn "%A" temp
    let first = Seq.head c
    let f = c
            |> Seq.skip 1
            |> Seq.fold step {numerator=bigint first; denominator=1I}
    Util.sumOfDigits f.numerator
