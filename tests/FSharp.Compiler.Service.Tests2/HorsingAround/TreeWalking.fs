module rec FSharp.Compiler.Service.Tests.HorsingAround.TreeWalking

open System.Collections.Immutable
open FSharp.Compiler.Service.Tests.HorsingAround.Types
open FSharp.Compiler.Syntax

let orNot v = v || false

/// Groups a list of SynModuleDecl each time an SynModuleDecl.Open is discovered.
let cutDeclsWhenOpen
    (allGroups: ImmutableQueue<ModuleDeclGroup>)
    (currentGroup: ImmutableQueue<SynModuleDecl>)
    (decls: SynModuleDecl list)
    =
    match decls with
    | [] ->
        if Seq.isEmpty currentGroup then
            allGroups
        else
            allGroups.Enqueue(ModuleDeclGroup.Decls currentGroup)
    // TODO: OpenType
    | SynModuleDecl.Open(SynOpenDeclTarget.ModuleOrNamespace(SynLongIdentPath path, _), _) :: rest ->
        cutDeclsWhenOpen (allGroups.Enqueue(ModuleDeclGroup.OpenStatement path)) ImmutableQueue.Empty rest
    | head :: tail -> cutDeclsWhenOpen allGroups (currentGroup.Enqueue head) tail

let findPathInSynAttributes (path: PathQuery) (a: SynAttributes) = false

let findPathInSynType (path: PathQuery) (t: SynType) (continuation: bool -> bool) =
    match t with
    | SynType.LongIdent(SynLongIdentPath typePath) -> path.IsMatch typePath |> continuation
    | _ -> continuation false

let findPathInSynPat (path: PathQuery) (pat: SynPat) (continuation: bool -> bool) =
    match pat with
    | SynPat.Paren(pat, _) -> findPathInSynPat path pat continuation
    | SynPat.Typed(pat, t, _) ->
        findPathInSynPat path pat (fun alreadyFound -> alreadyFound || findPathInSynType path t orNot |> continuation)
    | SynPat.Named _ -> continuation false
    | SynPat.LongIdent(argPats = SynArgPats.Pats pats) -> existsInParallel (fun p -> findPathInSynPat path p orNot) pats
    | _ -> continuation false

let findPathInSynReturnInfo (path: PathQuery) (returnInfo: SynBindingReturnInfo) = false
let findPathInExpr (path: PathQuery) (expr: SynExpr) = false

let findPathInSynBinding (path: PathQuery) (SynBinding(attributes = attributes; headPat = headPat; returnInfo = returnInfo; expr = expr)) =
    existsInParallel
        (fun item ->
            match item with
            | Choice1Of4 attributes -> findPathInSynAttributes path attributes
            | Choice2Of4 pat -> findPathInSynPat path pat orNot
            | Choice3Of4 returnInfo -> findPathInSynReturnInfo path returnInfo
            | Choice4Of4 expr -> findPathInExpr path expr)
        [|
            yield Choice1Of4(attributes)
            yield Choice2Of4(headPat)
            yield! ((Option.map Choice3Of4 >> Option.toArray) returnInfo)
            yield Choice4Of4(expr)
        |]

let findPathInModuleDecl (path: PathQuery) (decl: SynModuleDecl) =
    match decl with
    | SynModuleDecl.Let(bindings = bindings) -> existsInParallel (findPathInSynBinding path) bindings
    | _ -> false

let findPathInTopLevelModuleOrNamespace
    path
    (SynModuleOrNamespace(longId = LongIdentPath currentModuleOrNamespacePath; kind = kind; decls = decls))
    =
    let path = PathQuery.SubtractSharedNamespace path currentModuleOrNamespacePath

    if path.IsFound then
        // exactly the same namespace.
        // We return true as we cannot reliably say anything about things will be inferred.
        // An example case here could be that namespace A exposes a type in file A and file B has that exact same top level namespace.
        true
    else
        let groups = cutDeclsWhenOpen ImmutableQueue.Empty ImmutableQueue.Empty decls

        ((false, path), groups)
        ||> Seq.fold (fun (isFound, path) group ->
            if isFound then
                (isFound, path)
            else
                match group with
                | ModuleDeclGroup.OpenStatement openPath ->
                    // Every open statement might potentially bring us closer to finding a match.
                    // These need to be processed sequentially
                    let isExactMatch = path.IsExactMatch openPath

                    if isExactMatch then
                        true, path
                    else
                        let nextPath = PathQuery.SubtractSharedNamespace path openPath
                        (nextPath.IsFound, nextPath)
                | ModuleDeclGroup.Decls decls ->
                    // The lookup in other declarations can be done in parallel.
                    let isFound = existsInParallel (findPathInModuleDecl path) decls
                    (isFound, path))
        |> fst

let findPathInFile (ast: ParsedInput) path =
    match ast with
    | ParsedInput.SigFile _ -> failwith "no sig files"
    | ParsedInput.ImplFile(ParsedImplFileInput(contents = contents)) -> existsInParallel (findPathInTopLevelModuleOrNamespace path) contents
