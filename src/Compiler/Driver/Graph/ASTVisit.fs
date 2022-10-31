module FSharp.Compiler.Graph.ASTVisit

open FSharp.Compiler.Syntax
open FSharp.Compiler.SyntaxTrivia

let unsupported = "unsupported"
type ReferenceKind =
    | Type
    | ModuleOrNamespace

/// Reference to a module or type, found in the AST
type Reference =
    {
        Ident : LongIdent
        Kind : ReferenceKind
    }
    
type Abbreviation =
    {
        Alias : Ident
        Target : LongIdent
    }
    
/// Reference to a module or type, found in the AST
type ReferenceOrAbbreviation =
    | Reference of Reference
    | Abbreviation of Abbreviation

type private References = ReferenceOrAbbreviation seq

let rec visitSynModuleDecl (decl : SynModuleDecl) : References =
    // TODO
    match decl with
    | SynModuleDecl.Attributes(synAttributeLists, _) ->
        visitSynAttributeLists synAttributeLists
    | SynModuleDecl.Exception(synExceptionDefn, _) ->
        visitSynExceptionDefn synExceptionDefn
    | SynModuleDecl.Expr(synExpr, _) ->
        visitExpr synExpr
    | SynModuleDecl.HashDirective(parsedHashDirective, _) ->
        visitHashDirective parsedHashDirective
    | SynModuleDecl.Let(_, synBindings, _) ->
        visitBindings synBindings
    | SynModuleDecl.Open(synOpenDeclTarget, _) -> 
        visitSynOpenDeclTarget synOpenDeclTarget
    | SynModuleDecl.Types(synTypeDefns, _) ->
        visitSynTypeDefns synTypeDefns
    | SynModuleDecl.ModuleAbbrev(ident, longId, _) ->
        [ReferenceOrAbbreviation.Abbreviation({Alias = ident; Target = longId})]
    | SynModuleDecl.NamespaceFragment synModuleOrNamespace ->
        visitSynModuleOrNamespace synModuleOrNamespace
    | SynModuleDecl.NestedModule(synComponentInfo, _, synModuleDecls, _, _, synModuleDeclNestedModuleTrivia) ->
        seq {
            yield! visitSynComponentInfo synComponentInfo
            yield! Seq.collect visitSynModuleDecl synModuleDecls
            yield! visitSynModuleDeclNestedModuleTrivia synModuleDeclNestedModuleTrivia
        }

and visitSynModuleDeclNestedModuleTrivia (_ : SynModuleDeclNestedModuleTrivia) : References =
    [] // TODO check

and visitHashDirective (_ : ParsedHashDirective) : References =
    [] // TODO Check

and visitSynIdent (_ : SynIdent) : References =
    [] // TODO Check

and visitSynTupleTypeSegment (x : SynTupleTypeSegment) : References =
    match x with
    | SynTupleTypeSegment.Slash _ ->
        []
    | SynTupleTypeSegment.Star _ ->
        []
    | SynTupleTypeSegment.Type typeName ->
        visitType typeName

and visitSynTupleTypeSegments (x : SynTupleTypeSegment list) : References =
    Seq.collect visitSynTupleTypeSegment x 

and visitTypar (x : SynTypar) : References =
    match x with
    | SynTypar.SynTypar(_, _, _) ->
        [] // TODO check

and visitSynRationalConst (_ : SynRationalConst) =
    [] // TODO check

and visitSynConst (_ : SynConst) : References =
    [] // TODO Check

and visitSynTypes (x : SynType list) : References =
    Seq.collect visitType x

and visitTypeConstraints (x : SynTypeConstraint list) : References =
    Seq.collect visitTypeConstraint x

and visitSynTyparDecl (x : SynTyparDecl) : References =
    match x with
    | SynTyparDecl(synAttributeLists, synTypar) ->
        seq {
            yield! visitSynAttributeLists synAttributeLists
            yield! visitTypar synTypar
        }

and visitSynTyparDeclList (x : SynTyparDecl list) : References =
    Seq.collect visitSynTyparDecl x

and visitSynTyparDecls (x : SynTyparDecls) : References =
    match x with
    | SynTyparDecls.PostfixList(synTyparDecls, synTypeConstraints, _) ->
        seq {
            yield! visitSynTyparDeclList synTyparDecls
            yield! visitTypeConstraints synTypeConstraints
        }
    | SynTyparDecls.PrefixList(synTyparDecls, _) ->
        visitSynTyparDeclList synTyparDecls
    | SynTyparDecls.SinglePrefix(synTyparDecl, _) ->
        visitSynTyparDecl synTyparDecl

and visitSynValTyparDecls (x : SynValTyparDecls) : References =
    match x with
    | SynValTyparDecls(synTyparDeclsOption, _) ->
        match synTyparDeclsOption with
        | Some decls -> visitSynTyparDecls decls
        | None -> []

and visitValSig (x : SynValSig) : References =
    match x with
    | SynValSig(synAttributeLists, synIdent, synValTyparDecls, synType, synValInfo, _, _, preXmlDoc, synAccessOption, synExprOption, _, _) ->
        seq {
            yield! visitSynAttributeLists synAttributeLists
            yield! visitSynIdent synIdent
            yield! visitSynValTyparDecls synValTyparDecls
            yield! visitType synType
            yield! visitSynValInfo synValInfo
            yield! visitPreXmlDoc preXmlDoc
            match synAccessOption with | Some access -> yield! visitSynAccess access | None -> ()
            match synExprOption with | Some expr -> yield! visitExpr expr | None -> ()
        }

and visitSynTypeDefnSignRepr (x : SynTypeDefnSigRepr) : References =
    match x with
    | SynTypeDefnSigRepr.Exception synExceptionDefnRepr ->
        visitSynExceptionDefnRepr synExceptionDefnRepr
    | SynTypeDefnSigRepr.Simple(synTypeDefnSimpleRepr, _) ->
        visitTypeDefnSimpleRepr synTypeDefnSimpleRepr
    | SynTypeDefnSigRepr.ObjectModel(synTypeDefnKind, synMemberSigs, _) ->
        seq {
            yield! visitSynTypeDefnKind synTypeDefnKind
            yield! (Seq.collect visitMemberSig synMemberSigs)
        }    

and visitSynTypeDefnSign (x : SynTypeDefnSig) : References =
    match x with
    | SynTypeDefnSig(synComponentInfo, synTypeDefnSigRepr, _, _, _) ->
        seq {
            yield! visitSynComponentInfo synComponentInfo
            yield! visitSynTypeDefnSignRepr synTypeDefnSigRepr
        }

and visitMemberSig (x : SynMemberSig) : References =
    match x with
    | SynMemberSig.Inherit(inheritedType, _) ->
        visitType inheritedType
    | SynMemberSig.Interface(interfaceType, _) ->
        visitType interfaceType
    | SynMemberSig.Member(synValSig, _, _) ->
        seq {
            yield! visitValSig synValSig
        }
    | SynMemberSig.NestedType(synTypeDefnSig, _) ->
        visitSynTypeDefnSign synTypeDefnSig
    | SynMemberSig.ValField(synField, _) ->
        visitSynField synField

