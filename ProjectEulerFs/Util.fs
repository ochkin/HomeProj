module Util

let numberOfDigits n =
    if n < 0I then
      invalidArg "n" "Negative"
    else
      n.ToString().Length

let sumOfDigits n =
    let rec go i s =
        printfn "go %A %A" i s
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
    { n: 'T; d: 'T }


let inline Simplify fraction =
    let rec gcd a b =
        if LanguagePrimitives.GenericZero = a then b else gcd (b % a) a
    let div = gcd (min fraction.n fraction.d) (max fraction.n fraction.d)
    if LanguagePrimitives.GenericOne < div then
        { n = fraction.n / div; d = fraction.d / div}
    else
        fraction

let inline equal a b =
    a.n * b.d = a.d * b.n

let inline compare a b =
    compare (a.n * b.d) (a.d * b.n)

let inline max (a:Fraction<'T>) (b:Fraction<'T>) =
    if compare a b < 0 then b else a

let inline hash x =
    (269 * 47 + hash x.n) * 47 + hash x.d

// maths

let factorial n =
    let rec loop n f =
        if n = 1 then
            f
        else
            loop (n-1) (f*n)
    loop n 1