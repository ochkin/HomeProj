module misc

//type LazyTree<'T> =
//    | LazyBranch of 'T * ('T LazyTree list Lazy)
//
//let traverse (node:LazyTree<'T>) =
//     let q = System.Collections.Generic.Queue()
//     q.Enqueue node
//     let mutable i = 0
//     let LIMIT = 10
//     while 0 < q.Count && i < LIMIT do 
//        match q.Dequeue () with
//        | LazyBranch (item, children) ->
//            printfn "Node #%i: %A" i item
//            i <- i + 1
//            for child in children.Force() do
//                q.Enqueue child

type LazyTree<'T> =
    | LazyBranch of 'T * ('T LazyTree list Lazy)
with
    member this.Item = match this with LazyBranch(item,_) -> item
    member this.Children = match this with LazyBranch(_,children) -> children
    member this.Map func =
            let children =
                lazy
                (
                    this.Children.Force()
                    |> List.map (fun child -> child.Map func)
                )
            LazyBranch(func this.Item, children)

let traverse (node:LazyTree<'T>) =
        let q = System.Collections.Generic.Queue()
        q.Enqueue node
        let mutable i = 0
        let LIMIT = 10
        while 0 < q.Count && i < LIMIT do
        let node = q.Dequeue ()
        printfn "Node #%i: %A" i node.Map
        i <- i + 1
        for child in node.Children.Force() do
            q.Enqueue child