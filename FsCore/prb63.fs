module prb63

//let ns (digit:byte) =
//    let limit = 1.0 / (1.0 - log10(float digit))
//    Seq.initInfinite (fun i -> i)
//        |> Seq.skipWhile (fun i -> i < 1)
//        |> Seq.takeWhile (fun i -> float i <= limit)
let find (digit:byte) =
    let limit = 22.0 //1.0 / (1.0 - log10(float digit))
    let rec findRec n acc = seq {
        if float n <= limit then
            yield (n, acc)
            yield! findRec (n+1) (acc* (int digit |> bigint))
    }
    findRec 1 (int digit |> bigint)
let correct (n, power) =
    if n = (power |> Util.numberOfDigits) then
        //printfn "%A is %i digits" power n
        true
    else
        false
let solve () =
    [1uy .. 9uy] |> Seq.collect find |> Seq.where correct |> Seq.length