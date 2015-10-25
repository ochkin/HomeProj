namespace Ochkin.ProjectEuler

module prb60dp5 =
    open System.Linq
    open System.Collections.Generic

    let concatInt (a:int) (b:int) =
        int ((string a) + (string b))
    let isGoodPair (a: int) (b:int) =
        Prime.IsPrime (concatInt a b) && Prime.IsPrime (concatInt b a)

    let CAPACITY = 1000000
    let goodPairs = new Dictionary<int * int, bool>(CAPACITY)
    let checkGoodPair (i1 : int, i2 : int) =
        let tuple = (i1, i2)
        if goodPairs.ContainsKey(tuple) |> not then
            let a = Prime.GetPrimeN i1
            let b = Prime.GetPrimeN i2
            goodPairs.Add(tuple, isGoodPair a b)
        goodPairs.Item(tuple)

    let checkGoodSet (theset : prb60.Set5) =
        prb60.traverseSet5 theset |> Seq.forall checkGoodPair

    let sumUp (value:prb60.Set5)=
        match value with
            | (a,b,c,d,e) -> (Prime.GetPrimeN a) + (Prime.GetPrimeN b) + (Prime.GetPrimeN c) + (Prime.GetPrimeN d) + (Prime.GetPrimeN e)
    
    //Prime.ListAtkin 100000000 |> Prime.Reset
    let solve enough =
        prb60.traverse5N (1,2,3,4,5)
        |> Seq.where checkGoodSet
        |> Seq.take enough
        |> Seq.map sumUp
        |> Seq.min