module FSharp.Compiler.Service.Tests.HorsingAround.Lookup

open System.Collections.Concurrent
open System.Collections.Immutable
open FSharp.Compiler.Service.Tests.HorsingAround.Types
open FSharp.Compiler.Service.Tests.HorsingAround.ContentParser
open FSharp.Compiler.Service.Tests.HorsingAround.TreeWalking
open FSharp.Compiler.Service.Tests.Common
open NUnit.Framework

let mkResult (input: FSharpFileInput) (seenFiles: ImmutableQueue<FSharpFileResult>) : FSharpFileResult =
    let cache = ConcurrentDictionary<string, bool>()

    let usesPath (path: Path) =
        let key = String.concat "." path

        if cache.ContainsKey key then
            cache.[key]
        else
            let result = findPathInFile input.Tree (PathQuery(path, path))
            cache.TryAdd(key, result) |> ignore
            result

    let usedFiles =
        filterInParallel
            (fun (seenFile: FSharpFileResult) -> existsInParallel usesPath seenFile.ExposesPaths)
            (fun seenFile -> seenFile.FileName)
            seenFiles

    {
        Tree = input.Tree
        ExposesPaths = input.ExposesPaths
        Uses = usedFiles
    }

let mkFile (fileName: string) (code: string) : FSharpFileInput =
    let ast = parseSourceCode (fileName, code)
    let paths = collectInFile ast
    { Tree = ast; ExposesPaths = paths }

let mkProject (files: FSharpFileInput list) =
    let rec visit (seen: ImmutableQueue<FSharpFileResult>) (unseen: FSharpFileInput list) : ImmutableQueue<FSharpFileResult> =
        match unseen with
        | [] -> seen
        | next :: rest ->
            let result = mkResult next seen
            visit (seen.Enqueue result) rest

    match files with
    | [] -> Array.empty
    | [ singleFile ] ->
        [|
            {
                Tree = singleFile.Tree
                ExposesPaths = singleFile.ExposesPaths
                Uses = Array.empty
            }
        |]
    | files -> visit ImmutableQueue.Empty files |> Seq.toArray

let mkProjectAndReport (files: FSharpFileInput list) (assertions: string list) =
    let project, timespan = time mkProject files
    printfn "Took %A" timespan

    let report =
        project
        |> Array.map (fun result ->
            let fn (s: string) = System.IO.Path.GetFileName s
            let fileName = fn result.FileName

            let usedFiles =
                if Array.isEmpty result.Uses then
                    $"none for {fileName}"
                else
                    result.Uses
                    |> Seq.map (fun other -> $"{fn other} -> {fileName}")
                    |> String.concat "\n\t"

            $"%s{fileName}:\n\t%s{usedFiles}")
        |> String.concat "\n"

    printfn "%s" report

    assertions
    |> List.iter (fun assertion -> StringAssert.Contains(assertion, report))

[<Test>]
let ``simple open`` () =
    let fileA =
        mkFile
            "A.fs"
            """
module A

let a = 1
"""

    let fileB =
        mkFile
            "B.fs"
            """
module B

open A

let b = a + 1
"""

    mkProjectAndReport [ fileA; fileB ] [ "A.fs -> B.fs" ]

[<Test>]
let ``partial opens`` () =
    let fileA =
        mkFile
            "A.fs"
            """
module A1.A2.A3

type AType =
    {
        Name: string
    }
"""

    let fileB =
        mkFile
            "B.fs"
            """
module B

open A1

do ()

open A2

let a (x: A3.AType) = 0
do ()
"""

    mkProjectAndReport [ fileA; fileB ] [ "A.fs -> B.fs" ]

[<Test>]
let ``cached lookup for namespace`` () =
    let fileA =
        mkFile
            "A.fs"
            """
namespace Foo

type A = class end
"""

    let fileB =
        mkFile
            "B.fs"
            """
namespace Foo

type B = class end
"""

    let fileC =
        mkFile
            "C.fs"
            """
module Bar

open Foo

let x (a: A) (b: B) = ()
"""

    mkProjectAndReport [ fileA; fileB; fileC ] [ "A.fs -> B.fs"; "B.fs -> C.fs" ]

[<Test>]
let ``exact path match after partial open`` () =
    let fileA =
        mkFile
            "A.fs"
            """
module A1.A2.A3

type AType =
    {
        Name: string
    }
"""

    let fileB =
        mkFile
            "B.fs"
            """
module B

open A1

do ()
do ()
do ()

open A1.A2.A3

do ()
"""

    mkProjectAndReport [ fileA; fileB ] [ "A.fs -> B.fs" ]

[<Test>]
let ``partial path matched in type annotation`` () =
    let fileA =
        mkFile
            "A.fs"
            """
module A1.A2.A3

type AType =
    {
        Name: string
    }
"""

    let fileB =
        mkFile
            "B.fs"
            """
module B

open A1.A2

let _ = ()
let meh (a: A3) = 0
"""

    mkProjectAndReport [ fileA; fileB ] [ "A.fs -> B.fs" ]
