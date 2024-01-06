module FSharp.Compiler.Service.Driver.ExtractSignatureFromUntypedTree

open FSharp.Compiler.Text
open FSharp.Compiler.Syntax
open FSharp.Compiler.SyntaxTrivia
open FSharp.Compiler.SyntaxTreeOps
open FSharp.Compiler.Xml
open PrintSignature

let isPrivate (vis: SynAccess option) =
    match vis with
    | Some(SynAccess.Private _) -> true
    | _ -> false

let private wrapFunTypeInParens t =
    match t with
    | SynType.Fun _ -> SynType.Paren(t, Range.Zero)
    | _ -> t

let rec (|RemoveParens|) pat =
    match pat with
    | SynPat.Paren(pat, _) -> (|RemoveParens|) pat
    | pat -> pat

let private (|NewPattern|_|) (pat: SynPat) =
    match pat with
    | SynPat.LongIdent(longDotId = SynLongIdent(id = [ newIdent ]); argPats = SynArgPats.Pats pats) when newIdent.idText = "new" ->
        Some pats
    | _ -> None

let private unitType =
    SynType.LongIdent(SynLongIdent([ Ident("unit", Range.Zero) ], [], [ None ]))

let private mkTupleType (originalPatternCount: int) (ts: SynType list) : SynType * bool =
    match ts with
    | [] -> unitType, true
    | [ t ] -> t, originalPatternCount = 1
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

        SynType.Tuple(false, path, Range.Zero), ts.Length = originalPatternCount

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

let private parameterNameFromPattern (pat: SynPat) =
    match pat with
    | SynPat.Named(ident = SynIdent(ident, _)) -> Some ident
    | _ -> None

let rec extractTypeFromPattern (isTopLevel: bool) (pat: SynPat) : SynType option =
    match pat with
    | RemoveParens(SynPat.Const(SynConst.Unit, _)) -> Some unitType
    | RemoveParens(SynPat.Attrib(SynPat.Typed(pat, t, _), attributes, _)) ->
        let t = typeFromPat t pat
        Some(SynType.SignatureParameter(attributes, false, parameterNameFromPattern pat, t, Range.Zero))
    | RemoveParens(SynPat.Typed(pat, t, _)) ->
        let t = typeFromPat t pat
        Some(SynType.SignatureParameter([], false, parameterNameFromPattern pat, t, Range.Zero))
    | RemoveParens(SynPat.Tuple(elementPats = pats)) when isTopLevel ->
        let ts = List.choose (extractTypeFromPattern false) pats

        match ts with
        | [] -> None
        | head :: tail ->
            if ts.Length <> pats.Length then
                None
            else
                let path =
                    [
                        yield SynTupleTypeSegment.Type head
                        yield! (List.collect (fun t -> [ SynTupleTypeSegment.Star(Range.Zero); SynTupleTypeSegment.Type t ]) tail)
                    ]

                Some(SynType.Tuple(false, path, Range.Zero))
    | _ -> None

and extractTypeFromPatterns (ps: SynPat list) =
    List.choose (extractTypeFromPattern true) ps

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
        | SynTypeDefnRepr.ObjectModel(SynTypeDefnKind.Augmentation mWith, _synMemberDefns, _range) ->
            let trivia = { trivia with WithKeyword = Some mWith }
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
    (SynBinding(vis,
                _kind,
                isInline,
                isMutable,
                attributes,
                xmlDoc,
                (SynValData(memberFlags = memberFlagsOpt) as valData),
                headPat,
                returnInfo,
                expr,
                m,
                _debugPoint,
                trivia))
    : SynValSig option =
    let headPatVis =
        match headPat with
        | SynPat.Named(accessibility = vis)
        | SynPat.LongIdent(accessibility = vis) -> vis
        | _ -> None

    if isPrivate vis || isPrivate headPatVis || Option.isNone returnInfo then
        None
    else
        let trivia: SynValSigTrivia =
            let leadingKeyword =
                match memberFlagsOpt with
                | None -> SynLeadingKeyword.Val Range.Zero
                | Some mf ->
                    ignore mf
                    trivia.LeadingKeyword

            {
                LeadingKeyword = leadingKeyword
                InlineKeyword = trivia.InlineKeyword
                WithKeyword = None
                EqualsRange = None
            }

        match headPat, returnInfo with
        | SynPat.Named(ident = ident), Some(SynBindingReturnInfo(typeName = typeName)) ->
            let optExpr =
                let hasLiteralAttribute =
                    attributes
                    |> List.exists (fun al ->
                        al.Attributes
                        |> List.exists (fun a ->
                            match a.TypeName.LongIdent with
                            | [ literalIdent ] -> literalIdent.idText = "Literal"
                            | _ -> false))

                match expr with
                | SynExpr.Typed(expr = SynExpr.Const _) when hasLiteralAttribute -> Some expr
                | _ -> None

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
                    optExpr,
                    m,
                    trivia
                )

            Some valSig
        | SynPat.LongIdent(longDotId, _extraId, typarDecls, SynArgPats.Pats ps, vis, m), Some(SynBindingReturnInfo(typeName = returnType)) ->
            let typarDecls: SynValTyparDecls =
                Option.defaultValue (SynValTyparDecls(None, true)) typarDecls

            let functionName = List.tryLast longDotId.IdentsWithTrivia
            let parameterTypes = extractTypeFromPatterns ps

            if parameterTypes.Length <> ps.Length then
                None
            else
                match functionName with
                | None -> None
                | Some ident ->
                    let fullType, valInfo = mkReturnTypeAndValInfo parameterTypes returnType (Some expr)

                    let optExpr =
                        let hasLiteralAttribute =
                            attributes
                            |> List.exists (fun al ->
                                al.Attributes
                                |> List.exists (fun a ->
                                    match a.TypeName.LongIdent with
                                    | [ literalIdent ] -> literalIdent.idText = "Literal"
                                    | _ -> false))

                        match expr with
                        | SynExpr.Typed(expr = SynExpr.Const _) when hasLiteralAttribute -> Some expr
                        | _ -> None

                    let valSig =
                        SynValSig(attributes, ident, typarDecls, fullType, valInfo, isInline, isMutable, xmlDoc, vis, optExpr, m, trivia)

                    Some valSig
        | _ -> None

