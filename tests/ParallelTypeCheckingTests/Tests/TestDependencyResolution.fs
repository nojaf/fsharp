﻿module ParallelTypeCheckingTests.TestDependencyResolution

#nowarn "1182"

open FSharp.Compiler.Service.Tests.Common
open System.IO
open Buildalyzer
open ParallelTypeCheckingTests
open ParallelTypeCheckingTests.Types
open ParallelTypeCheckingTests.Utils
open ParallelTypeCheckingTests.DepResolving
open NUnit.Framework

let buildFiles (files: (string * string) seq) =
    files
    |> Seq.mapi (fun i (name, code) ->
        {
            Idx = FileIdx.make i
            AST = parse name code
        })
    |> Seq.toArray

let private assertGraphEqual (graph: DepsResult) (expected: (string * string list) seq) =
    let edges =
        graph.Edges()
        // Here we disregard directory path, but that's ok for current test cases.
        |> Array.map (fun (node, dep) -> node.ToString(), dep.ToString())

    let expectedEdges =
        expected
        |> Seq.collect (fun (node, deps) -> deps |> List.map (fun d -> node, d))

    Assert.That(edges, Is.EquivalentTo expectedEdges)

[<Test>]
let ``Simple 'open' reference is detected`` () =
    let files =
        [|
            "A.fs", """module A"""
            "B.fs",
            """module B
open A
"""
        |]
        |> buildFiles

    let deps = DependencyResolution.detectFileDependencies files

    let expectedEdges = [ "B.fs", [ "A.fs" ] ]
    assertGraphEqual deps expectedEdges

[<Test>]
let ``A top-level module with an attribute, belonging to a namespace, depends on another file that uses the same namespace`` () =
    let files =
        [|
            "A.fsi",
            """
namespace A.B.C
type X = X of int  
"""
            "B.fsi",
            """
[<System.Obsolete("This is enough for the algorithm to consider this module AutoOpen")>]  
module public A.B.C.D
val x: X
"""
        |]
        |> buildFiles

    let deps = DependencyResolution.detectFileDependencies files

    let expectedEdges = [ "B.fsi", [ "A.fsi" ] ]
    assertGraphEqual deps expectedEdges

[<Test>]
let ``When defining a top-level module, the implicit parent namespace is taken into account when considering references to the file - .fsi pair``
    ()
    =
    let files =
        [|
            "A.fsi",
            """
module A.B.C1
type X = X of int
"""
            "B.fsi",
            """
module A.B.C2
val x : C1.X -> unit
"""
        |]
        |> buildFiles

    let deps = DependencyResolution.detectFileDependencies files

    let expectedEdges = [ "B.fsi", [ "A.fsi" ] ]
    assertGraphEqual deps expectedEdges

[<Test>]
let ``When defining a top-level module, the implicit parent namespace is taken into account when considering references to the file - .fs, .fsi pair``
    ()
    =
    let files =
        [|
            "A.fs",
            """
module A.B.C1
type X = X of int
"""
            "B.fsi",
            """
module A.B.C2
val x : C1.X -> unit
"""
        |]
        |> buildFiles

    let deps = DependencyResolution.detectFileDependencies files

    let expectedEdges = [ "B.fsi", [ "A.fs" ] ]
    assertGraphEqual deps expectedEdges

[<Test>]
let ``When defining a top-level module, the implicit parent namespace is taken into account when considering references to the file - .fsi, .fs pair``
    ()
    =
    let files =
        [|
            "A.fsi",
            """
module A.B.C1
type X = X of int
"""
            "B.fs",
            """
module A.B.C2
let x : C1.X -> unit = failwith ""
"""
        |]
        |> buildFiles

    let deps = DependencyResolution.detectFileDependencies files

    let expectedEdges = [ "B.fs", [ "A.fsi" ] ]
    assertGraphEqual deps expectedEdges

[<Test>]
let ``When defining a top-level module, the implicit parent namespace is taken into account when considering references to the file - .fs, .fs pair``
    ()
    =
    let files =
        [|
            "A.fs",
            """
module A.B.C1
type X = X of int
"""
            "B.fs",
            """
module A.B.C2
let x : C1.X -> unit = failwith ""
"""
        |]
        |> buildFiles

    let deps = DependencyResolution.detectFileDependencies files

    let expectedEdges = [ "B.fs", [ "A.fs" ] ]
    assertGraphEqual deps expectedEdges

[<Test>]
let ``With no references there is no dependency`` () =
    let files =
        [| "A.fs", """module A"""; "B.fs", """module B; let x = 1""" |] |> buildFiles

    let deps = DependencyResolution.detectFileDependencies files

    let expectedEdges = []
    assertGraphEqual deps expectedEdges

[<Test>]
let ``Impl files always depend on their backing signature files, but not always on other signature files`` () =
    let files =
        [|
            "A.fsi",
            """
module A
"""
            "A.fs",
            """
module A
let x = 1
"""
            "B.fs",
            """
module B
let x = 1
"""
        |]
        |> buildFiles

    let deps = DependencyResolution.detectFileDependencies files

    let expectedEdges = [ "A.fs", [ "A.fsi" ] ]
    assertGraphEqual deps expectedEdges

[<Test>]
let ``Files with module or type abbreviations depend on all files above`` () =
    let files =
        [|
            "A.fs",
            """
module A
"""
            "B.fs",
            """
module B
module X = Y
"""
            "C.fs",
            """
module C
type X = Y
"""
        |]
        |> buildFiles

    let deps = DependencyResolution.detectFileDependencies files

    let expectedEdges = [ "B.fs", [ "A.fs" ]; "C.fs", [ "A.fs"; "B.fs" ] ]
    assertGraphEqual deps expectedEdges

