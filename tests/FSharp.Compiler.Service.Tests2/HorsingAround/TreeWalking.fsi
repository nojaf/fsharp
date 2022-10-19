module FSharp.Compiler.Service.Tests.HorsingAround.TreeWalking

open FSharp.Compiler.Service.Tests.HorsingAround.Types
open FSharp.Compiler.Syntax

val findPathInFile: ast: ParsedInput -> path: PathQuery -> bool
