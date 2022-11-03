﻿module FSharp.Compiler.Service.Tests.Diamond

open System
open NUnit.Framework
open FSharp.Compiler.CodeAnalysis

let fantomasCoreArgs inParallel =
    [|
        @"C:\Users\nojaf\Projects\main-fantomas\src\Fantomas.Core\fsc.exe"
        @"-o:C:\Users\nojaf\Projects\main-fantomas\src\Fantomas.Core\obj\Debug\netstandard2.0\Fantomas.Core.dll"
        @"-g"
        @"--debug:embedded"
        @"--embed:C:\Users\nojaf\Projects\main-fantomas\src\Fantomas.Core\obj\Debug\netstandard2.0\Fantomas.Core.AssemblyInfo.fs"
        // @"--sourcelink:C:\Users\nojaf\Projects\main-fantomas\src\Fantomas.Core\obj\Debug\netstandard2.0\Fantomas.Core.sourcelink.json"
        @"--noframework"
        @"--define:TRACE"
        @"--define:DEBUG"
        @"--define:NETSTANDARD"
        @"--define:NETSTANDARD2_0"
        @"--define:NETSTANDARD1_0_OR_GREATER"
        @"--define:NETSTANDARD1_1_OR_GREATER"
        @"--define:NETSTANDARD1_2_OR_GREATER"
        @"--define:NETSTANDARD1_3_OR_GREATER"
        @"--define:NETSTANDARD1_4_OR_GREATER"
        @"--define:NETSTANDARD1_5_OR_GREATER"
        @"--define:NETSTANDARD1_6_OR_GREATER"
        @"--define:NETSTANDARD2_0_OR_GREATER"
        @"--doc:C:\Users\nojaf\Projects\main-fantomas\src\Fantomas.Core\obj\Debug\netstandard2.0\Fantomas.Core.xml"
        @"--optimize-"
        @"-r:C:\Users\nojaf\Projects\main-fantomas\src\Fantomas.FCS\bin\Debug\netstandard2.0\Fantomas.FCS.dll"
        @"-r:C:\Users\nojaf\.nuget\packages\fsharp.core\6.0.1\lib\netstandard2.0\FSharp.Core.dll"
        @"-r:C:\Users\nojaf\.nuget\packages\netstandard.library\2.0.3\build\netstandard2.0\ref\Microsoft.Win32.Primitives.dll"
        @"-r:C:\Users\nojaf\.nuget\packages\netstandard.library\2.0.3\build\netstandard2.0\ref\mscorlib.dll"
        @"-r:C:\Users\nojaf\.nuget\packages\netstandard.library\2.0.3\build\netstandard2.0\ref\netstandard.dll"
        @"-r:C:\Users\nojaf\.nuget\packages\netstandard.library\2.0.3\build\netstandard2.0\ref\System.AppContext.dll"
        @"-r:C:\Users\nojaf\.nuget\packages\system.buffers\4.5.1\ref\netstandard2.0\System.Buffers.dll"
        @"-r:C:\Users\nojaf\.nuget\packages\netstandard.library\2.0.3\build\netstandard2.0\ref\System.Collections.Concurrent.dll"
        @"-r:C:\Users\nojaf\.nuget\packages\netstandard.library\2.0.3\build\netstandard2.0\ref\System.Collections.dll"
        @"-r:C:\Users\nojaf\.nuget\packages\netstandard.library\2.0.3\build\netstandard2.0\ref\System.Collections.NonGeneric.dll"
        @"-r:C:\Users\nojaf\.nuget\packages\netstandard.library\2.0.3\build\netstandard2.0\ref\System.Collections.Specialized.dll"
        @"-r:C:\Users\nojaf\.nuget\packages\netstandard.library\2.0.3\build\netstandard2.0\ref\System.ComponentModel.Composition.dll"
        @"-r:C:\Users\nojaf\.nuget\packages\netstandard.library\2.0.3\build\netstandard2.0\ref\System.ComponentModel.dll"
        @"-r:C:\Users\nojaf\.nuget\packages\netstandard.library\2.0.3\build\netstandard2.0\ref\System.ComponentModel.EventBasedAsync.dll"
        @"-r:C:\Users\nojaf\.nuget\packages\netstandard.library\2.0.3\build\netstandard2.0\ref\System.ComponentModel.Primitives.dll"
        @"-r:C:\Users\nojaf\.nuget\packages\netstandard.library\2.0.3\build\netstandard2.0\ref\System.ComponentModel.TypeConverter.dll"
        @"-r:C:\Users\nojaf\.nuget\packages\netstandard.library\2.0.3\build\netstandard2.0\ref\System.Console.dll"
        @"-r:C:\Users\nojaf\.nuget\packages\netstandard.library\2.0.3\build\netstandard2.0\ref\System.Core.dll"
        @"-r:C:\Users\nojaf\.nuget\packages\netstandard.library\2.0.3\build\netstandard2.0\ref\System.Data.Common.dll"
        @"-r:C:\Users\nojaf\.nuget\packages\netstandard.library\2.0.3\build\netstandard2.0\ref\System.Data.dll"
        @"-r:C:\Users\nojaf\.nuget\packages\netstandard.library\2.0.3\build\netstandard2.0\ref\System.Diagnostics.Contracts.dll"
        @"-r:C:\Users\nojaf\.nuget\packages\netstandard.library\2.0.3\build\netstandard2.0\ref\System.Diagnostics.Debug.dll"
        @"-r:C:\Users\nojaf\.nuget\packages\netstandard.library\2.0.3\build\netstandard2.0\ref\System.Diagnostics.FileVersionInfo.dll"
        @"-r:C:\Users\nojaf\.nuget\packages\netstandard.library\2.0.3\build\netstandard2.0\ref\System.Diagnostics.Process.dll"
        @"-r:C:\Users\nojaf\.nuget\packages\netstandard.library\2.0.3\build\netstandard2.0\ref\System.Diagnostics.StackTrace.dll"
        @"-r:C:\Users\nojaf\.nuget\packages\netstandard.library\2.0.3\build\netstandard2.0\ref\System.Diagnostics.TextWriterTraceListener.dll"
        @"-r:C:\Users\nojaf\.nuget\packages\netstandard.library\2.0.3\build\netstandard2.0\ref\System.Diagnostics.Tools.dll"
        @"-r:C:\Users\nojaf\.nuget\packages\netstandard.library\2.0.3\build\netstandard2.0\ref\System.Diagnostics.TraceSource.dll"
        @"-r:C:\Users\nojaf\.nuget\packages\netstandard.library\2.0.3\build\netstandard2.0\ref\System.Diagnostics.Tracing.dll"
        @"-r:C:\Users\nojaf\.nuget\packages\netstandard.library\2.0.3\build\netstandard2.0\ref\System.dll"
        @"-r:C:\Users\nojaf\.nuget\packages\netstandard.library\2.0.3\build\netstandard2.0\ref\System.Drawing.dll"
        @"-r:C:\Users\nojaf\.nuget\packages\netstandard.library\2.0.3\build\netstandard2.0\ref\System.Drawing.Primitives.dll"
        @"-r:C:\Users\nojaf\.nuget\packages\netstandard.library\2.0.3\build\netstandard2.0\ref\System.Dynamic.Runtime.dll"
        @"-r:C:\Users\nojaf\.nuget\packages\netstandard.library\2.0.3\build\netstandard2.0\ref\System.Globalization.Calendars.dll"
        @"-r:C:\Users\nojaf\.nuget\packages\netstandard.library\2.0.3\build\netstandard2.0\ref\System.Globalization.dll"
        @"-r:C:\Users\nojaf\.nuget\packages\netstandard.library\2.0.3\build\netstandard2.0\ref\System.Globalization.Extensions.dll"
        @"-r:C:\Users\nojaf\.nuget\packages\netstandard.library\2.0.3\build\netstandard2.0\ref\System.IO.Compression.dll"
        @"-r:C:\Users\nojaf\.nuget\packages\netstandard.library\2.0.3\build\netstandard2.0\ref\System.IO.Compression.FileSystem.dll"
        @"-r:C:\Users\nojaf\.nuget\packages\netstandard.library\2.0.3\build\netstandard2.0\ref\System.IO.Compression.ZipFile.dll"
        @"-r:C:\Users\nojaf\.nuget\packages\netstandard.library\2.0.3\build\netstandard2.0\ref\System.IO.dll"
        @"-r:C:\Users\nojaf\.nuget\packages\netstandard.library\2.0.3\build\netstandard2.0\ref\System.IO.FileSystem.dll"
        @"-r:C:\Users\nojaf\.nuget\packages\netstandard.library\2.0.3\build\netstandard2.0\ref\System.IO.FileSystem.DriveInfo.dll"
        @"-r:C:\Users\nojaf\.nuget\packages\netstandard.library\2.0.3\build\netstandard2.0\ref\System.IO.FileSystem.Primitives.dll"
        @"-r:C:\Users\nojaf\.nuget\packages\netstandard.library\2.0.3\build\netstandard2.0\ref\System.IO.FileSystem.Watcher.dll"
        @"-r:C:\Users\nojaf\.nuget\packages\netstandard.library\2.0.3\build\netstandard2.0\ref\System.IO.IsolatedStorage.dll"
        @"-r:C:\Users\nojaf\.nuget\packages\netstandard.library\2.0.3\build\netstandard2.0\ref\System.IO.MemoryMappedFiles.dll"
        @"-r:C:\Users\nojaf\.nuget\packages\netstandard.library\2.0.3\build\netstandard2.0\ref\System.IO.Pipes.dll"
        @"-r:C:\Users\nojaf\.nuget\packages\netstandard.library\2.0.3\build\netstandard2.0\ref\System.IO.UnmanagedMemoryStream.dll"
        @"-r:C:\Users\nojaf\.nuget\packages\netstandard.library\2.0.3\build\netstandard2.0\ref\System.Linq.dll"
        @"-r:C:\Users\nojaf\.nuget\packages\netstandard.library\2.0.3\build\netstandard2.0\ref\System.Linq.Expressions.dll"
        @"-r:C:\Users\nojaf\.nuget\packages\netstandard.library\2.0.3\build\netstandard2.0\ref\System.Linq.Parallel.dll"
        @"-r:C:\Users\nojaf\.nuget\packages\netstandard.library\2.0.3\build\netstandard2.0\ref\System.Linq.Queryable.dll"
        @"-r:C:\Users\nojaf\.nuget\packages\system.memory\4.5.4\lib\netstandard2.0\System.Memory.dll"
        @"-r:C:\Users\nojaf\.nuget\packages\netstandard.library\2.0.3\build\netstandard2.0\ref\System.Net.dll"
        @"-r:C:\Users\nojaf\.nuget\packages\netstandard.library\2.0.3\build\netstandard2.0\ref\System.Net.Http.dll"
        @"-r:C:\Users\nojaf\.nuget\packages\netstandard.library\2.0.3\build\netstandard2.0\ref\System.Net.NameResolution.dll"
        @"-r:C:\Users\nojaf\.nuget\packages\netstandard.library\2.0.3\build\netstandard2.0\ref\System.Net.NetworkInformation.dll"
        @"-r:C:\Users\nojaf\.nuget\packages\netstandard.library\2.0.3\build\netstandard2.0\ref\System.Net.Ping.dll"
        @"-r:C:\Users\nojaf\.nuget\packages\netstandard.library\2.0.3\build\netstandard2.0\ref\System.Net.Primitives.dll"
        @"-r:C:\Users\nojaf\.nuget\packages\netstandard.library\2.0.3\build\netstandard2.0\ref\System.Net.Requests.dll"
        @"-r:C:\Users\nojaf\.nuget\packages\netstandard.library\2.0.3\build\netstandard2.0\ref\System.Net.Security.dll"
        @"-r:C:\Users\nojaf\.nuget\packages\netstandard.library\2.0.3\build\netstandard2.0\ref\System.Net.Sockets.dll"
        @"-r:C:\Users\nojaf\.nuget\packages\netstandard.library\2.0.3\build\netstandard2.0\ref\System.Net.WebHeaderCollection.dll"
        @"-r:C:\Users\nojaf\.nuget\packages\netstandard.library\2.0.3\build\netstandard2.0\ref\System.Net.WebSockets.Client.dll"
        @"-r:C:\Users\nojaf\.nuget\packages\netstandard.library\2.0.3\build\netstandard2.0\ref\System.Net.WebSockets.dll"
        @"-r:C:\Users\nojaf\.nuget\packages\netstandard.library\2.0.3\build\netstandard2.0\ref\System.Numerics.dll"
        @"-r:C:\Users\nojaf\.nuget\packages\system.numerics.vectors\4.4.0\ref\netstandard2.0\System.Numerics.Vectors.dll"
        @"-r:C:\Users\nojaf\.nuget\packages\netstandard.library\2.0.3\build\netstandard2.0\ref\System.ObjectModel.dll"
        @"-r:C:\Users\nojaf\.nuget\packages\netstandard.library\2.0.3\build\netstandard2.0\ref\System.Reflection.dll"
        @"-r:C:\Users\nojaf\.nuget\packages\netstandard.library\2.0.3\build\netstandard2.0\ref\System.Reflection.Extensions.dll"
        @"-r:C:\Users\nojaf\.nuget\packages\netstandard.library\2.0.3\build\netstandard2.0\ref\System.Reflection.Primitives.dll"
        @"-r:C:\Users\nojaf\.nuget\packages\netstandard.library\2.0.3\build\netstandard2.0\ref\System.Resources.Reader.dll"
        @"-r:C:\Users\nojaf\.nuget\packages\netstandard.library\2.0.3\build\netstandard2.0\ref\System.Resources.ResourceManager.dll"
        @"-r:C:\Users\nojaf\.nuget\packages\netstandard.library\2.0.3\build\netstandard2.0\ref\System.Resources.Writer.dll"
        @"-r:C:\Users\nojaf\.nuget\packages\system.runtime.compilerservices.unsafe\4.5.3\ref\netstandard2.0\System.Runtime.CompilerServices.Unsafe.dll"
        @"-r:C:\Users\nojaf\.nuget\packages\netstandard.library\2.0.3\build\netstandard2.0\ref\System.Runtime.CompilerServices.VisualC.dll"
        @"-r:C:\Users\nojaf\.nuget\packages\netstandard.library\2.0.3\build\netstandard2.0\ref\System.Runtime.dll"
        @"-r:C:\Users\nojaf\.nuget\packages\netstandard.library\2.0.3\build\netstandard2.0\ref\System.Runtime.Extensions.dll"
        @"-r:C:\Users\nojaf\.nuget\packages\netstandard.library\2.0.3\build\netstandard2.0\ref\System.Runtime.Handles.dll"
        @"-r:C:\Users\nojaf\.nuget\packages\netstandard.library\2.0.3\build\netstandard2.0\ref\System.Runtime.InteropServices.dll"
        @"-r:C:\Users\nojaf\.nuget\packages\netstandard.library\2.0.3\build\netstandard2.0\ref\System.Runtime.InteropServices.RuntimeInformation.dll"
        @"-r:C:\Users\nojaf\.nuget\packages\netstandard.library\2.0.3\build\netstandard2.0\ref\System.Runtime.Numerics.dll"
        @"-r:C:\Users\nojaf\.nuget\packages\netstandard.library\2.0.3\build\netstandard2.0\ref\System.Runtime.Serialization.dll"
        @"-r:C:\Users\nojaf\.nuget\packages\netstandard.library\2.0.3\build\netstandard2.0\ref\System.Runtime.Serialization.Formatters.dll"
        @"-r:C:\Users\nojaf\.nuget\packages\netstandard.library\2.0.3\build\netstandard2.0\ref\System.Runtime.Serialization.Json.dll"
        @"-r:C:\Users\nojaf\.nuget\packages\netstandard.library\2.0.3\build\netstandard2.0\ref\System.Runtime.Serialization.Primitives.dll"
        @"-r:C:\Users\nojaf\.nuget\packages\netstandard.library\2.0.3\build\netstandard2.0\ref\System.Runtime.Serialization.Xml.dll"
        @"-r:C:\Users\nojaf\.nuget\packages\netstandard.library\2.0.3\build\netstandard2.0\ref\System.Security.Claims.dll"
        @"-r:C:\Users\nojaf\.nuget\packages\netstandard.library\2.0.3\build\netstandard2.0\ref\System.Security.Cryptography.Algorithms.dll"
        @"-r:C:\Users\nojaf\.nuget\packages\netstandard.library\2.0.3\build\netstandard2.0\ref\System.Security.Cryptography.Csp.dll"
        @"-r:C:\Users\nojaf\.nuget\packages\netstandard.library\2.0.3\build\netstandard2.0\ref\System.Security.Cryptography.Encoding.dll"
        @"-r:C:\Users\nojaf\.nuget\packages\netstandard.library\2.0.3\build\netstandard2.0\ref\System.Security.Cryptography.Primitives.dll"
        @"-r:C:\Users\nojaf\.nuget\packages\netstandard.library\2.0.3\build\netstandard2.0\ref\System.Security.Cryptography.X509Certificates.dll"
        @"-r:C:\Users\nojaf\.nuget\packages\netstandard.library\2.0.3\build\netstandard2.0\ref\System.Security.Principal.dll"
        @"-r:C:\Users\nojaf\.nuget\packages\netstandard.library\2.0.3\build\netstandard2.0\ref\System.Security.SecureString.dll"
        @"-r:C:\Users\nojaf\.nuget\packages\netstandard.library\2.0.3\build\netstandard2.0\ref\System.ServiceModel.Web.dll"
        @"-r:C:\Users\nojaf\.nuget\packages\netstandard.library\2.0.3\build\netstandard2.0\ref\System.Text.Encoding.dll"
        @"-r:C:\Users\nojaf\.nuget\packages\netstandard.library\2.0.3\build\netstandard2.0\ref\System.Text.Encoding.Extensions.dll"
        @"-r:C:\Users\nojaf\.nuget\packages\netstandard.library\2.0.3\build\netstandard2.0\ref\System.Text.RegularExpressions.dll"
        @"-r:C:\Users\nojaf\.nuget\packages\netstandard.library\2.0.3\build\netstandard2.0\ref\System.Threading.dll"
        @"-r:C:\Users\nojaf\.nuget\packages\netstandard.library\2.0.3\build\netstandard2.0\ref\System.Threading.Overlapped.dll"
        @"-r:C:\Users\nojaf\.nuget\packages\netstandard.library\2.0.3\build\netstandard2.0\ref\System.Threading.Tasks.dll"
        @"-r:C:\Users\nojaf\.nuget\packages\netstandard.library\2.0.3\build\netstandard2.0\ref\System.Threading.Tasks.Parallel.dll"
        @"-r:C:\Users\nojaf\.nuget\packages\netstandard.library\2.0.3\build\netstandard2.0\ref\System.Threading.Thread.dll"
        @"-r:C:\Users\nojaf\.nuget\packages\netstandard.library\2.0.3\build\netstandard2.0\ref\System.Threading.ThreadPool.dll"
        @"-r:C:\Users\nojaf\.nuget\packages\netstandard.library\2.0.3\build\netstandard2.0\ref\System.Threading.Timer.dll"
        @"-r:C:\Users\nojaf\.nuget\packages\netstandard.library\2.0.3\build\netstandard2.0\ref\System.Transactions.dll"
        @"-r:C:\Users\nojaf\.nuget\packages\netstandard.library\2.0.3\build\netstandard2.0\ref\System.ValueTuple.dll"
        @"-r:C:\Users\nojaf\.nuget\packages\netstandard.library\2.0.3\build\netstandard2.0\ref\System.Web.dll"
        @"-r:C:\Users\nojaf\.nuget\packages\netstandard.library\2.0.3\build\netstandard2.0\ref\System.Windows.dll"
        @"-r:C:\Users\nojaf\.nuget\packages\netstandard.library\2.0.3\build\netstandard2.0\ref\System.Xml.dll"
        @"-r:C:\Users\nojaf\.nuget\packages\netstandard.library\2.0.3\build\netstandard2.0\ref\System.Xml.Linq.dll"
        @"-r:C:\Users\nojaf\.nuget\packages\netstandard.library\2.0.3\build\netstandard2.0\ref\System.Xml.ReaderWriter.dll"
        @"-r:C:\Users\nojaf\.nuget\packages\netstandard.library\2.0.3\build\netstandard2.0\ref\System.Xml.Serialization.dll"
        @"-r:C:\Users\nojaf\.nuget\packages\netstandard.library\2.0.3\build\netstandard2.0\ref\System.Xml.XDocument.dll"
        @"-r:C:\Users\nojaf\.nuget\packages\netstandard.library\2.0.3\build\netstandard2.0\ref\System.Xml.XmlDocument.dll"
        @"-r:C:\Users\nojaf\.nuget\packages\netstandard.library\2.0.3\build\netstandard2.0\ref\System.Xml.XmlSerializer.dll"
        @"-r:C:\Users\nojaf\.nuget\packages\netstandard.library\2.0.3\build\netstandard2.0\ref\System.Xml.XPath.dll"
        @"-r:C:\Users\nojaf\.nuget\packages\netstandard.library\2.0.3\build\netstandard2.0\ref\System.Xml.XPath.XDocument.dll"
        @"--target:library"
        @"--nowarn:NU1603,IL2121"
        @"--warn:3"
        @"--warnon:3390"
        @"--warnaserror:3239,FS0025"
        @"--fullpaths"
        @"--flaterrors"
        @"--highentropyva+"
        @"--targetprofile:netstandard"
        @"--nocopyfsharpcore"
        @"--deterministic+"
        @"--simpleresolution"
        @"C:\Users\nojaf\Projects\main-fantomas\src\Fantomas.Core\obj\Debug\netstandard2.0\.NETStandard,Version=v2.0.AssemblyAttributes.fs"
        @"C:\Users\nojaf\Projects\main-fantomas\src\Fantomas.Core\obj\Debug\netstandard2.0\Fantomas.Core.AssemblyInfo.fs"
        @"C:\Users\nojaf\Projects\main-fantomas\src\Fantomas.Core\AssemblyInfo.fs"
        @"C:\Users\nojaf\Projects\main-fantomas\src\Fantomas.Core\ISourceTextExtensions.fs"
        @"C:\Users\nojaf\Projects\main-fantomas\src\Fantomas.Core\RangeHelpers.fs"
        @"C:\Users\nojaf\Projects\main-fantomas\src\Fantomas.Core\AstExtensions.fs"
        @"C:\Users\nojaf\Projects\main-fantomas\src\Fantomas.Core\TriviaTypes.fs"
        @"C:\Users\nojaf\Projects\main-fantomas\src\Fantomas.Core\Utils.fs"
        @"C:\Users\nojaf\Projects\main-fantomas\src\Fantomas.Core\SourceParser.fs"
        @"C:\Users\nojaf\Projects\main-fantomas\src\Fantomas.Core\AstTransformer.fs"
        @"C:\Users\nojaf\Projects\main-fantomas\src\Fantomas.Core\Version.fs"
        @"C:\Users\nojaf\Projects\main-fantomas\src\Fantomas.Core\Queue.fs"
        @"C:\Users\nojaf\Projects\main-fantomas\src\Fantomas.Core\FormatConfig.fs"
        @"C:\Users\nojaf\Projects\main-fantomas\src\Fantomas.Core\Defines.fs"
        @"C:\Users\nojaf\Projects\main-fantomas\src\Fantomas.Core\Trivia.fs"
        @"C:\Users\nojaf\Projects\main-fantomas\src\Fantomas.Core\SourceTransformer.fs"
        @"C:\Users\nojaf\Projects\main-fantomas\src\Fantomas.Core\Context.fs"
        @"C:\Users\nojaf\Projects\main-fantomas\src\Fantomas.Core\CodePrinter.fs"
        @"C:\Users\nojaf\Projects\main-fantomas\src\Fantomas.Core\CodeFormatterImpl.fs"
        @"C:\Users\nojaf\Projects\main-fantomas\src\Fantomas.Core\Validation.fs"
        @"C:\Users\nojaf\Projects\main-fantomas\src\Fantomas.Core\Selection.fs"
        // @"C:\Users\nojaf\Projects\main-fantomas\src\Fantomas.Core\CodeFormatter.fsi"
        @"C:\Users\nojaf\Projects\main-fantomas\src\Fantomas.Core\CodeFormatter.fs"
        if inParallel then
            "--test:ParallelCheckingWithSignatureFilesOn"
    |]

