module prb62

type digit = byte
let char2byte (c:char) =
    let raw = byte c
    raw - 48uy

let cubes (toSkip:int) =
    Seq.initInfinite uint64
        |> Seq.skip toSkip
        |> Seq.map (fun i -> i * i * i)
        |> Seq.map (fun s -> sprintf "%i" s |> Seq.map char2byte |> Array.ofSeq)

let groupsByNumDigits (toSkip:int) = seq {
    let mutable size = 0
    let mutable group = []
    for arr in cubes toSkip do
        if size < Array.length arr then
            yield group
            group <- []
            size <- Array.length arr
        else
            group <- arr::group
    }
let xor arr = Array.fold (fun state item -> state ^^^ item) (0uy) arr
let arePerm a b =
    let comp = Array.compareWith
                (fun (x:byte) (y:byte) -> x.CompareTo y)
                (Array.sort a)
                (Array.sort b)
    0 = comp
let solve (fake:int) : string =
    let splitByXor l = l |> List.groupBy xor |> List.map snd
    let groupByXor = groupsByNumDigits 150 |> Seq.collect splitByXor
    let rec findPerms found remaining =
        match remaining with
            | [] -> found
            | h::t ->
                let addNew =
                    match found |> List.tryFindIndex (fun group -> arePerm (List.head group) h) with
                        | Some 0 -> [h::found.[0]] @ found.[1..]
                        | Some i -> found.[..i-1] @ [h::found.[i]] @ found.[i+1..]
                        | None -> [h]::found
                findPerms addNew t
            
    let result =
        groupByXor
            |> Seq.map List.ofSeq
            |> Seq.where (fun l -> 4 < List.length l)
            |> Seq.collect (fun group -> findPerms [] group)
            |> Seq.where (fun group -> 5 = List.length group)
            |> Seq.head
    let numbers =
        result
            |> Seq.map (Array.map (fun byte -> byte :> obj))
            |> Seq.map (fun objects -> System.String.Join("", objects))
            |> Array.ofSeq
    System.String.Join(" ", numbers)