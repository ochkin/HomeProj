namespace Ochkin.ProjectEuler

module prb60dp4 =
    open System.Linq
    open System.Collections.Generic

    let CAPACITY = 1000
    let primes = new List<int>(CAPACITY)
    primes.Add(2)
    primes.Add(3)
    let hasDivisors sqrtn0 n =
        primes
            |> Seq.takeWhile (fun x -> x <= sqrtn0)
            |> Seq.exists (fun x -> n % x = 0)
    let addIfPrime x =
        let sqrtx = x |> float |> sqrt |> int
        if hasDivisors sqrtx x |> not then
            primes.Add(x)
    let isPrime n =
        if n < 2 then
            false
        else
            let mutable next = primes.Last()
            while primes.Last() < n do
                next <- next + 2
                addIfPrime next
            0 <= primes.BinarySearch(n)
    let getPrimeN n =
        let mutable current = primes.Last()
        while primes.Count < n+1 do
            current <- current + 2
            addIfPrime current
        primes.Item n
    let getDigits (n : int) =
        if n < 0 then
            -1
        else
            if 999999999 < n then
                10
            else
                if 99999999 < n then
                    9
                else
                    if 9999999 < n then
                        8
                    else
                        if 999999 < n then
                            7
                        else
                            if 99999 < n then
                                6
                            else
                                if 9999 < n then
                                    5
                                else
                                    if 999 < n then
                                        4
                                    else
                                        if 99 < n then
                                            3
                                        else
                                            if 9 < n then
                                                2
                                            else
                                                1
    let concatInt (a:int) (b:int) =
        int ((string a) + (string b))
//        let d = getDigits b
//        b + a * (pown 10 (getDigits b))
    let isGoodPair (a: int) (b:int) =
        isPrime (concatInt a b) && isPrime (concatInt b a)

    let goodPairs = new Dictionary<int * int, bool>(CAPACITY)
    let checkGoodPair (i1 : int, i2 : int) =
        let tuple = (i1, i2)
        if goodPairs.ContainsKey(tuple) |> not then
            let a = getPrimeN i1
            let b = getPrimeN i2
            goodPairs.Add(tuple, isGoodPair a b)
        goodPairs.Item(tuple)

    type Set4 = int*int*int*int
    let traverseSet4 (theset : Set4) : seq<int*int> = seq {
        match theset with
            | (a,b,c,d) ->
                yield (a,b)
                yield (a,c)
                yield (a,d)
                
                yield (b,c)
                yield (b,d)
                
                yield (c,d)
            }
    let checkGoodSet (theset : Set4) =
        let result = traverseSet4 theset |> Seq.forall checkGoodPair
        result

    let nextSet (current : Set4) : Set4 =
        match current with
        | (a,b,c,d) ->
            if a+1<b then
                (a+1, b, c, d)
            else
                if b+1<c then
                    (1, b+1, c, d)
                else
                    if c+1<d then
                        (1, 2, c+1, d)
                    else
                        (1, 2, 3, d+1)
    let rec traverse4N_1 (current : Set4) : seq<Set4> = seq {
            yield current
            yield! traverse4N_1 (nextSet current)
        }
    let rec traverse4N (current : Set4) : seq<Set4> =
        current |> Seq.unfold (fun state -> Some(state, nextSet state))
    let sumUp (value:Set4)=
        match value with
            | (a,b,c,d) -> (getPrimeN a) + (getPrimeN b) + (getPrimeN c) + (getPrimeN d)

    let solve enough =
        traverse4N (1,2,3,4)
        |> Seq.filter checkGoodSet
        |> Seq.take enough
        |> Seq.map sumUp //(fun (a,b,c,d) -> a+b+c+d)
        |> Seq.min
        //|> Seq.head
        


