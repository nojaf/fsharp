module FSharp.Compiler.Service.Tests.HorsingAround.Types

open System.Linq
open System.Collections.Immutable
open FSharp.Compiler.Syntax

type Path = string list

let (|LongIdentPath|) = List.map (fun (ident: Ident) -> ident.idText)
let (|SynLongIdentPath|) (sli: SynLongIdent) = (|LongIdentPath|) sli.LongIdent

type FSharpFileInput =
    {
        Tree: ParsedInput
        ExposesPaths: Path list
    }

type FSharpFileResult =
    {
        Tree: ParsedInput
        ExposesPaths: Path list
        Uses: string array
    }

    member x.FileName = x.Tree.FileName

[<RequireQualifiedAccess>]
type ModuleDeclGroup =
    | OpenStatement of Path
    | Decls of ImmutableQueue<SynModuleDecl>

type PathQuery =
    | PathQuery of fullPath: Path * currentPath: Path

    member pq.IsFound = let (PathQuery(_, currentPath)) = pq in List.isEmpty currentPath

    member pq.IsExactMatch pathToTest =
        let (PathQuery(fullPath, _)) = pq in fullPath = pathToTest

    static member SubtractSharedNamespace (PathQuery(fullPath, current) as pq) (input: Path) =
        match current, input with
        | cHead :: cRest, iHead :: iRest ->
            if cHead <> iHead then
                pq
            else
                PathQuery.SubtractSharedNamespace (PathQuery(fullPath, cRest)) iRest
        | _ -> pq

    member pq.IsMatch(pathToTest: Path) =
        let remains = PathQuery.SubtractSharedNamespace pq pathToTest
        remains.IsFound

let existsInParallel (f: 't -> bool) (items: 't seq) : bool = items.AsParallel().Any(f)

let filterInParallel (predicate: 't -> bool) (project: 't -> 'u) (items: 't seq) : 'u array =
    items.AsParallel().Where(predicate).Select(project).ToArray()


(*
Algorithm TL;DR
---------------

I do believe we can pinpoint relations between files without necessary traversing the entire AST each time.

Step 1:
In `ContentParser.fs`, we detect to what Paths each file adds something that can be used in another file.
This is done in parallel as we are merely scanned the AST.

Example:
A.fs

namespace Foo.Bar.A

type AType = class end

---
Result:
A.fs has one path "Foo.Bar.A"

Example:
B.fs

namespace Foo.Bar

module B =
    do someLongCrazySynExprWithALotOfNodes
    let meh _ = ()
    open A
    let moreStuff () = ()
    let evenMoreStuff () = ()

module C =
    let meh2 (a: A.AType) = ()

---
Result:
B.fs has two paths "Foo.Bar.B" and "Foo.Bar.C"

Step 2:
For each file check if it has a dependency on any a file that came before it in the order.

Example
A.fs will not be checked as it is the first file.

B.fs will be checked against A.fs

The check itself is a highly parallelize lookup in the AST of B.fs for all the paths where A.fs exposes anything.
(This happens in `TreeWalking.fs`, `Lookup.fs` orchestrates the files in `mkProject`)

In our example only the path "Foo.Bar.A" will be looked up,
but you can imagine there are multiple paths and they can all be checked in parallel.

A lot of PLinq can be used in this lookup, there is no need to see everything inside the AST of B.fs.
The first path match concludes the relationship between the files.

B.fs has two nested modules => look for it in parallel, first match wins.
Any open statement can be a partial match, this is taking into account when the current path is being looked for.

Any non-open statement like the `do someLongCrazySynExprWithALotOfNodes` or `let meh` can again be explored in parallel.
We can only do this, because there is no `open ` statement in between them.
Open needs to be processed in sequence, everything else is parallelized.

Inside the let binding (SynBinding level) we can even search in parallel if the attributes, headPat, returnInfo or expr has a match.
As `module B` and `module C` are being traversed in parallel, it is quite likely that the match will be found in `meh2` first.
As `someLongCrazySynExprWithALotOfNodes` might take some more time, given the size of AST tree nodes.

As mentioned, the moment we found a match, we don't need to look any further into the file.
I believe this might be the intriguing part of this algorithm.
You have the same effect with the Trie, however, you did need to construct that Trie for each file.
This algorithm has the potential to be very quickly find common cases.
The most trivial case where a file defines a single toplevel module,
and the dependent file just opens it at the top of the file, will be covered very fast.

PS: when we can cache the result of a Path lookup inside the AST.
If two files (X.fs and Y.fs) have types in the same namespace (path "Foo.Bar"),
we should only lookup that namespace path ("Foo.Bar") once in a third file ("Z.fs").

*)