and visitTypeConstraint (x : SynTypeConstraint) : References =
    match x with
    | SynTypeConstraint.WhereTyparIsValueType(typar, _) ->
        visitTypar typar
    | SynTypeConstraint.WhereTyparIsReferenceType(typar, _) ->
        visitTypar typar
    | SynTypeConstraint.WhereTyparIsUnmanaged(typar, _) ->
        visitTypar typar
    | SynTypeConstraint.WhereTyparSupportsNull(typar, _) ->
        visitTypar typar
    | SynTypeConstraint.WhereTyparIsComparable(typar, _) ->
        visitTypar typar
    | SynTypeConstraint.WhereTyparIsEquatable(typar, _) ->
        visitTypar typar
    | SynTypeConstraint.WhereTyparDefaultsToType(typar, typeName: SynType, _) ->
        seq {
            yield! visitTypar typar
            yield! visitType typeName
        }
    | SynTypeConstraint.WhereTyparSubtypeOfType(typar, typeName: SynType, _) ->
        seq {
            yield! visitTypar typar
            yield! visitType typeName
        }
    | SynTypeConstraint.WhereTyparSupportsMember(typars: SynType, memberSig: SynMemberSig, _) ->
        seq {
            yield! visitType typars
            yield! visitMemberSig memberSig
        }
    | SynTypeConstraint.WhereTyparIsEnum(typar, typeArgs: SynType list, _) ->
        seq {
            yield! visitTypar typar
            yield! visitSynTypes typeArgs
        }
    | SynTypeConstraint.WhereTyparIsDelegate(typar, typeArgs: SynType list, _) ->
        seq {
            yield! visitTypar typar
            yield! visitSynTypes typeArgs
        }
    | SynTypeConstraint.WhereSelfConstrained(selfConstraint, _) ->
        visitType selfConstraint

and visitType (x : SynType) : References =
    match x with
    | SynType.Anon _ ->
        []
    | SynType.App(typeName, _, typeArgs, _, _, _, _) ->
        seq {
            yield! visitType typeName
            yield! typeArgs |> Seq.collect visitType
        }
    | SynType.Array(_, elementType, _) ->
        visitType elementType
    | SynType.Fun(argType, returnType, _, _) ->
        seq {
            yield! visitType argType
            yield! visitType returnType
        }
    | SynType.Or(lhsType, rhsType, _, _) ->
        seq {
            yield! visitType lhsType
            yield! visitType rhsType
        }
    | SynType.Paren(innerType, _) ->
        visitType innerType
    | SynType.Tuple(_, synTupleTypeSegments, _) ->
        visitSynTupleTypeSegments synTupleTypeSegments
    | SynType.Var(synTypar, _) ->
        visitTypar synTypar
    | SynType.AnonRecd(_, fields, _) ->
        fields |> Seq.collect (fun (_, f) -> visitType f)
    | SynType.HashConstraint(innerType, _) ->
        visitType innerType
    | SynType.LongIdent synLongIdent ->
        visitSynLongIdent synLongIdent
    | SynType.MeasureDivide(synType, divisor, _) ->
        seq {
            yield! visitType synType
            yield! visitType divisor
        }
    | SynType.MeasurePower(baseMeasure, synRationalConst, _) ->
        seq {
            yield! visitType baseMeasure
            yield! visitSynRationalConst synRationalConst
        }
    | SynType.SignatureParameter(synAttributeLists, _, _, usedType, _) ->
        seq {
            yield! visitSynAttributeLists synAttributeLists
            yield! visitType usedType
        }
    | SynType.StaticConstant(synConst, _) ->
        visitSynConst synConst
    | SynType.LongIdentApp(typeName, synLongIdent, _, typeArgs, _, _, _) ->
        seq {
            yield! visitType typeName
            yield! visitSynLongIdent synLongIdent
            yield! visitSynTypes typeArgs
        }
    | SynType.StaticConstantExpr(synExpr, _) ->
        seq {
            yield! visitExpr synExpr
        }
    | SynType.StaticConstantNamed(synType, value, _) ->
        seq {
            yield! visitType synType
            yield! visitType value
        }
    | SynType.WithGlobalConstraints(typeName, synTypeConstraints, _) ->
        seq {
            yield! visitType typeName
            yield! visitTypeConstraints synTypeConstraints
        }

and visitPreXmlDoc (_ : FSharp.Compiler.Xml.PreXmlDoc) : References =
    [] // TODO Check

and visitSynAccess (_ : SynAccess) : References =
    [] // TODO check

and visitSynField (x : SynField) : References =
    match x with
    | SynField.SynField(synAttributeLists, _, _, fieldType, _, preXmlDoc, synAccessOption, _, _) ->
        seq {
            yield! visitSynAttributeLists synAttributeLists
            yield! visitType fieldType
            yield! visitPreXmlDoc preXmlDoc
            match synAccessOption with | Some access -> yield! visitSynAccess access | None -> ()
        }

and visitSynFields (x : SynField list) : References =
    Seq.collect visitSynField x

and visitSynUnionCaseKind (x : SynUnionCaseKind) : References =
    match x with
    | SynUnionCaseKind.Fields _ ->
        
        [] // TODO
    | SynUnionCaseKind.FullType(_, _) ->
        [] // TODO

and visitSynUnionCase (x : SynUnionCase) : References =
    match x with
    | SynUnionCase.SynUnionCase(synAttributeLists, synIdent, synUnionCaseKind, _, _, _, _) ->
        seq {
            yield! visitSynAttributeLists synAttributeLists
            yield! visitSynIdent synIdent
            yield! visitSynUnionCaseKind synUnionCaseKind
        }

and visitSynExceptionDefnRepr (x : SynExceptionDefnRepr) : References =
    match x with
    | SynExceptionDefnRepr.SynExceptionDefnRepr(synAttributeLists, synUnionCase, identsOption, preXmlDoc, synAccessOption, _) ->
        seq {
            yield! visitSynAttributeLists synAttributeLists
            yield! visitSynUnionCase synUnionCase
            match identsOption with | Some ident -> yield! visitLongIdent ident | None -> ()
            yield! visitPreXmlDoc preXmlDoc
            match synAccessOption with | Some synAccess -> yield! visitSynAccess synAccess | None -> ()
        }

and visitEnumCase (x : SynEnumCase) : References =
    match x with
    | SynEnumCase(synAttributeLists, synIdent, synConst, _, preXmlDoc, _, _) ->
        seq {
            yield! visitSynAttributeLists synAttributeLists
            yield! visitSynIdent synIdent
            yield! visitSynConst synConst
            yield! visitPreXmlDoc preXmlDoc
        }

and visitMulti (f) (items) : References = Seq.collect f items

and visitEnumCases = visitMulti visitEnumCase

