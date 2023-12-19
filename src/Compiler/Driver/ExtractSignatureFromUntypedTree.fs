module FSharp.Compiler.Service.Driver.ExtractSignatureFromUntypedTree

open FSharp.Compiler.Text
open FSharp.Compiler.Syntax
open FSharp.Compiler.SyntaxTrivia
open FSharp.Compiler.SyntaxTreeOps

let isPrivate (vis: SynAccess option) =
    match vis with
    | Some(SynAccess.Private _) -> true
    | _ -> false

let rec (|RemoveParens|) pat =
    match pat with
    | SynPat.Paren(pat, _) -> (|RemoveParens|) pat
    | pat -> pat

let typeFromPat t pat =
    let t =
        match t with
        | SynType.Fun _ -> SynType.Paren(t, Range.Zero)
        | _ -> t

    match pat with
    | SynPat.OptionalVal _ ->
        let optionType =
            SynType.LongIdent(SynLongIdent([ Ident("option", Range.Zero) ], [], [ None ]))

        SynType.App(optionType, None, [ t ], [], None, true, Range.Zero)
    | _ -> t

let rec extractSynModuleSigDecl (decl: SynModuleDecl) : SynModuleSigDecl seq =
    match decl with
    | SynModuleDecl.Attributes _
    | SynModuleDecl.Expr _
    | SynModuleDecl.NamespaceFragment _ -> []
    | SynModuleDecl.Open(target, m) -> [| SynModuleSigDecl.Open(target, m) |]
    | SynModuleDecl.HashDirective(parsedHashDirective, m) -> [| SynModuleSigDecl.HashDirective(parsedHashDirective, m) |]
    | SynModuleDecl.ModuleAbbrev(ident, lid, m) -> [| SynModuleSigDecl.ModuleAbbrev(ident, lid, m) |]
    | SynModuleDecl.Types(typeDefns, m) ->
        let typeDefns = extractSynTypeDefns typeDefns
        [| SynModuleSigDecl.Types(typeDefns, m) |]
    | SynModuleDecl.Let(_isRecursive, bindings, _m) -> extractVals bindings
    | SynModuleDecl.Exception(exnDefn, _m) -> extractException exnDefn
    | SynModuleDecl.NestedModule(SynComponentInfo(accessibility = vis) as moduleInfo, isRecursive, decls, _isContinuing, m, trivia) ->
        if isPrivate vis then
            Array.empty
        else

            let decls = extractSynModuleSigDecls decls

            let trivia: SynModuleSigDeclNestedModuleTrivia =
                {
                    ModuleKeyword = trivia.ModuleKeyword
                    EqualsRange = trivia.EqualsRange
                }

            [| SynModuleSigDecl.NestedModule(moduleInfo, isRecursive, decls, m, trivia) |]

and extractSynModuleSigDecls (decls: SynModuleDecl list) =
    Seq.collect extractSynModuleSigDecl decls |> Seq.toList

and extractSynTypeDefn (SynTypeDefn(typeInfo, typeRepr, members, implicitConstructor, m, trivia)) : SynTypeDefnSig option =
    let (SynComponentInfo(accessibility = vis; attributes = attributes)) = typeInfo

    if isPrivate vis then
        None
    else
        let (SynComponentInfo(longId = typeName; typeParams = typeParams)) = typeInfo
        let typeNameType: SynType = mkTypeName typeName typeParams
        let members = extractSynMemberSigs typeNameType members

        let trivia: SynTypeDefnSigTrivia =
            {
                LeadingKeyword = trivia.LeadingKeyword
                EqualsRange = trivia.EqualsRange
                WithKeyword = trivia.WithKeyword
            }

        match typeRepr with
        | SynTypeDefnRepr.Exception _ ->
            // The parser does not create this construct, `SynTypeDefnRepr.Exception`.
            // It is the extracted version of SynModuleDecl.Exception, used later in during type checking.
            None
        | SynTypeDefnRepr.ObjectModel(SynTypeDefnKind.Augmentation _, _synMemberDefns, _range) ->
            Some(SynTypeDefnSig(typeInfo, SynTypeDefnSigRepr.Simple(SynTypeDefnSimpleRepr.None Range.Zero, Range.Zero), members, m, trivia))
        | SynTypeDefnRepr.Simple(simpleRepr, _) ->
            Some(SynTypeDefnSig(typeInfo, SynTypeDefnSigRepr.Simple(simpleRepr, Range.Zero), members, m, trivia))

        | SynTypeDefnRepr.ObjectModel(SynTypeDefnKind.Unspecified, ms, _m) when Option.isSome implicitConstructor && attributes.IsEmpty ->
            // assume [<Class>] for now
            // TODO: if there is no implicitConstructor, the type might need an `[<Class>], [<Interface>] or [<Struct>]` attribute
            // in the component info.
            let typeInfo =
                let (SynComponentInfo(_, tp, c, lid, xmlDoc, pp, vis, m)) = typeInfo

                SynComponentInfo(
                    [
                        {
                            Attributes =
                                [
                                    {
                                        TypeName = SynLongIdent([ Ident("Class", Range.Zero) ], [], [ None ])
                                        ArgExpr = SynExpr.Const(SynConst.Unit, Range.Zero)
                                        Target = None
                                        AppliesToGetterAndSetter = false
                                        Range = Range.Zero
                                    }
                                ]
                            Range = Range.Zero
                        }
                    ],
                    tp,
                    c,
                    lid,
                    xmlDoc,
                    pp,
                    vis,
                    m
                )

            let objectMembers = extractSynMemberSigs typeNameType ms

            Some(
                SynTypeDefnSig(
                    typeInfo,
                    SynTypeDefnSigRepr.ObjectModel(SynTypeDefnKind.Unspecified, objectMembers, Range.Zero),
                    members,
                    m,
                    trivia
                )
            )
        | SynTypeDefnRepr.ObjectModel(kind, ms, _m) ->
            let objectMembers = extractSynMemberSigs typeNameType ms
            Some(SynTypeDefnSig(typeInfo, SynTypeDefnSigRepr.ObjectModel(kind, objectMembers, Range.Zero), members, m, trivia))

