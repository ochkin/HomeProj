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

type Fraction<'T> =
    { numerator: 'T; denominator: 'T }
    override x.ToString() = sprintf "%A/%A" x.numerator x.denominator 

let inline simplify fraction =
    let rec gcd a b =
        if LanguagePrimitives.GenericZero = a then b else gcd (b % a) a
    let div = gcd (min fraction.numerator fraction.denominator) (max fraction.numerator fraction.denominator)
    if LanguagePrimitives.GenericOne < div then
        { numerator = fraction.numerator / div; denominator = fraction.denominator / div}
    else
        fraction
