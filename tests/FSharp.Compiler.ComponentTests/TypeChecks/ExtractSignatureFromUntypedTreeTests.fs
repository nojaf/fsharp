module TypeChecks.ExtractSignatureFromUntypedTreeTests

open Xunit
open FSharp.Test.Compiler
open FSharp.Test

let private addMarker source = sprintf "#if TOPLEVELTYPED\n#endif\n%s" source

let private typeCheckWithInMemorySignature source =

    source
    |> addMarker
    |> FSharp
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
            (FSharp (addMarker head), List.indexed tail)
            ||> List.fold (fun acc (idx, source) ->
                acc
                |> withAdditionalSourceFile (
                    SourceCodeFileKind.Fs({ FileName = $"%i{idx}.fs"; SourceText = Some (addMarker source) })
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

[<Fact>]
let ``Marker on top of file`` () =
    typeCheckWithInMemorySignature """
namespace Foo
"""

[<Fact>]
let ``Inline member`` () =
    typeCheckWithInMemorySignature """
namespace Foo

open System.Threading.Tasks

type ConstantVal<'a>(value: Task<'a>) =
    inherit obj()

    new(x:int, value: Task<'a>) = ConstantVal<'a>(value)

[<Sealed>]
type T =
    member inline x.BindReturn(v: int, [<InlineIfLambda>] mapping: int -> int) : int =
        ignore (ConstantVal<string>(Task.FromResult ""))
        1
"""

[<Fact>]
let ``Leading keyword for constructor`` () =
    typeCheckWithInMemorySignature """
namespace Foo

type Bar() = class end
"""

[<Fact>]
let ``Type extension`` () =
    typeCheckWithInMemorySignature """
module Telplin

open System
open System.Threading.Tasks
open System.Threading

type CancellationTokenSource with
    /// Communicates a request for cancellation. Ignores ObjectDisposedException
    member cts.TryCancel() : unit =
        try
            cts.Cancel()
        with :? ObjectDisposedException ->
            ()
"""

[<Fact>]
let ``Inline type extension`` () =
        typeCheckWithInMemorySignature """
module Foo

type System.Int32 with
    member inline x.Source (a:int) : int = x - a
"""

[<Fact>]
let ``Override member`` () =
    typeCheckWithInMemorySignature """
namespace Foo

[<AbstractClass>]
type NodeBase() =
    abstract member Children: int array

type StringNode(content: string) =
    inherit NodeBase()
    member val Content = content
    override val Children = Array.empty
"""

[<Fact>]
let ``Lambda parameter in constructor`` () =
    typeCheckWithInMemorySignature """
namespace Foo

open System
open System.Threading.Tasks
open System.Threading

module F =
    type Bar(a:int, b: Task<int> -> Task<int>) =
        do ()
"""

[<Fact>]
let ``Invoke secondary constructor`` () =
    typeCheckWithInMemorySignatureWithMultiple [
        """
module F

type A(a:int) =
    new (b:string) = A(0)
"""
        """
module C

open F

let c () = A("")
""" ]

[<Fact>]
let ``Getter and setter`` () =
    typeCheckWithInMemorySignature """
namespace Foo

type Bar() =
    let mutable disableInMemoryProjectReferences : bool = false
    member __.DisableInMemoryProjectReferences
       with get () : bool = disableInMemoryProjectReferences
       and set (value : bool):unit = disableInMemoryProjectReferences <- value
"""

[<Fact>]
let ``Extension member`` () =
    typeCheckWithInMemorySignature """
module Telplin

open System
open System.Runtime.CompilerServices

[<Extension>]
type ReadOnlySpanExtensions =
  /// Note: empty string -> 1 line
  [<Extension>]
  static member CountLines(text: ReadOnlySpan<char>) : int =
    let mutable count = 0

    for _ in text.EnumerateLines() do
      count <- count + 1

    count
"""

[<Fact>]
let ``Literal in nested module`` () =
    typeCheckWithInMemorySignature """
namespace Foo

module Tracing =
  module SemanticConventions =
    module FCS =
      [<Literal>]
      let fileName : string = "fileName"
"""

[<Fact>]
let ``AbstractClass attribute should remain`` () =
    typeCheckWithInMemorySignature """
namespace Foo

[<AbstractClass>]
  type AbstractVal<'a>() =
    abstract Compute : unit -> unit
"""

[<Fact>]
let ``Val with get,set`` () =
    typeCheckWithInMemorySignature """
namespace Foo

open System

type Debounce<'a>(timeout: int, fn: 'a -> Async<unit>) as x =

  let mailbox =
    MailboxProcessor<'a>.Start(fun agent ->
      let rec loop ida idb arg =
        async {
          let! r = agent.TryReceive(x.Timeout)

          match r with
          | Some arg -> return! loop ida (idb + 1) (Some arg)
          | None when ida <> idb ->
            do! fn arg.Value
            return! loop 0 0 None
          | None -> return! loop 0 0 arg
        }

      loop 0 0 None)

  /// Calls the function, after debouncing has been applied.
  member _.Bounce<'a>(arg: 'a) : unit = mailbox.Post(arg)

  /// Timeout in ms
  member val Timeout : int = timeout with get, set
"""

[<Fact>]
let ``Optional parameter in tuple member`` () =
    typeCheckWithInMemorySignature """
namespace Foo

[<Class>]
type Foo =
    /// <summary>Good stuff</summary>
    /// <param name="v">Description about v</param>
    /// <param name="x"Optional x</param>
    /// <returns>A char</returns>
    static member Blah(v:int, ?x: string) : char = 'c'
"""