module prb68

/// get all permitations of n from 2*n numbers 1..2*n, where the first number is always the smallest
let permutationsOfN n = seq {
    for first = 1 to n + 1 do
        let available = Set.ofSeq <| seq { first+1 .. 2*n }
        /// get all permutations of n-1 numbers from first+1..2*n
        let rec go perm4 = seq {
            yield perm4

            let rec findPos revList =
                match revList with
                    | h :: t ->
                        let choices = t |> Set.ofList |> Set.difference available |> Seq.where (fun a -> h < a)
                        if Seq.isEmpty choices then
                            findPos t
                        else
                            Some ((List.length t), (Seq.min choices))
                    | [] -> None
            match findPos <| List.rev perm4 with
                | Some (position, next) ->
                    let nextPerm4_left = (List.take position perm4) @ [ next ]
                    let nextPerm4_right =
                        nextPerm4_left
                            |> Set.ofList
                            |> Set.difference available
                            |> Seq.sort
                            |> Seq.take (n - position - 2)
                            |> List.ofSeq
                    let nextPerm4 = nextPerm4_left @ nextPerm4_right    
                    yield! go nextPerm4
                | None -> ()
            }
        yield! go (available |> Seq.sort |> Seq.take (n-1) |> List.ofSeq)
                |> Seq.map (fun l -> first :: l)
    }
let rec simplePerm avail = seq {
    match avail with
        | h::[] -> yield avail
        | _ ->
            for a in avail do
                yield! simplePerm (List.except (Seq.singleton a) avail) |> Seq.map (fun perm -> a::perm)
    }

let n = 5
let all = permutationsOfN n
            |> Seq.collect (fun perm -> List.except perm [1 .. 2*n] |> simplePerm |> Seq.map (fun simple -> perm @ simple))
let getLines ring =
    let a = List.take n ring
    let b = List.skip n ring
    let c = match b with h::t -> t @ [h]
    Seq.zip3 a b c
let isMagic ring =
    let allEqual sequence =
        let h = Seq.head sequence
        Seq.forall (fun item -> h = item) sequence
    getLines ring |> Seq.map (fun (i,j,k) -> i+j+k) |> allEqual
let getLinesStr ring =
    let a = List.take n ring
    let b = List.skip n ring
    let c = match b with h::t -> t @ [h]
    let allDigits = Seq.map3 (fun i j k -> seq [i; j; k]) a b c |> Seq.collect (fun s -> s)
    System.String.Join<int>(System.String.Empty, allDigits)
let result = all |> Seq.where isMagic |> Seq.map getLinesStr |> Seq.where (fun str -> str.Length = 16) |> Seq.map uint64 |> Seq.max

//28797161103104548

//6531031914842725