and extractSynTypeDefns (typeDefns: SynTypeDefn list) =
    List.choose extractSynTypeDefn typeDefns

and mkTypeName (lid: LongIdent) (typeParams: SynTyparDecls option) : SynType =
    match typeParams with
    | None -> mkSynTypeLongIdent lid
    | Some typeParams ->
        let commas = List.replicate (typeParams.TyparDecls.Length - 1) Range.Zero

        let args =
            typeParams.TyparDecls
            |> List.map (function
                | SynTyparDecl(typar = typar) -> SynType.Var(typar, Range.Zero))

        SynType.App(mkSynTypeLongIdent lid, Some Range.Zero, args, commas, Some Range.Zero, false, Range.Zero)

and mkSynTypeLongIdent (longId: LongIdent) =
    let dots = List.replicate (longId.Length - 1) Range.Zero
    let trivia = List.replicate longId.Length None
    SynType.LongIdent(SynLongIdent(longId, dots, trivia))

and extractVal
    (SynBinding(vis, _kind, isInline, isMutable, attributes, xmlDoc, valData, headPat, returnInfo, _expr, m, _debugPoint, trivia))
    : SynValSig option =
    let headPatVis =
        match headPat with
        | SynPat.Named(accessibility = vis)
        | SynPat.LongIdent(accessibility = vis) -> vis
        | _ -> None

    if isPrivate vis || isPrivate headPatVis || Option.isNone returnInfo then
        None
    else
        match headPat, returnInfo with
        | SynPat.Named(ident, _, _, _), Some(SynBindingReturnInfo(typeName, _, _, _)) ->
            let trivia: SynValSigTrivia =
                {
                    LeadingKeyword = SynLeadingKeyword.Val trivia.LeadingKeyword.Range
                    InlineKeyword = None
                    WithKeyword = None
                    EqualsRange = None
                }

            let valSig =
                SynValSig(
                    attributes,
                    ident,
                    SynValTyparDecls(None, true),
                    typeName,
                    valData.SynValInfo,
                    isInline,
                    isMutable,
                    xmlDoc,
                    headPatVis,
                    None,
                    m,
                    trivia
                )

            Some valSig
        | SynPat.LongIdent(longDotId, _extraId, typarDecls, SynArgPats.Pats ps, vis, m), Some(SynBindingReturnInfo(returnType, _, _, _)) ->
            let typarDecls: SynValTyparDecls =
                Option.defaultValue (SynValTyparDecls(None, true)) typarDecls

            let functionName = List.tryLast longDotId.IdentsWithTrivia

            let parameterName pat =
                match pat with
                | SynPat.Named(ident = SynIdent(ident, _)) -> Some ident
                | _ -> None

            let parameterTypes =
                let rec visit (isTopLevel: bool) (pat: SynPat) : SynType option =
                    match pat with
                    | RemoveParens(SynPat.Const(SynConst.Unit, _)) ->
                        Some(SynType.LongIdent(SynLongIdent([ Ident("unit", Range.Zero) ], [], [ None ])))
                    | RemoveParens(SynPat.Attrib(SynPat.Typed(pat, t, _), attributes, _)) ->
                        let t = typeFromPat t pat
                        Some(SynType.SignatureParameter(attributes, false, parameterName pat, t, Range.Zero))
                    | RemoveParens(SynPat.Typed(pat, t, _)) ->
                        let t = typeFromPat t pat
                        Some(SynType.SignatureParameter(attributes, false, parameterName pat, t, Range.Zero))
                    | RemoveParens(SynPat.Tuple(elementPats = pats)) when isTopLevel ->
                        let ts = List.choose (visit false) pats

                        match ts with
                        | [] -> None
                        | head :: tail ->
                            if ts.Length <> pats.Length then
                                None
                            else
                                let path =
                                    [
                                        yield SynTupleTypeSegment.Type head
                                        yield!
                                            (List.collect
                                                (fun t -> [ SynTupleTypeSegment.Star(Range.Zero); SynTupleTypeSegment.Type t ])
                                                tail)
                                    ]

                                Some(SynType.Tuple(false, path, Range.Zero))

                    | _ -> None

                List.choose (visit true) ps

            if parameterTypes.Length <> ps.Length then
                None
            else
                match functionName with
                | None -> None
                | Some ident ->
                    let trivia: SynValSigTrivia =
                        {
                            LeadingKeyword = SynLeadingKeyword.Val trivia.LeadingKeyword.Range
                            InlineKeyword = None
                            WithKeyword = None
                            EqualsRange = None
                        }

                    let fullType, valInfo = mkReturnTypeAndValInfo parameterTypes returnType

                    let valSig =
                        SynValSig(attributes, ident, typarDecls, fullType, valInfo, isInline, isMutable, xmlDoc, vis, None, m, trivia)

                    Some valSig
        | _ -> None

