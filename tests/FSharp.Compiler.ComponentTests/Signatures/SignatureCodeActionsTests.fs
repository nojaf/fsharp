module Signatures.SignatureCodeActionsTests

#nowarn "57"

open System.Text
open System.IO
open FSharp.Compiler.Symbols
open FSharp.Compiler.Text
open FSharp.Test.Utilities
open Xunit
open FSharp.Test.CompilerAssertHelpers
open FSharp.Compiler.CodeAnalysis

type ISourceText with

    member x.GetContentAt(range: range) : string =
        let startLine = range.StartLine - 1
        let line = x.GetLineString startLine

        if range.StartLine = range.EndLine then
            let length = range.EndColumn - range.StartColumn
            line.Substring(range.StartColumn, length)
        else
            let firstLineContent = line.Substring(range.StartColumn)
            let sb = StringBuilder().AppendLine(firstLineContent)

            (sb, [ range.StartLine .. range.EndLine - 2 ])
            ||> List.fold (fun sb lineNumber -> sb.AppendLine(x.GetLineString lineNumber))
            |> fun sb ->
                let lastLine = x.GetLineString(range.EndLine - 1)

                sb.Append(lastLine.Substring(0, range.EndColumn)).ToString()

let getValText
    (predicate: FSharpMemberOrFunctionOrValue -> bool)
    (symbolUse: FSharpSymbolUse)
    : (FSharpMemberOrFunctionOrValue * string) option =
    match symbolUse.Symbol with
    | :? FSharpMemberOrFunctionOrValue as mfv ->
        if not (predicate mfv) then
            None
        else
            mfv.GetValSignatureText(symbolUse.DisplayContext, symbolUse.Range)
            |> Option.map (fun valText -> mfv, valText)
    | _ -> None

// Find any functions that are not present in the signature file.
// Detect them via all symbols
[<Fact>]
let ``Add missing binding to signature`` () =
    let signatureFile =
        """
module F

val a: b: int -> int
"""

    let implementationFile =
        """
module F

let a (b:int) = b

let c (d:char) = "meh"
"""

    let documentSource fileName =
        async {
            if fileName = "A.fsi" then
                return Some(SourceText.ofString signatureFile)
            elif fileName = "A.fs" then
                return Some(SourceText.ofString implementationFile)
            else
                return None
        }

    let checker =
        FSharpChecker.Create(documentSource = DocumentSource.Custom documentSource)

    let projectOptions: FSharpProjectOptions =
        { defaultProjectOptions TargetFramework.Current with
            SourceFiles = [| "A.fsi"; "A.fs" |] }

    let _, checkResults =
        checker.ParseAndCheckFileInProject("A.fs", 1, SourceText.ofString implementationFile, projectOptions)
        |> Async.RunSynchronously

    match checkResults with
    | FSharpCheckFileAnswer.Aborted -> ()
    | FSharpCheckFileAnswer.Succeeded checkFileResults ->
        let potentialLightBulbs =
            checkFileResults.GetAllUsesOfAllSymbolsInFile()
            |> Seq.choose (getValText (fun mfv -> not mfv.HasSignatureFile && mfv.IsFunction))
            |> Seq.toList

        match potentialLightBulbs with
        | [ _, "val c: d: char -> string" ] -> Assert.True true
        | other -> failwithf $"Expect single light bulb, got %A{other}"

