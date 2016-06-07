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


//[<CustomEquality; NoComparison>]
type Fraction<'T>=
// when 'T: comparison and 'T: equality
//                and 'T: (static member get_Zero: unit->'T)
//                and 'T: (static member get_One: unit->'T)
//                and 'T: (static member (/): 'T->'T->'T)
//                and 'T: (static member (%): 'T->'T->'T)
//                and 'T: (static member (*): 'T->'T->'T)> =
    { numerator: 'T; denominator: 'T }
//    member inline x.Simplied with get () : Fraction<'T> =
//        Fraction.Simplify x
//    override x.ToString () = "" // sprintf "%A/%A" x.numerator x.denominator
//    member this.CompareTo other = 
//                match other with
//                | :? 'a as other -> compare value other
//                | _ -> raise <| InvalidOperationException()
//    override x.Equals yobj = 
//        match yobj with
//        | null -> false
//        | sameRef when System.Object.ReferenceEquals(x, sameRef) -> true
//        | diffHash when diffHash.GetHashCode() <> x.GetHashCode() -> false
//        | :? Fraction<'T> as y ->
//            let x_ = Fraction<'T>.Simplify x
//            let y_ = Fraction<'T>.Simplify y
            //x_.numerator = y_.numerator && x_.denominator = y_.denominator
//        | _ -> false
//    override x.GetHashCode() =
//        (269 * 47 + hash x.numerator) * 47 + hash x.denominator

//    interface System.IComparable with
//      member x.CompareTo yobj = 
//          match yobj with
//          //| 😕 mything as y -> compare x.stamp y.stamp
//          | _ -> invalidArg "yobj" "cannot compare values of different types"

let inline Simplify fraction =
    let rec gcd a b =
        if LanguagePrimitives.GenericZero = a then b else gcd (b % a) a
    let div = gcd (min fraction.numerator fraction.denominator) (max fraction.numerator fraction.denominator)
    if LanguagePrimitives.GenericOne < div then
        { numerator = fraction.numerator / div; denominator = fraction.denominator / div}
    else
        fraction

let inline equal a b =
    a.numerator * b.denominator = a.denominator * b.numerator

let inline compare a b =
    compare (a.numerator * b.denominator) (a.denominator * b.numerator)

let inline max (a:Fraction<'T>) (b:Fraction<'T>) =
    if compare a b < 0 then b else a