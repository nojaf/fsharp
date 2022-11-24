﻿module ParallelTypeCheckingTests.TrieMapping

open System.Collections.Generic
open FSharp.Compiler.Syntax
open Microsoft.FSharp.Collections

let mergeTrieNodes (defaultChildSize: int) (tries: TrieNode array) =
    let rec mergeTrieNodesAux (root: TrieNode) (KeyValue (k, v)) =
        if root.Children.ContainsKey k then
            let node = root.Children.[k]

            match node.Current, v.Current with
            | TrieNodeInfo.Namespace (filesThatExposeTypes = currentFiles), TrieNodeInfo.Namespace (filesThatExposeTypes = otherFiles) ->
                for otherFile in otherFiles do
                    do ()
                    currentFiles.Add(otherFile) |> ignore
            | _ -> ()

            for kv in v.Children do
                mergeTrieNodesAux node kv

        else
            root.Children.Add(k, v)

    match Array.tryExactlyOne tries with
    | Some singleTrie ->
        assert (singleTrie.Current = TrieNodeInfo.Root)
        singleTrie
    | None ->
        let root =
            {
                Current = TrieNodeInfo.Root
                Children = Dictionary<_, _>(defaultChildSize)
            }

        do ()

        for trie in tries do
            assert (trie.Current = TrieNodeInfo.Root)

            for kv in trie.Children do
                mergeTrieNodesAux root kv

        root

let hs f = HashSet(Seq.singleton f)
let emptyHS () = HashSet(0)

let rec mkTrieNodeFor (file: FileWithAST, idx: int) : TrieNode =
    match file.AST with
    | ParsedInput.SigFile (ParsedSigFileInput (contents = contents)) ->
        contents
        |> List.choose (fun (SynModuleOrNamespaceSig (longId = longId; kind = kind; decls = decls; accessibility = _accessibility)) ->
            let hasTypesOrAutoOpenNestedModules =
                List.exists
                    (function
                    | SynModuleSigDecl.Types _ -> true
                    | SynModuleSigDecl.NestedModule(moduleInfo = SynComponentInfo (attributes = attributes)) ->
                        AlwaysLinkDetection.isAnyAttributeAutoOpen attributes
                    | _ -> false)
                    decls

            let isNamespace =
                match kind with
                | SynModuleOrNamespaceKind.AnonModule
                | SynModuleOrNamespaceKind.NamedModule -> false
                | SynModuleOrNamespaceKind.DeclaredNamespace
                | SynModuleOrNamespaceKind.GlobalNamespace -> true

            match longId with
            | [] -> None
            | _ ->
                let rootNode =
                    let rec visit continuation (xs: LongIdent) =
                        match xs with
                        | [] -> failwith "should even empty"
                        | [ finalPart ] ->
                            let name = finalPart.idText

                            let current =
                                if isNamespace then
                                    TrieNodeInfo.Namespace(
                                        name,
                                        (if hasTypesOrAutoOpenNestedModules then
                                             hs idx
                                         else
                                             emptyHS ())
                                    )
                                else
                                    TrieNodeInfo.Module(name, idx)

                            let children = List.choose (mkTrieForNestedSigModule idx) decls

                            continuation (
                                Dictionary<_, _>(
                                    Seq.singleton (
                                        KeyValuePair(
                                            name,
                                            {
                                                Current = current
                                                Children = Dictionary(children)
                                            }
                                        )
                                    )
                                )
                            )
                        | head :: tail ->
                            let name = head.idText

                            visit
                                (fun node ->
                                    let current = TrieNodeInfo.Namespace(name, emptyHS ())

                                    Dictionary<_, _>(Seq.singleton (KeyValuePair(name, { Current = current; Children = node })))
                                    |> continuation)
                                tail

                    visit id longId

                Some { Current = Root; Children = rootNode })
        |> List.toArray
        |> mergeTrieNodes contents.Length
    | ParsedInput.ImplFile (ParsedImplFileInput (contents = contents)) ->
        contents
        |> List.choose (fun (SynModuleOrNamespace (longId = longId; kind = kind; decls = decls; accessibility = _accessibility)) ->
            let hasTypesOrAutoOpenNestedModules =
                List.exists
                    (function
                    | SynModuleDecl.Types _ -> true
                    | SynModuleDecl.NestedModule(moduleInfo = SynComponentInfo (attributes = attributes)) ->
                        AlwaysLinkDetection.isAnyAttributeAutoOpen attributes
                    | _ -> false)
                    decls

            let isNamespace =
                match kind with
                | SynModuleOrNamespaceKind.AnonModule
                | SynModuleOrNamespaceKind.NamedModule -> false
                | SynModuleOrNamespaceKind.DeclaredNamespace
                | SynModuleOrNamespaceKind.GlobalNamespace -> true

            match longId with
            | [] -> None
            | _ ->
                let rootNode =
                    let rec visit continuation (xs: LongIdent) =
                        match xs with
                        | [] -> failwith "should even empty"
                        | [ finalPart ] ->
                            let name = finalPart.idText

                            let current =
                                if isNamespace then
                                    TrieNodeInfo.Namespace(
                                        name,
                                        (if hasTypesOrAutoOpenNestedModules then
                                             hs idx
                                         else
                                             emptyHS ())
                                    )
                                else
                                    TrieNodeInfo.Module(name, idx)

                            let children = List.choose (mkTrieForSynModuleDecl idx) decls

                            continuation (
                                Dictionary<_, _>(
                                    Seq.singleton (
                                        KeyValuePair(
                                            name,
                                            {
                                                Current = current
                                                Children = Dictionary(children)
                                            }
                                        )
                                    )
                                )
                            )
                        | head :: tail ->
                            let name = head.idText

                            visit
                                (fun node ->
                                    let current = TrieNodeInfo.Namespace(name, emptyHS ())

                                    Dictionary<_, _>(Seq.singleton (KeyValuePair(name, { Current = current; Children = node })))
                                    |> continuation)
                                tail

                    visit id longId

                Some { Current = Root; Children = rootNode })
        |> List.toArray
        |> mergeTrieNodes contents.Length