and visitSynUnionCases = visitMulti visitSynUnionCase

and visitParserDetail (_ : ParserDetail) : References =
    []

and visitTypeDefnSimpleRepr (x : SynTypeDefnSimpleRepr) : References =
    match x with
    | SynTypeDefnSimpleRepr.Enum(synEnumCases, _) ->
        visitEnumCases synEnumCases
    | SynTypeDefnSimpleRepr.Exception synExceptionDefnRepr ->
        visitSynExceptionDefnRepr synExceptionDefnRepr
    | SynTypeDefnSimpleRepr.General(synTypeDefnKind, inherits, _, _, _, _, _, _) ->
        seq {
            yield! visitSynTypeDefnKind synTypeDefnKind
            let inheritTypes = inherits |> List.map (fun (t, _, _) -> t)
            yield! visitSynTypes inheritTypes
        }
    | SynTypeDefnSimpleRepr.None _ ->
        []
    | SynTypeDefnSimpleRepr.Record(synAccessOption, recordFields, _) ->
        seq {
            match synAccessOption with | Some access -> yield! visitSynAccess access | None -> ()
            yield! visitSynFields recordFields
        }
    | SynTypeDefnSimpleRepr.Union(synAccessOption, synUnionCases, _) ->
        seq {
            match synAccessOption with | Some access -> yield! visitSynAccess access | None -> ()
            yield! visitSynUnionCases synUnionCases
        }
    | SynTypeDefnSimpleRepr.TypeAbbrev(parserDetail, rhsType, _) ->
        seq {
            yield! visitParserDetail parserDetail
            yield! visitType rhsType
        }
    | SynTypeDefnSimpleRepr.LibraryOnlyILAssembly(_, _) ->
        []

and visitSynArgInfo (x : SynArgInfo) : References =
    match x with
    | SynArgInfo(synAttributeLists, _, _) ->
        visitSynAttributeLists synAttributeLists

and visitSynValInfo (x : SynValInfo) : References =
    match x with
    | SynValInfo(curriedArgInfos, synArgInfo) ->
        seq {
            yield! curriedArgInfos |> Seq.concat |> Seq.collect visitSynArgInfo
            yield! visitSynArgInfo synArgInfo
        }

and visitSynTypeDefnKind (x : SynTypeDefnKind) : References =
    match x with
    | SynTypeDefnKind.Delegate(synType, synValInfo) ->
        seq {
            yield! visitType synType
            yield! visitSynValInfo synValInfo
        }
    | SynTypeDefnKind.Abbrev
    | SynTypeDefnKind.Augmentation _
    | SynTypeDefnKind.Class
    | SynTypeDefnKind.Interface
    | SynTypeDefnKind.Opaque
    | SynTypeDefnKind.Record
    | SynTypeDefnKind.Struct
    | SynTypeDefnKind.Union
    | SynTypeDefnKind.Unspecified
    | SynTypeDefnKind.IL ->
        []

and visitSynTypeDefnRepr (x : SynTypeDefnRepr) : References =
    match x with
    | SynTypeDefnRepr.Exception synExceptionDefnRepr ->
        visitSynExceptionDefnRepr synExceptionDefnRepr
    | SynTypeDefnRepr.Simple(synTypeDefnSimpleRepr, _) ->
        visitTypeDefnSimpleRepr synTypeDefnSimpleRepr
    | SynTypeDefnRepr.ObjectModel(synTypeDefnKind, synMemberDefns, _) ->
        seq {
            yield! visitSynTypeDefnKind synTypeDefnKind
            yield! visitSynMemberDefns synMemberDefns
        }

and visitSynValSig (x : SynValSig) : References =
    match x with
    | SynValSig(synAttributeLists, synIdent, synValTyparDecls, synType, synValInfo, _, _, preXmlDoc, synAccessOption, synExprOption, _, _) ->
        seq {
            yield! visitSynAttributeLists synAttributeLists
            yield! visitSynIdent synIdent
            yield! visitSynValTyparDecls synValTyparDecls
            yield! visitType synType
            yield! visitSynValInfo synValInfo
            yield! visitPreXmlDoc preXmlDoc
            match synAccessOption with | Some access -> yield! visitSynAccess access | None -> ()
            match synExprOption with | Some expr -> yield! visitExpr expr | None -> ()
        }

and visitSynMemberKind (_ : SynMemberKind) : References =
    []

and visitSynMemberFlags (_ : SynMemberFlags) : References =
    []
    
and visitSynSimplePats (x : SynSimplePats) : References =
    match x with
    | SynSimplePats.Typed(synSimplePats, targetType, _) ->
        seq {
            yield! visitSynSimplePats synSimplePats
            yield! visitType targetType
        }
    | SynSimplePats.SimplePats(synSimplePats, _) ->
        Seq.collect visitSynSimplePat synSimplePats

and visitSynSimplePatAlternativeIdInfoRef (_ : SynSimplePatAlternativeIdInfo ref) : References =
    [] // TODO Check

and visitSynSimplePat (x : SynSimplePat) : References =
    match x with
    | SynSimplePat.Attrib(synSimplePat, synAttributeLists, _) ->
        seq {
            yield! visitSynSimplePat synSimplePat
            yield! visitSynAttributeLists synAttributeLists
        }
    | SynSimplePat.Id(_, synSimplePatAlternativeIdInfoRefOption, _, _, _, _) ->
        seq {
            match synSimplePatAlternativeIdInfoRefOption with | Some info -> yield! visitSynSimplePatAlternativeIdInfoRef info | None -> ()
        }
    | SynSimplePat.Typed(synSimplePat, targetType, _) ->
        seq {
            yield! visitSynSimplePat synSimplePat
            yield! visitType targetType
        }

