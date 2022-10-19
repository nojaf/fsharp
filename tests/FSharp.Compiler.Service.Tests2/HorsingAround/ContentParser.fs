module FSharp.Compiler.Service.Tests.HorsingAround.ContentParser

open System.IO
open FSharp.Compiler.Syntax
open FSharp.Compiler.Service.Tests.Common
open NUnit.Framework

open FSharp.Compiler.Service.Tests.HorsingAround.Types

// TODO: filter out private types/bindings
let private isType =
    function
    | SynModuleDecl.Types _
    | SynModuleDecl.Exception _ -> true
    | _ -> false

let private isUsable =
    function
    | SynModuleDecl.Types _
    | SynModuleDecl.Exception _
    | SynModuleDecl.Let _ -> true
    | _ -> false

let private (|NestedModule|_|) =
    function
    | SynModuleDecl.NestedModule(moduleInfo = SynComponentInfo(longId = LongIdentPath path); decls = decls) -> Some(path, decls)
    | _ -> None

let rec private collectInNestedModule (leadingPath: Path) (decls: SynModuleDecl list) : Path list =
    let hasOwnContent = List.exists isUsable decls

    let nested =
        List.choose
            (function
            | NestedModule nm -> Some nm
            | _ -> None)
            decls
        |> List.collect (fun (nestedPath, nestedDecls) -> collectInNestedModule (leadingPath @ nestedPath) nestedDecls)

    if hasOwnContent then leadingPath :: nested else nested

let private collectInModuleOrNamespace (SynModuleOrNamespace(longId = LongIdentPath path; kind = kind; decls = decls)) =
    let nestedModules =
        decls
        |> List.collect (function
            | NestedModule(nestedPath, nestedDecls) -> collectInNestedModule (path @ nestedPath) nestedDecls
            | _ -> [])

    match kind with
    | SynModuleOrNamespaceKind.DeclaredNamespace
    | SynModuleOrNamespaceKind.GlobalNamespace ->
        // Only add a namespace when it exposes types
        if List.exists isType decls then
            path :: nestedModules
        else
            nestedModules
    | SynModuleOrNamespaceKind.AnonModule -> []
    | SynModuleOrNamespaceKind.NamedModule ->
        if List.exists isUsable decls then
            path :: nestedModules
        else
            nestedModules

// Check for content inside a file
let collectInFile (ast: ParsedInput) : Path list =
    match ast with
    | ParsedInput.ImplFile(ParsedImplFileInput(contents = contents)) -> List.collect collectInModuleOrNamespace contents
    | _ -> failwith "no signatures"

let time f x =
    System.Diagnostics.Stopwatch.StartNew() |> (fun sw -> (f x, sw.Elapsed))

let processFile path =
    let code = File.ReadAllText path
    let ast = getParseResults code
    let paths, timespan = time collectInFile ast
    printfn "Processed %s: \n\t%s\nin %A" path (List.map (String.concat ".") paths |> String.concat "\n\t") timespan

[<Test>]
let ``Fantomas.Core`` () =
    [|
        @"C:\Users\nojaf\Projects\main-fantomas\src\Fantomas.Core\obj\Debug\netstandard2.0\.NETStandard,Version=v2.0.AssemblyAttributes.fs"
        @"C:\Users\nojaf\Projects\main-fantomas\src\Fantomas.Core\obj\Debug\netstandard2.0\Fantomas.Core.AssemblyInfo.fs"
        @"C:\Users\nojaf\Projects\main-fantomas\src\Fantomas.Core\AssemblyInfo.fs"
        @"C:\Users\nojaf\Projects\main-fantomas\src\Fantomas.Core\ISourceTextExtensions.fs"
        @"C:\Users\nojaf\Projects\main-fantomas\src\Fantomas.Core\RangeHelpers.fs"
        @"C:\Users\nojaf\Projects\main-fantomas\src\Fantomas.Core\AstExtensions.fsi"
        @"C:\Users\nojaf\Projects\main-fantomas\src\Fantomas.Core\AstExtensions.fs"
        @"C:\Users\nojaf\Projects\main-fantomas\src\Fantomas.Core\TriviaTypes.fs"
        @"C:\Users\nojaf\Projects\main-fantomas\src\Fantomas.Core\Utils.fs"
        @"C:\Users\nojaf\Projects\main-fantomas\src\Fantomas.Core\SourceParser.fs"
        @"C:\Users\nojaf\Projects\main-fantomas\src\Fantomas.Core\AstTransformer.fsi"
        @"C:\Users\nojaf\Projects\main-fantomas\src\Fantomas.Core\AstTransformer.fs"
        @"C:\Users\nojaf\Projects\main-fantomas\src\Fantomas.Core\Version.fs"
        @"C:\Users\nojaf\Projects\main-fantomas\src\Fantomas.Core\Queue.fs"
        @"C:\Users\nojaf\Projects\main-fantomas\src\Fantomas.Core\FormatConfig.fs"
        @"C:\Users\nojaf\Projects\main-fantomas\src\Fantomas.Core\Defines.fsi"
        @"C:\Users\nojaf\Projects\main-fantomas\src\Fantomas.Core\Defines.fs"
        @"C:\Users\nojaf\Projects\main-fantomas\src\Fantomas.Core\Trivia.fsi"
        @"C:\Users\nojaf\Projects\main-fantomas\src\Fantomas.Core\Trivia.fs"
        @"C:\Users\nojaf\Projects\main-fantomas\src\Fantomas.Core\SourceTransformer.fs"
        @"C:\Users\nojaf\Projects\main-fantomas\src\Fantomas.Core\Context.fs"
        @"C:\Users\nojaf\Projects\main-fantomas\src\Fantomas.Core\CodePrinter.fsi"
        @"C:\Users\nojaf\Projects\main-fantomas\src\Fantomas.Core\CodePrinter.fs"
        @"C:\Users\nojaf\Projects\main-fantomas\src\Fantomas.Core\CodeFormatterImpl.fsi"
        @"C:\Users\nojaf\Projects\main-fantomas\src\Fantomas.Core\CodeFormatterImpl.fs"
        @"C:\Users\nojaf\Projects\main-fantomas\src\Fantomas.Core\Validation.fs"
        @"C:\Users\nojaf\Projects\main-fantomas\src\Fantomas.Core\Selection.fsi"
        @"C:\Users\nojaf\Projects\main-fantomas\src\Fantomas.Core\Selection.fs"
        @"C:\Users\nojaf\Projects\main-fantomas\src\Fantomas.Core\CodeFormatter.fsi"
        @"C:\Users\nojaf\Projects\main-fantomas\src\Fantomas.Core\CodeFormatter.fs"
    |]
    |> Array.filter (fun p -> p.EndsWith(".fs"))
    |> Array.iter processFile
