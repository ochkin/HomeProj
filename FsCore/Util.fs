module Util

let numberOfDigits n =
    if n < 0I then
      invalidArg "n" "Negative"
    else
      n.ToString().Length

let sumOfDigits n =
    let rec go i s =
        if i = 0I then
            s
        else
            let mutable rem = 0I
            let n2 = bigint.DivRem(i, 10I, &rem)
            go n2 (s+rem)
    go n 0I

let printSeq s =
    for x in s do
        printf " %A" x
    printfn ""

// fractions

type Fraction<'T>=
    { N: 'T; D: 'T }

let inline Simplify fraction =
    let rec gcd a b =
        if LanguagePrimitives.GenericZero = a then b else gcd (b % a) a
    let div = gcd (min fraction.N fraction.D) (max fraction.N fraction.D)
    if LanguagePrimitives.GenericOne < div then
        { N = fraction.N / div; D = fraction.D / div}
    else
        fraction

let inline equal a b =
    a.N * b.D = a.D * b.N

let inline compare a b =
    compare (a.N * b.D) (a.D * b.N)

let inline max (a:Fraction<'T>) (b:Fraction<'T>) =
    if compare a b < 0 then b else a

let inline hash x =
    (269 * 47 + hash x.N) * 47 + hash x.D

// maths

let factorial n =
    let rec loop n f =
        if n = 1 then
            f
        else
            loop (n-1) (f*n)
    loop n 1

// algorithms

let rec binarySearch a b (func: int64 -> int64) =
    let c = (a + b) / 2L
    match sign <| func c with
    | 0 -> Some c
    | -1 -> if a < c then binarySearch c b func else None
    | +1 -> if c < b then binarySearch a c func else None
    | _ -> failwith "invalid sign"