[<Test>]
let ``Test error`` () =
    let files =
        [|
            "A.fs",
            """
module A.B1
let x = 3
"""
            "C.fs",
            """
module A.B2
let x = 4
"""
        |]
        |> buildFiles

    let deps = DependencyResolution.detectFileDependencies files

    let expectedEdges = [ "C.fs", [ "A.fs" ] ]
    assertGraphEqual deps expectedEdges

[<Test>]
let ``Test error 2`` () =
    let files =
        [|
            "A.fs",
            """
module A.B1
let x = 3
"""
            "C.fs",
            """
module A.B2
let x = 4
"""
        |]
        |> buildFiles

    let deps = DependencyResolution.detectFileDependencies files

    let expectedEdges = [ "C.fs", [ "A.fs" ] ]
    assertGraphEqual deps expectedEdges

let sampleFiles =
    [
        "Abbr.fs",
        """
module Abbr

module X = A
"""
        "A.fsi",
        """
module A
val a : int
type X = int
"""
        "A.fs",
        """
module A
let a = 3
type X = int
"""
        "B.fs",
        """
namespace B
let b = 3
"""
        "C.fs",
        """
module C.X
let c = 3
"""
        "D.fs",
        """
module D
let d : A.X = 3
"""
        "E.fs",
        """
module E
let e = C.X.x
open A
let x = a
"""
        "F.fs",
        """
module F
open C
let x = X.c
"""
        "G.fs",
        """
namespace GH
type A = int
"""
        "H.fs",
        """
namespace GH
module GH2 =
    type B = int
"""
        "I.fs",
        """
namespace GH
module GH3 =
    type B = int
"""
    ]
    |> buildFiles

let analyseResult (result: DepsResult) =
    analyseEfficiency result

    let totalDeps = result.Graph |> Seq.sumBy (fun (KeyValue (_k, v)) -> v.Length)

    let topFirstDeps =
        result.Graph
        |> Seq.sumBy (fun (KeyValue (_k, v)) ->
            if v.Length = 0 then
                0
            else
                v |> Array.map (fun d -> result.Graph[d].Length) |> Array.max)

    printfn $"TotalDeps: {totalDeps}, topFirstDeps: {topFirstDeps}"
//
// open GiGraph.Dot.Extensions
// open GiGraph.Dot.Output.Options
// let makeDotFile (path : string) (graph : Graph<File>) : unit =
//     let g = DotGraph(directed=true)
//     g.Layout.Direction <- DotLayoutDirection.LeftToRight
//     let name (f : File) = $"{f.QualifiedName}.{Path.GetExtension(f.Name)}"
//     graph
//     |> Graph.collectEdges
//     |> Array.iter (fun (a, b) -> g.Edges.Add(name a, name b) |> ignore)
//     let _options = DotFormattingOptions()
//     printfn $"{g.Build()}"
//     g.SaveToFile(path)

[<Test>]
let ``Analyse hardcoded files`` () =
    let deps = DependencyResolution.detectFileDependencies sampleFiles
    printfn "Detected file dependencies:"
    deps.Graph |> Graph.print
// makeDotFile "graph.dot" deps.Graph

let private parseProjectAndGetSourceFiles (projectFile: string) =
    //let cacheDir = "."
    //let getName projectFile = Path.Combine(Path.GetFileName(projectFile), ".fsharp"
    let m = AnalyzerManager()
    let analyzer = m.GetProject(projectFile)
    let results = analyzer.Build()
    // TODO Generalise for multiple TFMs
    let res = results.Results |> Seq.head
    let files = res.SourceFiles
    log "built project using Buildalyzer"
    files

[<TestCase(__SOURCE_DIRECTORY__
           + @"\..\..\FSharp.Compiler.ComponentTests\FSharp.Compiler.ComponentTests.fsproj")>]
[<TestCase(__SOURCE_DIRECTORY__ + @"\..\..\..\src\Compiler\FSharp.Compiler.Service.fsproj")>]
[<Explicit("Slow as it uses Buildalyzer to analyse (build) projects first")>]
let ``Analyse whole projects and print statistics`` (projectFile: string) =
    log $"Start finding file dependency graph for {projectFile}"
    let files = parseProjectAndGetSourceFiles projectFile

    let files =
        files
        |> Array.Parallel.mapi (fun i f ->
            let code = System.IO.File.ReadAllText(f)
            let ast = parseSourceCode (f, code)

            {
                Idx = FileIdx.make i
                //Code = code
                AST = ast
            }: SourceFile)

    let N = files.Length
    log $"{N} files read and parsed"

    let graph = DependencyResolution.detectFileDependencies files
    log "Dependency graph calculated"

    let totalDeps =
        graph.Graph |> Seq.sumBy (fun (KeyValue (_file, deps)) -> deps.Length)

    let maxPossibleDeps = (N * (N - 1)) / 2

    let path = $"{Path.GetFileName(projectFile)}.deps.json"
    graph.Graph |> Graph.map (fun n -> n.Name) |> Graph.serialiseToJson path

    log
        $"Analysed {N} files, detected {totalDeps}/{maxPossibleDeps} file dependencies (%.1f{100.0 * double (totalDeps) / double (maxPossibleDeps)}%%)."

    analyseEfficiency graph

    let totalDeps = graph.Graph |> Seq.sumBy (fun (KeyValue (_k, v)) -> v.Length)

    let topFirstDeps =
        graph.Graph
        |> Seq.sumBy (fun (KeyValue (_k, v)) ->
            if v.Length = 0 then
                0
            else
                v |> Array.map (fun d -> graph.Graph[d].Length) |> Array.max)

    printfn $"TotalDeps: {totalDeps}, topFirstDeps: {topFirstDeps}, diff: {totalDeps - topFirstDeps}"

// makeDotFile "FCS.deps.dot" graph.Graph
