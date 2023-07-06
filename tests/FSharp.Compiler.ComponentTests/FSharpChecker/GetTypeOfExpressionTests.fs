module GetTypeOfExpressionTests

open Xunit
open FSharp.Compiler.Text
open FSharp.Test.Compiler

[<Fact>]
let ``Record type in Computation expression`` () =
    FSharp """
type Entry =
    {
        Idx: int
        FileName: string
        /// Own deps
        DependencyCount: int
        /// Being depended on
        DependentCount: int
        LineCount: int
    }

let x =
    {
        Entry.ReSharperFSharpRulezzz
    }
"""
    |> typecheckResults
    |> fun typeCheckResults ->
        let anyRange = typeCheckResults.GetAllUsesOfAllSymbolsInFile() |> Seq.head |> fun symbolUse -> symbolUse.Range
        let mExpr = Range.mkRange anyRange.FileName (Position.mkPos 14 4) (Position.mkPos 16 5)
        let t = typeCheckResults.GetTypeOfExpression(mExpr).Value
        Assert.True t.TypeDefinition.IsFSharpRecord
        