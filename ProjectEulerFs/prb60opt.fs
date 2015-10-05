namespace Ochkin.ProjectEuler

module prb60opt =
    open System.Linq

    type Set5 = int*int*int*int*int

    let primes = new System.Collections.Generic.List<int>(1000)
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
//            if n <= lastPrime && 0 <= primes.BinarySearch(n) then
//                true
//            else
//                let sqrtn = n |> float |> sqrt |> int
//                let mutable next = lastPrime
//                while primes.Last() < sqrtn do
//                    next <- next + 2
//                    addIfPrime next
//                hasDivisors sqrtn n |> not

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

    let rec traverse5N_1 (current : Set5) : seq<Set5> = seq {
            yield current
            yield! traverse5N_1 (nextSet current)
        }

    let rec traverse5N (current : Set5) : seq<Set5> =
        current |> Seq.unfold (fun state -> Some(state, nextSet state))

    let getPrimeN n =
        let mutable current = primes.Last()
        while primes.Count < n do
            current <- current + 2
            addIfPrime current
        primes.Item (n-1)

    let traverse5Prime : seq<Set5> =
        traverse5N (1,2,3,4,5) |> Seq.map (fun (a,b,c,d,e) -> (getPrimeN a,getPrimeN b,getPrimeN c,getPrimeN d,getPrimeN e))

    let concatInt (a:int) (b:int) =
        int ((string a) + (string b))

    let isGoodPair (a: int) (b:int) =
        isPrime (concatInt a b) && isPrime (concatInt b a)

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

    let isGoodSet (theset : Set5) =
        traverseSet5 theset |> Seq.forall (fun (a,b) -> isGoodPair a b)

    let print5 (set5 : Set5) =
        match set5 with
            | (a,b,c,d,e) ->
            System.Console.WriteLine("{0} {1} {2} {3} {4}",a,b,c,d,e)

    let solve enough =
        traverse5Prime
            |> Seq.filter isGoodSet
            |> Seq.take enough
            |> Seq.map (fun (a,b,c,d,e) -> a+b+c+d+e)
            |> Seq.min
            
//    let debug1 = 
//        let test1 = traverse5N (1,2,3,4,5) |> Seq.take 50 // |> Seq.map print5  |> ignore
//        for hz1 in test1 do
//            print5 hz1
//        0

//    let debug2 =        
//        let test2 = traverse5Prime |> Seq.take 10 // |> Seq.map print5  |> ignore
//        for hz2 in test2 do
//            print5 hz2
//            printfn "%b" (isGoodSet hz2)
//        0