and visitSynMemberDefn (defn : SynMemberDefn) : References =
    match defn with
    | SynMemberDefn.Inherit(baseType, _, _) ->
        visitType baseType
    | SynMemberDefn.Interface(interfaceType, _, synMemberDefnsOption, _) ->
        seq {
            yield! visitType interfaceType
            match synMemberDefnsOption with | Some defns -> yield! visitSynMemberDefns defns | None -> ()
        }
    | SynMemberDefn.Member(memberDefn, _) ->
        visitSynBinding memberDefn
    | SynMemberDefn.Open(synOpenDeclTarget, _) ->
        visitSynOpenDeclTarget synOpenDeclTarget
    | SynMemberDefn.AbstractSlot(synValSig, synMemberFlags, _) ->
        seq {
            yield! visitSynValSig synValSig
            yield! visitSynMemberFlags synMemberFlags
        }
    | SynMemberDefn.AutoProperty(synAttributeLists, _, _, synTypeOption, synMemberKind, synMemberFlags, memberFlagsForSet, preXmlDoc, synAccessOption, synExpr, _, _) ->
        seq {
            yield! visitSynAttributeLists synAttributeLists
            match synTypeOption with | Some synType -> yield! visitType synType | None -> ()
            yield! visitSynMemberKind synMemberKind
            yield! visitSynMemberFlags synMemberFlags
            yield! visitSynMemberFlags memberFlagsForSet
            yield! visitPreXmlDoc preXmlDoc
            match synAccessOption with | Some synAccess -> yield! visitSynAccess synAccess | None -> ()
            yield! visitExpr synExpr
        }
    | SynMemberDefn.ImplicitCtor(synAccessOption, synAttributeLists, synSimplePats, _, _, _) ->
        seq {
            match synAccessOption with | Some synAccess -> yield! visitSynAccess synAccess | None -> ()
            yield! visitSynAttributeLists synAttributeLists
            yield! visitSynSimplePats synSimplePats
        }
    | SynMemberDefn.ImplicitInherit(inheritType, inheritArgs, _, _) ->
        seq {
            yield! visitType inheritType
            yield! visitExpr inheritArgs
        }
    | SynMemberDefn.LetBindings(synBindings, _, _, _) ->
        visitBindings synBindings
    | SynMemberDefn.NestedType(synTypeDefn, synAccessOption, _) ->
        seq {
            yield! visitSynTypeDefn synTypeDefn
            match synAccessOption with | Some access -> yield! visitSynAccess access | None -> ()
        }
    | SynMemberDefn.ValField(fieldInfo, _) ->
        visitSynField fieldInfo
    | SynMemberDefn.GetSetMember(memberDefnForGet, memberDefnForSet, _, _) ->
        seq {
            match memberDefnForGet with | Some binding -> yield! visitSynBinding binding | None -> ()
            match memberDefnForSet with | Some binding -> yield! visitSynBinding binding | None -> ()
        }

and visitSynMemberDefns (defns : SynMemberDefn list) : References =
    Seq.collect visitSynMemberDefn defns

and visitSynTypeDefn (defn : SynTypeDefn) : References =
    match defn with
    | SynTypeDefn.SynTypeDefn(synComponentInfo, synTypeDefnRepr, synMemberDefns, synMemberDefnOption, _, _) ->
        seq {
            yield! visitSynComponentInfo synComponentInfo
            yield! visitSynTypeDefnRepr synTypeDefnRepr
            yield! visitSynMemberDefns synMemberDefns
            match synMemberDefnOption with Some defn -> yield! visitSynMemberDefn defn | None -> ()
        }

and visitSynTypeDefns (defns : SynTypeDefn list) : References =
    Seq.collect visitSynTypeDefn defns

and visitSynExceptionDefn (x : SynExceptionDefn) : References =
    match x with
    | SynExceptionDefn(synExceptionDefnRepr, _, synMemberDefns, _) ->
        seq {
            yield! visitSynExceptionDefnRepr synExceptionDefnRepr
            yield! visitSynMemberDefns synMemberDefns
        }

and visitSynValData (x : SynValData) : References =
    match x with
    | SynValData(synMemberFlagsOption, synValInfo, _) ->
        seq {
            match synMemberFlagsOption with | Some flags -> yield! visitSynMemberFlags flags | None -> ()
            yield! visitSynValInfo synValInfo
        } 

and visitSynPats (x : SynPat list) : References =
    Seq.collect visitPat x

and visitPat (x : SynPat) : References =
    match x with
    | SynPat.Ands(synPats, _) ->
        visitSynPats synPats
    | SynPat.As(lhsPat, rhsPat, _) ->
        visitSynPats [lhsPat; rhsPat]
    | SynPat.Attrib(synPat, synAttributeLists, _) ->
        seq {
            yield! visitPat synPat
            yield! visitSynAttributeLists synAttributeLists
        }
    | SynPat.Const(synConst, _) ->
        visitSynConst synConst
    | SynPat.Named(synIdent, _, synAccessOption, _) ->
        seq {
            yield! visitSynIdent synIdent
            match synAccessOption with | Some access -> yield! visitSynAccess access | None -> ()
        }
    | SynPat.Null _ ->
        []
    | SynPat.Or(lhsPat, rhsPat, _, _) ->
        seq {
            yield! visitSynPats [lhsPat; rhsPat]
        }
    | SynPat.Paren(synPat, _) ->
        visitPat synPat
    | SynPat.Record(fieldPats, _) ->
        fieldPats
        |> Seq.collect (fun ((longId, _), _, pat) ->
            seq {
                yield! visitLongIdent longId
                yield! visitPat pat
            }
        )
    | SynPat.Tuple(_, elementPats, _) ->
        visitSynPats elementPats
    | SynPat.Typed(synPat, targetType, _) ->
        seq {
            yield! visitPat synPat
            yield! visitType targetType
        }
    | SynPat.Wild _ ->
        []
    | SynPat.InstanceMember(_, _, _, synAccessOption, _) ->
        seq {
            match synAccessOption with | Some access -> yield! visitSynAccess access | None -> ()
        }
    | SynPat.IsInst(synType, _) ->
        visitType synType
    | SynPat.ListCons(lhsPat, rhsPat, _, _) ->
        seq {
            yield! visitPat lhsPat
            yield! visitPat rhsPat
        }
    | SynPat.LongIdent(synLongIdent, _, synValTyparDeclsOption, synArgPats, synAccessOption, _) ->
        seq {
            yield! visitSynLongIdent synLongIdent
            match synValTyparDeclsOption with | Some decls -> yield! visitSynValTyparDecls decls | None -> ()
            yield! visitSynArgPats synArgPats
            match synAccessOption with | Some access -> yield! visitSynAccess access | None -> ()
        }
    | SynPat.OptionalVal(_, _) ->
        []
    | SynPat.QuoteExpr(synExpr, _) ->
        visitExpr synExpr
    | SynPat.ArrayOrList(_, elementPats, _) ->
        visitSynPats elementPats
    | SynPat.DeprecatedCharRange(_, _, _) ->
        []
    | SynPat.FromParseError(synPat, _) ->
        visitPat synPat

and visitBindingReturnInfo (x : SynBindingReturnInfo) : References =
    match x with
    | SynBindingReturnInfo(typeName, _, synAttributeLists, _) ->
        seq {
            yield! visitType typeName
            yield! visitSynAttributeLists synAttributeLists
        }

and visitSynBinding (x : SynBinding) : References =
    match x with
    | SynBinding.SynBinding(synAccessOption, _synBindingKind, _isInline, _isMutable, synAttributeLists, preXmlDoc, synValData, headPat, synBindingReturnInfoOption, synExpr, _range, _debugPointAtBinding, _synBindingTrivia) ->
        seq {
            match synAccessOption with | Some access -> yield! visitSynAccess access | None -> ()
            yield! visitSynAttributeLists synAttributeLists
            yield! visitPreXmlDoc preXmlDoc
            yield! visitSynValData synValData
            yield! visitPat headPat
            match synBindingReturnInfoOption with | Some info -> yield! visitBindingReturnInfo info | None -> ()
            yield! visitExpr synExpr
        }

and visitBindings (bindings : SynBinding list) : References =
    Seq.collect visitSynBinding bindings

