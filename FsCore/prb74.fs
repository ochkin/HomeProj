module prb74

let factDigit digit =
    match digit with
    | 0 -> 1
    | 1 -> 1
    | 2 -> 2
    | 3 -> 6
    | 4 -> 24
    | 5 -> 120
    | 6 -> 720
    | 7 -> 5040
    | 8 -> 5040 * 8
    | 9 -> 5040 * 8 * 9
    | _ -> invalidArg "digit" "Digit is outside of 0..9 range"

let sumFact number =
    let digits = sprintf "%d" number
    assert Seq.forall System.Char.IsDigit digits
    let conv digit = int digit - 48
    digits |> Seq.map (conv >> factDigit) |> Seq.sum

let rec chain number = seq {
    yield number
    yield! chain (sumFact number)
    }

let getSize c =
    let accumulator state item =
        match state with
        | Some items -> if Set.contains item items
                        then None
                        else Some <| Set.add item items
        | None -> None
    Seq.scan accumulator (Some Set.empty) c
    |> Seq.takeWhile Option.isSome
    |> Seq.last
    |> Option.get
    |> Set.count

let solve() =
    seq { 10 .. 1000000 }
    |> Seq.map (chain >> getSize)
    |> Seq.filter (fun c -> 60 = c)
    |> Seq.length
    