and mkTrieForSynModuleDecl (fileIndex: int) (decl: SynModuleDecl) : KeyValuePair<string, TrieNode> option =
    match decl with
    | SynModuleDecl.NestedModule (moduleInfo = SynComponentInfo(longId = [ nestedModuleIdent ]); decls = decls) ->
        let name = nestedModuleIdent.idText
        let children = List.choose (mkTrieForSynModuleDecl fileIndex) decls

        Some(
            KeyValuePair(
                name,
                {
                    Current = TrieNodeInfo.Module(name, fileIndex)
                    Children = Dictionary(children)
                }
            )
        )
    | _ -> None

and mkTrieForNestedSigModule (fileIndex: int) (decl: SynModuleSigDecl) : KeyValuePair<string, TrieNode> option =
    match decl with
    | SynModuleSigDecl.NestedModule (moduleInfo = SynComponentInfo(longId = [ nestedModuleIdent ]); moduleDecls = decls) ->
        let name = nestedModuleIdent.idText
        let children = List.choose (mkTrieForNestedSigModule fileIndex) decls

        Some(
            KeyValuePair(
                name,
                {
                    Current = TrieNodeInfo.Module(name, fileIndex)
                    Children = Dictionary(children)
                }
            )
        )

    | _ -> None

let mkTrie (files: (FileWithAST * int) array) : TrieNode =
    mergeTrieNodes 0 (Array.Parallel.map mkTrieNodeFor files)

// ==================================================================================================================================================
// ==================================================================================================================================================
open FSharp.Compiler.Service.Tests.Common
open NUnit.Framework

[<Test>]
let ``Fantomas Core trie`` () =
    let files =
        [|
            @"C:\Users\nojaf\Projects\main-fantomas\src\Fantomas.Core\obj\Debug\netstandard2.0\.NETStandard,Version=v2.0.AssemblyAttributes.fs"
            @"C:\Users\nojaf\Projects\main-fantomas\src\Fantomas.Core\obj\Debug\netstandard2.0\Fantomas.Core.AssemblyInfo.fs"
            @"C:\Users\nojaf\Projects\main-fantomas\src\Fantomas.Core\AssemblyInfo.fs"
            @"C:\Users\nojaf\Projects\main-fantomas\src\Fantomas.Core\ISourceTextExtensions.fs"
            @"C:\Users\nojaf\Projects\main-fantomas\src\Fantomas.Core\RangeHelpers.fs"
            @"C:\Users\nojaf\Projects\main-fantomas\src\Fantomas.Core\AstExtensions.fsi"
            @"C:\Users\nojaf\Projects\main-fantomas\src\Fantomas.Core\TriviaTypes.fs"
            @"C:\Users\nojaf\Projects\main-fantomas\src\Fantomas.Core\Utils.fs"
            @"C:\Users\nojaf\Projects\main-fantomas\src\Fantomas.Core\SourceParser.fs"
            @"C:\Users\nojaf\Projects\main-fantomas\src\Fantomas.Core\AstTransformer.fsi"
            @"C:\Users\nojaf\Projects\main-fantomas\src\Fantomas.Core\Version.fs"
            @"C:\Users\nojaf\Projects\main-fantomas\src\Fantomas.Core\Queue.fs"
            @"C:\Users\nojaf\Projects\main-fantomas\src\Fantomas.Core\FormatConfig.fs"
            @"C:\Users\nojaf\Projects\main-fantomas\src\Fantomas.Core\Defines.fsi"
            @"C:\Users\nojaf\Projects\main-fantomas\src\Fantomas.Core\Trivia.fsi"
            @"C:\Users\nojaf\Projects\main-fantomas\src\Fantomas.Core\Trivia.fs"
            @"C:\Users\nojaf\Projects\main-fantomas\src\Fantomas.Core\SourceTransformer.fs"
            @"C:\Users\nojaf\Projects\main-fantomas\src\Fantomas.Core\Context.fs"
            @"C:\Users\nojaf\Projects\main-fantomas\src\Fantomas.Core\CodePrinter.fsi"
            @"C:\Users\nojaf\Projects\main-fantomas\src\Fantomas.Core\CodeFormatterImpl.fsi"
            @"C:\Users\nojaf\Projects\main-fantomas\src\Fantomas.Core\Validation.fs"
            @"C:\Users\nojaf\Projects\main-fantomas\src\Fantomas.Core\Selection.fsi"
            @"C:\Users\nojaf\Projects\main-fantomas\src\Fantomas.Core\CodeFormatter.fsi"
        |]
        |> Array.mapi (fun idx file ->
            let ast = parseSourceCode (file, System.IO.File.ReadAllText(file))
            { Idx = idx; File = file; AST = ast }, idx)

    let trie = mkTrie files
    ignore trie