and visitSynOpenDeclTarget (target : SynOpenDeclTarget) : References =
    match target with
    | SynOpenDeclTarget.Type(typeName, _range) ->
        visitType typeName
    | SynOpenDeclTarget.ModuleOrNamespace(synLongIdent, _range) ->
        [ReferenceOrAbbreviation.Reference {Ident = synLongIdent.LongIdent; Kind = ReferenceKind.ModuleOrNamespace}] 

and visitSynComponentInfo (info : SynComponentInfo) : References =
    match info with
    | SynComponentInfo(synAttributeLists, synTyparDeclsOption, _synTypeConstraints, _longId, preXmlDoc, _preferPostfix, synAccessOption, _range) ->
        seq {
            yield! visitSynAttributeLists synAttributeLists
            match synTyparDeclsOption with | Some decls -> yield! visitSynTyparDecls decls | None -> ()
            // Don't include this as it's a module definition rather than reference
            // yield! visitLongIdent longId
            yield! visitPreXmlDoc preXmlDoc
            match synAccessOption with | Some access -> yield! visitSynAccess access | None -> ()
        }

and visitLongIdent (ident : LongIdent) : References =
    [ReferenceOrAbbreviation.Reference {Ident = ident; Kind = ReferenceKind.Type}]
    
and visitSynLongIdent (ident : SynLongIdent) : References  =
    [ReferenceOrAbbreviation.Reference {Ident = ident.LongIdent; Kind = ReferenceKind.Type}]
   
and visitSynMatchClause (x : SynMatchClause) : References =
    match x with
    | SynMatchClause(synPat, synExprOption, resultExpr, _range, _debugPointAtTarget, _) ->
        seq {
            yield! visitPat synPat
            match synExprOption with | Some expr -> yield! visitExpr expr | None -> ()
            yield! visitExpr resultExpr
        }
   
and visitSynMatchClauses = visitMulti visitSynMatchClause
      
and visitExprOnly (_ : SeqExprOnly) : References =
    []
      
and visitSynInterpolatedStringPart (x : SynInterpolatedStringPart) : References =
    match x with
    | SynInterpolatedStringPart.String(_, _) ->
        []
    | SynInterpolatedStringPart.FillExpr(fillExpr, _) ->
        visitExpr fillExpr
      
and visitSynInterpolatedStringParts (x : SynInterpolatedStringPart list) : References =
    Seq.collect visitSynInterpolatedStringPart x
      
and visitSynInterfaceImpl (x : SynInterfaceImpl) : References =
    match x with
    | SynInterfaceImpl(interfaceTy, _, synBindings, synMemberDefns, _) ->
        seq {
            yield! visitType interfaceTy
            yield! visitBindings synBindings
            yield! visitSynMemberDefns synMemberDefns
        }
      
and visitSynInterfaceImpls (x : SynInterfaceImpl list) : References =
    Seq.collect visitSynInterfaceImpl x
      
and visitSynArgPats (x : SynArgPats) : References =
    match x with
    | SynArgPats.Pats synPats ->
        visitSynPats synPats
    | SynArgPats.NamePatPairs(tuples, _, _) ->
        tuples
        |> Seq.collect (fun (_, _, pat) -> visitPat pat)
      
and visitSynMemberSig (x : SynMemberSig) : References =
    match x with
    | SynMemberSig.Inherit(inheritedType, _) ->
        visitType inheritedType
    | SynMemberSig.Interface(interfaceType, _) ->
        visitType interfaceType
    | SynMemberSig.Member(synValSig, _, _) ->
        visitValSig synValSig
    | SynMemberSig.NestedType(synTypeDefnSig, _) ->
        visitSynTypeDefnSign synTypeDefnSig
    | SynMemberSig.ValField(synField, _) ->
        visitSynField synField
      
