module prb61

let polygonalFunction = [
    fun n -> n * (n+1) / 2
    fun n -> n*n
    fun n -> n * (3*n - 1) / 2
    fun n -> n * (2*n - 1)
    fun n -> n * (5*n - 3) / 2
    fun n -> n * (3*n - 2)]
let polygonalNumber = polygonalFunction
                        |> Seq.map Seq.initInfinite
                        |> Seq.map (Seq.skipWhile (fun p -> p < 1000))
                        |> Seq.map (Seq.takeWhile (fun p -> p < 10000))
                        |> Seq.map Set.ofSeq
                        |> Seq.toArray
let pair a b = a % 100 = b / 100
let rec search from cycle =
    if List.length cycle < 6 then
        let all = seq {
            match cycle with
                | [] ->
                    yield! polygonalNumber.[0]
                        |> Seq.map (fun n -> search [0] [n])
                | h::t ->
                    let where = Set.difference (set [0 .. 5]) (Set.ofList from)
                    for i in where do
                        yield!
                            polygonalNumber.[i]
                            |> Seq.where (pair h)
                            |> Seq.map (fun n -> search (i::from) (n::cycle))
            }
        all |> Seq.tryPick (fun result -> result)
    else
        if pair (List.head cycle) (List.last cycle) then
            Some cycle
        else
            None
let solve =
    match search [] [] with
        | Some x -> x |> List.sum
        | None -> -1
    