and mkReturnTypeAndValInfo (parameterTypes: SynType list) (returnType: SynType) (bodyExpr: SynExpr option) =
    let fullType =
        let mkFunType left right =
            SynType.Fun(left, right, Range.Zero, { ArrowRange = Range.Zero })

        let rec visit (pt: SynType list) =
            match pt with
            | [] -> failwith "unreachable for %A"
            | [ single ] -> single
            | h :: rest -> mkFunType h (visit rest)

        match parameterTypes with
        | [] ->
            match bodyExpr with
            | Some(SynExpr.Typed(expr = SynExpr.MatchLambda _)) -> returnType
            | _ -> SynType.Paren(returnType, Range.Zero)
        | _ ->
            let returnType =
                match returnType with
                | SynType.Fun _ -> SynType.Paren(returnType, Range.Zero)
                | _ -> returnType

            visit [ yield! parameterTypes; yield returnType ]

    let valInfo =
        let mapTypeToArgInfo (t: SynType) : SynArgInfo =
            match t with
            | SynType.SignatureParameter(attributes, optional, id, _, _) -> SynArgInfo(attributes, optional, id)
            | _ -> SynArgInfo([], false, None)

        let mapTypeToArgInfoList: SynType -> SynArgInfo list =
            function
            | SynType.Tuple(path = segments) -> getTypeFromTuplePath segments |> List.map mapTypeToArgInfo
            | t -> [ mapTypeToArgInfo t ]

        match fullType with
        | SynType.Fun _ when parameterTypes.IsEmpty ->
            match bodyExpr with
            | Some(SynExpr.Typed(expr = SynExpr.MatchLambda _)) ->
                let rec visit (t: SynType) (continuation: SynArgInfo list list -> SynArgInfo list list) : SynValInfo =
                    match t with
                    | SynType.Fun(argType, returnType, _, _) ->
                        visit returnType (fun argLists ->
                            let head = mapTypeToArgInfoList argType
                            head :: argLists |> continuation)
                    | returnType -> SynValInfo(continuation [], mapTypeToArgInfo returnType)

                visit fullType id
            | _ -> SynInfo.emptySynValData.SynValInfo
        | _ ->
            match parameterTypes with
            | [] -> SynInfo.emptySynValData.SynValInfo
            | ts ->
                let argInfos = List.map mapTypeToArgInfoList ts
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

/// Extract a `new: a:int -> TypeName`
and extractConstructorVal
    (synAttributeLists: SynAttributes)
    (preXmlDoc: PreXmlDoc)
    (vis: SynAccess option)
    (parameterTypes: SynType)
    (typeName: SynType)
    =
    let fullType, valInfo = mkReturnTypeAndValInfo [ parameterTypes ] typeName None

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
            { SynValSigTrivia.Zero with
                LeadingKeyword = SynLeadingKeyword.New Range.Zero
            }
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