and visitExpr (expr : SynExpr) =
    let l = System.Collections.Generic.List<ReferenceOrAbbreviation>()
    let go (items : References) =
        l.AddRange(items)
        
    match expr with
    | SynExpr.App(_, _, funcExpr, argExpr, _) ->
        visitExpr funcExpr |> go
        visitExpr argExpr |> go
    | SynExpr.Assert(synExpr, _) ->
        visitExpr synExpr |> go
    | SynExpr.Const(synConst, _) ->
        visitSynConst synConst |> go
    | SynExpr.Do(synExpr, _) ->
        visitExpr synExpr |> go
    | SynExpr.Downcast(synExpr, targetType, _) ->
        visitExpr synExpr |> go
        visitType targetType |> go
    | SynExpr.Dynamic(funcExpr, _, argExpr, _) ->
        visitExpr funcExpr |> go
        visitExpr argExpr |> go
    | SynExpr.Fixed(synExpr, _) ->
        visitExpr synExpr |> go
    | SynExpr.For(_, _, _, _, identBody, _, synExpr, doBody, _) ->
        visitExpr identBody |> go
        visitExpr synExpr |> go
        visitExpr doBody |> go
    | SynExpr.Ident _ ->
        ()
    | SynExpr.Lambda(_, _, synSimplePats, synExpr, tupleOption, _, _) ->
        visitSynSimplePats synSimplePats |> go
        visitExpr synExpr |> go
        match tupleOption with | Some (simplePats, expr) -> visitSynPats simplePats |> go; visitExpr expr |> go | None -> ()
    | SynExpr.Lazy(synExpr, _) ->
        visitExpr synExpr |> go
    | SynExpr.Match(_, synExpr, synMatchClauses, _, _) ->
        visitExpr synExpr |> go
        visitSynMatchClauses synMatchClauses |> go
    | SynExpr.New(_, targetType, synExpr, _) ->
        visitType targetType |> go
        visitExpr synExpr |> go
    | SynExpr.Null _ ->
        ()
    | SynExpr.Paren(synExpr, _, _, _) ->
        visitExpr synExpr |> go
    | SynExpr.Quote(synExpr, _, quotedExpr, _, _) ->
        visitExpr synExpr |> go
        visitExpr quotedExpr |> go
    | SynExpr.Record(tupleOption, copyInfo, _, _) ->
        match tupleOption with
        | Some(synType, synExpr, _, _, _) ->
            seq {
                yield! visitType synType
                yield! visitExpr synExpr
            }
            |> go
        | None ->
            ()
        match copyInfo with
        | Some(synExpr, _) ->
            visitExpr synExpr |> go
        | None ->
            ()
    | SynExpr.Sequential(_, _, synExpr, expr2, _) ->
        visitExpr synExpr |> go
        let mutable expr = expr2
        let mutable stop = false
        while not stop do
            match expr with
            | SynExpr.Sequential(_, _, synExpr, expr2, _) ->
                visitExpr synExpr |> go
                expr <- expr2
            | _ ->
                stop <- true
        visitExpr expr |> go
    | SynExpr.Set(targetExpr, rhsExpr, _) ->
        visitExpr targetExpr |> go
        visitExpr rhsExpr |> go
    | SynExpr.Tuple(_, synExprs, _, _) ->
        visitExprs synExprs |> go
    | SynExpr.Typar(synTypar, _) ->
        visitTypar synTypar |> go
    | SynExpr.Typed(synExpr, targetType, _) ->
        visitExpr synExpr |> go
        visitType targetType |> go
    | SynExpr.Upcast(synExpr, targetType, _) ->
        visitExpr synExpr |> go
        visitType targetType |> go
    | SynExpr.While(_, whileExpr, synExpr, _) ->
        visitExpr whileExpr |> go
        visitExpr synExpr |> go
    | SynExpr.AddressOf(_, synExpr, _, _) ->
        visitExpr synExpr |> go
    | SynExpr.AnonRecd(_, tupleOption, recordFields, _) ->
        match tupleOption with
        | Some(_, _) ->
            ()
        | None ->
            ()
        recordFields
        |> Seq.collect (fun (_, _, f) -> visitExpr f)
        |> go
    | SynExpr.ComputationExpr(_, synExpr, _) ->
        visitExpr synExpr |> go
    | SynExpr.DebugPoint(_, _, innerExpr) ->
        visitExpr innerExpr |> go
    | SynExpr.DoBang(synExpr, _) ->
        visitExpr synExpr |> go
    | SynExpr.DotGet(synExpr, _, synLongIdent, _) ->
        visitExpr synExpr |> go
        visitSynLongIdent synLongIdent |> go
    | SynExpr.DotSet(targetExpr, synLongIdent, rhsExpr, _) ->
        visitExpr targetExpr |> go
        visitSynLongIdent synLongIdent |> go
        visitExpr rhsExpr |> go
    | SynExpr.ForEach(_, _, seqExprOnly, _, synPat, enumExpr, bodyExpr, _) ->
        visitExprOnly seqExprOnly |> go
        visitPat synPat |> go
        visitExpr enumExpr |> go
        visitExpr bodyExpr |> go
    | SynExpr.ImplicitZero _ ->
        ()
    | SynExpr.IndexRange(synExprOption, _, exprOption, _, _, _) ->
        match synExprOption with | Some expr -> visitExpr expr |> go | None -> ()
        match exprOption with | Some expr -> visitExpr expr |> go | None -> ()
    | SynExpr.InferredDowncast(synExpr, _) ->
        visitExpr synExpr |> go
    | SynExpr.InferredUpcast(synExpr, _) ->
        visitExpr synExpr |> go
    | SynExpr.InterpolatedString(synInterpolatedStringParts, _, _) ->
        visitSynInterpolatedStringParts synInterpolatedStringParts |> go
    | SynExpr.JoinIn(lhsExpr, _, rhsExpr, _) ->
        visitExpr lhsExpr |> go
        visitExpr rhsExpr |> go
    | SynExpr.LongIdent(_, synLongIdent, synSimplePatAlternativeIdInfoRefOption, _) ->
        visitSynLongIdent synLongIdent |> go
        match synSimplePatAlternativeIdInfoRefOption with
        | Some info -> visitSynSimplePatAlternativeIdInfoRef info |> go
        | None -> ()
    | SynExpr.MatchBang(_, synExpr, synMatchClauses, _, _) ->
        visitExpr synExpr |> go
        visitSynMatchClauses synMatchClauses |> go
    | SynExpr.MatchLambda(_, _, synMatchClauses, _, _) ->
        visitSynMatchClauses synMatchClauses |> go
    | SynExpr.ObjExpr(objType, tupleOption, _, _, synMemberDefns, synInterfaceImpls, _, _) ->
        visitType objType |> go
        match tupleOption with
        | Some(synExpr, _) ->
            visitExpr synExpr |> go
        | None ->
            ()
        visitSynMemberDefns synMemberDefns |> go
        visitSynInterfaceImpls synInterfaceImpls |> go
    | SynExpr.TraitCall(supportTys, synMemberSig, argExpr, _) ->
        visitType supportTys |> go
        visitSynMemberSig synMemberSig |> go
        visitExpr argExpr |> go
    | SynExpr.TryFinally(tryExpr, finallyExpr, _, _, _, _) ->
        visitExpr tryExpr |> go
        visitExpr finallyExpr |> go
    | SynExpr.TryWith(tryExpr, synMatchClauses, _, _, _, _) ->
        visitExpr tryExpr |> go
        visitSynMatchClauses synMatchClauses |> go
    | SynExpr.TypeApp(synExpr, _, typeArgs, _, _, _, _) ->
        visitExpr synExpr |> go
        visitSynTypes typeArgs |> go
    | SynExpr.TypeTest(synExpr, targetType, _) ->
        visitExpr synExpr |> go
        visitType targetType |> go
    | SynExpr.ArbitraryAfterError(_, _) ->
        ()
    | SynExpr.ArrayOrList(_, synExprs, _) ->
        visitExprs synExprs |> go
    | SynExpr.DotIndexedGet(objectExpr, indexArgs, _, _) ->
        visitExpr objectExpr |> go
        visitExpr indexArgs |> go
    | SynExpr.DotIndexedSet(objectExpr, indexArgs, valueExpr, _, _, _) ->
        visitExpr objectExpr |> go
        visitExpr indexArgs |> go
        visitExpr valueExpr |> go
    | SynExpr.FromParseError(synExpr, _) ->
        visitExpr synExpr |> go
    | SynExpr.IfThenElse(synExpr, thenExpr, synExprOption, _, _, _, _) ->
        visitExpr synExpr |> go
        visitExpr thenExpr |> go
        match synExprOption with | Some expr -> visitExpr expr |> go | None -> ()
    | SynExpr.IndexFromEnd(synExpr, _) ->
        visitExpr synExpr |> go
    | SynExpr.LetOrUse(_, _, synBindings, synExpr, _, _) ->
        visitBindings synBindings |> go
        visitExpr synExpr |> go
    | SynExpr.LongIdentSet(synLongIdent, synExpr, _) ->
        visitSynLongIdent synLongIdent |> go
        visitExpr synExpr |> go
    | SynExpr.YieldOrReturn(_, synExpr, _) ->
        visitExpr synExpr |> go
    | SynExpr.ArrayOrListComputed(_, synExpr, _) ->
        visitExpr synExpr |> go
    | SynExpr.LetOrUseBang(_, _, _, synPat, synExpr, synExprAndBangs, body, _, _) ->
        visitPat synPat |> go
        visitExpr synExpr |> go
        Seq.collect visitExprAndBang synExprAndBangs |> go
        visitExpr body |> go
    | SynExpr.LibraryOnlyStaticOptimization(synStaticOptimizationConstraints, synExpr, optimizedExpr, _) ->
        visitStaticOptimizationConstraints synStaticOptimizationConstraints |> go
        visitExpr synExpr |> go
        visitExpr optimizedExpr |> go
    | SynExpr.NamedIndexedPropertySet(synLongIdent, synExpr, expr2, _) ->
        visitSynLongIdent synLongIdent |> go
        visitExpr synExpr |> go
        visitExpr expr2 |> go
    | SynExpr.SequentialOrImplicitYield(_, synExpr, expr2, ifNotStmt, _) ->
        visitExpr synExpr |> go
        visitExpr expr2 |> go
        visitExpr ifNotStmt |> go
    | SynExpr.YieldOrReturnFrom(_, synExpr, _) ->
        visitExpr synExpr |> go
    | SynExpr.DotNamedIndexedPropertySet(targetExpr, synLongIdent, argExpr, rhsExpr, _) ->
        visitExpr targetExpr |> go
        visitSynLongIdent synLongIdent |> go
        visitExpr argExpr |> go
        visitExpr rhsExpr |> go
    | SynExpr.LibraryOnlyILAssembly(_, typeArgs, synExprs, synTypes, _) ->
        visitSynTypes typeArgs |> go
        visitExprs synExprs |> go
        visitSynTypes synTypes |> go
    | SynExpr.DiscardAfterMissingQualificationAfterDot(synExpr, _) ->
        visitExpr synExpr |> go
    | SynExpr.LibraryOnlyUnionCaseFieldGet(synExpr, longId, _, _) ->
        visitExpr synExpr |> go
        visitLongIdent longId |> go
    | SynExpr.LibraryOnlyUnionCaseFieldSet(synExpr, longId, _, rhsExpr, _) ->
        visitExpr synExpr |> go
        visitLongIdent longId |> go
        visitExpr rhsExpr |> go
    
    l
    