// Find a function that is current unused, this won't work for members though
[<Fact>]
let ``Add missing binding to signature, alternate take`` () =
    let signatureFile =
        """
module F

val a: b: int -> int
"""

    let implementationFile =
        """
module F

let a (b:int) = b

let c (d:char) = "meh"
"""

    let documentSource fileName =
        async {
            if fileName = "A.fsi" then
                return Some(SourceText.ofString signatureFile)
            elif fileName = "A.fs" then
                return Some(SourceText.ofString implementationFile)
            else
                return None
        }

    let checker =
        FSharpChecker.Create(documentSource = DocumentSource.Custom documentSource)

    let fsharpCore =
        Path.Combine(__SOURCE_DIRECTORY__, "..", "..", "..", @"artifacts\bin\FSharp.Core\Debug\netstandard2.0\FSharp.Core.dll")
        |> FileInfo

    assert fsharpCore.Exists

    let projectOptions: FSharpProjectOptions =
        let projectOptions = defaultProjectOptions TargetFramework.Current

        { defaultProjectOptions TargetFramework.Current with
            SourceFiles = [| "A.fsi"; "A.fs" |]
            OtherOptions = [| yield! projectOptions.OtherOptions; yield "--warnon:1182" |] }

    let _, checkResults =
        checker.ParseAndCheckFileInProject("A.fs", 1, SourceText.ofString implementationFile, projectOptions)
        |> Async.RunSynchronously

    match checkResults with
    | FSharpCheckFileAnswer.Aborted -> ()
    | FSharpCheckFileAnswer.Succeeded checkFileResults ->
        let sourceText = SourceText.ofString implementationFile

        let potentialLightBulbs =
            checkFileResults.Diagnostics
            |> Array.choose (fun diag ->
                if diag.ErrorNumber <> 1182 then
                    None
                else
                    let line = sourceText.GetLineString(diag.StartLine - 1)

                    checkFileResults.GetSymbolUseAtLocation(diag.StartLine, diag.EndColumn, line, [ sourceText.GetContentAt(diag.Range) ])
                    |> Option.bind (getValText (fun mfv -> mfv.IsFunction)))
            |> Array.toList

        match potentialLightBulbs with
        | [ _, "val c: d: char -> string" ] -> Assert.True true
        | other -> failwithf $"Expect single light bulb, got %A{other}"

// Find the signature mismatch diagnostic and get an update based on the implementation tree.
[<Fact>]
let ``Update incorrect signature`` () =
    let signatureFile =
        """
module F

val a: b: int -> int
"""

    let implementationFile =
        """
module F

let a (b:int) = "yow, that is string man"
"""

    let documentSource fileName =
        async {
            if fileName = "A.fsi" then
                return Some(SourceText.ofString signatureFile)
            elif fileName = "A.fs" then
                return Some(SourceText.ofString implementationFile)
            else
                return None
        }

    let checker =
        FSharpChecker.Create(documentSource = DocumentSource.Custom documentSource)

    let fsharpCore =
        Path.Combine(__SOURCE_DIRECTORY__, "..", "..", "..", @"artifacts\bin\FSharp.Core\Debug\netstandard2.0\FSharp.Core.dll")
        |> FileInfo

    assert fsharpCore.Exists

    let projectOptions: FSharpProjectOptions =
        { defaultProjectOptions TargetFramework.Current with
            SourceFiles = [| "A.fsi"; "A.fs" |] }

    let checkResults =
        checker.ParseAndCheckFileInProject("A.fs", 1, SourceText.ofString implementationFile, projectOptions)
        |> Async.RunSynchronously
        |> snd
        |> function
            | FSharpCheckFileAnswer.Aborted -> failwith "yeah..."
            | FSharpCheckFileAnswer.Succeeded checkResults -> checkResults

    let invalidSignatureDiagnostic =
        checkResults.Diagnostics |> Array.find (fun diag -> diag.ErrorNumber = 34)

    let result =
        let sourceText = SourceText.ofString implementationFile
        let line = sourceText.GetLineString(invalidSignatureDiagnostic.StartLine - 1)
        let identifier = sourceText.GetContentAt(invalidSignatureDiagnostic.Range)

        checkResults.GetSymbolUseAtLocation(
            invalidSignatureDiagnostic.StartLine,
            invalidSignatureDiagnostic.EndColumn,
            line,
            [ identifier ]
        )
        |> Option.bind (fun su ->
            match su.Symbol with
            | :? FSharpMemberOrFunctionOrValue as mfv ->
                mfv.GetValSignatureText(su.DisplayContext, su.Range)
                |> Option.map (fun valText -> valText, mfv)
            | _ -> None)

    match result with
    | None -> failwith "Expected symbol to be found"
    | Some(valText, _mfv) -> Assert.Equal("val a: b: int -> string", valText)