and mkReturnTypeAndValInfo (parameterTypes: SynType list) (returnType: SynType) =
    let fullType =
        let mkFunType left right =
            SynType.Fun(left, right, Range.Zero, { ArrowRange = Range.Zero })

        let rec visit (pt: SynType list) =
            match pt with
            | [] -> failwith "unreachable for %A"
            | [ single ] -> single
            | h :: rest -> mkFunType h (visit rest)

        match parameterTypes with
        | [] -> returnType
        | _ ->
            let returnType =
                match returnType with
                | SynType.Fun _ -> SynType.Paren(returnType, Range.Zero)
                | _ -> returnType

            visit [ yield! parameterTypes; yield returnType ]

    let valInfo =
        let mapTypeToArgInfo t =
            match t with
            | SynType.SignatureParameter(attributes, optional, id, _, _) -> SynArgInfo(attributes, optional, id)
            | _ -> SynArgInfo([], false, None)

        match parameterTypes with
        | [] -> SynInfo.emptySynValData.SynValInfo
        | ts ->
            let argInfos =
                ts
                |> List.map (function
                    | SynType.Tuple(path = segments) -> getTypeFromTuplePath segments |> List.map mapTypeToArgInfo
                    | t -> [ mapTypeToArgInfo t ])

            let returnTypeInfo = mapTypeToArgInfo returnType

            SynValInfo(argInfos, returnTypeInfo)

    fullType, valInfo

and extractVals (bindings: SynBinding list) : SynModuleSigDecl list =
    List.choose
        (fun b ->
            extractVal b
            |> Option.map (fun valSig -> SynModuleSigDecl.Val(valSig, Range.Zero)))
        bindings

and extractException
    (SynExceptionDefn(SynExceptionDefnRepr(accessibility = vis; caseName = SynUnionCase(ident = SynIdent(ident, _))) as repr,
                      withKeyword,
                      members,
                      m))
    : SynModuleSigDecl seq =
    if isPrivate vis then
        Seq.empty
    else
        let typeName = mkTypeName [ ident ] None
        let members = List.choose (extractSynMemberSig typeName) members

        [|
            SynModuleSigDecl.Exception(SynExceptionSig(repr, withKeyword, members, m), m)
        |]

