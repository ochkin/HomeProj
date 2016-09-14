namespace Ochkin.ProjectEuler

module prb60On2 =
    open System.Linq
    open System.Collections.Generic

    let concatInt (a:int) (b:int) =
        int ((string a) + (string b))
    let isGoodPair a b =
        Prime.IsPrime (concatInt a b) && Prime.IsPrime (concatInt b a)

    let graph = new Dictionary<int, int Set>(1000)
    let addPair i1 i2 =
        if graph.ContainsKey(i1) then
            graph.[i1] <- Set.add i2 graph.[i1]
        else
            graph.Add(i1, set [i2])
    let checkGoodPair i1 i2 =
        let a = Prime.GetPrimeN i1
        let b = Prime.GetPrimeN i2
        if isGoodPair a b then
            addPair i1 i2
            addPair i2 i1
            true
        else
            false

    let sumUp2 ids =
        ids |> List.map Prime.GetPrimeN |> List.sum
    
    Prime.ListAtkinTPL 100000000 |> Prime.Reset
    let solve enough =
        let mutable go = true
        let mutable i = 1
        let mutable foundSet = None
        while go && 0 < i do
            for j = 0 to i - 1 do
                checkGoodPair j i |> ignore
            if graph.ContainsKey i && enough-1 <= Set.count graph.[i] then
                let rec search found needMore =
                    if needMore <= 0 then
                        Some found
                    else
                        let last = List.head found
                        let tail = List.tail found
                        let connected x y = Set.contains x graph.[y]
                        graph.[last]
                            |> Seq.filter (fun next -> List.forall (connected next) tail)
                            |> Seq.map (fun next -> search (next::found) (needMore - 1))
                            |> Seq.tryPick (fun item -> item)
                let smth = search [i] (enough - 1)
                if smth.IsSome then
                    go <- false
                    foundSet <- smth
            i <- i + 1
        foundSet.Value |> List.map Prime.GetPrimeN


