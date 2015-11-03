namespace Ochkin.ProjectEuler

module prb60dp5 =
    open System.Linq
    open System.Collections.Generic

    type Set5 = int*int*int*int*int
    let nextSet (current : Set5) : Set5 =
        match current with
        | (a,b,c,d,e) ->
            if a+1<b then
                (a+1,b,c,d,e)
            else
                if b+1<c then
                    (1, b+1, c, d, e)
                else
                    if c+1<d then
                        (1, 2, c+1, d, e)
                    else
                        if d+1<e then
                            (1, 2, 3, d+1, e)
                        else
                            (1, 2, 3, 4, e+1)
    let (*rec*) traverse5N (current : Set5) : seq<Set5> =
        current |> Seq.unfold (fun state -> Some(state, nextSet state))
    let traverseSet5 (theset : Set5) : seq<int*int> = seq {
        match theset with
            | (a,b,c,d,e) ->
                yield (a,b)
                yield (a,c)
                yield (a,d)
                yield (a,e)
                yield (b,c)
                yield (b,d)
                yield (b,e)
                yield (c,d)
                yield (c,e)
                yield (d,e)
            }










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

    let checkGoodSet theset =
        traverseSet5 theset |> Seq.forall checkGoodPair

    let sumUp (value:Set5)=
        match value with
            | (a,b,c,d,e) -> (Prime.GetPrimeN a) + (Prime.GetPrimeN b) + (Prime.GetPrimeN c) + (Prime.GetPrimeN d) + (Prime.GetPrimeN e)
    
    //Prime.ListAtkin 100000000 |> Prime.Reset
    let solve enough =
        traverse5N (1,2,3,4,5)
        |> Seq.filter checkGoodSet
        |> Seq.take enough
        |> Seq.map sumUp
        |> Seq.min