// TODO: complete the list
and extractSynMemberSig (typeName: SynType) (md: SynMemberDefn) : SynMemberSig option =
    match md with
    | SynMemberDefn.ValField(fieldInfo, m) -> Some(SynMemberSig.ValField(fieldInfo, m))
    | SynMemberDefn.Member(SynBinding(valData = SynValData(memberFlags = Some mf)) as binding, _range) ->
        extractVal binding
        |> Option.map (fun valSig ->
            let mf =
                match valSig.SynInfo.CurriedArgInfos, mf.MemberKind with
                | [], SynMemberKind.Member ->
                    { mf with
                        MemberKind = SynMemberKind.PropertyGet
                    }
                | _ -> mf

            SynMemberSig.Member(valSig, mf, Range.Zero, SynMemberSigMemberTrivia.Zero))

    | SynMemberDefn.AbstractSlot(slotSig = synValSig; flags = synMemberFlags) ->
        Some(SynMemberSig.Member(synValSig, synMemberFlags, Range.Zero, SynMemberSigMemberTrivia.Zero))

    | SynMemberDefn.Interface(interfaceType, _withKeyword, _synMemberDefnsOption, _range) ->
        Some(SynMemberSig.Interface(interfaceType, Range.Zero))

    | SynMemberDefn.Inherit(baseType, _identOption, _range) -> Some(SynMemberSig.Inherit(baseType, Range.Zero))

    | SynMemberDefn.ImplicitCtor(vis, synAttributeLists, synSimplePats, _selfIdentifier, preXmlDoc, _range, _) ->
        let parameterTypes, allParametersAreTyped =
            match synSimplePats with
            | SynSimplePats.SimplePats(pats = pats) ->
                let ts =
                    pats
                    |> List.choose (function
                        | SynSimplePat.Attrib(SynSimplePat.Typed(SynSimplePat.Id(ident = ident; isOptional = isOptional), t, _),
                                              attributes,
                                              m) -> Some(SynType.SignatureParameter(attributes, isOptional, Some ident, t, m))
                        | SynSimplePat.Typed(SynSimplePat.Id(ident = ident; isOptional = isOptional), t, m) ->
                            Some(SynType.SignatureParameter([], isOptional, Some ident, t, m))
                        | _ -> None)

                match ts with
                | [] -> SynType.LongIdent(SynLongIdent([ Ident("unit", Range.Zero) ], [], [ None ])), true
                | [ t ] -> t, pats.Length = 1
                | first :: rest ->
                    let path =
                        [
                            yield SynTupleTypeSegment.Type(first)
                            yield!
                                (List.collect
                                    (fun t ->
                                        [
                                            yield SynTupleTypeSegment.Star(Range.Zero)
                                            yield SynTupleTypeSegment.Type(t)
                                        ])
                                    rest)
                        ]

                    SynType.Tuple(false, path, Range.Zero), ts.Length = pats.Length

        if not allParametersAreTyped then
            None
        else
            let fullType, valInfo = mkReturnTypeAndValInfo [ parameterTypes ] typeName

            let valSig =
                SynValSig(
                    synAttributeLists,
                    SynIdent(Ident("new", Range.Zero), None),
                    SynValTyparDecls(None, false),
                    fullType,
                    valInfo,
                    false,
                    false,
                    preXmlDoc,
                    vis,
                    None,
                    Range.Zero,
                    SynValSigTrivia.Zero
                )

            Some(
                SynMemberSig.Member(
                    valSig,
                    {
                        IsInstance = false
                        IsDispatchSlot = false
                        IsOverrideOrExplicitImpl = false
                        IsFinal = false
                        GetterOrSetterIsCompilerGenerated = false
                        MemberKind = SynMemberKind.Constructor
                    },
                    Range.Zero,
                    SynMemberSigMemberTrivia.Zero
                )
            )
    | SynMemberDefn.ImplicitInherit(inheritType, _inheritArgs, _inheritAlias, _range) -> Some(SynMemberSig.Inherit(inheritType, Range.Zero))
    | _ -> None

and extractSynMemberSigs (typeName: SynType) (members: SynMemberDefn list) : SynMemberSig list =
    List.choose (extractSynMemberSig typeName) members

let rec extractSynModuleOrNamespaceSig
    (SynModuleOrNamespace(longId, isRecursive, kind, decls, xmlDoc, attribs, accessibility, m, trivia))
    : SynModuleOrNamespaceSig =
    let decls = extractSynModuleSigDecls decls

    let trivia: SynModuleOrNamespaceSigTrivia =
        {
            LeadingKeyword = trivia.LeadingKeyword
        }

    SynModuleOrNamespaceSig(longId, isRecursive, kind, decls, xmlDoc, attribs, accessibility, m, trivia)

/// Extract a signature file solely based on the untyped tree.
/// This approach assumes that all functions/members that are to be part of the public API are fully typed.
let extractSignatureFromImplementation (file: ParsedImplFileInput) : ParsedInput =
    let fileName = file.FileName.Replace(".fs", ".fsi")

    let qualifiedName =
        let name = file.QualifiedName.Id.idText.Replace("$fs", "$fsi")
        QualifiedNameOfFile(Ident(name, Range.Zero))

    let content: SynModuleOrNamespaceSig list =
        List.map extractSynModuleOrNamespaceSig file.Contents

    let trivia: ParsedSigFileInputTrivia =
        {
            ConditionalDirectives = file.Trivia.ConditionalDirectives
            CodeComments = file.Trivia.CodeComments
        }

    ParsedInput.SigFile(ParsedSigFileInput(fileName, qualifiedName, file.ScopedPragmas, file.HashDirectives, content, trivia, Set.empty))