let parallelOrNot2 =
    [|
        false
        false
        false
        false
        false
        false
        true
        true
        true
        true
        true
        true
        true
        true
    |]

[<TestCaseSource(nameof (parallelOrNot2))>]
let ``Fantomas.Core`` isParallel =
    let checker = FSharpChecker.Create()

    let diagnostics, _exitCode =
        checker.Compile(fantomasCoreArgs isParallel) |> Async.RunSynchronously

    if diagnostics.Length > 0 then
        Assert.Fail($"Got errors, {diagnostics}")
    else
        Assert.Pass()

[<Test>]
let ``Debug Fantomas.Core`` () =
    let checker = FSharpChecker.Create()

    let diagnostics, _exitCode =
        checker.Compile(fantomasCoreArgs true) |> Async.RunSynchronously

    if diagnostics.Length > 0 then
        Assert.Fail($"Got errors, {diagnostics}")
    else
        Assert.Pass()

let fantomasCoreTestsArgs inParallel =
    [|
        "fsc.exe"
        @"-o:C:\Users\nojaf\Projects\main-fantomas\src\Fantomas.Core.Tests\obj\Debug\net6.0\Fantomas.Core.Tests.dll"
        @"-g"
        @"--debug:embedded"
        @"--noframework"
        @"--define:TRACE"
        @"--define:DEBUG"
        @"--define:NET"
        @"--define:NET6_0"
        @"--define:NETCOREAPP"
        @"--define:NET5_0_OR_GREATER"
        @"--define:NET6_0_OR_GREATER"
        @"--define:NETCOREAPP1_0_OR_GREATER"
        @"--define:NETCOREAPP1_1_OR_GREATER"
        @"--define:NETCOREAPP2_0_OR_GREATER"
        @"--define:NETCOREAPP2_1_OR_GREATER"
        @"--define:NETCOREAPP2_2_OR_GREATER"
        @"--define:NETCOREAPP3_0_OR_GREATER"
        @"--define:NETCOREAPP3_1_OR_GREATER"
        @"--doc:C:\Users\nojaf\Projects\main-fantomas\src\Fantomas.Core.Tests\obj\Debug\net6.0\Fantomas.Core.Tests.xml"
        @"--optimize-"
        @"--tailcalls-"
        @"-r:C:\Users\nojaf\Projects\main-fantomas\src\Fantomas.Core\bin\Debug\netstandard2.0\Fantomas.Core.dll"
        @"-r:C:\Users\nojaf\Projects\main-fantomas\src\Fantomas.FCS\bin\Debug\netstandard2.0\Fantomas.FCS.dll"
        @"-r:C:\Users\nojaf\.nuget\packages\fscheck\2.16.5\lib\netstandard2.0\FsCheck.dll"
        @"-r:C:\Users\nojaf\.nuget\packages\fsharp.core\6.0.1\lib\netstandard2.1\FSharp.Core.dll"
        @"-r:C:\Users\nojaf\.nuget\packages\fsunit\4.2.0\lib\netstandard2.0\FsUnit.NUnit.dll"
        @"-r:C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\6.0.10\ref\net6.0\Microsoft.CSharp.dll"
        @"-r:C:\Users\nojaf\.nuget\packages\microsoft.testplatform.testhost\17.3.2\lib\netcoreapp2.1\Microsoft.TestPlatform.CommunicationUtilities.dll"
        @"-r:C:\Users\nojaf\.nuget\packages\microsoft.testplatform.testhost\17.3.2\lib\netcoreapp2.1\Microsoft.TestPlatform.CoreUtilities.dll"
        @"-r:C:\Users\nojaf\.nuget\packages\microsoft.testplatform.testhost\17.3.2\lib\netcoreapp2.1\Microsoft.TestPlatform.CrossPlatEngine.dll"
        @"-r:C:\Users\nojaf\.nuget\packages\microsoft.testplatform.testhost\17.3.2\lib\netcoreapp2.1\Microsoft.TestPlatform.PlatformAbstractions.dll"
        @"-r:C:\Users\nojaf\.nuget\packages\microsoft.testplatform.testhost\17.3.2\lib\netcoreapp2.1\Microsoft.TestPlatform.Utilities.dll"
        @"-r:C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\6.0.10\ref\net6.0\Microsoft.VisualBasic.Core.dll"
        @"-r:C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\6.0.10\ref\net6.0\Microsoft.VisualBasic.dll"
        @"-r:C:\Users\nojaf\.nuget\packages\microsoft.codecoverage\17.3.2\lib\netcoreapp1.0\Microsoft.VisualStudio.CodeCoverage.Shim.dll"
        @"-r:C:\Users\nojaf\.nuget\packages\microsoft.testplatform.testhost\17.3.2\lib\netcoreapp2.1\Microsoft.VisualStudio.TestPlatform.Common.dll"
        @"-r:C:\Users\nojaf\.nuget\packages\microsoft.testplatform.testhost\17.3.2\lib\netcoreapp2.1\Microsoft.VisualStudio.TestPlatform.ObjectModel.dll"
        @"-r:C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\6.0.10\ref\net6.0\Microsoft.Win32.Primitives.dll"
        @"-r:C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\6.0.10\ref\net6.0\Microsoft.Win32.Registry.dll"
        @"-r:C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\6.0.10\ref\net6.0\mscorlib.dll"
        @"-r:C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\6.0.10\ref\net6.0\netstandard.dll"
        @"-r:C:\Users\nojaf\.nuget\packages\newtonsoft.json\9.0.1\lib\netstandard1.0\Newtonsoft.Json.dll"
        @"-r:C:\Users\nojaf\.nuget\packages\nuget.frameworks\5.11.0\lib\netstandard2.0\NuGet.Frameworks.dll"
        @"-r:C:\Users\nojaf\.nuget\packages\nunit\3.13.3\lib\netstandard2.0\nunit.framework.dll"
        @"-r:C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\6.0.10\ref\net6.0\System.AppContext.dll"
        @"-r:C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\6.0.10\ref\net6.0\System.Buffers.dll"
        @"-r:C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\6.0.10\ref\net6.0\System.Collections.Concurrent.dll"
        @"-r:C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\6.0.10\ref\net6.0\System.Collections.dll"
        @"-r:C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\6.0.10\ref\net6.0\System.Collections.Immutable.dll"
        @"-r:C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\6.0.10\ref\net6.0\System.Collections.NonGeneric.dll"
        @"-r:C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\6.0.10\ref\net6.0\System.Collections.Specialized.dll"
        @"-r:C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\6.0.10\ref\net6.0\System.ComponentModel.Annotations.dll"
        @"-r:C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\6.0.10\ref\net6.0\System.ComponentModel.DataAnnotations.dll"
        @"-r:C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\6.0.10\ref\net6.0\System.ComponentModel.dll"
        @"-r:C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\6.0.10\ref\net6.0\System.ComponentModel.EventBasedAsync.dll"
        @"-r:C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\6.0.10\ref\net6.0\System.ComponentModel.Primitives.dll"
        @"-r:C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\6.0.10\ref\net6.0\System.ComponentModel.TypeConverter.dll"
        @"-r:C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\6.0.10\ref\net6.0\System.Configuration.dll"
        @"-r:C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\6.0.10\ref\net6.0\System.Console.dll"
        @"-r:C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\6.0.10\ref\net6.0\System.Core.dll"
        @"-r:C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\6.0.10\ref\net6.0\System.Data.Common.dll"
        @"-r:C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\6.0.10\ref\net6.0\System.Data.DataSetExtensions.dll"
        @"-r:C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\6.0.10\ref\net6.0\System.Data.dll"
        @"-r:C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\6.0.10\ref\net6.0\System.Diagnostics.Contracts.dll"
        @"-r:C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\6.0.10\ref\net6.0\System.Diagnostics.Debug.dll"
        @"-r:C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\6.0.10\ref\net6.0\System.Diagnostics.DiagnosticSource.dll"
        @"-r:C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\6.0.10\ref\net6.0\System.Diagnostics.FileVersionInfo.dll"
        @"-r:C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\6.0.10\ref\net6.0\System.Diagnostics.Process.dll"
        @"-r:C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\6.0.10\ref\net6.0\System.Diagnostics.StackTrace.dll"
        @"-r:C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\6.0.10\ref\net6.0\System.Diagnostics.TextWriterTraceListener.dll"
        @"-r:C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\6.0.10\ref\net6.0\System.Diagnostics.Tools.dll"
        @"-r:C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\6.0.10\ref\net6.0\System.Diagnostics.TraceSource.dll"
        @"-r:C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\6.0.10\ref\net6.0\System.Diagnostics.Tracing.dll"
        @"-r:C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\6.0.10\ref\net6.0\System.dll"
        @"-r:C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\6.0.10\ref\net6.0\System.Drawing.dll"
        @"-r:C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\6.0.10\ref\net6.0\System.Drawing.Primitives.dll"
        @"-r:C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\6.0.10\ref\net6.0\System.Dynamic.Runtime.dll"
        @"-r:C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\6.0.10\ref\net6.0\System.Formats.Asn1.dll"
        @"-r:C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\6.0.10\ref\net6.0\System.Globalization.Calendars.dll"
        @"-r:C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\6.0.10\ref\net6.0\System.Globalization.dll"
        @"-r:C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\6.0.10\ref\net6.0\System.Globalization.Extensions.dll"
        @"-r:C:\Users\nojaf\.nuget\packages\system.io.abstractions\17.2.3\lib\net6.0\System.IO.Abstractions.dll"
        @"-r:C:\Users\nojaf\.nuget\packages\system.io.abstractions.testinghelpers\17.2.3\lib\net6.0\System.IO.Abstractions.TestingHelpers.dll"
        @"-r:C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\6.0.10\ref\net6.0\System.IO.Compression.Brotli.dll"
        @"-r:C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\6.0.10\ref\net6.0\System.IO.Compression.dll"
        @"-r:C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\6.0.10\ref\net6.0\System.IO.Compression.FileSystem.dll"
        @"-r:C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\6.0.10\ref\net6.0\System.IO.Compression.ZipFile.dll"
        @"-r:C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\6.0.10\ref\net6.0\System.IO.dll"
        @"-r:C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\6.0.10\ref\net6.0\System.IO.FileSystem.AccessControl.dll"
        @"-r:C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\6.0.10\ref\net6.0\System.IO.FileSystem.dll"
        @"-r:C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\6.0.10\ref\net6.0\System.IO.FileSystem.DriveInfo.dll"
        @"-r:C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\6.0.10\ref\net6.0\System.IO.FileSystem.Primitives.dll"
        @"-r:C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\6.0.10\ref\net6.0\System.IO.FileSystem.Watcher.dll"
        @"-r:C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\6.0.10\ref\net6.0\System.IO.IsolatedStorage.dll"
        @"-r:C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\6.0.10\ref\net6.0\System.IO.MemoryMappedFiles.dll"
        @"-r:C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\6.0.10\ref\net6.0\System.IO.Pipes.AccessControl.dll"
        @"-r:C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\6.0.10\ref\net6.0\System.IO.Pipes.dll"
        @"-r:C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\6.0.10\ref\net6.0\System.IO.UnmanagedMemoryStream.dll"
        @"-r:C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\6.0.10\ref\net6.0\System.Linq.dll"
        @"-r:C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\6.0.10\ref\net6.0\System.Linq.Expressions.dll"
        @"-r:C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\6.0.10\ref\net6.0\System.Linq.Parallel.dll"
        @"-r:C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\6.0.10\ref\net6.0\System.Linq.Queryable.dll"
        @"-r:C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\6.0.10\ref\net6.0\System.Memory.dll"
        @"-r:C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\6.0.10\ref\net6.0\System.Net.dll"
        @"-r:C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\6.0.10\ref\net6.0\System.Net.Http.dll"
        @"-r:C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\6.0.10\ref\net6.0\System.Net.Http.Json.dll"
        @"-r:C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\6.0.10\ref\net6.0\System.Net.HttpListener.dll"
        @"-r:C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\6.0.10\ref\net6.0\System.Net.Mail.dll"
        @"-r:C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\6.0.10\ref\net6.0\System.Net.NameResolution.dll"
        @"-r:C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\6.0.10\ref\net6.0\System.Net.NetworkInformation.dll"
        @"-r:C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\6.0.10\ref\net6.0\System.Net.Ping.dll"
        @"-r:C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\6.0.10\ref\net6.0\System.Net.Primitives.dll"
        @"-r:C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\6.0.10\ref\net6.0\System.Net.Requests.dll"
        @"-r:C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\6.0.10\ref\net6.0\System.Net.Security.dll"
        @"-r:C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\6.0.10\ref\net6.0\System.Net.ServicePoint.dll"
        @"-r:C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\6.0.10\ref\net6.0\System.Net.Sockets.dll"
        @"-r:C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\6.0.10\ref\net6.0\System.Net.WebClient.dll"
        @"-r:C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\6.0.10\ref\net6.0\System.Net.WebHeaderCollection.dll"
        @"-r:C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\6.0.10\ref\net6.0\System.Net.WebProxy.dll"
        @"-r:C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\6.0.10\ref\net6.0\System.Net.WebSockets.Client.dll"
        @"-r:C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\6.0.10\ref\net6.0\System.Net.WebSockets.dll"
        @"-r:C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\6.0.10\ref\net6.0\System.Numerics.dll"
        @"-r:C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\6.0.10\ref\net6.0\System.Numerics.Vectors.dll"
        @"-r:C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\6.0.10\ref\net6.0\System.ObjectModel.dll"
        @"-r:C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\6.0.10\ref\net6.0\System.Reflection.DispatchProxy.dll"
        @"-r:C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\6.0.10\ref\net6.0\System.Reflection.dll"
        @"-r:C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\6.0.10\ref\net6.0\System.Reflection.Emit.dll"
        @"-r:C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\6.0.10\ref\net6.0\System.Reflection.Emit.ILGeneration.dll"
        @"-r:C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\6.0.10\ref\net6.0\System.Reflection.Emit.Lightweight.dll"
        @"-r:C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\6.0.10\ref\net6.0\System.Reflection.Extensions.dll"
        @"-r:C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\6.0.10\ref\net6.0\System.Reflection.Metadata.dll"
        @"-r:C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\6.0.10\ref\net6.0\System.Reflection.Primitives.dll"
        @"-r:C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\6.0.10\ref\net6.0\System.Reflection.TypeExtensions.dll"
        @"-r:C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\6.0.10\ref\net6.0\System.Resources.Reader.dll"
        @"-r:C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\6.0.10\ref\net6.0\System.Resources.ResourceManager.dll"
        @"-r:C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\6.0.10\ref\net6.0\System.Resources.Writer.dll"
        @"-r:C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\6.0.10\ref\net6.0\System.Runtime.CompilerServices.Unsafe.dll"
        @"-r:C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\6.0.10\ref\net6.0\System.Runtime.CompilerServices.VisualC.dll"
        @"-r:C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\6.0.10\ref\net6.0\System.Runtime.dll"
        @"-r:C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\6.0.10\ref\net6.0\System.Runtime.Extensions.dll"
        @"-r:C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\6.0.10\ref\net6.0\System.Runtime.Handles.dll"
        @"-r:C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\6.0.10\ref\net6.0\System.Runtime.InteropServices.dll"
        @"-r:C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\6.0.10\ref\net6.0\System.Runtime.InteropServices.RuntimeInformation.dll"
        @"-r:C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\6.0.10\ref\net6.0\System.Runtime.Intrinsics.dll"
        @"-r:C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\6.0.10\ref\net6.0\System.Runtime.Loader.dll"
        @"-r:C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\6.0.10\ref\net6.0\System.Runtime.Numerics.dll"
        @"-r:C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\6.0.10\ref\net6.0\System.Runtime.Serialization.dll"
        @"-r:C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\6.0.10\ref\net6.0\System.Runtime.Serialization.Formatters.dll"
        @"-r:C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\6.0.10\ref\net6.0\System.Runtime.Serialization.Json.dll"
        @"-r:C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\6.0.10\ref\net6.0\System.Runtime.Serialization.Primitives.dll"
        @"-r:C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\6.0.10\ref\net6.0\System.Runtime.Serialization.Xml.dll"
        @"-r:C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\6.0.10\ref\net6.0\System.Security.AccessControl.dll"
        @"-r:C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\6.0.10\ref\net6.0\System.Security.Claims.dll"
        @"-r:C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\6.0.10\ref\net6.0\System.Security.Cryptography.Algorithms.dll"
        @"-r:C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\6.0.10\ref\net6.0\System.Security.Cryptography.Cng.dll"
        @"-r:C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\6.0.10\ref\net6.0\System.Security.Cryptography.Csp.dll"
        @"-r:C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\6.0.10\ref\net6.0\System.Security.Cryptography.Encoding.dll"
        @"-r:C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\6.0.10\ref\net6.0\System.Security.Cryptography.OpenSsl.dll"
        @"-r:C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\6.0.10\ref\net6.0\System.Security.Cryptography.Primitives.dll"
        @"-r:C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\6.0.10\ref\net6.0\System.Security.Cryptography.X509Certificates.dll"
        @"-r:C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\6.0.10\ref\net6.0\System.Security.dll"
        @"-r:C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\6.0.10\ref\net6.0\System.Security.Principal.dll"
        @"-r:C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\6.0.10\ref\net6.0\System.Security.Principal.Windows.dll"
        @"-r:C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\6.0.10\ref\net6.0\System.Security.SecureString.dll"
        @"-r:C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\6.0.10\ref\net6.0\System.ServiceModel.Web.dll"
        @"-r:C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\6.0.10\ref\net6.0\System.ServiceProcess.dll"
        @"-r:C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\6.0.10\ref\net6.0\System.Text.Encoding.CodePages.dll"
        @"-r:C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\6.0.10\ref\net6.0\System.Text.Encoding.dll"
        @"-r:C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\6.0.10\ref\net6.0\System.Text.Encoding.Extensions.dll"
        @"-r:C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\6.0.10\ref\net6.0\System.Text.Encodings.Web.dll"
        @"-r:C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\6.0.10\ref\net6.0\System.Text.Json.dll"
        @"-r:C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\6.0.10\ref\net6.0\System.Text.RegularExpressions.dll"
        @"-r:C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\6.0.10\ref\net6.0\System.Threading.Channels.dll"
        @"-r:C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\6.0.10\ref\net6.0\System.Threading.dll"
        @"-r:C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\6.0.10\ref\net6.0\System.Threading.Overlapped.dll"
        @"-r:C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\6.0.10\ref\net6.0\System.Threading.Tasks.Dataflow.dll"
        @"-r:C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\6.0.10\ref\net6.0\System.Threading.Tasks.dll"
        @"-r:C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\6.0.10\ref\net6.0\System.Threading.Tasks.Extensions.dll"
        @"-r:C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\6.0.10\ref\net6.0\System.Threading.Tasks.Parallel.dll"
        @"-r:C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\6.0.10\ref\net6.0\System.Threading.Thread.dll"
        @"-r:C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\6.0.10\ref\net6.0\System.Threading.ThreadPool.dll"
        @"-r:C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\6.0.10\ref\net6.0\System.Threading.Timer.dll"
        @"-r:C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\6.0.10\ref\net6.0\System.Transactions.dll"
        @"-r:C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\6.0.10\ref\net6.0\System.Transactions.Local.dll"
        @"-r:C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\6.0.10\ref\net6.0\System.ValueTuple.dll"
        @"-r:C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\6.0.10\ref\net6.0\System.Web.dll"
        @"-r:C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\6.0.10\ref\net6.0\System.Web.HttpUtility.dll"
        @"-r:C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\6.0.10\ref\net6.0\System.Windows.dll"
        @"-r:C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\6.0.10\ref\net6.0\System.Xml.dll"
        @"-r:C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\6.0.10\ref\net6.0\System.Xml.Linq.dll"
        @"-r:C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\6.0.10\ref\net6.0\System.Xml.ReaderWriter.dll"
        @"-r:C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\6.0.10\ref\net6.0\System.Xml.Serialization.dll"
        @"-r:C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\6.0.10\ref\net6.0\System.Xml.XDocument.dll"
        @"-r:C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\6.0.10\ref\net6.0\System.Xml.XmlDocument.dll"
        @"-r:C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\6.0.10\ref\net6.0\System.Xml.XmlSerializer.dll"
        @"-r:C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\6.0.10\ref\net6.0\System.Xml.XPath.dll"
        @"-r:C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\6.0.10\ref\net6.0\System.Xml.XPath.XDocument.dll"
        @"-r:C:\Users\nojaf\.nuget\packages\microsoft.testplatform.testhost\17.3.2\lib\netcoreapp2.1\testhost.dll"
        @"-r:C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\6.0.10\ref\net6.0\WindowsBase.dll"
        @"--target:exe"
        @"--nowarn:FS0988,IL2121"
        @"--warn:3"
        @"--warnon:3390"
        @"--warnaserror:FS0025"
        @"--fullpaths"
        @"--flaterrors"
        @"--highentropyva+"
        @"--targetprofile:netcore"
        @"--nocopyfsharpcore"
        @"--deterministic+"
        @"--simpleresolution"
        @"C:\Users\nojaf\Projects\main-fantomas\src\Fantomas.Core.Tests\obj\Debug\net6.0\.NETCoreApp,Version=v6.0.AssemblyAttributes.fs"
        @"C:\Users\nojaf\Projects\main-fantomas\src\Fantomas.Core.Tests\obj\Debug\net6.0\Fantomas.Core.Tests.AssemblyInfo.fs"
        @"C:\Users\nojaf\Projects\main-fantomas\src\Fantomas.Core.Tests\FsUnit.fs"
        @"C:\Users\nojaf\Projects\main-fantomas\src\Fantomas.Core.Tests\TestHelpers.fs"
        @"C:\Users\nojaf\Projects\main-fantomas\src\Fantomas.Core.Tests\UtilsTests.fs"
        @"C:\Users\nojaf\Projects\main-fantomas\src\Fantomas.Core.Tests\QueueTests.fs"
        @"C:\Users\nojaf\Projects\main-fantomas\src\Fantomas.Core.Tests\DefinesTests.fs"
        @"C:\Users\nojaf\Projects\main-fantomas\src\Fantomas.Core.Tests\TriviaTests.fs"
        @"C:\Users\nojaf\Projects\main-fantomas\src\Fantomas.Core.Tests\StringTests.fs"
        @"C:\Users\nojaf\Projects\main-fantomas\src\Fantomas.Core.Tests\CommentTests.fs"
        @"C:\Users\nojaf\Projects\main-fantomas\src\Fantomas.Core.Tests\ShebangTests.fs"
        @"C:\Users\nojaf\Projects\main-fantomas\src\Fantomas.Core.Tests\OperatorTests.fs"
        @"C:\Users\nojaf\Projects\main-fantomas\src\Fantomas.Core.Tests\ComparisonTests.fs"
        @"C:\Users\nojaf\Projects\main-fantomas\src\Fantomas.Core.Tests\ControlStructureTests.fs"
        @"C:\Users\nojaf\Projects\main-fantomas\src\Fantomas.Core.Tests\PipingTests.fs"
        @"C:\Users\nojaf\Projects\main-fantomas\src\Fantomas.Core.Tests\AppTests.fs"
        @"C:\Users\nojaf\Projects\main-fantomas\src\Fantomas.Core.Tests\ModuleTests.fs"
        @"C:\Users\nojaf\Projects\main-fantomas\src\Fantomas.Core.Tests\UnionTests.fs"
        @"C:\Users\nojaf\Projects\main-fantomas\src\Fantomas.Core.Tests\RecordTests.fs"
        @"C:\Users\nojaf\Projects\main-fantomas\src\Fantomas.Core.Tests\TypeDeclarationTests.fs"
        @"C:\Users\nojaf\Projects\main-fantomas\src\Fantomas.Core.Tests\InterfaceTests.fs"
        @"C:\Users\nojaf\Projects\main-fantomas\src\Fantomas.Core.Tests\ClassTests.fs"
        @"C:\Users\nojaf\Projects\main-fantomas\src\Fantomas.Core.Tests\StructTests.fs"
        @"C:\Users\nojaf\Projects\main-fantomas\src\Fantomas.Core.Tests\SignatureTests.fs"
        @"C:\Users\nojaf\Projects\main-fantomas\src\Fantomas.Core.Tests\PatternMatchingTests.fs"
        @"C:\Users\nojaf\Projects\main-fantomas\src\Fantomas.Core.Tests\ActivePatternTests.fs"
        @"C:\Users\nojaf\Projects\main-fantomas\src\Fantomas.Core.Tests\QuotationTests.fs"
        @"C:\Users\nojaf\Projects\main-fantomas\src\Fantomas.Core.Tests\FunctionDefinitionTests.fs"
        @"C:\Users\nojaf\Projects\main-fantomas\src\Fantomas.Core.Tests\AttributeTests.fs"
        @"C:\Users\nojaf\Projects\main-fantomas\src\Fantomas.Core.Tests\ListTests.fs"
        @"C:\Users\nojaf\Projects\main-fantomas\src\Fantomas.Core.Tests\CompilerDirectivesTests.fs"
        @"C:\Users\nojaf\Projects\main-fantomas\src\Fantomas.Core.Tests\ComputationExpressionTests.fs"
        @"C:\Users\nojaf\Projects\main-fantomas\src\Fantomas.Core.Tests\TypeProviderTests.fs"
        @"C:\Users\nojaf\Projects\main-fantomas\src\Fantomas.Core.Tests\FormattingSelectionOnlyTests.fs"
        @"C:\Users\nojaf\Projects\main-fantomas\src\Fantomas.Core.Tests\ValidationTests.fs"
        @"C:\Users\nojaf\Projects\main-fantomas\src\Fantomas.Core.Tests\SpecialConstructsTests.fs"
        @"C:\Users\nojaf\Projects\main-fantomas\src\Fantomas.Core.Tests\FormatAstTests.fs"
        @"C:\Users\nojaf\Projects\main-fantomas\src\Fantomas.Core.Tests\LetBindingTests.fs"
        @"C:\Users\nojaf\Projects\main-fantomas\src\Fantomas.Core.Tests\NoTrailingSpacesTests.fs"
        @"C:\Users\nojaf\Projects\main-fantomas\src\Fantomas.Core.Tests\LazyTests.fs"
        @"C:\Users\nojaf\Projects\main-fantomas\src\Fantomas.Core.Tests\SynLongIdentTests.fs"
        @"C:\Users\nojaf\Projects\main-fantomas\src\Fantomas.Core.Tests\DynamicOperatorTests.fs"
        @"C:\Users\nojaf\Projects\main-fantomas\src\Fantomas.Core.Tests\SynExprSetTests.fs"
        @"C:\Users\nojaf\Projects\main-fantomas\src\Fantomas.Core.Tests\SynConstTests.fs"
        @"C:\Users\nojaf\Projects\main-fantomas\src\Fantomas.Core.Tests\TupleTests.fs"
        @"C:\Users\nojaf\Projects\main-fantomas\src\Fantomas.Core.Tests\LambdaTests.fs"
        @"C:\Users\nojaf\Projects\main-fantomas\src\Fantomas.Core.Tests\IfThenElseTests.fs"
        @"C:\Users\nojaf\Projects\main-fantomas\src\Fantomas.Core.Tests\ContextTests.fs"
        @"C:\Users\nojaf\Projects\main-fantomas\src\Fantomas.Core.Tests\SpaceBeforeSemicolonTests.fs"
        @"C:\Users\nojaf\Projects\main-fantomas\src\Fantomas.Core.Tests\SpaceBeforeParameterTests.fs"
        @"C:\Users\nojaf\Projects\main-fantomas\src\Fantomas.Core.Tests\SpaceBeforeLowercaseInvocationTests.fs"
        @"C:\Users\nojaf\Projects\main-fantomas\src\Fantomas.Core.Tests\SpaceBeforeUppercaseInvocationTests.fs"
        @"C:\Users\nojaf\Projects\main-fantomas\src\Fantomas.Core.Tests\SpaceBeforeClassConstructorTests.fs"
        @"C:\Users\nojaf\Projects\main-fantomas\src\Fantomas.Core.Tests\SpaceBeforeMemberTests.fs"
        @"C:\Users\nojaf\Projects\main-fantomas\src\Fantomas.Core.Tests\MultilineBlockBracketsOnSameColumnRecordTests.fs"
        @"C:\Users\nojaf\Projects\main-fantomas\src\Fantomas.Core.Tests\MultilineBlockBracketsOnSameColumnArrayOrListTests.fs"
        @"C:\Users\nojaf\Projects\main-fantomas\src\Fantomas.Core.Tests\NewlineBetweenTypeDefinitionAndMembersTests.fs"
        @"C:\Users\nojaf\Projects\main-fantomas\src\Fantomas.Core.Tests\MaxValueBindingWidthTests.fs"
        @"C:\Users\nojaf\Projects\main-fantomas\src\Fantomas.Core.Tests\MaxFunctionBindingWidthTests.fs"
        @"C:\Users\nojaf\Projects\main-fantomas\src\Fantomas.Core.Tests\InterpolatedStringTests.fs"
        @"C:\Users\nojaf\Projects\main-fantomas\src\Fantomas.Core.Tests\DotGetTests.fs"
        @"C:\Users\nojaf\Projects\main-fantomas\src\Fantomas.Core.Tests\HashDirectiveTests.fs"
        @"C:\Users\nojaf\Projects\main-fantomas\src\Fantomas.Core.Tests\NumberOfItemsListOrArrayTests.fs"
        @"C:\Users\nojaf\Projects\main-fantomas\src\Fantomas.Core.Tests\NumberOfItemsRecordTests.fs"
        @"C:\Users\nojaf\Projects\main-fantomas\src\Fantomas.Core.Tests\SynExprNewTests.fs"
        @"C:\Users\nojaf\Projects\main-fantomas\src\Fantomas.Core.Tests\CastTests.fs"
        @"C:\Users\nojaf\Projects\main-fantomas\src\Fantomas.Core.Tests\MultiLineLambdaClosingNewlineTests.fs"
        @"C:\Users\nojaf\Projects\main-fantomas\src\Fantomas.Core.Tests\OpenTypeTests.fs"
        @"C:\Users\nojaf\Projects\main-fantomas\src\Fantomas.Core.Tests\CodeFormatterTests.fs"
        @"C:\Users\nojaf\Projects\main-fantomas\src\Fantomas.Core.Tests\DotIndexedGetTests.fs"
        @"C:\Users\nojaf\Projects\main-fantomas\src\Fantomas.Core.Tests\DotIndexedSetTests.fs"
        @"C:\Users\nojaf\Projects\main-fantomas\src\Fantomas.Core.Tests\MultilineFunctionApplicationsInConditionExpressionsTests.fs"
        @"C:\Users\nojaf\Projects\main-fantomas\src\Fantomas.Core.Tests\KeepIndentInBranchTests.fs"
        @"C:\Users\nojaf\Projects\main-fantomas\src\Fantomas.Core.Tests\BlankLinesAroundNestedMultilineExpressions.fs"
        @"C:\Users\nojaf\Projects\main-fantomas\src\Fantomas.Core.Tests\ColMultilineItemTests.fs"
        @"C:\Users\nojaf\Projects\main-fantomas\src\Fantomas.Core.Tests\BarBeforeDiscriminatedUnionDeclarationTests.fs"
        @"C:\Users\nojaf\Projects\main-fantomas\src\Fantomas.Core.Tests\SpaceBeforeColonTests.fs"
        @"C:\Users\nojaf\Projects\main-fantomas\src\Fantomas.Core.Tests\LibraryOnlySynExprTests.fs"
        @"C:\Users\nojaf\Projects\main-fantomas\src\Fantomas.Core.Tests\IndexSyntaxTests.fs"
        @"C:\Users\nojaf\Projects\main-fantomas\src\Fantomas.Core.Tests\InsertFinalNewlineTests.fs"
        @"C:\Users\nojaf\Projects\main-fantomas\src\Fantomas.Core.Tests\SingleExprTests.fs"
        @"C:\Users\nojaf\Projects\main-fantomas\src\Fantomas.Core.Tests\KeepMaxEmptyLinesTests.fs"
        @"C:\Users\nojaf\Projects\main-fantomas\src\Fantomas.Core.Tests\Stroustrup\SynBindingValueExpressionTests.fs"
        @"C:\Users\nojaf\Projects\main-fantomas\src\Fantomas.Core.Tests\Stroustrup\SynBindingFunctionExpressionTests.fs"
        @"C:\Users\nojaf\Projects\main-fantomas\src\Fantomas.Core.Tests\Stroustrup\SynBindingFunctionLongPatternExpressionTests.fs"
        @"C:\Users\nojaf\Projects\main-fantomas\src\Fantomas.Core.Tests\Stroustrup\SynBindingFunctionWithReturnTypeExpressionTests.fs"
        @"C:\Users\nojaf\Projects\main-fantomas\src\Fantomas.Core.Tests\Stroustrup\LetOrUseBangExpressionTests.fs"
        @"C:\Users\nojaf\Projects\main-fantomas\src\Fantomas.Core.Tests\Stroustrup\YieldOrReturnExpressionTests.fs"
        @"C:\Users\nojaf\Projects\main-fantomas\src\Fantomas.Core.Tests\Stroustrup\YieldOrReturnBangExpressionTests.fs"
        @"C:\Users\nojaf\Projects\main-fantomas\src\Fantomas.Core.Tests\Stroustrup\SynExprAndBangExpressionTests.fs"
        @"C:\Users\nojaf\Projects\main-fantomas\src\Fantomas.Core.Tests\Stroustrup\LongIdentSetExpressionTests.fs"
        @"C:\Users\nojaf\Projects\main-fantomas\src\Fantomas.Core.Tests\Stroustrup\DotIndexedSetExpressionTests.fs"
        @"C:\Users\nojaf\Projects\main-fantomas\src\Fantomas.Core.Tests\Stroustrup\SetExpressionTests.fs"
        @"C:\Users\nojaf\Projects\main-fantomas\src\Fantomas.Core.Tests\Stroustrup\DotSetExpressionTests.fs"
        @"C:\Users\nojaf\Projects\main-fantomas\src\Fantomas.Core.Tests\Stroustrup\LambdaExpressionTests.fs"
        @"C:\Users\nojaf\Projects\main-fantomas\src\Fantomas.Core.Tests\Stroustrup\MultiLineLambdaClosingNewlineExpressionTests.fs"
        @"C:\Users\nojaf\Projects\main-fantomas\src\Fantomas.Core.Tests\Stroustrup\SynMatchClauseExpressionTests.fs"
        @"C:\Users\nojaf\Projects\main-fantomas\src\Fantomas.Core.Tests\Stroustrup\KeepIndentInBranchExpressionTests.fs"
        @"C:\Users\nojaf\Projects\main-fantomas\src\Fantomas.Core.Tests\Stroustrup\SynTypeDefnSimpleReprRecordTests.fs"
        @"C:\Users\nojaf\Projects\main-fantomas\src\Fantomas.Core.Tests\Stroustrup\SynTypeDefnSigReprSimpleTests.fs"
        @"C:\Users\nojaf\Projects\main-fantomas\src\Fantomas.Core.Tests\Stroustrup\NamedArgumentExpressionTests.fs"
        @"C:\Users\nojaf\Projects\main-fantomas\src\Fantomas.Core.Tests\Stroustrup\ElmishTests.fs"
        @"C:\Users\nojaf\Projects\main-fantomas\src\Fantomas.Core.Tests\Stroustrup\FunctionApplicationSingleListTests.fs"
        @"C:\Users\nojaf\Projects\main-fantomas\src\Fantomas.Core.Tests\Stroustrup\FunctionApplicationDualListTests.fs"
        @"C:\Users\nojaf\Projects\main-fantomas\src\Fantomas.Core.Tests\IdentTests.fs"
        @"C:\Users\nojaf\Projects\main-fantomas\src\Fantomas.Core.Tests\RecordDeclarationsWithXMLDocTests.fs"
        @"C:\Users\nojaf\Projects\main-fantomas\src\Fantomas.Core.Tests\AstExtensionsTests.fs"
        @"C:\Users\nojaf\Projects\main-fantomas\src\Fantomas.Core.Tests\MaxIfThenShortWidthTests.fs"
        @"C:\Users\nojaf\Projects\main-fantomas\src\Fantomas.Core.Tests\CodePrinterHelperFunctionsTests.fs"
        @"C:\Users\nojaf\Projects\main-fantomas\src\Fantomas.Core.Tests\ASTTransformerTests.fs"
        @"C:\Users\nojaf\Projects\main-fantomas\src\Fantomas.Core.Tests\DotSetTests.fs"
        @"C:\Users\nojaf\Projects\main-fantomas\src\Fantomas.Core.Tests\InterfaceStaticMethodTests.fs"
        @"C:\Users\nojaf\Projects\main-fantomas\src\Fantomas.Core.Tests\ExternTests.fs"
        @"C:\Users\nojaf\Projects\main-fantomas\src\Fantomas.Core.Tests\TypeAnnotationTests.fs"
        @"C:\Users\nojaf\Projects\main-fantomas\src\Fantomas.Core.Tests\BaseConstructorTests.fs"
        if inParallel then
            "--test:ParallelCheckingWithSignatureFilesOn"
    |]

