module Par

open System.Linq

let pseq (source: seq<'T>) =
    ParallelEnumerable.AsParallel source

let pmap (selector: 'T -> 'U) (source: ParallelQuery<'T>) =
    ParallelEnumerable.Select(source, selector)

let pfilter (predicate: 'T -> bool) (source: ParallelQuery<'T>) =
     ParallelEnumerable.Where(source, predicate)

let plength (source: ParallelQuery<'T>) =
    ParallelEnumerable.Count source