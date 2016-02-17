module prb64

type Fraction =
    { numerator: int; denominator: int }
    override x.ToString() = sprintf "%A/%A" x.numerator x.denominator 
type Irrational =
    { r: Fraction; i: Fraction }
    override x.ToString() = sprintf "%A + sqrt * %A" x.r x.i

let rec gcd a b =
    if a = 0 then b else gcd (b % a) a
let simplify fraction =
    let div = gcd (min fraction.numerator fraction.denominator) (max fraction.numerator fraction.denominator)
    if div > 1 then
        { numerator = fraction.numerator / div; denominator = fraction.denominator / div}
    else
        fraction
let simplifyI ir =
    { r = simplify ir.r; i = simplify ir.i}
let IntegralPart lowerRange x =
    (x.r.numerator * x.i.denominator + lowerRange * x.i.numerator * x.r.denominator) / 
    (x.r.denominator * x.i.denominator)

let x0 = { r={numerator=0; denominator=1}; i={numerator=1; denominator=1}}

let invert rootBase ir =
    let (a, b, c, d) = (ir.r.numerator, ir.r.denominator, ir.i.numerator, ir.i.denominator)
    let den = rootBase * c * c * b * b - a * a * d * d
    { r = {numerator = -a*b*d*d; denominator=den}; i = {numerator = b*b*c*d; denominator=den} }
let getSequence sq =
    let lowerRange = float sq |> sqrt |> int
    let rec findSequ4nce xs =
        let x = List.head xs
        let a = IntegralPart lowerRange x
        let xMinusA = { r = {numerator = x.r.numerator - x.r.denominator * a; denominator = x.r.denominator} ; i = x.i}
        let inverted = xMinusA |> invert sq
        let xn =
            inverted
                |> simplifyI
        if List.contains xn xs then
            xs
        else
            findSequ4nce (xn :: xs)
    findSequ4nce [x0]
let solve () =
    seq {
        let mutable n = 2
        for sq in Seq.init 101 (fun i -> i * i) |> Seq.skipWhile (fun i -> i < n) do
            yield! seq { n .. (sq-1) } 
            n <- sq + 1
        }
        |> Seq.map getSequence
        |> Seq.map List.length
        |> Seq.where (fun l -> 0 = l % 2)
        |> Seq.length