and visitStaticOptimizationConstraint (x : SynStaticOptimizationConstraint) : References =
    match x with
    | SynStaticOptimizationConstraint.WhenTyparIsStruct(synTypar, _) ->
        visitTypar synTypar
    | SynStaticOptimizationConstraint.WhenTyparTyconEqualsTycon(synTypar, rhsType, _) ->
        seq {
            yield! visitTypar synTypar
            yield! visitType rhsType
        }
    
and visitStaticOptimizationConstraints = visitMulti visitStaticOptimizationConstraint
    
and visitExprAndBang (x : SynExprAndBang) : References =
    match x with
    | SynExprAndBang(_, _, _, synPat, synExpr, _, _) ->
        seq {
            yield! visitPat synPat
            yield! visitExpr synExpr
        }
    
and visitExprs = visitMulti visitExpr
    
and visitSynAttribute (attribute : SynAttribute) : References  =
    seq {
        yield! visitSynLongIdent attribute.TypeName
        yield! visitExpr attribute.ArgExpr
    }
                
and visitSynAttributeList (attributeList : SynAttributeList) : References  =
    attributeList.Attributes
    |> Seq.collect visitSynAttribute

and visitSynAttributeLists (attributeLists : SynAttributeList list) : References  =
    Seq.collect visitSynAttributeList attributeLists

and visitSynModuleOrNamespace (x : SynModuleOrNamespace) : References  =
    match x with
    | SynModuleOrNamespace.SynModuleOrNamespace(_, _, _, synModuleDecls, _, synAttributeLists, _, _, _) ->
        seq {
            // Don't include 'longId' as that's module definition rather than reference
            yield! synModuleDecls |> Seq.collect visitSynModuleDecl
            yield! visitSynAttributeLists synAttributeLists 
        }
        
// Sigs

and visitSynMemberSigs = visitMulti visitSynMemberSig

and visitSynExceptionSig (x : SynExceptionSig) : References =
    match x with
    | SynExceptionSig(synExceptionDefnRepr, _, synMemberSigs, _) ->
        seq {
            yield! visitSynExceptionDefnRepr synExceptionDefnRepr
            yield! visitSynMemberSigs synMemberSigs
        }

and visitSynTypeDefnSigs = visitMulti visitSynTypeDefnSign

and visitSynModuleSigDecl (x : SynModuleSigDecl) : References =
    match x with
    | SynModuleSigDecl.Exception(synExceptionSig, _) ->
        visitSynExceptionSig synExceptionSig
    | SynModuleSigDecl.Open(synOpenDeclTarget, _) ->
        visitSynOpenDeclTarget synOpenDeclTarget
    | SynModuleSigDecl.Types(synTypeDefnSigs, _) ->
        visitSynTypeDefnSigs synTypeDefnSigs
    | SynModuleSigDecl.Val(synValSig, _) ->
        visitSynValSig synValSig
    | SynModuleSigDecl.HashDirective(_, _) ->
        []
    | SynModuleSigDecl.ModuleAbbrev(_, _, _) ->
        // TODO Module abbrevation can break the algorithm.
        // We need to either give up when seeing this or handle it properly.
        //
        // Consider the following:
        // module A = module A1 = let x = 1
        // module B = A
        // let x = B.A1.x
        failwith "Module abbreviations are not currently supported"
    | SynModuleSigDecl.NamespaceFragment synModuleOrNamespaceSig ->
        visitSynModuleOrNamespaceSig synModuleOrNamespaceSig
    | SynModuleSigDecl.NestedModule(synComponentInfo, _, synModuleSigDecls, _, _) ->
        seq {
            yield! visitSynComponentInfo synComponentInfo
            yield! visitSynModuleSigDecls synModuleSigDecls
        }

and visitSynModuleSigDecls = visitMulti visitSynModuleSigDecl

and visitSynModuleOrNamespaceSig (x : SynModuleOrNamespaceSig) : References  =
    match x with
    | SynModuleOrNamespaceSig.SynModuleOrNamespaceSig(_, _, _, synModuleDecls, _, synAttributeLists, _, _, _) ->
        seq {
            // Don't include 'longId' as that's module definition rather than reference
            yield! synModuleDecls |> Seq.collect visitSynModuleSigDecl
            yield! visitSynAttributeLists synAttributeLists 
        }

and findModuleAndTypeRefs (input : ParsedInput) =
    // TODO It is questionable whether we correctly distinguish between and handle module and type references - needs verification
    match input with
    | ParsedInput.SigFile(ParsedSigFileInput(_, _, _, _, synModuleOrNamespaceSigs, _)) ->
        synModuleOrNamespaceSigs
        |> Seq.collect visitSynModuleOrNamespaceSig
        |> Seq.toArray
    | ParsedInput.ImplFile(ParsedImplFileInput(_, _, _, _, _, synModuleOrNamespaces, _, _)) ->
        synModuleOrNamespaces
        |> Seq.collect visitSynModuleOrNamespace
        |> Seq.toArray

// TODO Improve detection mechanism
let mightHaveAutoOpen (synAttributeLists : SynAttributeList list) : bool =
    let attributes =
        synAttributeLists
        |> List.collect (fun attributes -> attributes.Attributes)
    match attributes with
    // No attributes found - no [<AutoOpen>] possible
    | [] -> false
    // Some attributes found - we can't know for sure if one of them is the AutoOpenAttribute (possibly hidden with a type alias), so we say 'yes'.
    | _ -> true

