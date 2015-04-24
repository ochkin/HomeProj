namespace Ochkin.ProjectEuler

module prb56 =
    open System
    open System.Numerics

    //let sumDigit (x : bigint) =
    //    let str = x.ToString()
    //    let digits = str.ToCharArray() |> List.ofArray |> List.map (fun (symbol : System.Char) -> (symbol - '0'))
    //    digits

    let digsum b n =
        let rec loop acc = function
            | n when n > 0 ->
                let m, r = Math.DivRem(n, b)
                loop (acc + r) m
            | _ -> acc
        loop 0 n

    let sumDig (x : bigint) =
        let based = bigint(10)
        let rec loop2 (acc : int) = function
            | (n : bigint) when not n.IsZero ->
                let rem = ref 0I
                let newN = bigint.DivRem(n, based, rem)
                loop2 (acc + int(!rem)) newN
            | _ -> acc
        loop2 0 x

    let solve56 =
        let fromA = 49
        let toA = 99
        seq { for a in fromA .. toA do
                let current = ref 1I
                for b in 1 .. 99 do
                    current := bigint.Multiply(!current, bigint(a))
                    yield sumDig !current
            } |> Seq.max


    let Test56 =
        let res1 = digsum 10 948736
        printfn "digsum 10 948736 = %A" res1
        let res2 = sumDig 948736I
        printfn "sumDig 948736I = %A" res2
        let res3 = solve56
        printfn "solve56 = %A" res3