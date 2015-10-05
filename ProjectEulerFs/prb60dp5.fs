namespace Ochkin.ProjectEuler

module prb60dp5 =
    open System.Linq
    open System.Collections.Generic

    let CAPACITY = 500000
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

    let concatInt (a:int) (b:int) =
        int ((string a) + (string b))
    let isGoodPair (a: int) (b:int) =
        isPrime (concatInt a b) && isPrime (concatInt b a)

    //let sum = new System.Collections.Generic.Dictionary<int,int>(CAPACITY)
    //let count = new System.Collections.Generic.Dictionary<int,int>(CAPACITY)
//    let addGoodPair i j =
//        if sum.ContainsKey(i) |> not then
//            count.Add(i, 1)
//            sum.Add(i, primes.Item(i))
//        count.Item(i) <- count.Item(i) + 1
//        sum.Item(i) <- sum.Item(i) + primes.Item(j)
//        count.Item(i)
//    let naturals =
//        Seq.initInfinite (fun i -> i)
//    let halfSurface = seq {
//        for i in naturals do
//            yield! naturals.TakeWhile (fun j -> j < i) |> Seq.map (fun j -> (j, i))
//        }
//    let checkPair pair =
//        if isGoodPair (getPrimeN (fst pair)) (getPrimeN (snd pair)) then
//            let currentCnt = addGoodPair (fst pair) (snd pair)
//            5 = currentCnt
//        else
//            false
    let goodPairs = new Dictionary<int * int, bool>(CAPACITY)
    let checkGoodPair (i1 : int, i2 : int) =
        //let tuple = System.Tuple.Create(i1, i2)
        let tuple = (i1, i2)
        if goodPairs.ContainsKey(tuple) |> not then
            let a = getPrimeN i1
            let b = getPrimeN i2
            goodPairs.Add(tuple, isGoodPair a b)
        goodPairs.Item(tuple)

    let checkGoodSet (theset : prb60.Set5) =
        prb60.traverseSet5 theset |> Seq.forall checkGoodPair

    let sumUp (value:prb60.Set5)=
        match value with
            | (a,b,c,d,e) -> (getPrimeN a) + (getPrimeN b) + (getPrimeN c) + (getPrimeN d) + (getPrimeN e)

    let solve enough =
        prb60.traverse5N (1,2,3,4,5)
        |> Seq.where checkGoodSet
        |> Seq.take enough
        |> Seq.map sumUp
        |> Seq.min
        


