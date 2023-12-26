module TypeChecks.ExtractSignatureFromUntypedTreeTests

open Xunit
open FSharp.Test.Compiler
open FSharp.Test

let private typeCheckWithInMemorySignature source =
    FSharp source
    |> withOptions [ "--test:GraphBasedChecking" ]
    |> compile
    |> shouldSucceed
    |> ignore

let private typeCheckWithInMemorySignatureWithMultiple (sources: string list) =
    let library =
        match sources with
        | []
        | [ _ ] -> failwith "Expected multiple sources"
        | head :: tail ->
            (FSharp head, List.indexed tail)
            ||> List.fold (fun acc (idx, source) ->
                acc
                |> withAdditionalSourceFile (
                    SourceCodeFileKind.Fs({ FileName = $"%i{idx}.fs"; SourceText = Some source })
                )
            )

    library
    |> withOptions [ "--test:GraphBasedChecking" ]
    |> compile
    |> shouldSucceed
    |> ignore

[<Fact>]
let ``Typed binding`` () =
    typeCheckWithInMemorySignature """
module M

let f (g:int) : int = g - 1
"""

[<Fact>]
let ``Literal attribute`` () =
    typeCheckWithInMemorySignature """
module M

[<Literal>]
let InternalsVisibleTo: string = "Fantomas.Tests"
"""

// The point of this test is to ensure the arity in SynValInfo is correctly mapped for the function return type.

[<Fact>]
let ``Function return type`` () =
    typeCheckWithInMemorySignature """
module M
    
let moreThanOne<'a> : 'a list -> bool =
    function
    | []
    | [ _ ] -> false
    | _ -> true
"""

[<Fact>]
let ``Function return type with tuple`` () =
    typeCheckWithInMemorySignature """
module M
    
open System

let moreThanOne : char list * string -> float * TimeSpan -> int = fun _ _ -> 7
"""

[<Fact>]
let ``Auto property`` () =
    typeCheckWithInMemorySignatureWithMultiple [
        """
module M
    
type SingleTextNode(idText: string) =
    member val Text: string = idText
"""
        """
module C

open M

let a (n: SingleTextNode): string = n.Text
""" ]

[<Fact>]
let ``Override auto property`` () =
    typeCheckWithInMemorySignature """
module M

[<AbstractClass>]
type NodeBase() =
    abstract Content: string

type StringNode(content: string) =
    inherit NodeBase()
    override val Content: string = content
"""

[<Fact>]
let ``Generic value with function return type`` () =
    typeCheckWithInMemorySignature """
module M

let sepNone<'a> : 'a -> 'a = id
"""

[<Fact>]
let ``Function return type with lambda body`` () =
    typeCheckWithInMemorySignature """
module M

let c : int -> int -> char = fun _ _ -> 'c'
"""
