module FSharp.Compiler.Service.Tests.TailRecursiveTests

open System.Collections.Generic
open FSharp.Compiler.Text
open FSharp.Compiler.Syntax
open NUnit.Framework

// TODO:
// - Figure out partial application
// - Add more test where syn expr can lead to multiple paths
//      (if/then/else, try/with, try/finally, sequential)
// - Think about computation expressions
// - What are the effects of parentheses?
// - What to do when the defined function does not take parameters?
// - what happens when the function is aliased, does that still count?

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
        | SynExpr.Match(clauses = clauses)
        | SynExpr.MatchLambda(matchClauses = clauses) ->
            let continuations =
                List.map (fun (SynMatchClause(resultExpr = expr)) -> visit expr) clauses

            let finalContinuation xs = continuation (List.collect id xs)

            Continuation.sequence continuations finalContinuation
        | SynExpr.LetOrUse(body = body) -> visit body continuation
        | SynExpr.Sequential(expr2 = expr2) -> visit expr2 continuation
        | SynExpr.IfThenElse(thenExpr = thenExpr; elseExpr = elseExpr) ->
            visit thenExpr (fun thenRanges ->
                match elseExpr with
                | None -> continuation thenRanges
                | Some elseExpr -> visit elseExpr (fun elseRanges -> continuation (thenRanges @ elseRanges)))
        | _ -> continuation [ expr.Range ]

    visit expr id

let findFunctionInvocations (functionName: string) (expr: SynExpr) : range list =
    let rec visit (expr: SynExpr) (continuation: range list -> range list) =
        match expr with
        | SynExpr.Match(clauses = clauses)
        | SynExpr.MatchLambda(matchClauses = clauses) ->
            let continuations =
                List.collect
                    (fun (SynMatchClause(whenExpr = whenExpr; resultExpr = resultExpr)) ->
                        match whenExpr with
                        | None -> [ visit resultExpr ]
                        | Some whenExpr -> [ visit whenExpr; visit resultExpr ])
                    clauses

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
        | SynExpr.IfThenElse(ifExpr = ifExpr; thenExpr = thenExpr; elseExpr = elseExpr) ->
            let continuations =
                [
                    yield visit ifExpr
                    yield visit thenExpr
                    match elseExpr with
                    | None -> ()
                    | Some elseExpr -> yield visit elseExpr
                ]

            let finalContinuation = List.collect id >> continuation
            Continuation.sequence continuations finalContinuation
        | SynExpr.Sequential(expr1 = expr1; expr2 = expr2) -> visit expr1 (fun r1 -> visit expr2 (fun r2 -> continuation (r1 @ r2)))
        | _ -> continuation []

    visit expr id

let isTailRecursive (SynBinding(headPat = pat; expr = body)) =
    let functionName =
        match pat with
        | SynPat.LongIdent(longDotId = longDotId) -> List.tryLast longDotId.LongIdent
        | SynPat.Named(ident = SynIdent(ident, _)) -> Some ident
        | _ -> None

    match functionName with
    | None ->
        let returnExpressions = findReturnExpressions body
        returnExpressions.Length, 0, false
    | Some functionName ->
        let invocations = findFunctionInvocations functionName.idText body
        let returnExpressions = findReturnExpressions body

        let isTailRecursive =
            invocations
            |> List.filter (fun invocationRange -> returnExpressions |> List.exists (Range.equals invocationRange) |> not)
            |> Seq.isEmpty

        returnExpressions.Length, invocations.Length, isTailRecursive

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
        ReturnPathsCount: int
        FunctionInvocationCount: int
    }

    override x.ToString() = x.Title

let expectTailRecursive title (returnPathsCount, functionInvocationCount) code =
    {
        Title = title
        Code = code
        IsTailRecursive = true
        ReturnPathsCount = returnPathsCount
        FunctionInvocationCount = functionInvocationCount
    }

let expectNonTailRecursive title (returnPathsCount, functionInvocationCount) code =
    {
        Title = title
        Code = code
        IsTailRecursive = false
        ReturnPathsCount = returnPathsCount
        FunctionInvocationCount = functionInvocationCount
    }

let examples =
    [
        expectTailRecursive
            "Return function in match clause"
            (2, 1)
            """
[<TailRecursive>]
let rec addOneInner (input : int list) (acc : int list) : int list = 
    match input with
    | [] -> acc
    | x :: xs -> addOneInner xs ((x + 1) :: acc)
"""
        expectTailRecursive
            "No function invocation"
            (1, 0)
            """
let rec a b = b + 1
"""
        expectNonTailRecursive
            "Function invocation as non return expression"
            (2, 1)
            """
let rec normalizeTuplePat pats : SynPat list =
    match pats with
    | SynPat.Tuple (false, innerPats, _) :: rest ->
        let innerExprs = normalizeTuplePat (List.rev innerPats)
        innerExprs @ rest
    | _ -> pats
"""
        expectTailRecursive
            "If/then/else with sequential"
            (3, 1)
            """
let rec visit predicate node =
    if predicate node then
        do ()
        visit predicate node.Left
    elif node.IsFinal then
        true
    else
        false
    """
        expectTailRecursive
            "Match lambda"
            (2, 1)
            """
let rec loop acc = function
    | [] -> List.rev acc
    | x::xs -> loop (f x::acc) xs
"""
        expectNonTailRecursive
            "Function is invoked in infix application"
            (2, 1)
            """
let rec foo n =
    match n with
    | 0 -> 0
    | _ -> 2 + foo (n-1)
"""
        expectTailRecursive
            "Function without defined parameters"
            (3, 1)
            """
let rec lastItem =
    function
    | [] -> None
    | [ single ] -> Some single
    | _ :: tail -> lastItem tail
"""
    ]

[<TestCaseSource(nameof examples)>]
let ``Assert function is tail recursive`` (example: Example) =
    let binding = getBinding example.Code
    let returnExprCount, invocationCount, isTailRecursive = isTailRecursive binding
    Assert.AreEqual(example.ReturnPathsCount, returnExprCount)
    Assert.AreEqual(example.FunctionInvocationCount, invocationCount)
    Assert.AreEqual(example.IsTailRecursive, isTailRecursive)