[<Test>]
let ``Debug Fantomas.Core.Tests`` () =
    let checker = FSharpChecker.Create()

    let diagnostics, _exitCode =
        checker.Compile(fantomasCoreTestsArgs true) |> Async.RunSynchronously

    if diagnostics.Length > 0 then
        Assert.Fail($"Got errors, {diagnostics}")
    else
        Assert.Pass()

[<TestCaseSource(nameof (parallelOrNot2))>]
let ``Fantomas.Core.Tests`` isParallel =
    let checker = FSharpChecker.Create()

    let diagnostics, _exitCode =
        checker.Compile(fantomasCoreTestsArgs isParallel) |> Async.RunSynchronously

    if diagnostics.Length > 0 then
        Assert.Fail($"Got errors, {diagnostics}")
    else
        Assert.Pass()

let fscProj inParallel =
    let args =
        System.IO.File.ReadAllLines(@"C:\Users\nojaf\Projects\main-fsharp\src\Compiler\args.txt")

    [|
        yield "fsc.exe"
        yield! args
        if inParallel then
            yield "--test:ParallelCheckingWithSignatureFilesOn"
    |]

[<TestCaseSource(nameof (parallelOrNot2))>]
let ``FSC proj`` isParallel =
    let checker = FSharpChecker.Create()

    let diagnostics, _exitCode =
        checker.Compile(fscProj isParallel) |> Async.RunSynchronously

    if diagnostics.Length > 0 then
        Assert.Fail($"Got errors, {diagnostics}")
    else
        Assert.Pass()

