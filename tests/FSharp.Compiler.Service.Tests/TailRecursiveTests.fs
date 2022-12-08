module FSharp.Compiler.Service.Tests.TailRecursiveTests

open System.Collections.Generic
open FSharp.Compiler.Text
open FSharp.Compiler.Syntax
open NUnit.Framework

[<RequireQualifiedAccess>]
module Continuation =
    let rec sequence<'a, 'ret> (recursions: (('a -> 'ret) -> 'ret) list) (finalContinuation: 'a list -> 'ret) : 'ret =
        match recursions with
        | [] -> [] |> finalContinuation
        | recurse :: recurses -> recurse (fun ret -> sequence recurses (fun rets -> ret :: rets |> finalContinuation))

let (|App|_|) e =
    let rec visit expr continuation =
        match expr with
        | SynExpr.App(funcExpr = funcExpr; argExpr = argExpr) ->
            visit funcExpr (fun (head, xs: Queue<SynExpr>) ->
                xs.Enqueue(argExpr)
                continuation (head, xs))
        | e -> continuation (e, Queue())

    let head, xs = visit e id

    if xs.Count = 0 then
        None
    else
        Some(head, Seq.toList xs, e.Range)

let findReturnExpressions (expr: SynExpr) : range list =
    let rec visit (expr: SynExpr) (continuation: range list -> range list) =
        match expr with
        | SynExpr.Typed(expr = expr) -> visit expr continuation
        | SynExpr.Match(clauses = clauses) ->
            let continuations =
                List.map (fun (SynMatchClause(resultExpr = expr)) -> visit expr) clauses

            let finalContinuation xs = continuation (List.collect id xs)

            Continuation.sequence continuations finalContinuation
        | SynExpr.LetOrUse(body = body) -> visit body continuation
        | _ -> continuation [ expr.Range ]

    visit expr id

let findFunctionInvocations (functionName: string) (expr: SynExpr) : range list =
    let rec visit (expr: SynExpr) (continuation: range list -> range list) =
        match expr with
        | SynExpr.Match(clauses = clauses) ->
            let continuations =
                List.map (fun (SynMatchClause(resultExpr = expr)) -> visit expr) clauses

            let finalContinuation xs = continuation (List.collect id xs)

            Continuation.sequence continuations finalContinuation
        | App(funcExpr, args, range) ->
            let continuations = List.map visit args

            let finalContinuation xs =
                let ranges = List.collect id xs

                match funcExpr with
                | SynExpr.Ident ident when ident.idText = functionName -> range :: ranges
                | _ -> ranges
                |> continuation

            Continuation.sequence continuations finalContinuation
        | SynExpr.Ident ident ->
            if ident.idText = functionName then
                continuation [ ident.idRange ]
            else
                continuation []
        | SynExpr.Typed(expr = expr) -> visit expr continuation
        | SynExpr.LetOrUse(bindings = bindings; body = body) ->
            let continuations =
                [
                    yield! List.map (fun (SynBinding(expr = expr)) -> visit expr) bindings
                    yield visit body
                ]

            let finalContinuation xs = List.collect id xs |> continuation

            Continuation.sequence continuations finalContinuation
        | _ -> continuation []

    visit expr id

let isTailRecursive (SynBinding(headPat = pat; expr = body)) =
    let functionName =
        match pat with
        | SynPat.LongIdent(longDotId = longDotId) -> List.tryLast longDotId.LongIdent
        | _ -> None

    match functionName with
    | None -> false
    | Some functionName ->
        let invocations = findFunctionInvocations functionName.idText body
        let returnExpressions = findReturnExpressions body

        invocations
        |> List.filter (fun invocationRange -> returnExpressions |> List.exists (Range.equals invocationRange) |> not)
        |> Seq.isEmpty

let private getBinding code =
    let sourceCode = parseSourceCode ("A.fs", code)

    match sourceCode with
    | ParsedInput.ImplFile(ParsedImplFileInput(contents = [ SynModuleOrNamespace(decls = [ SynModuleDecl.Let(true, [ binding ], _) ]) ])) ->
        binding
    | ast -> failwith $"Unexpected input, was expecting a single recursive function, got:\n{ast}"

type Example =
    {
        Title: string
        Code: string
        IsTailRecursive: bool
    }

    override x.ToString() = x.Title

let expectTailRecursive title code =
    {
        Title = title
        Code = code
        IsTailRecursive = true
    }

let expectNonTailRecursive title code =
    {
        Title = title
        Code = code
        IsTailRecursive = false
    }

let examples =
    [
        expectTailRecursive
            "Return function in match clause"
            """
[<TailRecursive>]
let rec addOneInner (input : int list) (acc : int list) : int list = 
    match input with
    | [] -> acc
    | x :: xs -> addOneInner xs ((x + 1) :: acc)
"""
        expectTailRecursive
            "No function invocation"
            """
let rec a b = b + 1
"""
        expectNonTailRecursive
            "Function invocation as non return expression"
            """
let rec normalizeTuplePat pats : SynPat list =
    match pats with
    | SynPat.Tuple (false, innerPats, _) :: rest ->
        let innerExprs = normalizeTuplePat (List.rev innerPats)
        innerExprs @ rest
    | _ -> pats
"""
    ]

[<TestCaseSource(nameof examples)>]
let ``Assert function is tail recursive`` (example: Example) =
    let binding = getBinding example.Code
    let isTailRecursive = isTailRecursive binding
    Assert.AreEqual(example.IsTailRecursive, isTailRecursive)