type Eit =
    | Nested of LongIdent[]
    | SomeTypeLikeStuff
    
let combine (parent : LongIdent) (children : Eit) =
    match children with
    | Eit.Nested idents ->
        idents
        |> Array.map (fun child -> List.append parent child)
    | Eit.SomeTypeLikeStuff ->
        [|parent|]

let rec topStuffForSynModuleOrNamespace (x : SynModuleOrNamespace) : LongIdent[] =
    match x with
    | SynModuleOrNamespace(longId, _, _, synModuleDecls, _, synAttributeLists, _, _, _) ->
        if mightHaveAutoOpen synAttributeLists then
            // Contents of a module that's potentially AutoOpen are available from its parent without a prefix.
            // Treat it as a type - as soon as the parent module is reachable, consider the file being used
            [|LongIdent.Empty|]
        else
            synModuleDecls
            |> moduleDecls
            |> combine longId

and moduleDecls (x : SynModuleDecl list) : Eit =
    let emptyState = Eit.Nested [||]
    x
    |> List.toArray
    |> Array.map moduleDecl
    |> Array.fold (fun state item ->
        match state, item with
        | Eit.SomeTypeLikeStuff, _
        | _, Eit.SomeTypeLikeStuff -> Eit.SomeTypeLikeStuff
        | Eit.Nested old, Eit.Nested current -> Eit.Nested (Array.append old current)
    ) emptyState

and moduleDecl (x : SynModuleDecl) : Eit =
    match x with
    | SynModuleDecl.Attributes _ 
    | SynModuleDecl.Exception _
    | SynModuleDecl.Expr _
    | SynModuleDecl.Let _
    | SynModuleDecl.Types _
    | SynModuleDecl.ModuleAbbrev _ ->
        Eit.SomeTypeLikeStuff
    | SynModuleDecl.HashDirective _
    | SynModuleDecl.Open _ ->
        Eit.Nested [||] // Elements can be ignored
    | SynModuleDecl.NamespaceFragment synModuleOrNamespace ->
        topStuffForSynModuleOrNamespace synModuleOrNamespace
        |> Eit.Nested
    | SynModuleDecl.NestedModule(synComponentInfo, _, synModuleDecls, _, _, _) ->
        match synComponentInfo with
        | SynComponentInfo(synAttributeLists, _, _, longId, _, _, _, _) ->
            let idents = 
                if mightHaveAutoOpen synAttributeLists then
                    // Contents of a module that's potentially AutoOpen are available everywhere, so treat it as if it had no name ('root' module).
                    [|LongIdent.Empty|]
                else
                    synModuleDecls
                    |> moduleDecls
                    |> combine longId
            Eit.Nested idents
            
let rec topStuffForSynModuleOrNamespaceSig (x : SynModuleOrNamespaceSig) : LongIdent[] =
    match x with
    | SynModuleOrNamespaceSig(longId, _, _, synModuleDecls, _, synAttributeLists, _, _, _) ->
        if mightHaveAutoOpen synAttributeLists then
            // Contents of a module that's potentially AutoOpen are available everywhere, so treat it as if it had no name ('root' module).
            [|LongIdent.Empty|]
        else
            synModuleDecls
            |> moduleSigDecls
            |> combine longId

and moduleSigDecls (x : SynModuleSigDecl list) : Eit =
    let emptyState = Eit.Nested [||]
    x
    |> List.toArray
    |> Array.map moduleSigDecl
    |> Array.fold (fun state item ->
        match state, item with
        | Eit.SomeTypeLikeStuff, _
        | _, Eit.SomeTypeLikeStuff -> Eit.SomeTypeLikeStuff
        | Eit.Nested old, Eit.Nested current -> Eit.Nested (Array.append old current)
    ) emptyState
    
and moduleSigDecl (x : SynModuleSigDecl) : Eit =
    match x with
    | SynModuleSigDecl.Val _
    | SynModuleSigDecl.Exception _
    | SynModuleSigDecl.Types _
    | SynModuleSigDecl.ModuleAbbrev _ ->
        Eit.SomeTypeLikeStuff
    | SynModuleSigDecl.HashDirective _
    | SynModuleSigDecl.Open _ ->
        Eit.Nested [||] // Elements can be ignored
    | SynModuleSigDecl.NamespaceFragment synModuleOrNamespace ->
        topStuffForSynModuleOrNamespaceSig synModuleOrNamespace
        |> Eit.Nested
    | SynModuleSigDecl.NestedModule(synComponentInfo, _, synModuleSigDecls, _, _) ->
        match synComponentInfo with
        | SynComponentInfo(synAttributeLists, _, _, longId, _, _, _, _) ->
            let idents = 
                if mightHaveAutoOpen synAttributeLists then
                    // Contents of a module that's potentially AutoOpen are available everywhere, so treat it as if it had no name ('root' module).
                    [|LongIdent.Empty|]
                else
                    synModuleSigDecls
                    |> moduleSigDecls
                    |> combine longId
            Eit.Nested idents
            
/// Extract partial module references from partial module or type references
let extractModuleSegments (stuff : ReferenceOrAbbreviation seq) : LongIdent[] * bool =
    
    let refs =
        stuff
        |> Seq.choose (function | ReferenceOrAbbreviation.Reference r -> Some r | ReferenceOrAbbreviation.Abbreviation _ -> None)
        |> Seq.toArray
    let abbreviations =
        stuff
        |> Seq.choose (function | ReferenceOrAbbreviation.Reference _ -> None | ReferenceOrAbbreviation.Abbreviation a -> Some a)
        |> Seq.toArray
    
    let moduleRefs =
        refs
        |> Seq.choose (fun x ->
            match x.Kind with
            | ModuleOrNamespace -> x.Ident |> Some
            | Type ->
                // Remove the last segment as it contains the type name
                match x.Ident.Length with
                | 0
                | 1 -> None
                | n -> x.Ident.GetSlice(Some 0, n - 2 |> Some) |> Some
        )
        |> Seq.toArray
    let containsModuleAbbreviations = abbreviations.Length > 0
    
    moduleRefs, containsModuleAbbreviations

let findModuleRefs (ast : ParsedInput) =
    let typeAndModuleRefs = findModuleAndTypeRefs ast
    extractModuleSegments typeAndModuleRefs

// TODO Handle 'global' namespace correctly
/// Extract the top-level module/namespaces from the AST
let topModuleOrNamespaces (input : ParsedInput) =
    match input with
    | ParsedInput.ImplFile f ->
        match f.Contents with
        | [] -> failwith $"No modules or namespaces found in file '{f.FileName}'"
        | items ->
            items
            |> List.toArray
            |> Array.collect topStuffForSynModuleOrNamespace
    | ParsedInput.SigFile f ->
        match f.Contents with
        | [] -> failwith $"No modules or namespaces found in file '{f.FileName}'"
        | items ->
            items
            |> List.toArray
            |> Array.collect topStuffForSynModuleOrNamespaceSig