[<Test>]
let ``debug FSC proj`` () =
    let checker = FSharpChecker.Create()

    let diagnostics, _exitCode = checker.Compile(fscProj true) |> Async.RunSynchronously

    if diagnostics.Length > 0 then
        Assert.Fail($"Got errors, {diagnostics}")
    else
        Assert.Pass()

let diamondArgs inParallel =
    let args =
        System.IO.File.ReadAllLines(@"C:\Users\nojaf\Projects\safesparrow-fsharp\tests\DiamondTest\args.txt")

    [|
        yield "fsc.exe"
        yield! args
        if inParallel then
            yield "--test:ParallelCheckingWithSignatureFilesOn"
    |]

[<Test>]
let ``debug diamond`` () =
    let checker = FSharpChecker.Create()

    let diagnostics, _exitCode =
        checker.Compile(diamondArgs true) |> Async.RunSynchronously

    if diagnostics.Length > 0 then
        Assert.Fail($"Got errors, {diagnostics}")
    else
        Assert.Pass()

open System.IO
open FSharp.Compiler.Symbols

[<Test>]
let ``Graph from typed tree`` () =
    let fsProj =
        @"C:\Users\nojaf\Projects\main-fantomas\src\Fantomas.Core\Fantomas.Core.fsproj"

    let projectDir = Path.GetDirectoryName(fsProj)

    let projectOptions: FSharpProjectOptions =
        {
            ProjectFileName = "Fantomas.Core"
            ProjectId = None
            SourceFiles =
                [|
                    @"C:\Users\nojaf\Projects\main-fantomas\src\Fantomas.Core\obj\Debug\netstandard2.0\.NETStandard,Version=v2.0.AssemblyAttributes.fs"
                    @"C:\Users\nojaf\Projects\main-fantomas\src\Fantomas.Core\obj\Debug\netstandard2.0\Fantomas.Core.AssemblyInfo.fs"
                    @"C:\Users\nojaf\Projects\main-fantomas\src\Fantomas.Core\AssemblyInfo.fs"
                    @"C:\Users\nojaf\Projects\main-fantomas\src\Fantomas.Core\ISourceTextExtensions.fs"
                    @"C:\Users\nojaf\Projects\main-fantomas\src\Fantomas.Core\RangeHelpers.fs"
                    @"C:\Users\nojaf\Projects\main-fantomas\src\Fantomas.Core\AstExtensions.fsi"
                    @"C:\Users\nojaf\Projects\main-fantomas\src\Fantomas.Core\AstExtensions.fs"
                    @"C:\Users\nojaf\Projects\main-fantomas\src\Fantomas.Core\TriviaTypes.fs"
                    @"C:\Users\nojaf\Projects\main-fantomas\src\Fantomas.Core\Utils.fs"
                    @"C:\Users\nojaf\Projects\main-fantomas\src\Fantomas.Core\SourceParser.fs"
                    @"C:\Users\nojaf\Projects\main-fantomas\src\Fantomas.Core\AstTransformer.fsi"
                    @"C:\Users\nojaf\Projects\main-fantomas\src\Fantomas.Core\AstTransformer.fs"
                    @"C:\Users\nojaf\Projects\main-fantomas\src\Fantomas.Core\Version.fs"
                    @"C:\Users\nojaf\Projects\main-fantomas\src\Fantomas.Core\Queue.fs"
                    @"C:\Users\nojaf\Projects\main-fantomas\src\Fantomas.Core\FormatConfig.fs"
                    @"C:\Users\nojaf\Projects\main-fantomas\src\Fantomas.Core\Defines.fsi"
                    @"C:\Users\nojaf\Projects\main-fantomas\src\Fantomas.Core\Defines.fs"
                    @"C:\Users\nojaf\Projects\main-fantomas\src\Fantomas.Core\Trivia.fsi"
                    @"C:\Users\nojaf\Projects\main-fantomas\src\Fantomas.Core\Trivia.fs"
                    @"C:\Users\nojaf\Projects\main-fantomas\src\Fantomas.Core\SourceTransformer.fs"
                    @"C:\Users\nojaf\Projects\main-fantomas\src\Fantomas.Core\Context.fs"
                    @"C:\Users\nojaf\Projects\main-fantomas\src\Fantomas.Core\CodePrinter.fsi"
                    @"C:\Users\nojaf\Projects\main-fantomas\src\Fantomas.Core\CodePrinter.fs"
                    @"C:\Users\nojaf\Projects\main-fantomas\src\Fantomas.Core\CodeFormatterImpl.fsi"
                    @"C:\Users\nojaf\Projects\main-fantomas\src\Fantomas.Core\CodeFormatterImpl.fs"
                    @"C:\Users\nojaf\Projects\main-fantomas\src\Fantomas.Core\Validation.fs"
                    @"C:\Users\nojaf\Projects\main-fantomas\src\Fantomas.Core\Selection.fsi"
                    @"C:\Users\nojaf\Projects\main-fantomas\src\Fantomas.Core\Selection.fs"
                    @"C:\Users\nojaf\Projects\main-fantomas\src\Fantomas.Core\CodeFormatter.fsi"
                    @"C:\Users\nojaf\Projects\main-fantomas\src\Fantomas.Core\CodeFormatter.fs"
                |]
            OtherOptions =
                [|
                    @"-o:C:\Users\nojaf\Projects\main-fantomas\src\Fantomas.Core\obj\Debug\netstandard2.0\Fantomas.Core.dll"
                    @"-g"
                    @"--debug:embedded"
                    @"--embed:C:\Users\nojaf\Projects\main-fantomas\src\Fantomas.Core\obj\Debug\netstandard2.0\Fantomas.Core.AssemblyInfo.fs"
                    @"--noframework"
                    @"--define:TRACE"
                    @"--define:DEBUG"
                    @"--define:NETSTANDARD"
                    @"--define:NETSTANDARD2_0"
                    @"--define:NETSTANDARD1_0_OR_GREATER"
                    @"--define:NETSTANDARD1_1_OR_GREATER"
                    @"--define:NETSTANDARD1_2_OR_GREATER"
                    @"--define:NETSTANDARD1_3_OR_GREATER"
                    @"--define:NETSTANDARD1_4_OR_GREATER"
                    @"--define:NETSTANDARD1_5_OR_GREATER"
                    @"--define:NETSTANDARD1_6_OR_GREATER"
                    @"--define:NETSTANDARD2_0_OR_GREATER"
                    @"--doc:C:\Users\nojaf\Projects\main-fantomas\src\Fantomas.Core\obj\Debug\netstandard2.0\Fantomas.Core.xml"
                    @"--optimize-"
                    @"-r:C:\Users\nojaf\Projects\main-fantomas\src\Fantomas.FCS\bin\Debug\netstandard2.0\Fantomas.FCS.dll"
                    @"-r:C:\Users\nojaf\.nuget\packages\fsharp.core\6.0.1\lib\netstandard2.0\FSharp.Core.dll"
                    @"-r:C:\Users\nojaf\.nuget\packages\netstandard.library\2.0.3\build\netstandard2.0\ref\Microsoft.Win32.Primitives.dll"
                    @"-r:C:\Users\nojaf\.nuget\packages\netstandard.library\2.0.3\build\netstandard2.0\ref\mscorlib.dll"
                    @"-r:C:\Users\nojaf\.nuget\packages\netstandard.library\2.0.3\build\netstandard2.0\ref\netstandard.dll"
                    @"-r:C:\Users\nojaf\.nuget\packages\netstandard.library\2.0.3\build\netstandard2.0\ref\System.AppContext.dll"
                    @"-r:C:\Users\nojaf\.nuget\packages\system.buffers\4.5.1\ref\netstandard2.0\System.Buffers.dll"
                    @"-r:C:\Users\nojaf\.nuget\packages\netstandard.library\2.0.3\build\netstandard2.0\ref\System.Collections.Concurrent.dll"
                    @"-r:C:\Users\nojaf\.nuget\packages\netstandard.library\2.0.3\build\netstandard2.0\ref\System.Collections.dll"
                    @"-r:C:\Users\nojaf\.nuget\packages\netstandard.library\2.0.3\build\netstandard2.0\ref\System.Collections.NonGeneric.dll"
                    @"-r:C:\Users\nojaf\.nuget\packages\netstandard.library\2.0.3\build\netstandard2.0\ref\System.Collections.Specialized.dll"
                    @"-r:C:\Users\nojaf\.nuget\packages\netstandard.library\2.0.3\build\netstandard2.0\ref\System.ComponentModel.Composition.dll"
                    @"-r:C:\Users\nojaf\.nuget\packages\netstandard.library\2.0.3\build\netstandard2.0\ref\System.ComponentModel.dll"
                    @"-r:C:\Users\nojaf\.nuget\packages\netstandard.library\2.0.3\build\netstandard2.0\ref\System.ComponentModel.EventBasedAsync.dll"
                    @"-r:C:\Users\nojaf\.nuget\packages\netstandard.library\2.0.3\build\netstandard2.0\ref\System.ComponentModel.Primitives.dll"
                    @"-r:C:\Users\nojaf\.nuget\packages\netstandard.library\2.0.3\build\netstandard2.0\ref\System.ComponentModel.TypeConverter.dll"
                    @"-r:C:\Users\nojaf\.nuget\packages\netstandard.library\2.0.3\build\netstandard2.0\ref\System.Console.dll"
                    @"-r:C:\Users\nojaf\.nuget\packages\netstandard.library\2.0.3\build\netstandard2.0\ref\System.Core.dll"
                    @"-r:C:\Users\nojaf\.nuget\packages\netstandard.library\2.0.3\build\netstandard2.0\ref\System.Data.Common.dll"
                    @"-r:C:\Users\nojaf\.nuget\packages\netstandard.library\2.0.3\build\netstandard2.0\ref\System.Data.dll"
                    @"-r:C:\Users\nojaf\.nuget\packages\netstandard.library\2.0.3\build\netstandard2.0\ref\System.Diagnostics.Contracts.dll"
                    @"-r:C:\Users\nojaf\.nuget\packages\netstandard.library\2.0.3\build\netstandard2.0\ref\System.Diagnostics.Debug.dll"
                    @"-r:C:\Users\nojaf\.nuget\packages\netstandard.library\2.0.3\build\netstandard2.0\ref\System.Diagnostics.FileVersionInfo.dll"
                    @"-r:C:\Users\nojaf\.nuget\packages\netstandard.library\2.0.3\build\netstandard2.0\ref\System.Diagnostics.Process.dll"
                    @"-r:C:\Users\nojaf\.nuget\packages\netstandard.library\2.0.3\build\netstandard2.0\ref\System.Diagnostics.StackTrace.dll"
                    @"-r:C:\Users\nojaf\.nuget\packages\netstandard.library\2.0.3\build\netstandard2.0\ref\System.Diagnostics.TextWriterTraceListener.dll"
                    @"-r:C:\Users\nojaf\.nuget\packages\netstandard.library\2.0.3\build\netstandard2.0\ref\System.Diagnostics.Tools.dll"
                    @"-r:C:\Users\nojaf\.nuget\packages\netstandard.library\2.0.3\build\netstandard2.0\ref\System.Diagnostics.TraceSource.dll"
                    @"-r:C:\Users\nojaf\.nuget\packages\netstandard.library\2.0.3\build\netstandard2.0\ref\System.Diagnostics.Tracing.dll"
                    @"-r:C:\Users\nojaf\.nuget\packages\netstandard.library\2.0.3\build\netstandard2.0\ref\System.dll"
                    @"-r:C:\Users\nojaf\.nuget\packages\netstandard.library\2.0.3\build\netstandard2.0\ref\System.Drawing.dll"
                    @"-r:C:\Users\nojaf\.nuget\packages\netstandard.library\2.0.3\build\netstandard2.0\ref\System.Drawing.Primitives.dll"
                    @"-r:C:\Users\nojaf\.nuget\packages\netstandard.library\2.0.3\build\netstandard2.0\ref\System.Dynamic.Runtime.dll"
                    @"-r:C:\Users\nojaf\.nuget\packages\netstandard.library\2.0.3\build\netstandard2.0\ref\System.Globalization.Calendars.dll"
                    @"-r:C:\Users\nojaf\.nuget\packages\netstandard.library\2.0.3\build\netstandard2.0\ref\System.Globalization.dll"
                    @"-r:C:\Users\nojaf\.nuget\packages\netstandard.library\2.0.3\build\netstandard2.0\ref\System.Globalization.Extensions.dll"
                    @"-r:C:\Users\nojaf\.nuget\packages\netstandard.library\2.0.3\build\netstandard2.0\ref\System.IO.Compression.dll"
                    @"-r:C:\Users\nojaf\.nuget\packages\netstandard.library\2.0.3\build\netstandard2.0\ref\System.IO.Compression.FileSystem.dll"
                    @"-r:C:\Users\nojaf\.nuget\packages\netstandard.library\2.0.3\build\netstandard2.0\ref\System.IO.Compression.ZipFile.dll"
                    @"-r:C:\Users\nojaf\.nuget\packages\netstandard.library\2.0.3\build\netstandard2.0\ref\System.IO.dll"
                    @"-r:C:\Users\nojaf\.nuget\packages\netstandard.library\2.0.3\build\netstandard2.0\ref\System.IO.FileSystem.dll"
                    @"-r:C:\Users\nojaf\.nuget\packages\netstandard.library\2.0.3\build\netstandard2.0\ref\System.IO.FileSystem.DriveInfo.dll"
                    @"-r:C:\Users\nojaf\.nuget\packages\netstandard.library\2.0.3\build\netstandard2.0\ref\System.IO.FileSystem.Primitives.dll"
                    @"-r:C:\Users\nojaf\.nuget\packages\netstandard.library\2.0.3\build\netstandard2.0\ref\System.IO.FileSystem.Watcher.dll"
                    @"-r:C:\Users\nojaf\.nuget\packages\netstandard.library\2.0.3\build\netstandard2.0\ref\System.IO.IsolatedStorage.dll"
                    @"-r:C:\Users\nojaf\.nuget\packages\netstandard.library\2.0.3\build\netstandard2.0\ref\System.IO.MemoryMappedFiles.dll"
                    @"-r:C:\Users\nojaf\.nuget\packages\netstandard.library\2.0.3\build\netstandard2.0\ref\System.IO.Pipes.dll"
                    @"-r:C:\Users\nojaf\.nuget\packages\netstandard.library\2.0.3\build\netstandard2.0\ref\System.IO.UnmanagedMemoryStream.dll"
                    @"-r:C:\Users\nojaf\.nuget\packages\netstandard.library\2.0.3\build\netstandard2.0\ref\System.Linq.dll"
                    @"-r:C:\Users\nojaf\.nuget\packages\netstandard.library\2.0.3\build\netstandard2.0\ref\System.Linq.Expressions.dll"
                    @"-r:C:\Users\nojaf\.nuget\packages\netstandard.library\2.0.3\build\netstandard2.0\ref\System.Linq.Parallel.dll"
                    @"-r:C:\Users\nojaf\.nuget\packages\netstandard.library\2.0.3\build\netstandard2.0\ref\System.Linq.Queryable.dll"
                    @"-r:C:\Users\nojaf\.nuget\packages\system.memory\4.5.4\lib\netstandard2.0\System.Memory.dll"
                    @"-r:C:\Users\nojaf\.nuget\packages\netstandard.library\2.0.3\build\netstandard2.0\ref\System.Net.dll"
                    @"-r:C:\Users\nojaf\.nuget\packages\netstandard.library\2.0.3\build\netstandard2.0\ref\System.Net.Http.dll"
                    @"-r:C:\Users\nojaf\.nuget\packages\netstandard.library\2.0.3\build\netstandard2.0\ref\System.Net.NameResolution.dll"
                    @"-r:C:\Users\nojaf\.nuget\packages\netstandard.library\2.0.3\build\netstandard2.0\ref\System.Net.NetworkInformation.dll"
                    @"-r:C:\Users\nojaf\.nuget\packages\netstandard.library\2.0.3\build\netstandard2.0\ref\System.Net.Ping.dll"
                    @"-r:C:\Users\nojaf\.nuget\packages\netstandard.library\2.0.3\build\netstandard2.0\ref\System.Net.Primitives.dll"
                    @"-r:C:\Users\nojaf\.nuget\packages\netstandard.library\2.0.3\build\netstandard2.0\ref\System.Net.Requests.dll"
                    @"-r:C:\Users\nojaf\.nuget\packages\netstandard.library\2.0.3\build\netstandard2.0\ref\System.Net.Security.dll"
                    @"-r:C:\Users\nojaf\.nuget\packages\netstandard.library\2.0.3\build\netstandard2.0\ref\System.Net.Sockets.dll"
                    @"-r:C:\Users\nojaf\.nuget\packages\netstandard.library\2.0.3\build\netstandard2.0\ref\System.Net.WebHeaderCollection.dll"
                    @"-r:C:\Users\nojaf\.nuget\packages\netstandard.library\2.0.3\build\netstandard2.0\ref\System.Net.WebSockets.Client.dll"
                    @"-r:C:\Users\nojaf\.nuget\packages\netstandard.library\2.0.3\build\netstandard2.0\ref\System.Net.WebSockets.dll"
                    @"-r:C:\Users\nojaf\.nuget\packages\netstandard.library\2.0.3\build\netstandard2.0\ref\System.Numerics.dll"
                    @"-r:C:\Users\nojaf\.nuget\packages\system.numerics.vectors\4.4.0\ref\netstandard2.0\System.Numerics.Vectors.dll"
                    @"-r:C:\Users\nojaf\.nuget\packages\netstandard.library\2.0.3\build\netstandard2.0\ref\System.ObjectModel.dll"
                    @"-r:C:\Users\nojaf\.nuget\packages\netstandard.library\2.0.3\build\netstandard2.0\ref\System.Reflection.dll"
                    @"-r:C:\Users\nojaf\.nuget\packages\netstandard.library\2.0.3\build\netstandard2.0\ref\System.Reflection.Extensions.dll"
                    @"-r:C:\Users\nojaf\.nuget\packages\netstandard.library\2.0.3\build\netstandard2.0\ref\System.Reflection.Primitives.dll"
                    @"-r:C:\Users\nojaf\.nuget\packages\netstandard.library\2.0.3\build\netstandard2.0\ref\System.Resources.Reader.dll"
                    @"-r:C:\Users\nojaf\.nuget\packages\netstandard.library\2.0.3\build\netstandard2.0\ref\System.Resources.ResourceManager.dll"
                    @"-r:C:\Users\nojaf\.nuget\packages\netstandard.library\2.0.3\build\netstandard2.0\ref\System.Resources.Writer.dll"
                    @"-r:C:\Users\nojaf\.nuget\packages\system.runtime.compilerservices.unsafe\4.5.3\ref\netstandard2.0\System.Runtime.CompilerServices.Unsafe.dll"
                    @"-r:C:\Users\nojaf\.nuget\packages\netstandard.library\2.0.3\build\netstandard2.0\ref\System.Runtime.CompilerServices.VisualC.dll"
                    @"-r:C:\Users\nojaf\.nuget\packages\netstandard.library\2.0.3\build\netstandard2.0\ref\System.Runtime.dll"
                    @"-r:C:\Users\nojaf\.nuget\packages\netstandard.library\2.0.3\build\netstandard2.0\ref\System.Runtime.Extensions.dll"
                    @"-r:C:\Users\nojaf\.nuget\packages\netstandard.library\2.0.3\build\netstandard2.0\ref\System.Runtime.Handles.dll"
                    @"-r:C:\Users\nojaf\.nuget\packages\netstandard.library\2.0.3\build\netstandard2.0\ref\System.Runtime.InteropServices.dll"
                    @"-r:C:\Users\nojaf\.nuget\packages\netstandard.library\2.0.3\build\netstandard2.0\ref\System.Runtime.InteropServices.RuntimeInformation.dll"
                    @"-r:C:\Users\nojaf\.nuget\packages\netstandard.library\2.0.3\build\netstandard2.0\ref\System.Runtime.Numerics.dll"
                    @"-r:C:\Users\nojaf\.nuget\packages\netstandard.library\2.0.3\build\netstandard2.0\ref\System.Runtime.Serialization.dll"
                    @"-r:C:\Users\nojaf\.nuget\packages\netstandard.library\2.0.3\build\netstandard2.0\ref\System.Runtime.Serialization.Formatters.dll"
                    @"-r:C:\Users\nojaf\.nuget\packages\netstandard.library\2.0.3\build\netstandard2.0\ref\System.Runtime.Serialization.Json.dll"
                    @"-r:C:\Users\nojaf\.nuget\packages\netstandard.library\2.0.3\build\netstandard2.0\ref\System.Runtime.Serialization.Primitives.dll"
                    @"-r:C:\Users\nojaf\.nuget\packages\netstandard.library\2.0.3\build\netstandard2.0\ref\System.Runtime.Serialization.Xml.dll"
                    @"-r:C:\Users\nojaf\.nuget\packages\netstandard.library\2.0.3\build\netstandard2.0\ref\System.Security.Claims.dll"
                    @"-r:C:\Users\nojaf\.nuget\packages\netstandard.library\2.0.3\build\netstandard2.0\ref\System.Security.Cryptography.Algorithms.dll"
                    @"-r:C:\Users\nojaf\.nuget\packages\netstandard.library\2.0.3\build\netstandard2.0\ref\System.Security.Cryptography.Csp.dll"
                    @"-r:C:\Users\nojaf\.nuget\packages\netstandard.library\2.0.3\build\netstandard2.0\ref\System.Security.Cryptography.Encoding.dll"
                    @"-r:C:\Users\nojaf\.nuget\packages\netstandard.library\2.0.3\build\netstandard2.0\ref\System.Security.Cryptography.Primitives.dll"
                    @"-r:C:\Users\nojaf\.nuget\packages\netstandard.library\2.0.3\build\netstandard2.0\ref\System.Security.Cryptography.X509Certificates.dll"
                    @"-r:C:\Users\nojaf\.nuget\packages\netstandard.library\2.0.3\build\netstandard2.0\ref\System.Security.Principal.dll"
                    @"-r:C:\Users\nojaf\.nuget\packages\netstandard.library\2.0.3\build\netstandard2.0\ref\System.Security.SecureString.dll"
                    @"-r:C:\Users\nojaf\.nuget\packages\netstandard.library\2.0.3\build\netstandard2.0\ref\System.ServiceModel.Web.dll"
                    @"-r:C:\Users\nojaf\.nuget\packages\netstandard.library\2.0.3\build\netstandard2.0\ref\System.Text.Encoding.dll"
                    @"-r:C:\Users\nojaf\.nuget\packages\netstandard.library\2.0.3\build\netstandard2.0\ref\System.Text.Encoding.Extensions.dll"
                    @"-r:C:\Users\nojaf\.nuget\packages\netstandard.library\2.0.3\build\netstandard2.0\ref\System.Text.RegularExpressions.dll"
                    @"-r:C:\Users\nojaf\.nuget\packages\netstandard.library\2.0.3\build\netstandard2.0\ref\System.Threading.dll"
                    @"-r:C:\Users\nojaf\.nuget\packages\netstandard.library\2.0.3\build\netstandard2.0\ref\System.Threading.Overlapped.dll"
                    @"-r:C:\Users\nojaf\.nuget\packages\netstandard.library\2.0.3\build\netstandard2.0\ref\System.Threading.Tasks.dll"
                    @"-r:C:\Users\nojaf\.nuget\packages\netstandard.library\2.0.3\build\netstandard2.0\ref\System.Threading.Tasks.Parallel.dll"
                    @"-r:C:\Users\nojaf\.nuget\packages\netstandard.library\2.0.3\build\netstandard2.0\ref\System.Threading.Thread.dll"
                    @"-r:C:\Users\nojaf\.nuget\packages\netstandard.library\2.0.3\build\netstandard2.0\ref\System.Threading.ThreadPool.dll"
                    @"-r:C:\Users\nojaf\.nuget\packages\netstandard.library\2.0.3\build\netstandard2.0\ref\System.Threading.Timer.dll"
                    @"-r:C:\Users\nojaf\.nuget\packages\netstandard.library\2.0.3\build\netstandard2.0\ref\System.Transactions.dll"
                    @"-r:C:\Users\nojaf\.nuget\packages\netstandard.library\2.0.3\build\netstandard2.0\ref\System.ValueTuple.dll"
                    @"-r:C:\Users\nojaf\.nuget\packages\netstandard.library\2.0.3\build\netstandard2.0\ref\System.Web.dll"
                    @"-r:C:\Users\nojaf\.nuget\packages\netstandard.library\2.0.3\build\netstandard2.0\ref\System.Windows.dll"
                    @"-r:C:\Users\nojaf\.nuget\packages\netstandard.library\2.0.3\build\netstandard2.0\ref\System.Xml.dll"
                    @"-r:C:\Users\nojaf\.nuget\packages\netstandard.library\2.0.3\build\netstandard2.0\ref\System.Xml.Linq.dll"
                    @"-r:C:\Users\nojaf\.nuget\packages\netstandard.library\2.0.3\build\netstandard2.0\ref\System.Xml.ReaderWriter.dll"
                    @"-r:C:\Users\nojaf\.nuget\packages\netstandard.library\2.0.3\build\netstandard2.0\ref\System.Xml.Serialization.dll"
                    @"-r:C:\Users\nojaf\.nuget\packages\netstandard.library\2.0.3\build\netstandard2.0\ref\System.Xml.XDocument.dll"
                    @"-r:C:\Users\nojaf\.nuget\packages\netstandard.library\2.0.3\build\netstandard2.0\ref\System.Xml.XmlDocument.dll"
                    @"-r:C:\Users\nojaf\.nuget\packages\netstandard.library\2.0.3\build\netstandard2.0\ref\System.Xml.XmlSerializer.dll"
                    @"-r:C:\Users\nojaf\.nuget\packages\netstandard.library\2.0.3\build\netstandard2.0\ref\System.Xml.XPath.dll"
                    @"-r:C:\Users\nojaf\.nuget\packages\netstandard.library\2.0.3\build\netstandard2.0\ref\System.Xml.XPath.XDocument.dll"
                    @"--target:library"
                    @"--nowarn:NU1603,IL2121"
                    @"--warn:3"
                    @"--warnon:3390"
                    @"--warnaserror:3239,FS0025"
                    @"--fullpaths"
                    @"--flaterrors"
                    @"--highentropyva+"
                    @"--targetprofile:netstandard"
                    @"--nocopyfsharpcore"
                    @"--deterministic+"
                    @"--simpleresolution"
                |]
            ReferencedProjects = Array.empty
            IsIncompleteTypeCheckEnvironment = false
            UseScriptResolutionRules = false
            LoadTime = DateTime.Now
            UnresolvedReferences = None
            OriginalLoadReferences = []
            Stamp = None
        }

    let checker = FSharpChecker.Create(keepAssemblyContents = true)
    let result = checker.ParseAndCheckProject(projectOptions) |> Async.RunSynchronously

    let files =
        result.AssemblyContents.ImplementationFiles
        |> List.choose (fun implContents ->
            match implContents.Declarations with
            | [ FSharpImplementationFileDeclaration.Entity(entity, _decls) ] -> Some entity.DeclarationLocation.FileName
            | d -> failwithf "was %A" d)

    ()
