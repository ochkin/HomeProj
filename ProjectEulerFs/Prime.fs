module Prime
open System.Collections.Generic
open System.Linq

// Classic implementation
let primes = new List<int>(500000)
primes.AddRange [ 2; 3; 5; 7; 11; 13; 17; 19; 23; 29; 31; 37; 41; 43; 47; 53; 59; 61; 67; 71; 73; 79; 83; 89; 97; 101; 103; 107; 109; 113; 127; 131; 137; 139; 149; 151; 157; 163; 167; 173; 179; 181; 191; 193; 197; 199; 211; 223; 227; 229; 233; 239; 241; 251; 257; 263; 269; 271; 277; 281; 283; 293; 307; 311; 313; 317; 331; 337; 347; 349; 353; 359; 367; 373; 379; 383; 389; 397; 401; 409; 419; 421; 431; 433; 439; 443; 449; 457; 461; 463; 467; 479; 487; 491; 499; 503; 509; 521; 523; 541; 547; 557; 563; 569; 571; 577; 587; 593; 599; 601; 607; 613; 617; 619; 631; 641; 643; 647; 653; 659; 661; 673; 677; 683; 691; 701; 709; 719; 727; 733; 739; 743; 751; 757; 761; 769; 773; 787; 797; 809; 811; 821; 823; 827; 829; 839; 853; 857; 859; 863; 877; 881; 883; 887; 907; 911; 919; 929; 937; 941; 947; 953; 967; 971; 977; 983; 991; 997 ]
let Reset startingSet =
    primes.Clear()
    primes.AddRange(startingSet)
let hasDivisors sqrtn0 n =
    primes
        |> Seq.takeWhile (fun x -> x <= sqrtn0)
        |> Seq.exists (fun x -> n % x = 0)
let addIfPrime x =
    let sqrtx = x |> float |> sqrt |> int
    if hasDivisors sqrtx x |> not then
        primes.Add(x)
let IsPrime n =
    if n < 2 then
        false
    else
        let mutable next = primes.Last()
        let mutable step = if next % 3 = 1 then 4 else 2
        while primes.Last() < n do
            next <- next + step
            step <- 6 - step
            addIfPrime next
        0 <= primes.BinarySearch(n)
let GetPrimeN n =
    let mutable current = primes.Last()
    let mutable step = if current % 3 = 1 then 4 else 2
    while primes.Count < n+1 do
        current <- current + step
        step <- 6 - step
        addIfPrime current
    primes.Item n


// Sieve of Atkins
//type number = uint32
let rec BinarySearch target arr =
    match Array.length arr with
      | 0 -> None
      | i -> let middle = i / 2
             match  sign <| compare target arr.[middle] with
               | 0  -> Some(target)
               | -1 -> BinarySearch target arr.[..middle-1]
               | _  -> BinarySearch target arr.[middle+1..]
let ListAtkin (topCandidate : int) =
//    let start = [2; 3; 5]
    let sieve = Array.zeroCreate<bool> (topCandidate + 1)
    Array.set sieve 2 true
    Array.set sieve 3 true
    Array.set sieve 5 true
//    for n in 6 .. topCandidate do
//        let r = n % 60
//        match r with
//            | 1 | 13 | 17 | 29 | 37 | 41 | 49 | 53 -> System.Console.WriteLine(1)
//            | 7 | 19 | 31 | 43 -> System.Console.WriteLine(2)
//            | 11 | 23 | 47 | 59 -> System.Console.WriteLine(3)
    // 4x2 + y2
    let set1 = Set.ofList [1; 13; 17; 29; 37; 41; 49; 53]
    let mutable x = 1
    let mutable y = 1
    let mutable go = true
    let mutable x2 = 4 * x * x
    while go do
        let n = x2 + y*y
        if n <= topCandidate then
            if Set.contains (n % 60) set1 then
                Array.get sieve n |> not |> Array.set sieve n

            y <- y + 2
        else
            y <- 1
            x <- x + 1
            x2 <- 4 * x * x
            if topCandidate < x2 + 1 then
                go <- false

    let set2 = Set.ofList [7; 19; 31; 43]
    x <- 1
    y <- 2
    go <- true
    x2 <- 3 * x * x
    while go do
        let n = x2 + y*y
        if n <= topCandidate then
            if Set.contains (n % 60) set2 then
                Array.get sieve n |> not |> Array.set sieve n

            y <- y + 2
        else
            y <- 2
            x <- x + 2
            x2 <- 3 * x * x
            if topCandidate < x2 + 4 then
                go <- false

    let set3 = Set.ofList [11; 23; 47; 59]
    x <- 2
    y <- x - 1
    go <- true
    x2 <- 3 * x * x
    while go do
        let n = x2 - y*y
        if n <= topCandidate && 0 < y then
            if Set.contains (n % 60) set3 then
                Array.get sieve n |> not |> Array.set sieve n

            y <- y - 2
        else
            x <- x + 1
            y <- x - 1
            x2 <- 3 * x * x
            if topCandidate < x2 - y*y then
                go <- false

    //let sqrtTC = int( sqrt ( float ( topCandidate)))
    //let squares1 = seq { for i in 7 .. topCandidate -> i * i } |> Seq.takeWhile (fun n -> n <= topCandidate)
    let squares =
        seq { 7 .. topCandidate}
            |> Seq.filter (fun n -> Array.get sieve n)
            |> Seq.map (fun n -> n * n)
            |> Seq.takeWhile (fun n -> n <= topCandidate)
    for n2 in squares do
//        if Array.get sieve n then
//            let n2 = n * n
//            let rec recN2 current =
//                seq {
//                    yield current
//                    yield! recN2 (current + n2)
//                 }
            n2
                |> Seq.unfold (fun state -> Some(state, state + n2))
                |> Seq.takeWhile (fun x -> x <= topCandidate)
                |> Seq.iter (fun x ->
//                    if topCandidate < x then
                    //printf "%d\n" x                
                    Array.set sieve x false)

    sieve
        |> Array.mapi (fun i t -> if t then Some i else None)
        |> Array.choose (fun t -> t)