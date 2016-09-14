namespace Ochkin.ProjectEuler

module prb58 =
    open System.Linq
    let (*mutable private*) array = new System.Collections.Generic.List<int>(5000)
    array.Add(2);
    array.Add(3);
    (*let isPrime n =
        let isDivisable n0 =
            let sqrtn = n0 |> float |> sqrt |> int
            array
                |> Seq.takeWhile (fun x -> x <= sqrtn)
                |> Seq.exists (fun x -> n0 % x = 0)
        let mutable next = array.Last()
        while array.Last() < n do
            next <- next + 2
            if not (isDivisable next) then
                array.Add(next)
        //array.Contains(n)
        n = array.Last()// or 0 < array.BinarySearch(n)*)
    let isPrime n =
        let isDivisable sqrtn0 n0 =
            array
                |> Seq.takeWhile (fun x -> x <= sqrtn0)
                |> Seq.exists (fun x -> n0 % x = 0)
        let sqrtn = n |> float |> sqrt |> int
        let mutable next = array.Last()
        while array.Last() < sqrtn do
            next <- next + 2
            if not (isDivisable (next |> float |> sqrt |> int) next) then
                array.Add(next)
        not (isDivisable sqrtn n)

    let length = 7
    let primesN = 8
    let totalDiagN = 13
    let isP n = if isPrime n then 1 else 0
    let solve58 =
        Seq.initInfinite (fun i -> 4 + i)
            |> Seq.map (fun n -> (4*n*n-2*n+1, 4*n*n+1, 4*n*n+2*n+1, 4*n*n+4*n+1))
            |> Seq.map (fun (a, b, c, d) -> (isP a)+(isP b)+(isP c)+(isP d))
            |> Seq.scan (fun acc elem -> acc + elem) primesN
            |> Seq.mapi (fun i x -> (float x) / float (1 + 4 * (i+3)))
            |> Seq.findIndex (fun x -> x < 0.1)
            //|> Seq.nth 2000
            //|> fun i -> 2*(i+3) + 1

        
