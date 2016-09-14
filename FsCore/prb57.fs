namespace Ochkin.ProjectEuler

module prb57 =
    let countDigit x =
        x.ToString().Length
    let countDigit2 (y:bigint) =
        y.ToString().Length

    let rec enumFractions n a b =
        seq {
            yield (a, b)
            if n % 50 = 0 then
                printfn "%A %A %A" n a b 
            yield! enumFractions (n + 1) (a + 2*b) (a + b)
        }
    let rec enumFractions2 (n) (a:bigint) (b:bigint) = 
        seq {
            yield (a, b)
            if n % 50 = 0 then
                printfn "%A %A %A" n a b 
            yield! enumFractions2 (n + 1) (bigint.Add(a, bigint.Multiply(2I,b))) (bigint.Add(a, b))
        }

    let solve57 =
        enumFractions 0 1 1
            |> Seq.take 1001
            |> Seq.filter (fun (a, b) -> countDigit b < countDigit a)
            |> Seq.length

    let solve57new =
        enumFractions2 0 1I 1I
            |> Seq.take 1001
            |> Seq.filter (fun (a, b) -> countDigit2 b < countDigit2 a)
            |> Seq.length