// TODO: complete the list
and extractSynMemberSig (typeName: SynType) (md: SynMemberDefn) : SynMemberSig option =
    match md with
    | SynMemberDefn.Open _
    | SynMemberDefn.LetBindings _
    | SynMemberDefn.NestedType _ -> None
    | SynMemberDefn.ValField(fieldInfo, m) -> Some(SynMemberSig.ValField(fieldInfo, m))
    | SynMemberDefn.Member(
        memberDefn = SynBinding(headPat = NewPattern parameters; attributes = synAttributeLists; xmlDoc = preXmlDoc; accessibility = vis)) ->
        let ts = extractTypeFromPatterns parameters
        let parameterTypes, isFullyTyped = mkTupleType parameters.Length ts

        if not isFullyTyped then
            None
        else
            extractConstructorVal synAttributeLists preXmlDoc vis parameterTypes typeName
    | SynMemberDefn.Member(SynBinding(valData = SynValData(memberFlags = mf)) as binding, _range) ->
        extractVal binding
        |> Option.bind (fun valSig -> mf |> Option.map (fun mf -> valSig, mf))
        |> Option.map (fun (valSig, mf) ->
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
                                              m) ->
                            Some(SynType.SignatureParameter(attributes, isOptional, Some ident, wrapFunTypeInParens t, m))
                        | SynSimplePat.Typed(SynSimplePat.Id(ident = ident; isOptional = isOptional), t, m) ->
                            Some(SynType.SignatureParameter([], isOptional, Some ident, wrapFunTypeInParens t, m))
                        | _ -> None)

                mkTupleType pats.Length ts

        if not allParametersAreTyped then
            None
        else
            extractConstructorVal synAttributeLists preXmlDoc vis parameterTypes typeName
    | SynMemberDefn.ImplicitInherit(inheritType, _inheritArgs, _inheritAlias, _range) -> Some(SynMemberSig.Inherit(inheritType, Range.Zero))
    | SynMemberDefn.AutoProperty(
        attributes = attributes; ident = ident; memberFlags = memberFlags; typeOpt = typeOpt; xmlDoc = xmlDoc; accessibility = vis) ->
        typeOpt
        |> Option.map (fun t ->
            let synMemberFlags: SynMemberFlags =
                { memberFlags with
                    MemberKind = SynMemberKind.PropertyGet
                }

            let arity =
                SynValInfo.SynValInfo(
                    curriedArgInfos = [],
                    returnInfo = SynArgInfo.SynArgInfo(attributes = [], optional = false, ident = None)
                )

            let synValSig =
                SynValSig(
                    attributes,
                    SynIdent(ident, None),
                    SynValTyparDecls(None, true),
                    t,
                    arity,
                    false,
                    false,
                    xmlDoc,
                    vis,
                    None,
                    Range.Zero,
                    { SynValSigTrivia.Zero with
                        LeadingKeyword = SynLeadingKeyword.Val Range.Zero
                    }
                )

            SynMemberSig.Member(synValSig, synMemberFlags, Range.Zero, SynMemberSigMemberTrivia.Zero))
    // TODO: support for DisableInMemoryProjectReferences
    | SynMemberDefn.GetSetMember(memberDefnForGet, memberDefnForSet, _, _synMemberGetSetTrivia) ->
        match memberDefnForGet, memberDefnForSet with
        | Some(SynBinding(
            attributes = attributes
            xmlDoc = xmlDoc
            headPat = SynPat.LongIdent(argPats = SynArgPats.Pats(pats = [ _ ]); longDotId = lid; accessibility = vis)
            returnInfo = Some getReturnInfo)),
          // TODO: check for typed setter parameter?
          Some(SynBinding(headPat = SynPat.LongIdent(argPats = SynArgPats.Pats(pats = [ _ ])); returnInfo = Some _)) ->
            let ident = lid.LongIdent |> List.last

            let synMemberFlags: SynMemberFlags =
                {
                    IsInstance = true
                    IsDispatchSlot = false
                    IsOverrideOrExplicitImpl = false
                    IsFinal = false
                    GetterOrSetterIsCompilerGenerated = false
                    MemberKind = SynMemberKind.PropertyGetSet
                }

            let arity =
                SynValInfo.SynValInfo(
                    curriedArgInfos = [],
                    returnInfo = SynArgInfo.SynArgInfo(attributes = [], optional = false, ident = None)
                )

            let t =
                match getReturnInfo with
                | SynBindingReturnInfo(typeName = t) -> t

            let synValSig =
                SynValSig(
                    attributes,
                    SynIdent(ident, None),
                    SynValTyparDecls(None, true),
                    t,
                    arity,
                    false,
                    false,
                    xmlDoc,
                    vis,
                    None,
                    Range.Zero,
                    { SynValSigTrivia.Zero with
                        LeadingKeyword = SynLeadingKeyword.Member Range.Zero
                    }
                )

            Some(SynMemberSig.Member(synValSig, synMemberFlags, Range.Zero, SynMemberSigMemberTrivia.Zero))
        | _ -> failwithf $"SynMemberDefn.GetSetMember at %A{md.Range} %s{md.Range.FileName}"

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

    let sigFileInput =
        ParsedSigFileInput(fileName, qualifiedName, file.ScopedPragmas, file.HashDirectives, content, trivia, Set.empty)

    let signatureText =
        let oak = ASTTransformer.mkOak sigFileInput
        let ctx = CodePrinter.genFile oak Context.Context.Default
        Context.dump false ctx

    let sigFile = file.FileName + "i"
    System.IO.File.WriteAllText(sigFile, signatureText)

    ParsedInput.SigFile sigFileInput
