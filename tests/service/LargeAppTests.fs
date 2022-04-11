module FSharp.Compiler.Service.Tests.LargeAppTests

open System
open System.Collections.Generic
open System.Diagnostics.Tracing
open NUnit.Framework
open FSharp.Compiler.CodeAnalysis
open FSharp.Compiler.Diagnostics
open FSharp.Compiler.Service.Tests

// Create an interactive checker instance
let mutable checker = Unchecked.defaultof<FSharpChecker>

type Listener() =
    inherit System.Diagnostics.Tracing.EventListener()

    let occurrences = Dictionary<string, int>()
    let mutable source = null

    override __.OnEventSourceCreated newSource =
        if newSource.Name = "FSharpCompiler" then
            ``base``.EnableEvents(newSource, EventLevel.LogAlways, EventKeywords.All)
            source <- newSource

    override __.OnEventWritten eventArgs =
        let payload = eventArgs.Payload
        if payload.Count = 2 then
            match payload.[0], payload.[1] with
            | :? string as message, (:? int as logCompilerFunctionId) ->
                if logCompilerFunctionId = int LogCompilerFunctionId.Service_IncrementalBuildersCache_BuildingNewCache then
                    if occurrences.ContainsKey(message) then
                        occurrences.[message] <- occurrences.[message] + 1
                    else
                        occurrences.Add(message, 1)
            | _ -> ()

    interface System.IDisposable with
        member __.Dispose() =
            if isNull source then
                ()
            else
                ``base``.DisableEvents(source)

[<SetUp>]
let init () =
    use listener = new Listener()
    checker <- FSharpChecker.Create(projectCacheSize = 500)

[<Test>]
let ``Parsing file from project zero`` () =
    let targetFile =  __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project0\MyModule.fs"

    let rootProjectOptions: FSharpProjectOptions =
        match LargeAppProjects.Project0() with
        | FSharpReferencedProject.FSharpReference (options = options) -> options
        | _ -> failwith "expected first line to be an F# project"

    let parseResults, checkResults =
        checker.GetBackgroundCheckResultsForFileInProject(targetFile, rootProjectOptions, userOpName = "nojaf")
        |> Async.RunSynchronously

    Assert.False(parseResults.ParseHadErrors)
    Assert.True(Array.isEmpty checkResults.OpenDeclarations)
