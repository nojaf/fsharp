module rec FSharp.Compiler.Service.Tests.LargeAppProjects

open System
open System.Collections.Generic
open FSharp.Compiler.CodeAnalysis

let private getTimeStamp () = DateTime.Now
let private loader _ = None

let memoization (f: string -> 'a) =
    let cache = Dictionary<_, _>()

    (fun x ->
        match cache.TryGetValue(x) with
        | true, cachedValue -> cachedValue
        | _ ->
            let result = f x
            cache.Add(x, result)
            result)

let Project0 _ =
    let projectOptions =
        { ProjectFileName = @"C:\Users\nojaf\Projects\fsharp\tests\service\data\LargeApp\Project0\Project0.fsproj"
          ProjectId = None
          SourceFiles =
            [| @"C:\Users\nojaf\Projects\fsharp\artifacts\obj\Project0\Debug\net472\Project0.AssemblyInfo.fs"
               @"C:\Users\nojaf\Projects\fsharp\tests\service\data\LargeApp\Project0\MyModule.fs" |]
          OtherOptions =
            [| @"-o:C:\Users\nojaf\Projects\fsharp\artifacts\obj\Project0\Debug\net472\Project0.dll"
               @"-g"
               @"--debug:portable"
               @"--noframework"
               @"--define:TRACE"
               @"--define:DEBUG"
               @"--define:ENABLE_MONO_SUPPORT"
               @"--define:NETFRAMEWORK"
               @"--define:NET472"
               @"--define:NET20_OR_GREATER"
               @"--define:NET30_OR_GREATER"
               @"--define:NET35_OR_GREATER"
               @"--define:NET40_OR_GREATER"
               @"--define:NET45_OR_GREATER"
               @"--define:NET451_OR_GREATER"
               @"--define:NET452_OR_GREATER"
               @"--define:NET46_OR_GREATER"
               @"--define:NET461_OR_GREATER"
               @"--define:NET462_OR_GREATER"
               @"--define:NET47_OR_GREATER"
               @"--define:NET471_OR_GREATER"
               @"--define:NET472_OR_GREATER"
               @"--doc:C:\Users\nojaf\Projects\fsharp\artifacts\obj\Project0\Debug\net472\Project0.xml"
               @"--publicsign+"
               @"--optimize-"
               @"--tailcalls-"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\mscorlib.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project116\Debug\net472\Project116.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project118\Debug\net472\Project118.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project140\Debug\net472\Project140.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project142\Debug\net472\Project142.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project144\Debug\net472\Project144.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project146\Debug\net472\Project146.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project149\Debug\net472\Project149.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project151\Debug\net472\Project151.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project161\Debug\net472\Project161.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project164\Debug\net472\Project164.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project168\Debug\net472\Project168.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project17\Debug\net472\Project17.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project189\Debug\net472\Project189.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project190\Debug\net472\Project190.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project22\Debug\net472\Project22.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project23\Debug\net472\Project23.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project243\Debug\net472\Project243.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project264\Debug\net472\Project264.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project272\Debug\net472\Project272.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project281\Debug\net472\Project281.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project323\Debug\net472\Project323.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project326\Debug\net472\Project326.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project327\Debug\net472\Project327.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project329\Debug\net472\Project329.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project346\Debug\net472\Project346.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project40\Debug\net472\Project40.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Core.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Data.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Drawing.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.IO.Compression.FileSystem.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Numerics.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Runtime.Serialization.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Xml.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Xml.Linq.dll"
               @"--target:library"
               @"--nowarn:FS2003,NU5105"
               @"--warn:3"
               @"--warnaserror:3239,1182,0025"
               @"--fullpaths"
               @"--flaterrors"
               @"--subsystemversion:6.00"
               @"--highentropyva+"
               @"--targetprofile:mscorlib"
               @"--nocopyfsharpcore"
               @"--deterministic+"
               @"--simpleresolution"
               @"--nowarn:3384" |]
          ReferencedProjects =
            [| memoProject116 "Project116"
               memoProject118 "Project118"
               memoProject151 "Project151"
               memoProject17 "Project17"
               memoProject22 "Project22"
               memoProject23 "Project23"
               memoProject243 "Project243"
               memoProject264 "Project264"
               memoProject272 "Project272"
               memoProject281 "Project281"
               memoProject329 "Project329"
               memoProject346 "Project346"
               memoProject40 "Project40" |]
          IsIncompleteTypeCheckEnvironment = false
          UseScriptResolutionRules = false
          LoadTime = DateTime.Now
          UnresolvedReferences = None
          OriginalLoadReferences = []
          Stamp = None }

    FSharpReferencedProject.CreateFSharp(
        @"C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project0\Debug\net472\Project0.dll",
        projectOptions
    )

let memoProject0 = memoization Project0

let Project116 _ =
    let projectOptions =
        { ProjectFileName = @"C:\Users\nojaf\Projects\fsharp\tests\service\data\LargeApp\Project116\Project116.fsproj"
          ProjectId = None
          SourceFiles =
            [| @"C:\Users\nojaf\Projects\fsharp\artifacts\obj\Project116\Debug\net472\Project116.AssemblyInfo.fs"
               @"C:\Users\nojaf\Projects\fsharp\tests\service\data\LargeApp\Project116\MyModule.fs" |]
          OtherOptions =
            [| @"-o:C:\Users\nojaf\Projects\fsharp\artifacts\obj\Project116\Debug\net472\Project116.dll"
               @"-g"
               @"--debug:portable"
               @"--noframework"
               @"--define:TRACE"
               @"--define:DEBUG"
               @"--define:ENABLE_MONO_SUPPORT"
               @"--define:NETFRAMEWORK"
               @"--define:NET472"
               @"--define:NET20_OR_GREATER"
               @"--define:NET30_OR_GREATER"
               @"--define:NET35_OR_GREATER"
               @"--define:NET40_OR_GREATER"
               @"--define:NET45_OR_GREATER"
               @"--define:NET451_OR_GREATER"
               @"--define:NET452_OR_GREATER"
               @"--define:NET46_OR_GREATER"
               @"--define:NET461_OR_GREATER"
               @"--define:NET462_OR_GREATER"
               @"--define:NET47_OR_GREATER"
               @"--define:NET471_OR_GREATER"
               @"--define:NET472_OR_GREATER"
               @"--doc:C:\Users\nojaf\Projects\fsharp\artifacts\obj\Project116\Debug\net472\Project116.xml"
               @"--publicsign+"
               @"--optimize-"
               @"--tailcalls-"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\mscorlib.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project11\Debug\net472\Project11.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project117\Debug\net472\Project117.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project14\Debug\net472\Project14.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project140\Debug\net472\Project140.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project142\Debug\net472\Project142.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project146\Debug\net472\Project146.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project149\Debug\net472\Project149.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project151\Debug\net472\Project151.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project161\Debug\net472\Project161.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project164\Debug\net472\Project164.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project168\Debug\net472\Project168.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project17\Debug\net472\Project17.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project21\Debug\net472\Project21.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project22\Debug\net472\Project22.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project23\Debug\net472\Project23.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project24\Debug\net472\Project24.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project243\Debug\net472\Project243.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project272\Debug\net472\Project272.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project298\Debug\net472\Project298.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project326\Debug\net472\Project326.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Core.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Data.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Drawing.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.IO.Compression.FileSystem.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Numerics.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Runtime.Serialization.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Xml.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Xml.Linq.dll"
               @"--target:library"
               @"--nowarn:FS2003,NU5105"
               @"--warn:3"
               @"--warnaserror:3239,1182,0025"
               @"--fullpaths"
               @"--flaterrors"
               @"--subsystemversion:6.00"
               @"--highentropyva+"
               @"--targetprofile:mscorlib"
               @"--nocopyfsharpcore"
               @"--deterministic+"
               @"--simpleresolution"
               @"--nowarn:3384" |]
          ReferencedProjects =
            [| memoProject117 "Project117"
               memoProject11 "Project11"
               memoProject14 "Project14"
               memoProject151 "Project151"
               memoProject17 "Project17"
               memoProject21 "Project21"
               memoProject22 "Project22"
               memoProject23 "Project23"
               memoProject243 "Project243"
               memoProject24 "Project24"
               memoProject272 "Project272" |]
          IsIncompleteTypeCheckEnvironment = false
          UseScriptResolutionRules = false
          LoadTime = DateTime.Now
          UnresolvedReferences = None
          OriginalLoadReferences = []
          Stamp = None }

    FSharpReferencedProject.CreateFSharp(
        @"C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project116\Debug\net472\Project116.dll",
        projectOptions
    )

let memoProject116 = memoization Project116

let Project117 _ =
    let projectOptions =
        { ProjectFileName = @"C:\Users\nojaf\Projects\fsharp\tests\service\data\LargeApp\Project117\Project117.fsproj"
          ProjectId = None
          SourceFiles =
            [| @"C:\Users\nojaf\Projects\fsharp\artifacts\obj\Project117\Debug\net472\Project117.AssemblyInfo.fs"
               @"C:\Users\nojaf\Projects\fsharp\tests\service\data\LargeApp\Project117\MyModule.fs" |]
          OtherOptions =
            [| @"-o:C:\Users\nojaf\Projects\fsharp\artifacts\obj\Project117\Debug\net472\Project117.dll"
               @"-g"
               @"--debug:portable"
               @"--noframework"
               @"--define:TRACE"
               @"--define:DEBUG"
               @"--define:ENABLE_MONO_SUPPORT"
               @"--define:NETFRAMEWORK"
               @"--define:NET472"
               @"--define:NET20_OR_GREATER"
               @"--define:NET30_OR_GREATER"
               @"--define:NET35_OR_GREATER"
               @"--define:NET40_OR_GREATER"
               @"--define:NET45_OR_GREATER"
               @"--define:NET451_OR_GREATER"
               @"--define:NET452_OR_GREATER"
               @"--define:NET46_OR_GREATER"
               @"--define:NET461_OR_GREATER"
               @"--define:NET462_OR_GREATER"
               @"--define:NET47_OR_GREATER"
               @"--define:NET471_OR_GREATER"
               @"--define:NET472_OR_GREATER"
               @"--doc:C:\Users\nojaf\Projects\fsharp\artifacts\obj\Project117\Debug\net472\Project117.xml"
               @"--publicsign+"
               @"--optimize-"
               @"--tailcalls-"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\mscorlib.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project14\Debug\net472\Project14.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project140\Debug\net472\Project140.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project142\Debug\net472\Project142.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project149\Debug\net472\Project149.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project151\Debug\net472\Project151.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project161\Debug\net472\Project161.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project164\Debug\net472\Project164.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project168\Debug\net472\Project168.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project17\Debug\net472\Project17.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project21\Debug\net472\Project21.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project214\Debug\net472\Project214.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project22\Debug\net472\Project22.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project24\Debug\net472\Project24.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project295\Debug\net472\Project295.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project298\Debug\net472\Project298.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project326\Debug\net472\Project326.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Core.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Data.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Drawing.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.IO.Compression.FileSystem.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Numerics.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Runtime.Serialization.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Xml.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Xml.Linq.dll"
               @"--target:library"
               @"--nowarn:FS2003,NU5105"
               @"--warn:3"
               @"--warnaserror:3239,1182,0025"
               @"--fullpaths"
               @"--flaterrors"
               @"--subsystemversion:6.00"
               @"--highentropyva+"
               @"--targetprofile:mscorlib"
               @"--nocopyfsharpcore"
               @"--deterministic+"
               @"--simpleresolution"
               @"--nowarn:3384" |]
          ReferencedProjects =
            [| memoProject14 "Project14"
               memoProject151 "Project151"
               memoProject17 "Project17"
               memoProject21 "Project21"
               memoProject22 "Project22"
               memoProject24 "Project24" |]
          IsIncompleteTypeCheckEnvironment = false
          UseScriptResolutionRules = false
          LoadTime = DateTime.Now
          UnresolvedReferences = None
          OriginalLoadReferences = []
          Stamp = None }

    FSharpReferencedProject.CreateFSharp(
        @"C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project117\Debug\net472\Project117.dll",
        projectOptions
    )

let memoProject117 = memoization Project117

let Project264 _ =
    let projectOptions =
        { ProjectFileName = @"C:\Users\nojaf\Projects\fsharp\tests\service\data\LargeApp\Project264\Project264.fsproj"
          ProjectId = None
          SourceFiles =
            [| @"C:\Users\nojaf\Projects\fsharp\artifacts\obj\Project264\Debug\net472\Project264.AssemblyInfo.fs"
               @"C:\Users\nojaf\Projects\fsharp\tests\service\data\LargeApp\Project264\MyModule.fs" |]
          OtherOptions =
            [| @"-o:C:\Users\nojaf\Projects\fsharp\artifacts\obj\Project264\Debug\net472\Project264.dll"
               @"-g"
               @"--debug:portable"
               @"--noframework"
               @"--define:TRACE"
               @"--define:DEBUG"
               @"--define:ENABLE_MONO_SUPPORT"
               @"--define:NETFRAMEWORK"
               @"--define:NET472"
               @"--define:NET20_OR_GREATER"
               @"--define:NET30_OR_GREATER"
               @"--define:NET35_OR_GREATER"
               @"--define:NET40_OR_GREATER"
               @"--define:NET45_OR_GREATER"
               @"--define:NET451_OR_GREATER"
               @"--define:NET452_OR_GREATER"
               @"--define:NET46_OR_GREATER"
               @"--define:NET461_OR_GREATER"
               @"--define:NET462_OR_GREATER"
               @"--define:NET47_OR_GREATER"
               @"--define:NET471_OR_GREATER"
               @"--define:NET472_OR_GREATER"
               @"--doc:C:\Users\nojaf\Projects\fsharp\artifacts\obj\Project264\Debug\net472\Project264.xml"
               @"--publicsign+"
               @"--optimize-"
               @"--tailcalls-"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\mscorlib.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Core.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Data.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Drawing.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.IO.Compression.FileSystem.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Numerics.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Runtime.Serialization.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Xml.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Xml.Linq.dll"
               @"--target:library"
               @"--nowarn:FS2003,NU5105"
               @"--warn:3"
               @"--warnaserror:3239,1182,0025"
               @"--fullpaths"
               @"--flaterrors"
               @"--subsystemversion:6.00"
               @"--highentropyva+"
               @"--targetprofile:mscorlib"
               @"--nocopyfsharpcore"
               @"--deterministic+"
               @"--simpleresolution"
               @"--nowarn:3384" |]
          ReferencedProjects = [||]
          IsIncompleteTypeCheckEnvironment = false
          UseScriptResolutionRules = false
          LoadTime = DateTime.Now
          UnresolvedReferences = None
          OriginalLoadReferences = []
          Stamp = None }

    FSharpReferencedProject.CreateFSharp(
        @"C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project264\Debug\net472\Project264.dll",
        projectOptions
    )

let memoProject264 = memoization Project264

let Project206 _ =
    let projectOptions =
        { ProjectFileName = @"C:\Users\nojaf\Projects\fsharp\tests\service\data\LargeApp\Project206\Project206.fsproj"
          ProjectId = None
          SourceFiles =
            [| @"C:\Users\nojaf\Projects\fsharp\artifacts\obj\Project206\Debug\net472\Project206.AssemblyInfo.fs"
               @"C:\Users\nojaf\Projects\fsharp\tests\service\data\LargeApp\Project206\MyModule.fs" |]
          OtherOptions =
            [| @"-o:C:\Users\nojaf\Projects\fsharp\artifacts\obj\Project206\Debug\net472\Project206.dll"
               @"-g"
               @"--debug:portable"
               @"--noframework"
               @"--define:TRACE"
               @"--define:DEBUG"
               @"--define:ENABLE_MONO_SUPPORT"
               @"--define:NETFRAMEWORK"
               @"--define:NET472"
               @"--define:NET20_OR_GREATER"
               @"--define:NET30_OR_GREATER"
               @"--define:NET35_OR_GREATER"
               @"--define:NET40_OR_GREATER"
               @"--define:NET45_OR_GREATER"
               @"--define:NET451_OR_GREATER"
               @"--define:NET452_OR_GREATER"
               @"--define:NET46_OR_GREATER"
               @"--define:NET461_OR_GREATER"
               @"--define:NET462_OR_GREATER"
               @"--define:NET47_OR_GREATER"
               @"--define:NET471_OR_GREATER"
               @"--define:NET472_OR_GREATER"
               @"--doc:C:\Users\nojaf\Projects\fsharp\artifacts\obj\Project206\Debug\net472\Project206.xml"
               @"--publicsign+"
               @"--optimize-"
               @"--tailcalls-"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\mscorlib.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project144\Debug\net472\Project144.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Core.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Data.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Drawing.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.IO.Compression.FileSystem.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Numerics.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Runtime.Serialization.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Xml.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Xml.Linq.dll"
               @"--target:library"
               @"--nowarn:FS2003,NU5105"
               @"--warn:3"
               @"--warnaserror:3239,1182,0025"
               @"--fullpaths"
               @"--flaterrors"
               @"--subsystemversion:6.00"
               @"--highentropyva+"
               @"--targetprofile:mscorlib"
               @"--nocopyfsharpcore"
               @"--deterministic+"
               @"--simpleresolution"
               @"--nowarn:3384" |]
          ReferencedProjects = [||]
          IsIncompleteTypeCheckEnvironment = false
          UseScriptResolutionRules = false
          LoadTime = DateTime.Now
          UnresolvedReferences = None
          OriginalLoadReferences = []
          Stamp = None }

    FSharpReferencedProject.CreateFSharp(
        @"C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project206\Debug\net472\Project206.dll",
        projectOptions
    )

let memoProject206 = memoization Project206

let Project310 _ =
    let projectOptions =
        { ProjectFileName = @"C:\Users\nojaf\Projects\fsharp\tests\service\data\LargeApp\Project310\Project310.fsproj"
          ProjectId = None
          SourceFiles =
            [| @"C:\Users\nojaf\Projects\fsharp\artifacts\obj\Project310\Debug\net472\Project310.AssemblyInfo.fs"
               @"C:\Users\nojaf\Projects\fsharp\tests\service\data\LargeApp\Project310\MyModule.fs" |]
          OtherOptions =
            [| @"-o:C:\Users\nojaf\Projects\fsharp\artifacts\obj\Project310\Debug\net472\Project310.dll"
               @"-g"
               @"--debug:portable"
               @"--noframework"
               @"--define:TRACE"
               @"--define:DEBUG"
               @"--define:ENABLE_MONO_SUPPORT"
               @"--define:NETFRAMEWORK"
               @"--define:NET472"
               @"--define:NET20_OR_GREATER"
               @"--define:NET30_OR_GREATER"
               @"--define:NET35_OR_GREATER"
               @"--define:NET40_OR_GREATER"
               @"--define:NET45_OR_GREATER"
               @"--define:NET451_OR_GREATER"
               @"--define:NET452_OR_GREATER"
               @"--define:NET46_OR_GREATER"
               @"--define:NET461_OR_GREATER"
               @"--define:NET462_OR_GREATER"
               @"--define:NET47_OR_GREATER"
               @"--define:NET471_OR_GREATER"
               @"--define:NET472_OR_GREATER"
               @"--doc:C:\Users\nojaf\Projects\fsharp\artifacts\obj\Project310\Debug\net472\Project310.xml"
               @"--publicsign+"
               @"--optimize-"
               @"--tailcalls-"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\mscorlib.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project264\Debug\net472\Project264.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Core.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Data.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Drawing.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.IO.Compression.FileSystem.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Numerics.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Runtime.Serialization.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Xml.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Xml.Linq.dll"
               @"--target:library"
               @"--nowarn:FS2003,NU5105"
               @"--warn:3"
               @"--warnaserror:3239,1182,0025"
               @"--fullpaths"
               @"--flaterrors"
               @"--subsystemversion:6.00"
               @"--highentropyva+"
               @"--targetprofile:mscorlib"
               @"--nocopyfsharpcore"
               @"--deterministic+"
               @"--simpleresolution"
               @"--nowarn:3384" |]
          ReferencedProjects = [| memoProject264 "Project264" |]
          IsIncompleteTypeCheckEnvironment = false
          UseScriptResolutionRules = false
          LoadTime = DateTime.Now
          UnresolvedReferences = None
          OriginalLoadReferences = []
          Stamp = None }

    FSharpReferencedProject.CreateFSharp(
        @"C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project310\Debug\net472\Project310.dll",
        projectOptions
    )

let memoProject310 = memoization Project310

let Project14 _ =
    let projectOptions =
        { ProjectFileName = @"C:\Users\nojaf\Projects\fsharp\tests\service\data\LargeApp\Project14\Project14.fsproj"
          ProjectId = None
          SourceFiles =
            [| @"C:\Users\nojaf\Projects\fsharp\artifacts\obj\Project14\Debug\net472\Project14.AssemblyInfo.fs"
               @"C:\Users\nojaf\Projects\fsharp\tests\service\data\LargeApp\Project14\MyModule.fs" |]
          OtherOptions =
            [| @"-o:C:\Users\nojaf\Projects\fsharp\artifacts\obj\Project14\Debug\net472\Project14.dll"
               @"-g"
               @"--debug:portable"
               @"--noframework"
               @"--define:TRACE"
               @"--define:DEBUG"
               @"--define:ENABLE_MONO_SUPPORT"
               @"--define:NETFRAMEWORK"
               @"--define:NET472"
               @"--define:NET20_OR_GREATER"
               @"--define:NET30_OR_GREATER"
               @"--define:NET35_OR_GREATER"
               @"--define:NET40_OR_GREATER"
               @"--define:NET45_OR_GREATER"
               @"--define:NET451_OR_GREATER"
               @"--define:NET452_OR_GREATER"
               @"--define:NET46_OR_GREATER"
               @"--define:NET461_OR_GREATER"
               @"--define:NET462_OR_GREATER"
               @"--define:NET47_OR_GREATER"
               @"--define:NET471_OR_GREATER"
               @"--define:NET472_OR_GREATER"
               @"--doc:C:\Users\nojaf\Projects\fsharp\artifacts\obj\Project14\Debug\net472\Project14.xml"
               @"--publicsign+"
               @"--optimize-"
               @"--tailcalls-"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\mscorlib.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project138\Debug\net472\Project138.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project140\Debug\net472\Project140.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project144\Debug\net472\Project144.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project146\Debug\net472\Project146.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project151\Debug\net472\Project151.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project164\Debug\net472\Project164.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project22\Debug\net472\Project22.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project243\Debug\net472\Project243.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project272\Debug\net472\Project272.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project289\Debug\net472\Project289.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project295\Debug\net472\Project295.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project298\Debug\net472\Project298.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Core.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Data.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Drawing.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.IO.Compression.FileSystem.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Numerics.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Runtime.Serialization.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Xml.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Xml.Linq.dll"
               @"--target:library"
               @"--nowarn:FS2003,NU5105"
               @"--warn:3"
               @"--warnaserror:3239,1182,0025"
               @"--fullpaths"
               @"--flaterrors"
               @"--subsystemversion:6.00"
               @"--highentropyva+"
               @"--targetprofile:mscorlib"
               @"--nocopyfsharpcore"
               @"--deterministic+"
               @"--simpleresolution"
               @"--nowarn:3384" |]
          ReferencedProjects =
            [| memoProject151 "Project151"
               memoProject22 "Project22"
               memoProject243 "Project243"
               memoProject272 "Project272"
               memoProject289 "Project289" |]
          IsIncompleteTypeCheckEnvironment = false
          UseScriptResolutionRules = false
          LoadTime = DateTime.Now
          UnresolvedReferences = None
          OriginalLoadReferences = []
          Stamp = None }

    FSharpReferencedProject.CreateFSharp(
        @"C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project14\Debug\net472\Project14.dll",
        projectOptions
    )

let memoProject14 = memoization Project14

let Project151 _ =
    let projectOptions =
        { ProjectFileName = @"C:\Users\nojaf\Projects\fsharp\tests\service\data\LargeApp\Project151\Project151.fsproj"
          ProjectId = None
          SourceFiles =
            [| @"C:\Users\nojaf\Projects\fsharp\artifacts\obj\Project151\Debug\net472\Project151.AssemblyInfo.fs"
               @"C:\Users\nojaf\Projects\fsharp\tests\service\data\LargeApp\Project151\MyModule.fs" |]
          OtherOptions =
            [| @"-o:C:\Users\nojaf\Projects\fsharp\artifacts\obj\Project151\Debug\net472\Project151.dll"
               @"-g"
               @"--debug:portable"
               @"--noframework"
               @"--define:TRACE"
               @"--define:DEBUG"
               @"--define:ENABLE_MONO_SUPPORT"
               @"--define:NETFRAMEWORK"
               @"--define:NET472"
               @"--define:NET20_OR_GREATER"
               @"--define:NET30_OR_GREATER"
               @"--define:NET35_OR_GREATER"
               @"--define:NET40_OR_GREATER"
               @"--define:NET45_OR_GREATER"
               @"--define:NET451_OR_GREATER"
               @"--define:NET452_OR_GREATER"
               @"--define:NET46_OR_GREATER"
               @"--define:NET461_OR_GREATER"
               @"--define:NET462_OR_GREATER"
               @"--define:NET47_OR_GREATER"
               @"--define:NET471_OR_GREATER"
               @"--define:NET472_OR_GREATER"
               @"--doc:C:\Users\nojaf\Projects\fsharp\artifacts\obj\Project151\Debug\net472\Project151.xml"
               @"--publicsign+"
               @"--optimize-"
               @"--tailcalls-"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\mscorlib.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project222\Debug\net472\Project222.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project272\Debug\net472\Project272.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Core.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Data.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Drawing.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.IO.Compression.FileSystem.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Numerics.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Runtime.Serialization.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Xml.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Xml.Linq.dll"
               @"--target:library"
               @"--nowarn:FS2003,NU5105"
               @"--warn:3"
               @"--warnaserror:3239,1182,0025"
               @"--fullpaths"
               @"--flaterrors"
               @"--subsystemversion:6.00"
               @"--highentropyva+"
               @"--targetprofile:mscorlib"
               @"--nocopyfsharpcore"
               @"--deterministic+"
               @"--simpleresolution"
               @"--nowarn:3384" |]
          ReferencedProjects =
            [| memoProject222 "Project222"
               memoProject272 "Project272" |]
          IsIncompleteTypeCheckEnvironment = false
          UseScriptResolutionRules = false
          LoadTime = DateTime.Now
          UnresolvedReferences = None
          OriginalLoadReferences = []
          Stamp = None }

    FSharpReferencedProject.CreateFSharp(
        @"C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project151\Debug\net472\Project151.dll",
        projectOptions
    )

let memoProject151 = memoization Project151

let Project222 _ =
    let projectOptions =
        { ProjectFileName = @"C:\Users\nojaf\Projects\fsharp\tests\service\data\LargeApp\Project222\Project222.fsproj"
          ProjectId = None
          SourceFiles =
            [| @"C:\Users\nojaf\Projects\fsharp\artifacts\obj\Project222\Debug\net472\Project222.AssemblyInfo.fs"
               @"C:\Users\nojaf\Projects\fsharp\tests\service\data\LargeApp\Project222\MyModule.fs" |]
          OtherOptions =
            [| @"-o:C:\Users\nojaf\Projects\fsharp\artifacts\obj\Project222\Debug\net472\Project222.dll"
               @"-g"
               @"--debug:portable"
               @"--noframework"
               @"--define:TRACE"
               @"--define:DEBUG"
               @"--define:ENABLE_MONO_SUPPORT"
               @"--define:NETFRAMEWORK"
               @"--define:NET472"
               @"--define:NET20_OR_GREATER"
               @"--define:NET30_OR_GREATER"
               @"--define:NET35_OR_GREATER"
               @"--define:NET40_OR_GREATER"
               @"--define:NET45_OR_GREATER"
               @"--define:NET451_OR_GREATER"
               @"--define:NET452_OR_GREATER"
               @"--define:NET46_OR_GREATER"
               @"--define:NET461_OR_GREATER"
               @"--define:NET462_OR_GREATER"
               @"--define:NET47_OR_GREATER"
               @"--define:NET471_OR_GREATER"
               @"--define:NET472_OR_GREATER"
               @"--doc:C:\Users\nojaf\Projects\fsharp\artifacts\obj\Project222\Debug\net472\Project222.xml"
               @"--publicsign+"
               @"--optimize-"
               @"--tailcalls-"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\mscorlib.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project193\Debug\net472\Project193.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project224\Debug\net472\Project224.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project273\Debug\net472\Project273.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Core.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Data.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Drawing.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.IO.Compression.FileSystem.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Numerics.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Runtime.Serialization.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Xml.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Xml.Linq.dll"
               @"--target:library"
               @"--nowarn:FS2003,NU5105"
               @"--warn:3"
               @"--warnaserror:3239,1182,0025"
               @"--fullpaths"
               @"--flaterrors"
               @"--subsystemversion:6.00"
               @"--highentropyva+"
               @"--targetprofile:mscorlib"
               @"--nocopyfsharpcore"
               @"--deterministic+"
               @"--simpleresolution"
               @"--nowarn:3384" |]
          ReferencedProjects = [| memoProject273 "Project273" |]
          IsIncompleteTypeCheckEnvironment = false
          UseScriptResolutionRules = false
          LoadTime = DateTime.Now
          UnresolvedReferences = None
          OriginalLoadReferences = []
          Stamp = None }

    FSharpReferencedProject.CreateFSharp(
        @"C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project222\Debug\net472\Project222.dll",
        projectOptions
    )

let memoProject222 = memoization Project222

let Project273 _ =
    let projectOptions =
        { ProjectFileName = @"C:\Users\nojaf\Projects\fsharp\tests\service\data\LargeApp\Project273\Project273.fsproj"
          ProjectId = None
          SourceFiles =
            [| @"C:\Users\nojaf\Projects\fsharp\artifacts\obj\Project273\Debug\net472\Project273.AssemblyInfo.fs"
               @"C:\Users\nojaf\Projects\fsharp\tests\service\data\LargeApp\Project273\MyModule.fs" |]
          OtherOptions =
            [| @"-o:C:\Users\nojaf\Projects\fsharp\artifacts\obj\Project273\Debug\net472\Project273.dll"
               @"-g"
               @"--debug:portable"
               @"--noframework"
               @"--define:TRACE"
               @"--define:DEBUG"
               @"--define:ENABLE_MONO_SUPPORT"
               @"--define:NETFRAMEWORK"
               @"--define:NET472"
               @"--define:NET20_OR_GREATER"
               @"--define:NET30_OR_GREATER"
               @"--define:NET35_OR_GREATER"
               @"--define:NET40_OR_GREATER"
               @"--define:NET45_OR_GREATER"
               @"--define:NET451_OR_GREATER"
               @"--define:NET452_OR_GREATER"
               @"--define:NET46_OR_GREATER"
               @"--define:NET461_OR_GREATER"
               @"--define:NET462_OR_GREATER"
               @"--define:NET47_OR_GREATER"
               @"--define:NET471_OR_GREATER"
               @"--define:NET472_OR_GREATER"
               @"--doc:C:\Users\nojaf\Projects\fsharp\artifacts\obj\Project273\Debug\net472\Project273.xml"
               @"--publicsign+"
               @"--optimize-"
               @"--tailcalls-"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\mscorlib.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project138\Debug\net472\Project138.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project164\Debug\net472\Project164.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project209\Debug\net472\Project209.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Core.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Data.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Drawing.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.IO.Compression.FileSystem.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Numerics.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Runtime.Serialization.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Xml.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Xml.Linq.dll"
               @"--target:library"
               @"--nowarn:FS2003,NU5105"
               @"--warn:3"
               @"--warnaserror:3239,1182,0025"
               @"--fullpaths"
               @"--flaterrors"
               @"--subsystemversion:6.00"
               @"--highentropyva+"
               @"--targetprofile:mscorlib"
               @"--nocopyfsharpcore"
               @"--deterministic+"
               @"--simpleresolution"
               @"--nowarn:3384" |]
          ReferencedProjects = [||]
          IsIncompleteTypeCheckEnvironment = false
          UseScriptResolutionRules = false
          LoadTime = DateTime.Now
          UnresolvedReferences = None
          OriginalLoadReferences = []
          Stamp = None }

    FSharpReferencedProject.CreateFSharp(
        @"C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project273\Debug\net472\Project273.dll",
        projectOptions
    )

let memoProject273 = memoization Project273

let Project272 _ =
    let projectOptions =
        { ProjectFileName = @"C:\Users\nojaf\Projects\fsharp\tests\service\data\LargeApp\Project272\Project272.fsproj"
          ProjectId = None
          SourceFiles =
            [| @"C:\Users\nojaf\Projects\fsharp\artifacts\obj\Project272\Debug\net472\Project272.AssemblyInfo.fs"
               @"C:\Users\nojaf\Projects\fsharp\tests\service\data\LargeApp\Project272\MyModule.fs" |]
          OtherOptions =
            [| @"-o:C:\Users\nojaf\Projects\fsharp\artifacts\obj\Project272\Debug\net472\Project272.dll"
               @"-g"
               @"--debug:portable"
               @"--noframework"
               @"--define:TRACE"
               @"--define:DEBUG"
               @"--define:ENABLE_MONO_SUPPORT"
               @"--define:NETFRAMEWORK"
               @"--define:NET472"
               @"--define:NET20_OR_GREATER"
               @"--define:NET30_OR_GREATER"
               @"--define:NET35_OR_GREATER"
               @"--define:NET40_OR_GREATER"
               @"--define:NET45_OR_GREATER"
               @"--define:NET451_OR_GREATER"
               @"--define:NET452_OR_GREATER"
               @"--define:NET46_OR_GREATER"
               @"--define:NET461_OR_GREATER"
               @"--define:NET462_OR_GREATER"
               @"--define:NET47_OR_GREATER"
               @"--define:NET471_OR_GREATER"
               @"--define:NET472_OR_GREATER"
               @"--doc:C:\Users\nojaf\Projects\fsharp\artifacts\obj\Project272\Debug\net472\Project272.xml"
               @"--publicsign+"
               @"--optimize-"
               @"--tailcalls-"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\mscorlib.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project146\Debug\net472\Project146.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Core.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Data.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Drawing.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.IO.Compression.FileSystem.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Numerics.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Runtime.Serialization.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Xml.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Xml.Linq.dll"
               @"--target:library"
               @"--nowarn:FS2003,NU5105"
               @"--warn:3"
               @"--warnaserror:3239,1182,0025"
               @"--fullpaths"
               @"--flaterrors"
               @"--subsystemversion:6.00"
               @"--highentropyva+"
               @"--targetprofile:mscorlib"
               @"--nocopyfsharpcore"
               @"--deterministic+"
               @"--simpleresolution"
               @"--nowarn:3384" |]
          ReferencedProjects = [||]
          IsIncompleteTypeCheckEnvironment = false
          UseScriptResolutionRules = false
          LoadTime = DateTime.Now
          UnresolvedReferences = None
          OriginalLoadReferences = []
          Stamp = None }

    FSharpReferencedProject.CreateFSharp(
        @"C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project272\Debug\net472\Project272.dll",
        projectOptions
    )

let memoProject272 = memoization Project272

let Project22 _ =
    let projectOptions =
        { ProjectFileName = @"C:\Users\nojaf\Projects\fsharp\tests\service\data\LargeApp\Project22\Project22.fsproj"
          ProjectId = None
          SourceFiles =
            [| @"C:\Users\nojaf\Projects\fsharp\artifacts\obj\Project22\Debug\net472\Project22.AssemblyInfo.fs"
               @"C:\Users\nojaf\Projects\fsharp\tests\service\data\LargeApp\Project22\MyModule.fs" |]
          OtherOptions =
            [| @"-o:C:\Users\nojaf\Projects\fsharp\artifacts\obj\Project22\Debug\net472\Project22.dll"
               @"-g"
               @"--debug:portable"
               @"--noframework"
               @"--define:TRACE"
               @"--define:DEBUG"
               @"--define:ENABLE_MONO_SUPPORT"
               @"--define:NETFRAMEWORK"
               @"--define:NET472"
               @"--define:NET20_OR_GREATER"
               @"--define:NET30_OR_GREATER"
               @"--define:NET35_OR_GREATER"
               @"--define:NET40_OR_GREATER"
               @"--define:NET45_OR_GREATER"
               @"--define:NET451_OR_GREATER"
               @"--define:NET452_OR_GREATER"
               @"--define:NET46_OR_GREATER"
               @"--define:NET461_OR_GREATER"
               @"--define:NET462_OR_GREATER"
               @"--define:NET47_OR_GREATER"
               @"--define:NET471_OR_GREATER"
               @"--define:NET472_OR_GREATER"
               @"--doc:C:\Users\nojaf\Projects\fsharp\artifacts\obj\Project22\Debug\net472\Project22.xml"
               @"--publicsign+"
               @"--optimize-"
               @"--tailcalls-"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\mscorlib.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project138\Debug\net472\Project138.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project140\Debug\net472\Project140.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project142\Debug\net472\Project142.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project144\Debug\net472\Project144.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project146\Debug\net472\Project146.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project149\Debug\net472\Project149.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project151\Debug\net472\Project151.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project161\Debug\net472\Project161.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project164\Debug\net472\Project164.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project172\Debug\net472\Project172.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project189\Debug\net472\Project189.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project192\Debug\net472\Project192.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project213\Debug\net472\Project213.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project214\Debug\net472\Project214.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project243\Debug\net472\Project243.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project250\Debug\net472\Project250.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project262\Debug\net472\Project262.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project264\Debug\net472\Project264.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project272\Debug\net472\Project272.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project298\Debug\net472\Project298.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project323\Debug\net472\Project323.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Core.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Data.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Drawing.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.IO.Compression.FileSystem.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Numerics.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Runtime.Serialization.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Xml.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Xml.Linq.dll"
               @"--target:library"
               @"--nowarn:FS2003,NU5105"
               @"--warn:3"
               @"--warnaserror:3239,1182,0025"
               @"--fullpaths"
               @"--flaterrors"
               @"--subsystemversion:6.00"
               @"--highentropyva+"
               @"--targetprofile:mscorlib"
               @"--nocopyfsharpcore"
               @"--deterministic+"
               @"--simpleresolution"
               @"--nowarn:3384" |]
          ReferencedProjects =
            [| memoProject151 "Project151"
               memoProject213 "Project213"
               memoProject243 "Project243"
               memoProject250 "Project250"
               memoProject262 "Project262"
               memoProject264 "Project264"
               memoProject272 "Project272" |]
          IsIncompleteTypeCheckEnvironment = false
          UseScriptResolutionRules = false
          LoadTime = DateTime.Now
          UnresolvedReferences = None
          OriginalLoadReferences = []
          Stamp = None }

    FSharpReferencedProject.CreateFSharp(
        @"C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project22\Debug\net472\Project22.dll",
        projectOptions
    )

let memoProject22 = memoization Project22

let Project213 _ =
    let projectOptions =
        { ProjectFileName = @"C:\Users\nojaf\Projects\fsharp\tests\service\data\LargeApp\Project213\Project213.fsproj"
          ProjectId = None
          SourceFiles =
            [| @"C:\Users\nojaf\Projects\fsharp\artifacts\obj\Project213\Debug\net472\Project213.AssemblyInfo.fs"
               @"C:\Users\nojaf\Projects\fsharp\tests\service\data\LargeApp\Project213\MyModule.fs" |]
          OtherOptions =
            [| @"-o:C:\Users\nojaf\Projects\fsharp\artifacts\obj\Project213\Debug\net472\Project213.dll"
               @"-g"
               @"--debug:portable"
               @"--noframework"
               @"--define:TRACE"
               @"--define:DEBUG"
               @"--define:ENABLE_MONO_SUPPORT"
               @"--define:NETFRAMEWORK"
               @"--define:NET472"
               @"--define:NET20_OR_GREATER"
               @"--define:NET30_OR_GREATER"
               @"--define:NET35_OR_GREATER"
               @"--define:NET40_OR_GREATER"
               @"--define:NET45_OR_GREATER"
               @"--define:NET451_OR_GREATER"
               @"--define:NET452_OR_GREATER"
               @"--define:NET46_OR_GREATER"
               @"--define:NET461_OR_GREATER"
               @"--define:NET462_OR_GREATER"
               @"--define:NET47_OR_GREATER"
               @"--define:NET471_OR_GREATER"
               @"--define:NET472_OR_GREATER"
               @"--doc:C:\Users\nojaf\Projects\fsharp\artifacts\obj\Project213\Debug\net472\Project213.xml"
               @"--publicsign+"
               @"--optimize-"
               @"--tailcalls-"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\mscorlib.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project243\Debug\net472\Project243.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Core.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Data.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Drawing.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.IO.Compression.FileSystem.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Numerics.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Runtime.Serialization.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Xml.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Xml.Linq.dll"
               @"--target:library"
               @"--nowarn:FS2003,NU5105"
               @"--warn:3"
               @"--warnaserror:3239,1182,0025"
               @"--fullpaths"
               @"--flaterrors"
               @"--subsystemversion:6.00"
               @"--highentropyva+"
               @"--targetprofile:mscorlib"
               @"--nocopyfsharpcore"
               @"--deterministic+"
               @"--simpleresolution"
               @"--nowarn:3384" |]
          ReferencedProjects = [| memoProject243 "Project243" |]
          IsIncompleteTypeCheckEnvironment = false
          UseScriptResolutionRules = false
          LoadTime = DateTime.Now
          UnresolvedReferences = None
          OriginalLoadReferences = []
          Stamp = None }

    FSharpReferencedProject.CreateFSharp(
        @"C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project213\Debug\net472\Project213.dll",
        projectOptions
    )

let memoProject213 = memoization Project213

let Project243 _ =
    let projectOptions =
        { ProjectFileName = @"C:\Users\nojaf\Projects\fsharp\tests\service\data\LargeApp\Project243\Project243.fsproj"
          ProjectId = None
          SourceFiles =
            [| @"C:\Users\nojaf\Projects\fsharp\artifacts\obj\Project243\Debug\net472\Project243.AssemblyInfo.fs"
               @"C:\Users\nojaf\Projects\fsharp\tests\service\data\LargeApp\Project243\MyModule.fs" |]
          OtherOptions =
            [| @"-o:C:\Users\nojaf\Projects\fsharp\artifacts\obj\Project243\Debug\net472\Project243.dll"
               @"-g"
               @"--debug:portable"
               @"--noframework"
               @"--define:TRACE"
               @"--define:DEBUG"
               @"--define:ENABLE_MONO_SUPPORT"
               @"--define:NETFRAMEWORK"
               @"--define:NET472"
               @"--define:NET20_OR_GREATER"
               @"--define:NET30_OR_GREATER"
               @"--define:NET35_OR_GREATER"
               @"--define:NET40_OR_GREATER"
               @"--define:NET45_OR_GREATER"
               @"--define:NET451_OR_GREATER"
               @"--define:NET452_OR_GREATER"
               @"--define:NET46_OR_GREATER"
               @"--define:NET461_OR_GREATER"
               @"--define:NET462_OR_GREATER"
               @"--define:NET47_OR_GREATER"
               @"--define:NET471_OR_GREATER"
               @"--define:NET472_OR_GREATER"
               @"--doc:C:\Users\nojaf\Projects\fsharp\artifacts\obj\Project243\Debug\net472\Project243.xml"
               @"--publicsign+"
               @"--optimize-"
               @"--tailcalls-"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\mscorlib.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project236\Debug\net472\Project236.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project311\Debug\net472\Project311.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Core.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Data.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Drawing.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.IO.Compression.FileSystem.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Numerics.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Runtime.Serialization.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Xml.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Xml.Linq.dll"
               @"--target:library"
               @"--nowarn:FS2003,NU5105"
               @"--warn:3"
               @"--warnaserror:3239,1182,0025"
               @"--fullpaths"
               @"--flaterrors"
               @"--subsystemversion:6.00"
               @"--highentropyva+"
               @"--targetprofile:mscorlib"
               @"--nocopyfsharpcore"
               @"--deterministic+"
               @"--simpleresolution"
               @"--nowarn:3384" |]
          ReferencedProjects =
            [| memoProject236 "Project236"
               memoProject311 "Project311" |]
          IsIncompleteTypeCheckEnvironment = false
          UseScriptResolutionRules = false
          LoadTime = DateTime.Now
          UnresolvedReferences = None
          OriginalLoadReferences = []
          Stamp = None }

    FSharpReferencedProject.CreateFSharp(
        @"C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project243\Debug\net472\Project243.dll",
        projectOptions
    )

let memoProject243 = memoization Project243

let Project236 _ =
    let projectOptions =
        { ProjectFileName = @"C:\Users\nojaf\Projects\fsharp\tests\service\data\LargeApp\Project236\Project236.fsproj"
          ProjectId = None
          SourceFiles =
            [| @"C:\Users\nojaf\Projects\fsharp\artifacts\obj\Project236\Debug\net472\Project236.AssemblyInfo.fs"
               @"C:\Users\nojaf\Projects\fsharp\tests\service\data\LargeApp\Project236\MyModule.fs" |]
          OtherOptions =
            [| @"-o:C:\Users\nojaf\Projects\fsharp\artifacts\obj\Project236\Debug\net472\Project236.dll"
               @"-g"
               @"--debug:portable"
               @"--noframework"
               @"--define:TRACE"
               @"--define:DEBUG"
               @"--define:ENABLE_MONO_SUPPORT"
               @"--define:NETFRAMEWORK"
               @"--define:NET472"
               @"--define:NET20_OR_GREATER"
               @"--define:NET30_OR_GREATER"
               @"--define:NET35_OR_GREATER"
               @"--define:NET40_OR_GREATER"
               @"--define:NET45_OR_GREATER"
               @"--define:NET451_OR_GREATER"
               @"--define:NET452_OR_GREATER"
               @"--define:NET46_OR_GREATER"
               @"--define:NET461_OR_GREATER"
               @"--define:NET462_OR_GREATER"
               @"--define:NET47_OR_GREATER"
               @"--define:NET471_OR_GREATER"
               @"--define:NET472_OR_GREATER"
               @"--doc:C:\Users\nojaf\Projects\fsharp\artifacts\obj\Project236\Debug\net472\Project236.xml"
               @"--publicsign+"
               @"--optimize-"
               @"--tailcalls-"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\mscorlib.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project250\Debug\net472\Project250.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Core.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Data.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Drawing.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.IO.Compression.FileSystem.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Numerics.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Runtime.Serialization.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Xml.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Xml.Linq.dll"
               @"--target:library"
               @"--nowarn:FS2003,NU5105"
               @"--warn:3"
               @"--warnaserror:3239,1182,0025"
               @"--fullpaths"
               @"--flaterrors"
               @"--subsystemversion:6.00"
               @"--highentropyva+"
               @"--targetprofile:mscorlib"
               @"--nocopyfsharpcore"
               @"--deterministic+"
               @"--simpleresolution"
               @"--nowarn:3384" |]
          ReferencedProjects = [| memoProject250 "Project250" |]
          IsIncompleteTypeCheckEnvironment = false
          UseScriptResolutionRules = false
          LoadTime = DateTime.Now
          UnresolvedReferences = None
          OriginalLoadReferences = []
          Stamp = None }

    FSharpReferencedProject.CreateFSharp(
        @"C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project236\Debug\net472\Project236.dll",
        projectOptions
    )

let memoProject236 = memoization Project236

let Project250 _ =
    let projectOptions =
        { ProjectFileName = @"C:\Users\nojaf\Projects\fsharp\tests\service\data\LargeApp\Project250\Project250.fsproj"
          ProjectId = None
          SourceFiles =
            [| @"C:\Users\nojaf\Projects\fsharp\artifacts\obj\Project250\Debug\net472\Project250.AssemblyInfo.fs"
               @"C:\Users\nojaf\Projects\fsharp\tests\service\data\LargeApp\Project250\MyModule.fs" |]
          OtherOptions =
            [| @"-o:C:\Users\nojaf\Projects\fsharp\artifacts\obj\Project250\Debug\net472\Project250.dll"
               @"-g"
               @"--debug:portable"
               @"--noframework"
               @"--define:TRACE"
               @"--define:DEBUG"
               @"--define:ENABLE_MONO_SUPPORT"
               @"--define:NETFRAMEWORK"
               @"--define:NET472"
               @"--define:NET20_OR_GREATER"
               @"--define:NET30_OR_GREATER"
               @"--define:NET35_OR_GREATER"
               @"--define:NET40_OR_GREATER"
               @"--define:NET45_OR_GREATER"
               @"--define:NET451_OR_GREATER"
               @"--define:NET452_OR_GREATER"
               @"--define:NET46_OR_GREATER"
               @"--define:NET461_OR_GREATER"
               @"--define:NET462_OR_GREATER"
               @"--define:NET47_OR_GREATER"
               @"--define:NET471_OR_GREATER"
               @"--define:NET472_OR_GREATER"
               @"--doc:C:\Users\nojaf\Projects\fsharp\artifacts\obj\Project250\Debug\net472\Project250.xml"
               @"--publicsign+"
               @"--optimize-"
               @"--tailcalls-"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\mscorlib.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project151\Debug\net472\Project151.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Core.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Data.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Drawing.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.IO.Compression.FileSystem.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Numerics.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Runtime.Serialization.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Xml.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Xml.Linq.dll"
               @"--target:library"
               @"--nowarn:FS2003,NU5105"
               @"--warn:3"
               @"--warnaserror:3239,1182,0025"
               @"--fullpaths"
               @"--flaterrors"
               @"--subsystemversion:6.00"
               @"--highentropyva+"
               @"--targetprofile:mscorlib"
               @"--nocopyfsharpcore"
               @"--deterministic+"
               @"--simpleresolution"
               @"--nowarn:3384" |]
          ReferencedProjects = [| memoProject151 "Project151" |]
          IsIncompleteTypeCheckEnvironment = false
          UseScriptResolutionRules = false
          LoadTime = DateTime.Now
          UnresolvedReferences = None
          OriginalLoadReferences = []
          Stamp = None }

    FSharpReferencedProject.CreateFSharp(
        @"C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project250\Debug\net472\Project250.dll",
        projectOptions
    )

let memoProject250 = memoization Project250

let Project311 _ =
    let projectOptions =
        { ProjectFileName = @"C:\Users\nojaf\Projects\fsharp\tests\service\data\LargeApp\Project311\Project311.fsproj"
          ProjectId = None
          SourceFiles =
            [| @"C:\Users\nojaf\Projects\fsharp\artifacts\obj\Project311\Debug\net472\Project311.AssemblyInfo.fs"
               @"C:\Users\nojaf\Projects\fsharp\tests\service\data\LargeApp\Project311\MyModule.fs" |]
          OtherOptions =
            [| @"-o:C:\Users\nojaf\Projects\fsharp\artifacts\obj\Project311\Debug\net472\Project311.dll"
               @"-g"
               @"--debug:portable"
               @"--noframework"
               @"--define:TRACE"
               @"--define:DEBUG"
               @"--define:ENABLE_MONO_SUPPORT"
               @"--define:NETFRAMEWORK"
               @"--define:NET472"
               @"--define:NET20_OR_GREATER"
               @"--define:NET30_OR_GREATER"
               @"--define:NET35_OR_GREATER"
               @"--define:NET40_OR_GREATER"
               @"--define:NET45_OR_GREATER"
               @"--define:NET451_OR_GREATER"
               @"--define:NET452_OR_GREATER"
               @"--define:NET46_OR_GREATER"
               @"--define:NET461_OR_GREATER"
               @"--define:NET462_OR_GREATER"
               @"--define:NET47_OR_GREATER"
               @"--define:NET471_OR_GREATER"
               @"--define:NET472_OR_GREATER"
               @"--doc:C:\Users\nojaf\Projects\fsharp\artifacts\obj\Project311\Debug\net472\Project311.xml"
               @"--publicsign+"
               @"--optimize-"
               @"--tailcalls-"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\mscorlib.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project250\Debug\net472\Project250.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Core.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Data.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Drawing.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.IO.Compression.FileSystem.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Numerics.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Runtime.Serialization.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Xml.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Xml.Linq.dll"
               @"--target:library"
               @"--nowarn:FS2003,NU5105"
               @"--warn:3"
               @"--warnaserror:3239,1182,0025"
               @"--fullpaths"
               @"--flaterrors"
               @"--subsystemversion:6.00"
               @"--highentropyva+"
               @"--targetprofile:mscorlib"
               @"--nocopyfsharpcore"
               @"--deterministic+"
               @"--simpleresolution"
               @"--nowarn:3384" |]
          ReferencedProjects = [| memoProject250 "Project250" |]
          IsIncompleteTypeCheckEnvironment = false
          UseScriptResolutionRules = false
          LoadTime = DateTime.Now
          UnresolvedReferences = None
          OriginalLoadReferences = []
          Stamp = None }

    FSharpReferencedProject.CreateFSharp(
        @"C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project311\Debug\net472\Project311.dll",
        projectOptions
    )

let memoProject311 = memoization Project311

let Project262 _ =
    let projectOptions =
        { ProjectFileName = @"C:\Users\nojaf\Projects\fsharp\tests\service\data\LargeApp\Project262\Project262.fsproj"
          ProjectId = None
          SourceFiles =
            [| @"C:\Users\nojaf\Projects\fsharp\artifacts\obj\Project262\Debug\net472\Project262.AssemblyInfo.fs"
               @"C:\Users\nojaf\Projects\fsharp\tests\service\data\LargeApp\Project262\MyModule.fs" |]
          OtherOptions =
            [| @"-o:C:\Users\nojaf\Projects\fsharp\artifacts\obj\Project262\Debug\net472\Project262.dll"
               @"-g"
               @"--debug:portable"
               @"--noframework"
               @"--define:TRACE"
               @"--define:DEBUG"
               @"--define:ENABLE_MONO_SUPPORT"
               @"--define:NETFRAMEWORK"
               @"--define:NET472"
               @"--define:NET20_OR_GREATER"
               @"--define:NET30_OR_GREATER"
               @"--define:NET35_OR_GREATER"
               @"--define:NET40_OR_GREATER"
               @"--define:NET45_OR_GREATER"
               @"--define:NET451_OR_GREATER"
               @"--define:NET452_OR_GREATER"
               @"--define:NET46_OR_GREATER"
               @"--define:NET461_OR_GREATER"
               @"--define:NET462_OR_GREATER"
               @"--define:NET47_OR_GREATER"
               @"--define:NET471_OR_GREATER"
               @"--define:NET472_OR_GREATER"
               @"--doc:C:\Users\nojaf\Projects\fsharp\artifacts\obj\Project262\Debug\net472\Project262.xml"
               @"--publicsign+"
               @"--optimize-"
               @"--tailcalls-"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\mscorlib.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project264\Debug\net472\Project264.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Core.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Data.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Drawing.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.IO.Compression.FileSystem.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Numerics.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Runtime.Serialization.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Xml.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Xml.Linq.dll"
               @"--target:library"
               @"--nowarn:FS2003,NU5105"
               @"--warn:3"
               @"--warnaserror:3239,1182,0025"
               @"--fullpaths"
               @"--flaterrors"
               @"--subsystemversion:6.00"
               @"--highentropyva+"
               @"--targetprofile:mscorlib"
               @"--nocopyfsharpcore"
               @"--deterministic+"
               @"--simpleresolution"
               @"--nowarn:3384" |]
          ReferencedProjects = [| memoProject264 "Project264" |]
          IsIncompleteTypeCheckEnvironment = false
          UseScriptResolutionRules = false
          LoadTime = DateTime.Now
          UnresolvedReferences = None
          OriginalLoadReferences = []
          Stamp = None }

    FSharpReferencedProject.CreateFSharp(
        @"C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project262\Debug\net472\Project262.dll",
        projectOptions
    )

let memoProject262 = memoization Project262

let Project289 _ =
    let projectOptions =
        { ProjectFileName = @"C:\Users\nojaf\Projects\fsharp\tests\service\data\LargeApp\Project289\Project289.fsproj"
          ProjectId = None
          SourceFiles =
            [| @"C:\Users\nojaf\Projects\fsharp\artifacts\obj\Project289\Debug\net472\Project289.AssemblyInfo.fs"
               @"C:\Users\nojaf\Projects\fsharp\tests\service\data\LargeApp\Project289\MyModule.fs" |]
          OtherOptions =
            [| @"-o:C:\Users\nojaf\Projects\fsharp\artifacts\obj\Project289\Debug\net472\Project289.dll"
               @"-g"
               @"--debug:portable"
               @"--noframework"
               @"--define:TRACE"
               @"--define:DEBUG"
               @"--define:ENABLE_MONO_SUPPORT"
               @"--define:NETFRAMEWORK"
               @"--define:NET472"
               @"--define:NET20_OR_GREATER"
               @"--define:NET30_OR_GREATER"
               @"--define:NET35_OR_GREATER"
               @"--define:NET40_OR_GREATER"
               @"--define:NET45_OR_GREATER"
               @"--define:NET451_OR_GREATER"
               @"--define:NET452_OR_GREATER"
               @"--define:NET46_OR_GREATER"
               @"--define:NET461_OR_GREATER"
               @"--define:NET462_OR_GREATER"
               @"--define:NET47_OR_GREATER"
               @"--define:NET471_OR_GREATER"
               @"--define:NET472_OR_GREATER"
               @"--doc:C:\Users\nojaf\Projects\fsharp\artifacts\obj\Project289\Debug\net472\Project289.xml"
               @"--publicsign+"
               @"--optimize-"
               @"--tailcalls-"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\mscorlib.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project138\Debug\net472\Project138.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project140\Debug\net472\Project140.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project144\Debug\net472\Project144.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project146\Debug\net472\Project146.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project151\Debug\net472\Project151.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project164\Debug\net472\Project164.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project192\Debug\net472\Project192.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project243\Debug\net472\Project243.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project272\Debug\net472\Project272.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Core.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Data.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Drawing.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.IO.Compression.FileSystem.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Numerics.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Runtime.Serialization.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Xml.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Xml.Linq.dll"
               @"--target:library"
               @"--nowarn:FS2003,NU5105"
               @"--warn:3"
               @"--warnaserror:3239,1182,0025"
               @"--fullpaths"
               @"--flaterrors"
               @"--subsystemversion:6.00"
               @"--highentropyva+"
               @"--targetprofile:mscorlib"
               @"--nocopyfsharpcore"
               @"--deterministic+"
               @"--simpleresolution"
               @"--nowarn:3384" |]
          ReferencedProjects =
            [| memoProject151 "Project151"
               memoProject243 "Project243"
               memoProject272 "Project272" |]
          IsIncompleteTypeCheckEnvironment = false
          UseScriptResolutionRules = false
          LoadTime = DateTime.Now
          UnresolvedReferences = None
          OriginalLoadReferences = []
          Stamp = None }

    FSharpReferencedProject.CreateFSharp(
        @"C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project289\Debug\net472\Project289.dll",
        projectOptions
    )

let memoProject289 = memoization Project289

let Project265 _ =
    let projectOptions =
        { ProjectFileName = @"C:\Users\nojaf\Projects\fsharp\tests\service\data\LargeApp\Project265\Project265.fsproj"
          ProjectId = None
          SourceFiles =
            [| @"C:\Users\nojaf\Projects\fsharp\artifacts\obj\Project265\Debug\net472\Project265.AssemblyInfo.fs"
               @"C:\Users\nojaf\Projects\fsharp\tests\service\data\LargeApp\Project265\MyModule.fs" |]
          OtherOptions =
            [| @"-o:C:\Users\nojaf\Projects\fsharp\artifacts\obj\Project265\Debug\net472\Project265.dll"
               @"-g"
               @"--debug:portable"
               @"--noframework"
               @"--define:TRACE"
               @"--define:DEBUG"
               @"--define:ENABLE_MONO_SUPPORT"
               @"--define:NETFRAMEWORK"
               @"--define:NET472"
               @"--define:NET20_OR_GREATER"
               @"--define:NET30_OR_GREATER"
               @"--define:NET35_OR_GREATER"
               @"--define:NET40_OR_GREATER"
               @"--define:NET45_OR_GREATER"
               @"--define:NET451_OR_GREATER"
               @"--define:NET452_OR_GREATER"
               @"--define:NET46_OR_GREATER"
               @"--define:NET461_OR_GREATER"
               @"--define:NET462_OR_GREATER"
               @"--define:NET47_OR_GREATER"
               @"--define:NET471_OR_GREATER"
               @"--define:NET472_OR_GREATER"
               @"--doc:C:\Users\nojaf\Projects\fsharp\artifacts\obj\Project265\Debug\net472\Project265.xml"
               @"--publicsign+"
               @"--optimize-"
               @"--tailcalls-"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\mscorlib.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Core.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Data.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Drawing.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.IO.Compression.FileSystem.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Numerics.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Runtime.Serialization.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Xml.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Xml.Linq.dll"
               @"--target:library"
               @"--nowarn:FS2003,NU5105"
               @"--warn:3"
               @"--warnaserror:3239,1182,0025"
               @"--fullpaths"
               @"--flaterrors"
               @"--subsystemversion:6.00"
               @"--highentropyva+"
               @"--targetprofile:mscorlib"
               @"--nocopyfsharpcore"
               @"--deterministic+"
               @"--simpleresolution"
               @"--nowarn:3384" |]
          ReferencedProjects = [||]
          IsIncompleteTypeCheckEnvironment = false
          UseScriptResolutionRules = false
          LoadTime = DateTime.Now
          UnresolvedReferences = None
          OriginalLoadReferences = []
          Stamp = None }

    FSharpReferencedProject.CreateFSharp(
        @"C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project265\Debug\net472\Project265.dll",
        projectOptions
    )

let memoProject265 = memoization Project265

let Project300 _ =
    let projectOptions =
        { ProjectFileName = @"C:\Users\nojaf\Projects\fsharp\tests\service\data\LargeApp\Project300\Project300.fsproj"
          ProjectId = None
          SourceFiles =
            [| @"C:\Users\nojaf\Projects\fsharp\artifacts\obj\Project300\Debug\net472\Project300.AssemblyInfo.fs"
               @"C:\Users\nojaf\Projects\fsharp\tests\service\data\LargeApp\Project300\MyModule.fs" |]
          OtherOptions =
            [| @"-o:C:\Users\nojaf\Projects\fsharp\artifacts\obj\Project300\Debug\net472\Project300.dll"
               @"-g"
               @"--debug:portable"
               @"--noframework"
               @"--define:TRACE"
               @"--define:DEBUG"
               @"--define:ENABLE_MONO_SUPPORT"
               @"--define:NETFRAMEWORK"
               @"--define:NET472"
               @"--define:NET20_OR_GREATER"
               @"--define:NET30_OR_GREATER"
               @"--define:NET35_OR_GREATER"
               @"--define:NET40_OR_GREATER"
               @"--define:NET45_OR_GREATER"
               @"--define:NET451_OR_GREATER"
               @"--define:NET452_OR_GREATER"
               @"--define:NET46_OR_GREATER"
               @"--define:NET461_OR_GREATER"
               @"--define:NET462_OR_GREATER"
               @"--define:NET47_OR_GREATER"
               @"--define:NET471_OR_GREATER"
               @"--define:NET472_OR_GREATER"
               @"--doc:C:\Users\nojaf\Projects\fsharp\artifacts\obj\Project300\Debug\net472\Project300.xml"
               @"--publicsign+"
               @"--optimize-"
               @"--tailcalls-"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\mscorlib.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project144\Debug\net472\Project144.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project164\Debug\net472\Project164.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project172\Debug\net472\Project172.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project184\Debug\net472\Project184.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project189\Debug\net472\Project189.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project206\Debug\net472\Project206.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Core.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Data.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Drawing.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.IO.Compression.FileSystem.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Numerics.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Runtime.Serialization.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Xml.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Xml.Linq.dll"
               @"--target:library"
               @"--nowarn:FS2003,NU5105"
               @"--warn:3"
               @"--warnaserror:3239,1182,0025"
               @"--fullpaths"
               @"--flaterrors"
               @"--subsystemversion:6.00"
               @"--highentropyva+"
               @"--targetprofile:mscorlib"
               @"--nocopyfsharpcore"
               @"--deterministic+"
               @"--simpleresolution"
               @"--nowarn:3384" |]
          ReferencedProjects = [| memoProject206 "Project206" |]
          IsIncompleteTypeCheckEnvironment = false
          UseScriptResolutionRules = false
          LoadTime = DateTime.Now
          UnresolvedReferences = None
          OriginalLoadReferences = []
          Stamp = None }

    FSharpReferencedProject.CreateFSharp(
        @"C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project300\Debug\net472\Project300.dll",
        projectOptions
    )

let memoProject300 = memoization Project300

let Project319 _ =
    let projectOptions =
        { ProjectFileName = @"C:\Users\nojaf\Projects\fsharp\tests\service\data\LargeApp\Project319\Project319.fsproj"
          ProjectId = None
          SourceFiles =
            [| @"C:\Users\nojaf\Projects\fsharp\artifacts\obj\Project319\Debug\net472\Project319.AssemblyInfo.fs"
               @"C:\Users\nojaf\Projects\fsharp\tests\service\data\LargeApp\Project319\MyModule.fs" |]
          OtherOptions =
            [| @"-o:C:\Users\nojaf\Projects\fsharp\artifacts\obj\Project319\Debug\net472\Project319.dll"
               @"-g"
               @"--debug:portable"
               @"--noframework"
               @"--define:TRACE"
               @"--define:DEBUG"
               @"--define:ENABLE_MONO_SUPPORT"
               @"--define:NETFRAMEWORK"
               @"--define:NET472"
               @"--define:NET20_OR_GREATER"
               @"--define:NET30_OR_GREATER"
               @"--define:NET35_OR_GREATER"
               @"--define:NET40_OR_GREATER"
               @"--define:NET45_OR_GREATER"
               @"--define:NET451_OR_GREATER"
               @"--define:NET452_OR_GREATER"
               @"--define:NET46_OR_GREATER"
               @"--define:NET461_OR_GREATER"
               @"--define:NET462_OR_GREATER"
               @"--define:NET47_OR_GREATER"
               @"--define:NET471_OR_GREATER"
               @"--define:NET472_OR_GREATER"
               @"--doc:C:\Users\nojaf\Projects\fsharp\artifacts\obj\Project319\Debug\net472\Project319.xml"
               @"--publicsign+"
               @"--optimize-"
               @"--tailcalls-"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\mscorlib.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project142\Debug\net472\Project142.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Core.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Data.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Drawing.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.IO.Compression.FileSystem.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Numerics.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Runtime.Serialization.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Xml.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Xml.Linq.dll"
               @"--target:library"
               @"--nowarn:FS2003,NU5105"
               @"--warn:3"
               @"--warnaserror:3239,1182,0025"
               @"--fullpaths"
               @"--flaterrors"
               @"--subsystemversion:6.00"
               @"--highentropyva+"
               @"--targetprofile:mscorlib"
               @"--nocopyfsharpcore"
               @"--deterministic+"
               @"--simpleresolution"
               @"--nowarn:3384" |]
          ReferencedProjects = [||]
          IsIncompleteTypeCheckEnvironment = false
          UseScriptResolutionRules = false
          LoadTime = DateTime.Now
          UnresolvedReferences = None
          OriginalLoadReferences = []
          Stamp = None }

    FSharpReferencedProject.CreateFSharp(
        @"C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project319\Debug\net472\Project319.dll",
        projectOptions
    )

let memoProject319 = memoization Project319

let Project334 _ =
    let projectOptions =
        { ProjectFileName = @"C:\Users\nojaf\Projects\fsharp\tests\service\data\LargeApp\Project334\Project334.fsproj"
          ProjectId = None
          SourceFiles =
            [| @"C:\Users\nojaf\Projects\fsharp\artifacts\obj\Project334\Debug\net472\Project334.AssemblyInfo.fs"
               @"C:\Users\nojaf\Projects\fsharp\tests\service\data\LargeApp\Project334\MyModule.fs" |]
          OtherOptions =
            [| @"-o:C:\Users\nojaf\Projects\fsharp\artifacts\obj\Project334\Debug\net472\Project334.dll"
               @"-g"
               @"--debug:portable"
               @"--noframework"
               @"--define:TRACE"
               @"--define:DEBUG"
               @"--define:ENABLE_MONO_SUPPORT"
               @"--define:NETFRAMEWORK"
               @"--define:NET472"
               @"--define:NET20_OR_GREATER"
               @"--define:NET30_OR_GREATER"
               @"--define:NET35_OR_GREATER"
               @"--define:NET40_OR_GREATER"
               @"--define:NET45_OR_GREATER"
               @"--define:NET451_OR_GREATER"
               @"--define:NET452_OR_GREATER"
               @"--define:NET46_OR_GREATER"
               @"--define:NET461_OR_GREATER"
               @"--define:NET462_OR_GREATER"
               @"--define:NET47_OR_GREATER"
               @"--define:NET471_OR_GREATER"
               @"--define:NET472_OR_GREATER"
               @"--doc:C:\Users\nojaf\Projects\fsharp\artifacts\obj\Project334\Debug\net472\Project334.xml"
               @"--publicsign+"
               @"--optimize-"
               @"--tailcalls-"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\mscorlib.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project122\Debug\net472\Project122.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project138\Debug\net472\Project138.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project140\Debug\net472\Project140.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project142\Debug\net472\Project142.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project144\Debug\net472\Project144.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project149\Debug\net472\Project149.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project161\Debug\net472\Project161.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project164\Debug\net472\Project164.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project168\Debug\net472\Project168.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project172\Debug\net472\Project172.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project184\Debug\net472\Project184.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project189\Debug\net472\Project189.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project209\Debug\net472\Project209.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project214\Debug\net472\Project214.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project240\Debug\net472\Project240.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project264\Debug\net472\Project264.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project265\Debug\net472\Project265.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Core.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Data.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Drawing.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.IO.Compression.FileSystem.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Numerics.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Runtime.Serialization.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Xml.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Xml.Linq.dll"
               @"--target:library"
               @"--nowarn:FS2003,NU5105"
               @"--warn:3"
               @"--warnaserror:3239,1182,0025"
               @"--fullpaths"
               @"--flaterrors"
               @"--subsystemversion:6.00"
               @"--highentropyva+"
               @"--targetprofile:mscorlib"
               @"--nocopyfsharpcore"
               @"--deterministic+"
               @"--simpleresolution"
               @"--nowarn:3384" |]
          ReferencedProjects =
            [| memoProject122 "Project122"
               memoProject240 "Project240"
               memoProject264 "Project264"
               memoProject265 "Project265" |]
          IsIncompleteTypeCheckEnvironment = false
          UseScriptResolutionRules = false
          LoadTime = DateTime.Now
          UnresolvedReferences = None
          OriginalLoadReferences = []
          Stamp = None }

    FSharpReferencedProject.CreateFSharp(
        @"C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project334\Debug\net472\Project334.dll",
        projectOptions
    )

let memoProject334 = memoization Project334

let Project122 _ =
    let projectOptions =
        { ProjectFileName = @"C:\Users\nojaf\Projects\fsharp\tests\service\data\LargeApp\Project122\Project122.fsproj"
          ProjectId = None
          SourceFiles =
            [| @"C:\Users\nojaf\Projects\fsharp\artifacts\obj\Project122\Debug\net472\Project122.AssemblyInfo.fs"
               @"C:\Users\nojaf\Projects\fsharp\tests\service\data\LargeApp\Project122\MyModule.fs" |]
          OtherOptions =
            [| @"-o:C:\Users\nojaf\Projects\fsharp\artifacts\obj\Project122\Debug\net472\Project122.dll"
               @"-g"
               @"--debug:portable"
               @"--noframework"
               @"--define:TRACE"
               @"--define:DEBUG"
               @"--define:ENABLE_MONO_SUPPORT"
               @"--define:NETFRAMEWORK"
               @"--define:NET472"
               @"--define:NET20_OR_GREATER"
               @"--define:NET30_OR_GREATER"
               @"--define:NET35_OR_GREATER"
               @"--define:NET40_OR_GREATER"
               @"--define:NET45_OR_GREATER"
               @"--define:NET451_OR_GREATER"
               @"--define:NET452_OR_GREATER"
               @"--define:NET46_OR_GREATER"
               @"--define:NET461_OR_GREATER"
               @"--define:NET462_OR_GREATER"
               @"--define:NET47_OR_GREATER"
               @"--define:NET471_OR_GREATER"
               @"--define:NET472_OR_GREATER"
               @"--doc:C:\Users\nojaf\Projects\fsharp\artifacts\obj\Project122\Debug\net472\Project122.xml"
               @"--publicsign+"
               @"--optimize-"
               @"--tailcalls-"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\mscorlib.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project126\Debug\net472\Project126.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Core.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Data.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Drawing.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.IO.Compression.FileSystem.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Numerics.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Runtime.Serialization.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Xml.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Xml.Linq.dll"
               @"--target:library"
               @"--nowarn:FS2003,NU5105"
               @"--warn:3"
               @"--warnaserror:3239,1182,0025"
               @"--fullpaths"
               @"--flaterrors"
               @"--subsystemversion:6.00"
               @"--highentropyva+"
               @"--targetprofile:mscorlib"
               @"--nocopyfsharpcore"
               @"--deterministic+"
               @"--simpleresolution"
               @"--nowarn:3384" |]
          ReferencedProjects = [||]
          IsIncompleteTypeCheckEnvironment = false
          UseScriptResolutionRules = false
          LoadTime = DateTime.Now
          UnresolvedReferences = None
          OriginalLoadReferences = []
          Stamp = None }

    FSharpReferencedProject.CreateFSharp(
        @"C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project122\Debug\net472\Project122.dll",
        projectOptions
    )

let memoProject122 = memoization Project122

let Project240 _ =
    let projectOptions =
        { ProjectFileName = @"C:\Users\nojaf\Projects\fsharp\tests\service\data\LargeApp\Project240\Project240.fsproj"
          ProjectId = None
          SourceFiles =
            [| @"C:\Users\nojaf\Projects\fsharp\artifacts\obj\Project240\Debug\net472\Project240.AssemblyInfo.fs"
               @"C:\Users\nojaf\Projects\fsharp\tests\service\data\LargeApp\Project240\MyModule.fs" |]
          OtherOptions =
            [| @"-o:C:\Users\nojaf\Projects\fsharp\artifacts\obj\Project240\Debug\net472\Project240.dll"
               @"-g"
               @"--debug:portable"
               @"--noframework"
               @"--define:TRACE"
               @"--define:DEBUG"
               @"--define:ENABLE_MONO_SUPPORT"
               @"--define:NETFRAMEWORK"
               @"--define:NET472"
               @"--define:NET20_OR_GREATER"
               @"--define:NET30_OR_GREATER"
               @"--define:NET35_OR_GREATER"
               @"--define:NET40_OR_GREATER"
               @"--define:NET45_OR_GREATER"
               @"--define:NET451_OR_GREATER"
               @"--define:NET452_OR_GREATER"
               @"--define:NET46_OR_GREATER"
               @"--define:NET461_OR_GREATER"
               @"--define:NET462_OR_GREATER"
               @"--define:NET47_OR_GREATER"
               @"--define:NET471_OR_GREATER"
               @"--define:NET472_OR_GREATER"
               @"--doc:C:\Users\nojaf\Projects\fsharp\artifacts\obj\Project240\Debug\net472\Project240.xml"
               @"--publicsign+"
               @"--optimize-"
               @"--tailcalls-"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\mscorlib.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project140\Debug\net472\Project140.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project142\Debug\net472\Project142.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project149\Debug\net472\Project149.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project161\Debug\net472\Project161.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Core.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Data.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Drawing.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.IO.Compression.FileSystem.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Numerics.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Runtime.Serialization.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Xml.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Xml.Linq.dll"
               @"--target:library"
               @"--nowarn:FS2003,NU5105"
               @"--warn:3"
               @"--warnaserror:3239,1182,0025"
               @"--fullpaths"
               @"--flaterrors"
               @"--subsystemversion:6.00"
               @"--highentropyva+"
               @"--targetprofile:mscorlib"
               @"--nocopyfsharpcore"
               @"--deterministic+"
               @"--simpleresolution"
               @"--nowarn:3384" |]
          ReferencedProjects = [||]
          IsIncompleteTypeCheckEnvironment = false
          UseScriptResolutionRules = false
          LoadTime = DateTime.Now
          UnresolvedReferences = None
          OriginalLoadReferences = []
          Stamp = None }

    FSharpReferencedProject.CreateFSharp(
        @"C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project240\Debug\net472\Project240.dll",
        projectOptions
    )

let memoProject240 = memoization Project240

let Project17 _ =
    let projectOptions =
        { ProjectFileName = @"C:\Users\nojaf\Projects\fsharp\tests\service\data\LargeApp\Project17\Project17.fsproj"
          ProjectId = None
          SourceFiles =
            [| @"C:\Users\nojaf\Projects\fsharp\artifacts\obj\Project17\Debug\net472\Project17.AssemblyInfo.fs"
               @"C:\Users\nojaf\Projects\fsharp\tests\service\data\LargeApp\Project17\MyModule.fs" |]
          OtherOptions =
            [| @"-o:C:\Users\nojaf\Projects\fsharp\artifacts\obj\Project17\Debug\net472\Project17.dll"
               @"-g"
               @"--debug:portable"
               @"--noframework"
               @"--define:TRACE"
               @"--define:DEBUG"
               @"--define:ENABLE_MONO_SUPPORT"
               @"--define:NETFRAMEWORK"
               @"--define:NET472"
               @"--define:NET20_OR_GREATER"
               @"--define:NET30_OR_GREATER"
               @"--define:NET35_OR_GREATER"
               @"--define:NET40_OR_GREATER"
               @"--define:NET45_OR_GREATER"
               @"--define:NET451_OR_GREATER"
               @"--define:NET452_OR_GREATER"
               @"--define:NET46_OR_GREATER"
               @"--define:NET461_OR_GREATER"
               @"--define:NET462_OR_GREATER"
               @"--define:NET47_OR_GREATER"
               @"--define:NET471_OR_GREATER"
               @"--define:NET472_OR_GREATER"
               @"--doc:C:\Users\nojaf\Projects\fsharp\artifacts\obj\Project17\Debug\net472\Project17.xml"
               @"--publicsign+"
               @"--optimize-"
               @"--tailcalls-"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\mscorlib.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project122\Debug\net472\Project122.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project138\Debug\net472\Project138.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project140\Debug\net472\Project140.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project142\Debug\net472\Project142.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project146\Debug\net472\Project146.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project149\Debug\net472\Project149.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project151\Debug\net472\Project151.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project161\Debug\net472\Project161.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project164\Debug\net472\Project164.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project168\Debug\net472\Project168.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project214\Debug\net472\Project214.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project22\Debug\net472\Project22.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project23\Debug\net472\Project23.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project240\Debug\net472\Project240.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project272\Debug\net472\Project272.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project295\Debug\net472\Project295.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project297\Debug\net472\Project297.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project298\Debug\net472\Project298.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project326\Debug\net472\Project326.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project344\Debug\net472\Project344.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Core.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Data.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Drawing.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.IO.Compression.FileSystem.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Numerics.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Runtime.Serialization.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Xml.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Xml.Linq.dll"
               @"--target:library"
               @"--nowarn:FS2003,NU5105"
               @"--warn:3"
               @"--warnaserror:3239,1182,0025"
               @"--fullpaths"
               @"--flaterrors"
               @"--subsystemversion:6.00"
               @"--highentropyva+"
               @"--targetprofile:mscorlib"
               @"--nocopyfsharpcore"
               @"--deterministic+"
               @"--simpleresolution"
               @"--nowarn:3384" |]
          ReferencedProjects =
            [| memoProject122 "Project122"
               memoProject151 "Project151"
               memoProject22 "Project22"
               memoProject23 "Project23"
               memoProject240 "Project240"
               memoProject272 "Project272" |]
          IsIncompleteTypeCheckEnvironment = false
          UseScriptResolutionRules = false
          LoadTime = DateTime.Now
          UnresolvedReferences = None
          OriginalLoadReferences = []
          Stamp = None }

    FSharpReferencedProject.CreateFSharp(
        @"C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project17\Debug\net472\Project17.dll",
        projectOptions
    )

let memoProject17 = memoization Project17

let Project23 _ =
    let projectOptions =
        { ProjectFileName = @"C:\Users\nojaf\Projects\fsharp\tests\service\data\LargeApp\Project23\Project23.fsproj"
          ProjectId = None
          SourceFiles =
            [| @"C:\Users\nojaf\Projects\fsharp\artifacts\obj\Project23\Debug\net472\Project23.AssemblyInfo.fs"
               @"C:\Users\nojaf\Projects\fsharp\tests\service\data\LargeApp\Project23\MyModule.fs" |]
          OtherOptions =
            [| @"-o:C:\Users\nojaf\Projects\fsharp\artifacts\obj\Project23\Debug\net472\Project23.dll"
               @"-g"
               @"--debug:portable"
               @"--noframework"
               @"--define:TRACE"
               @"--define:DEBUG"
               @"--define:ENABLE_MONO_SUPPORT"
               @"--define:NETFRAMEWORK"
               @"--define:NET472"
               @"--define:NET20_OR_GREATER"
               @"--define:NET30_OR_GREATER"
               @"--define:NET35_OR_GREATER"
               @"--define:NET40_OR_GREATER"
               @"--define:NET45_OR_GREATER"
               @"--define:NET451_OR_GREATER"
               @"--define:NET452_OR_GREATER"
               @"--define:NET46_OR_GREATER"
               @"--define:NET461_OR_GREATER"
               @"--define:NET462_OR_GREATER"
               @"--define:NET47_OR_GREATER"
               @"--define:NET471_OR_GREATER"
               @"--define:NET472_OR_GREATER"
               @"--doc:C:\Users\nojaf\Projects\fsharp\artifacts\obj\Project23\Debug\net472\Project23.xml"
               @"--publicsign+"
               @"--optimize-"
               @"--tailcalls-"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\mscorlib.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project11\Debug\net472\Project11.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project138\Debug\net472\Project138.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project14\Debug\net472\Project14.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project140\Debug\net472\Project140.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project142\Debug\net472\Project142.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project146\Debug\net472\Project146.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project149\Debug\net472\Project149.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project151\Debug\net472\Project151.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project161\Debug\net472\Project161.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project164\Debug\net472\Project164.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project168\Debug\net472\Project168.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project189\Debug\net472\Project189.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project213\Debug\net472\Project213.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project22\Debug\net472\Project22.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project239\Debug\net472\Project239.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project24\Debug\net472\Project24.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project243\Debug\net472\Project243.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project247\Debug\net472\Project247.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project264\Debug\net472\Project264.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project272\Debug\net472\Project272.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project326\Debug\net472\Project326.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project329\Debug\net472\Project329.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project334\Debug\net472\Project334.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project87\Debug\net472\Project87.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Core.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Data.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Drawing.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.IO.Compression.FileSystem.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Numerics.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Runtime.Serialization.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Xml.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Xml.Linq.dll"
               @"--target:library"
               @"--nowarn:FS2003,NU5105"
               @"--warn:3"
               @"--warnaserror:3239,1182,0025"
               @"--fullpaths"
               @"--flaterrors"
               @"--subsystemversion:6.00"
               @"--highentropyva+"
               @"--targetprofile:mscorlib"
               @"--nocopyfsharpcore"
               @"--deterministic+"
               @"--simpleresolution"
               @"--nowarn:3384" |]
          ReferencedProjects =
            [| memoProject11 "Project11"
               memoProject14 "Project14"
               memoProject151 "Project151"
               memoProject213 "Project213"
               memoProject22 "Project22"
               memoProject239 "Project239"
               memoProject243 "Project243"
               memoProject247 "Project247"
               memoProject24 "Project24"
               memoProject264 "Project264"
               memoProject272 "Project272"
               memoProject329 "Project329"
               memoProject334 "Project334" |]
          IsIncompleteTypeCheckEnvironment = false
          UseScriptResolutionRules = false
          LoadTime = DateTime.Now
          UnresolvedReferences = None
          OriginalLoadReferences = []
          Stamp = None }

    FSharpReferencedProject.CreateFSharp(
        @"C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project23\Debug\net472\Project23.dll",
        projectOptions
    )

let memoProject23 = memoization Project23

let Project11 _ =
    let projectOptions =
        { ProjectFileName = @"C:\Users\nojaf\Projects\fsharp\tests\service\data\LargeApp\Project11\Project11.fsproj"
          ProjectId = None
          SourceFiles =
            [| @"C:\Users\nojaf\Projects\fsharp\artifacts\obj\Project11\Debug\net472\Project11.AssemblyInfo.fs"
               @"C:\Users\nojaf\Projects\fsharp\tests\service\data\LargeApp\Project11\MyModule.fs" |]
          OtherOptions =
            [| @"-o:C:\Users\nojaf\Projects\fsharp\artifacts\obj\Project11\Debug\net472\Project11.dll"
               @"-g"
               @"--debug:portable"
               @"--noframework"
               @"--define:TRACE"
               @"--define:DEBUG"
               @"--define:ENABLE_MONO_SUPPORT"
               @"--define:NETFRAMEWORK"
               @"--define:NET472"
               @"--define:NET20_OR_GREATER"
               @"--define:NET30_OR_GREATER"
               @"--define:NET35_OR_GREATER"
               @"--define:NET40_OR_GREATER"
               @"--define:NET45_OR_GREATER"
               @"--define:NET451_OR_GREATER"
               @"--define:NET452_OR_GREATER"
               @"--define:NET46_OR_GREATER"
               @"--define:NET461_OR_GREATER"
               @"--define:NET462_OR_GREATER"
               @"--define:NET47_OR_GREATER"
               @"--define:NET471_OR_GREATER"
               @"--define:NET472_OR_GREATER"
               @"--doc:C:\Users\nojaf\Projects\fsharp\artifacts\obj\Project11\Debug\net472\Project11.xml"
               @"--publicsign+"
               @"--optimize-"
               @"--tailcalls-"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\mscorlib.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project14\Debug\net472\Project14.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project140\Debug\net472\Project140.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project142\Debug\net472\Project142.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project146\Debug\net472\Project146.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project15\Debug\net472\Project15.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project151\Debug\net472\Project151.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project164\Debug\net472\Project164.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project192\Debug\net472\Project192.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project22\Debug\net472\Project22.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project24\Debug\net472\Project24.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project243\Debug\net472\Project243.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project250\Debug\net472\Project250.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project261\Debug\net472\Project261.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project272\Debug\net472\Project272.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project289\Debug\net472\Project289.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project346\Debug\net472\Project346.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Core.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Data.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Drawing.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.IO.Compression.FileSystem.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Numerics.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Runtime.Serialization.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Xml.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Xml.Linq.dll"
               @"--target:library"
               @"--nowarn:FS2003,NU5105"
               @"--warn:3"
               @"--warnaserror:3239,1182,0025"
               @"--fullpaths"
               @"--flaterrors"
               @"--subsystemversion:6.00"
               @"--highentropyva+"
               @"--targetprofile:mscorlib"
               @"--nocopyfsharpcore"
               @"--deterministic+"
               @"--simpleresolution"
               @"--nowarn:3384" |]
          ReferencedProjects =
            [| memoProject14 "Project14"
               memoProject151 "Project151"
               memoProject15 "Project15"
               memoProject22 "Project22"
               memoProject243 "Project243"
               memoProject24 "Project24"
               memoProject250 "Project250"
               memoProject261 "Project261"
               memoProject272 "Project272"
               memoProject289 "Project289"
               memoProject346 "Project346" |]
          IsIncompleteTypeCheckEnvironment = false
          UseScriptResolutionRules = false
          LoadTime = DateTime.Now
          UnresolvedReferences = None
          OriginalLoadReferences = []
          Stamp = None }

    FSharpReferencedProject.CreateFSharp(
        @"C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project11\Debug\net472\Project11.dll",
        projectOptions
    )

let memoProject11 = memoization Project11

let Project15 _ =
    let projectOptions =
        { ProjectFileName = @"C:\Users\nojaf\Projects\fsharp\tests\service\data\LargeApp\Project15\Project15.fsproj"
          ProjectId = None
          SourceFiles =
            [| @"C:\Users\nojaf\Projects\fsharp\artifacts\obj\Project15\Debug\net472\Project15.AssemblyInfo.fs"
               @"C:\Users\nojaf\Projects\fsharp\tests\service\data\LargeApp\Project15\MyModule.fs" |]
          OtherOptions =
            [| @"-o:C:\Users\nojaf\Projects\fsharp\artifacts\obj\Project15\Debug\net472\Project15.dll"
               @"-g"
               @"--debug:portable"
               @"--noframework"
               @"--define:TRACE"
               @"--define:DEBUG"
               @"--define:ENABLE_MONO_SUPPORT"
               @"--define:NETFRAMEWORK"
               @"--define:NET472"
               @"--define:NET20_OR_GREATER"
               @"--define:NET30_OR_GREATER"
               @"--define:NET35_OR_GREATER"
               @"--define:NET40_OR_GREATER"
               @"--define:NET45_OR_GREATER"
               @"--define:NET451_OR_GREATER"
               @"--define:NET452_OR_GREATER"
               @"--define:NET46_OR_GREATER"
               @"--define:NET461_OR_GREATER"
               @"--define:NET462_OR_GREATER"
               @"--define:NET47_OR_GREATER"
               @"--define:NET471_OR_GREATER"
               @"--define:NET472_OR_GREATER"
               @"--doc:C:\Users\nojaf\Projects\fsharp\artifacts\obj\Project15\Debug\net472\Project15.xml"
               @"--publicsign+"
               @"--optimize-"
               @"--tailcalls-"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\mscorlib.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project146\Debug\net472\Project146.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project151\Debug\net472\Project151.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project22\Debug\net472\Project22.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project272\Debug\net472\Project272.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Core.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Data.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Drawing.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.IO.Compression.FileSystem.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Numerics.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Runtime.Serialization.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Xml.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Xml.Linq.dll"
               @"--target:library"
               @"--nowarn:FS2003,NU5105"
               @"--warn:3"
               @"--warnaserror:3239,1182,0025"
               @"--fullpaths"
               @"--flaterrors"
               @"--subsystemversion:6.00"
               @"--highentropyva+"
               @"--targetprofile:mscorlib"
               @"--nocopyfsharpcore"
               @"--deterministic+"
               @"--simpleresolution"
               @"--nowarn:3384" |]
          ReferencedProjects =
            [| memoProject151 "Project151"
               memoProject22 "Project22"
               memoProject272 "Project272" |]
          IsIncompleteTypeCheckEnvironment = false
          UseScriptResolutionRules = false
          LoadTime = DateTime.Now
          UnresolvedReferences = None
          OriginalLoadReferences = []
          Stamp = None }

    FSharpReferencedProject.CreateFSharp(
        @"C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project15\Debug\net472\Project15.dll",
        projectOptions
    )

let memoProject15 = memoization Project15

let Project24 _ =
    let projectOptions =
        { ProjectFileName = @"C:\Users\nojaf\Projects\fsharp\tests\service\data\LargeApp\Project24\Project24.fsproj"
          ProjectId = None
          SourceFiles =
            [| @"C:\Users\nojaf\Projects\fsharp\artifacts\obj\Project24\Debug\net472\Project24.AssemblyInfo.fs"
               @"C:\Users\nojaf\Projects\fsharp\tests\service\data\LargeApp\Project24\MyModule.fs" |]
          OtherOptions =
            [| @"-o:C:\Users\nojaf\Projects\fsharp\artifacts\obj\Project24\Debug\net472\Project24.dll"
               @"-g"
               @"--debug:portable"
               @"--noframework"
               @"--define:TRACE"
               @"--define:DEBUG"
               @"--define:ENABLE_MONO_SUPPORT"
               @"--define:NETFRAMEWORK"
               @"--define:NET472"
               @"--define:NET20_OR_GREATER"
               @"--define:NET30_OR_GREATER"
               @"--define:NET35_OR_GREATER"
               @"--define:NET40_OR_GREATER"
               @"--define:NET45_OR_GREATER"
               @"--define:NET451_OR_GREATER"
               @"--define:NET452_OR_GREATER"
               @"--define:NET46_OR_GREATER"
               @"--define:NET461_OR_GREATER"
               @"--define:NET462_OR_GREATER"
               @"--define:NET47_OR_GREATER"
               @"--define:NET471_OR_GREATER"
               @"--define:NET472_OR_GREATER"
               @"--doc:C:\Users\nojaf\Projects\fsharp\artifacts\obj\Project24\Debug\net472\Project24.xml"
               @"--publicsign+"
               @"--optimize-"
               @"--tailcalls-"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\mscorlib.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project10\Debug\net472\Project10.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project140\Debug\net472\Project140.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project144\Debug\net472\Project144.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project146\Debug\net472\Project146.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project15\Debug\net472\Project15.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project151\Debug\net472\Project151.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project164\Debug\net472\Project164.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project22\Debug\net472\Project22.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project243\Debug\net472\Project243.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project272\Debug\net472\Project272.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Core.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Data.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Drawing.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.IO.Compression.FileSystem.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Numerics.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Runtime.Serialization.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Xml.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Xml.Linq.dll"
               @"--target:library"
               @"--nowarn:FS2003,NU5105"
               @"--warn:3"
               @"--warnaserror:3239,1182,0025"
               @"--fullpaths"
               @"--flaterrors"
               @"--subsystemversion:6.00"
               @"--highentropyva+"
               @"--targetprofile:mscorlib"
               @"--nocopyfsharpcore"
               @"--deterministic+"
               @"--simpleresolution"
               @"--nowarn:3384" |]
          ReferencedProjects =
            [| memoProject10 "Project10"
               memoProject151 "Project151"
               memoProject15 "Project15"
               memoProject22 "Project22"
               memoProject243 "Project243"
               memoProject272 "Project272" |]
          IsIncompleteTypeCheckEnvironment = false
          UseScriptResolutionRules = false
          LoadTime = DateTime.Now
          UnresolvedReferences = None
          OriginalLoadReferences = []
          Stamp = None }

    FSharpReferencedProject.CreateFSharp(
        @"C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project24\Debug\net472\Project24.dll",
        projectOptions
    )

let memoProject24 = memoization Project24

let Project10 _ =
    let projectOptions =
        { ProjectFileName = @"C:\Users\nojaf\Projects\fsharp\tests\service\data\LargeApp\Project10\Project10.fsproj"
          ProjectId = None
          SourceFiles =
            [| @"C:\Users\nojaf\Projects\fsharp\artifacts\obj\Project10\Debug\net472\Project10.AssemblyInfo.fs"
               @"C:\Users\nojaf\Projects\fsharp\tests\service\data\LargeApp\Project10\MyModule.fs" |]
          OtherOptions =
            [| @"-o:C:\Users\nojaf\Projects\fsharp\artifacts\obj\Project10\Debug\net472\Project10.dll"
               @"-g"
               @"--debug:portable"
               @"--noframework"
               @"--define:TRACE"
               @"--define:DEBUG"
               @"--define:ENABLE_MONO_SUPPORT"
               @"--define:NETFRAMEWORK"
               @"--define:NET472"
               @"--define:NET20_OR_GREATER"
               @"--define:NET30_OR_GREATER"
               @"--define:NET35_OR_GREATER"
               @"--define:NET40_OR_GREATER"
               @"--define:NET45_OR_GREATER"
               @"--define:NET451_OR_GREATER"
               @"--define:NET452_OR_GREATER"
               @"--define:NET46_OR_GREATER"
               @"--define:NET461_OR_GREATER"
               @"--define:NET462_OR_GREATER"
               @"--define:NET47_OR_GREATER"
               @"--define:NET471_OR_GREATER"
               @"--define:NET472_OR_GREATER"
               @"--doc:C:\Users\nojaf\Projects\fsharp\artifacts\obj\Project10\Debug\net472\Project10.xml"
               @"--publicsign+"
               @"--optimize-"
               @"--tailcalls-"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\mscorlib.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project15\Debug\net472\Project15.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project22\Debug\net472\Project22.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Core.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Data.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Drawing.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.IO.Compression.FileSystem.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Numerics.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Runtime.Serialization.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Xml.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Xml.Linq.dll"
               @"--target:library"
               @"--nowarn:FS2003,NU5105"
               @"--warn:3"
               @"--warnaserror:3239,1182,0025"
               @"--fullpaths"
               @"--flaterrors"
               @"--subsystemversion:6.00"
               @"--highentropyva+"
               @"--targetprofile:mscorlib"
               @"--nocopyfsharpcore"
               @"--deterministic+"
               @"--simpleresolution"
               @"--nowarn:3384" |]
          ReferencedProjects =
            [| memoProject15 "Project15"
               memoProject22 "Project22" |]
          IsIncompleteTypeCheckEnvironment = false
          UseScriptResolutionRules = false
          LoadTime = DateTime.Now
          UnresolvedReferences = None
          OriginalLoadReferences = []
          Stamp = None }

    FSharpReferencedProject.CreateFSharp(
        @"C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project10\Debug\net472\Project10.dll",
        projectOptions
    )

let memoProject10 = memoization Project10

let Project261 _ =
    let projectOptions =
        { ProjectFileName = @"C:\Users\nojaf\Projects\fsharp\tests\service\data\LargeApp\Project261\Project261.fsproj"
          ProjectId = None
          SourceFiles =
            [| @"C:\Users\nojaf\Projects\fsharp\artifacts\obj\Project261\Debug\net472\Project261.AssemblyInfo.fs"
               @"C:\Users\nojaf\Projects\fsharp\tests\service\data\LargeApp\Project261\MyModule.fs" |]
          OtherOptions =
            [| @"-o:C:\Users\nojaf\Projects\fsharp\artifacts\obj\Project261\Debug\net472\Project261.dll"
               @"-g"
               @"--debug:portable"
               @"--noframework"
               @"--define:TRACE"
               @"--define:DEBUG"
               @"--define:ENABLE_MONO_SUPPORT"
               @"--define:NETFRAMEWORK"
               @"--define:NET472"
               @"--define:NET20_OR_GREATER"
               @"--define:NET30_OR_GREATER"
               @"--define:NET35_OR_GREATER"
               @"--define:NET40_OR_GREATER"
               @"--define:NET45_OR_GREATER"
               @"--define:NET451_OR_GREATER"
               @"--define:NET452_OR_GREATER"
               @"--define:NET46_OR_GREATER"
               @"--define:NET461_OR_GREATER"
               @"--define:NET462_OR_GREATER"
               @"--define:NET47_OR_GREATER"
               @"--define:NET471_OR_GREATER"
               @"--define:NET472_OR_GREATER"
               @"--doc:C:\Users\nojaf\Projects\fsharp\artifacts\obj\Project261\Debug\net472\Project261.xml"
               @"--publicsign+"
               @"--optimize-"
               @"--tailcalls-"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\mscorlib.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project140\Debug\net472\Project140.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project144\Debug\net472\Project144.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project146\Debug\net472\Project146.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project151\Debug\net472\Project151.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project164\Debug\net472\Project164.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project243\Debug\net472\Project243.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project272\Debug\net472\Project272.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Core.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Data.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Drawing.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.IO.Compression.FileSystem.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Numerics.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Runtime.Serialization.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Xml.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Xml.Linq.dll"
               @"--target:library"
               @"--nowarn:FS2003,NU5105"
               @"--warn:3"
               @"--warnaserror:3239,1182,0025"
               @"--fullpaths"
               @"--flaterrors"
               @"--subsystemversion:6.00"
               @"--highentropyva+"
               @"--targetprofile:mscorlib"
               @"--nocopyfsharpcore"
               @"--deterministic+"
               @"--simpleresolution"
               @"--nowarn:3384" |]
          ReferencedProjects =
            [| memoProject151 "Project151"
               memoProject243 "Project243"
               memoProject272 "Project272" |]
          IsIncompleteTypeCheckEnvironment = false
          UseScriptResolutionRules = false
          LoadTime = DateTime.Now
          UnresolvedReferences = None
          OriginalLoadReferences = []
          Stamp = None }

    FSharpReferencedProject.CreateFSharp(
        @"C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project261\Debug\net472\Project261.dll",
        projectOptions
    )

let memoProject261 = memoization Project261

let Project346 _ =
    let projectOptions =
        { ProjectFileName = @"C:\Users\nojaf\Projects\fsharp\tests\service\data\LargeApp\Project346\Project346.fsproj"
          ProjectId = None
          SourceFiles =
            [| @"C:\Users\nojaf\Projects\fsharp\artifacts\obj\Project346\Debug\net472\Project346.AssemblyInfo.fs"
               @"C:\Users\nojaf\Projects\fsharp\tests\service\data\LargeApp\Project346\MyModule.fs" |]
          OtherOptions =
            [| @"-o:C:\Users\nojaf\Projects\fsharp\artifacts\obj\Project346\Debug\net472\Project346.dll"
               @"-g"
               @"--debug:portable"
               @"--noframework"
               @"--define:TRACE"
               @"--define:DEBUG"
               @"--define:ENABLE_MONO_SUPPORT"
               @"--define:NETFRAMEWORK"
               @"--define:NET472"
               @"--define:NET20_OR_GREATER"
               @"--define:NET30_OR_GREATER"
               @"--define:NET35_OR_GREATER"
               @"--define:NET40_OR_GREATER"
               @"--define:NET45_OR_GREATER"
               @"--define:NET451_OR_GREATER"
               @"--define:NET452_OR_GREATER"
               @"--define:NET46_OR_GREATER"
               @"--define:NET461_OR_GREATER"
               @"--define:NET462_OR_GREATER"
               @"--define:NET47_OR_GREATER"
               @"--define:NET471_OR_GREATER"
               @"--define:NET472_OR_GREATER"
               @"--doc:C:\Users\nojaf\Projects\fsharp\artifacts\obj\Project346\Debug\net472\Project346.xml"
               @"--publicsign+"
               @"--optimize-"
               @"--tailcalls-"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\mscorlib.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project138\Debug\net472\Project138.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project140\Debug\net472\Project140.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project142\Debug\net472\Project142.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project146\Debug\net472\Project146.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project149\Debug\net472\Project149.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project151\Debug\net472\Project151.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project161\Debug\net472\Project161.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project164\Debug\net472\Project164.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project168\Debug\net472\Project168.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project172\Debug\net472\Project172.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project189\Debug\net472\Project189.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project190\Debug\net472\Project190.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project192\Debug\net472\Project192.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project213\Debug\net472\Project213.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project214\Debug\net472\Project214.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project222\Debug\net472\Project222.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project243\Debug\net472\Project243.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project250\Debug\net472\Project250.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project264\Debug\net472\Project264.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project268\Debug\net472\Project268.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project272\Debug\net472\Project272.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project295\Debug\net472\Project295.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project297\Debug\net472\Project297.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project301\Debug\net472\Project301.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project326\Debug\net472\Project326.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project329\Debug\net472\Project329.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Core.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Data.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Drawing.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.IO.Compression.FileSystem.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Numerics.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Runtime.Serialization.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Xml.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Xml.Linq.dll"
               @"--target:library"
               @"--nowarn:FS2003,NU5105"
               @"--warn:3"
               @"--warnaserror:3239,1182,0025"
               @"--fullpaths"
               @"--flaterrors"
               @"--subsystemversion:6.00"
               @"--highentropyva+"
               @"--targetprofile:mscorlib"
               @"--nocopyfsharpcore"
               @"--deterministic+"
               @"--simpleresolution"
               @"--nowarn:3384" |]
          ReferencedProjects =
            [| memoProject151 "Project151"
               memoProject213 "Project213"
               memoProject222 "Project222"
               memoProject243 "Project243"
               memoProject250 "Project250"
               memoProject264 "Project264"
               memoProject268 "Project268"
               memoProject272 "Project272"
               memoProject329 "Project329" |]
          IsIncompleteTypeCheckEnvironment = false
          UseScriptResolutionRules = false
          LoadTime = DateTime.Now
          UnresolvedReferences = None
          OriginalLoadReferences = []
          Stamp = None }

    FSharpReferencedProject.CreateFSharp(
        @"C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project346\Debug\net472\Project346.dll",
        projectOptions
    )

let memoProject346 = memoization Project346

let Project268 _ =
    let projectOptions =
        { ProjectFileName = @"C:\Users\nojaf\Projects\fsharp\tests\service\data\LargeApp\Project268\Project268.fsproj"
          ProjectId = None
          SourceFiles =
            [| @"C:\Users\nojaf\Projects\fsharp\artifacts\obj\Project268\Debug\net472\Project268.AssemblyInfo.fs"
               @"C:\Users\nojaf\Projects\fsharp\tests\service\data\LargeApp\Project268\MyModule.fs" |]
          OtherOptions =
            [| @"-o:C:\Users\nojaf\Projects\fsharp\artifacts\obj\Project268\Debug\net472\Project268.dll"
               @"-g"
               @"--debug:portable"
               @"--noframework"
               @"--define:TRACE"
               @"--define:DEBUG"
               @"--define:ENABLE_MONO_SUPPORT"
               @"--define:NETFRAMEWORK"
               @"--define:NET472"
               @"--define:NET20_OR_GREATER"
               @"--define:NET30_OR_GREATER"
               @"--define:NET35_OR_GREATER"
               @"--define:NET40_OR_GREATER"
               @"--define:NET45_OR_GREATER"
               @"--define:NET451_OR_GREATER"
               @"--define:NET452_OR_GREATER"
               @"--define:NET46_OR_GREATER"
               @"--define:NET461_OR_GREATER"
               @"--define:NET462_OR_GREATER"
               @"--define:NET47_OR_GREATER"
               @"--define:NET471_OR_GREATER"
               @"--define:NET472_OR_GREATER"
               @"--doc:C:\Users\nojaf\Projects\fsharp\artifacts\obj\Project268\Debug\net472\Project268.xml"
               @"--publicsign+"
               @"--optimize-"
               @"--tailcalls-"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\mscorlib.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project138\Debug\net472\Project138.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project140\Debug\net472\Project140.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project142\Debug\net472\Project142.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project144\Debug\net472\Project144.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project149\Debug\net472\Project149.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project164\Debug\net472\Project164.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project168\Debug\net472\Project168.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project172\Debug\net472\Project172.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project184\Debug\net472\Project184.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project189\Debug\net472\Project189.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project192\Debug\net472\Project192.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project213\Debug\net472\Project213.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project222\Debug\net472\Project222.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project243\Debug\net472\Project243.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project264\Debug\net472\Project264.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project265\Debug\net472\Project265.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project300\Debug\net472\Project300.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project310\Debug\net472\Project310.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project326\Debug\net472\Project326.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project329\Debug\net472\Project329.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project334\Debug\net472\Project334.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project344\Debug\net472\Project344.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Core.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Data.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Drawing.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.IO.Compression.FileSystem.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Numerics.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Runtime.Serialization.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Xml.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Xml.Linq.dll"
               @"--target:library"
               @"--nowarn:FS2003,NU5105"
               @"--warn:3"
               @"--warnaserror:3239,1182,0025"
               @"--fullpaths"
               @"--flaterrors"
               @"--subsystemversion:6.00"
               @"--highentropyva+"
               @"--targetprofile:mscorlib"
               @"--nocopyfsharpcore"
               @"--deterministic+"
               @"--simpleresolution"
               @"--nowarn:3384" |]
          ReferencedProjects =
            [| memoProject213 "Project213"
               memoProject222 "Project222"
               memoProject243 "Project243"
               memoProject264 "Project264"
               memoProject265 "Project265"
               memoProject300 "Project300"
               memoProject310 "Project310"
               memoProject329 "Project329"
               memoProject334 "Project334" |]
          IsIncompleteTypeCheckEnvironment = false
          UseScriptResolutionRules = false
          LoadTime = DateTime.Now
          UnresolvedReferences = None
          OriginalLoadReferences = []
          Stamp = None }

    FSharpReferencedProject.CreateFSharp(
        @"C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project268\Debug\net472\Project268.dll",
        projectOptions
    )

let memoProject268 = memoization Project268

let Project329 _ =
    let projectOptions =
        { ProjectFileName = @"C:\Users\nojaf\Projects\fsharp\tests\service\data\LargeApp\Project329\Project329.fsproj"
          ProjectId = None
          SourceFiles =
            [| @"C:\Users\nojaf\Projects\fsharp\artifacts\obj\Project329\Debug\net472\Project329.AssemblyInfo.fs"
               @"C:\Users\nojaf\Projects\fsharp\tests\service\data\LargeApp\Project329\MyModule.fs" |]
          OtherOptions =
            [| @"-o:C:\Users\nojaf\Projects\fsharp\artifacts\obj\Project329\Debug\net472\Project329.dll"
               @"-g"
               @"--debug:portable"
               @"--noframework"
               @"--define:TRACE"
               @"--define:DEBUG"
               @"--define:ENABLE_MONO_SUPPORT"
               @"--define:NETFRAMEWORK"
               @"--define:NET472"
               @"--define:NET20_OR_GREATER"
               @"--define:NET30_OR_GREATER"
               @"--define:NET35_OR_GREATER"
               @"--define:NET40_OR_GREATER"
               @"--define:NET45_OR_GREATER"
               @"--define:NET451_OR_GREATER"
               @"--define:NET452_OR_GREATER"
               @"--define:NET46_OR_GREATER"
               @"--define:NET461_OR_GREATER"
               @"--define:NET462_OR_GREATER"
               @"--define:NET47_OR_GREATER"
               @"--define:NET471_OR_GREATER"
               @"--define:NET472_OR_GREATER"
               @"--doc:C:\Users\nojaf\Projects\fsharp\artifacts\obj\Project329\Debug\net472\Project329.xml"
               @"--publicsign+"
               @"--optimize-"
               @"--tailcalls-"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\mscorlib.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project124\Debug\net472\Project124.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project127\Debug\net472\Project127.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project138\Debug\net472\Project138.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project140\Debug\net472\Project140.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project142\Debug\net472\Project142.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project144\Debug\net472\Project144.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project146\Debug\net472\Project146.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project149\Debug\net472\Project149.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project151\Debug\net472\Project151.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project161\Debug\net472\Project161.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project164\Debug\net472\Project164.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project168\Debug\net472\Project168.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project172\Debug\net472\Project172.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project184\Debug\net472\Project184.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project189\Debug\net472\Project189.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project192\Debug\net472\Project192.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project193\Debug\net472\Project193.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project213\Debug\net472\Project213.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project214\Debug\net472\Project214.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project222\Debug\net472\Project222.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project226\Debug\net472\Project226.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project239\Debug\net472\Project239.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project243\Debug\net472\Project243.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project250\Debug\net472\Project250.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project264\Debug\net472\Project264.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project272\Debug\net472\Project272.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project300\Debug\net472\Project300.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project310\Debug\net472\Project310.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project323\Debug\net472\Project323.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project324\Debug\net472\Project324.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project326\Debug\net472\Project326.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Core.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Data.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Drawing.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.IO.Compression.FileSystem.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Numerics.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Runtime.Serialization.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Xml.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Xml.Linq.dll"
               @"--target:library"
               @"--nowarn:FS2003,NU5105"
               @"--warn:3"
               @"--warnaserror:3239,1182,0025"
               @"--fullpaths"
               @"--flaterrors"
               @"--subsystemversion:6.00"
               @"--highentropyva+"
               @"--targetprofile:mscorlib"
               @"--nocopyfsharpcore"
               @"--deterministic+"
               @"--simpleresolution"
               @"--nowarn:3384" |]
          ReferencedProjects =
            [| memoProject124 "Project124"
               memoProject151 "Project151"
               memoProject213 "Project213"
               memoProject222 "Project222"
               memoProject239 "Project239"
               memoProject243 "Project243"
               memoProject250 "Project250"
               memoProject264 "Project264"
               memoProject272 "Project272"
               memoProject300 "Project300"
               memoProject310 "Project310"
               memoProject324 "Project324" |]
          IsIncompleteTypeCheckEnvironment = false
          UseScriptResolutionRules = false
          LoadTime = DateTime.Now
          UnresolvedReferences = None
          OriginalLoadReferences = []
          Stamp = None }

    FSharpReferencedProject.CreateFSharp(
        @"C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project329\Debug\net472\Project329.dll",
        projectOptions
    )

let memoProject329 = memoization Project329

let Project124 _ =
    let projectOptions =
        { ProjectFileName = @"C:\Users\nojaf\Projects\fsharp\tests\service\data\LargeApp\Project124\Project124.fsproj"
          ProjectId = None
          SourceFiles =
            [| @"C:\Users\nojaf\Projects\fsharp\artifacts\obj\Project124\Debug\net472\Project124.AssemblyInfo.fs"
               @"C:\Users\nojaf\Projects\fsharp\tests\service\data\LargeApp\Project124\MyModule.fs" |]
          OtherOptions =
            [| @"-o:C:\Users\nojaf\Projects\fsharp\artifacts\obj\Project124\Debug\net472\Project124.dll"
               @"-g"
               @"--debug:portable"
               @"--noframework"
               @"--define:TRACE"
               @"--define:DEBUG"
               @"--define:ENABLE_MONO_SUPPORT"
               @"--define:NETFRAMEWORK"
               @"--define:NET472"
               @"--define:NET20_OR_GREATER"
               @"--define:NET30_OR_GREATER"
               @"--define:NET35_OR_GREATER"
               @"--define:NET40_OR_GREATER"
               @"--define:NET45_OR_GREATER"
               @"--define:NET451_OR_GREATER"
               @"--define:NET452_OR_GREATER"
               @"--define:NET46_OR_GREATER"
               @"--define:NET461_OR_GREATER"
               @"--define:NET462_OR_GREATER"
               @"--define:NET47_OR_GREATER"
               @"--define:NET471_OR_GREATER"
               @"--define:NET472_OR_GREATER"
               @"--doc:C:\Users\nojaf\Projects\fsharp\artifacts\obj\Project124\Debug\net472\Project124.xml"
               @"--publicsign+"
               @"--optimize-"
               @"--tailcalls-"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\mscorlib.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project122\Debug\net472\Project122.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project125\Debug\net472\Project125.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project213\Debug\net472\Project213.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Core.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Data.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Drawing.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.IO.Compression.FileSystem.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Numerics.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Runtime.Serialization.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Xml.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Xml.Linq.dll"
               @"--target:library"
               @"--nowarn:FS2003,NU5105"
               @"--warn:3"
               @"--warnaserror:3239,1182,0025"
               @"--fullpaths"
               @"--flaterrors"
               @"--subsystemversion:6.00"
               @"--highentropyva+"
               @"--targetprofile:mscorlib"
               @"--nocopyfsharpcore"
               @"--deterministic+"
               @"--simpleresolution"
               @"--nowarn:3384" |]
          ReferencedProjects =
            [| memoProject122 "Project122"
               memoProject213 "Project213" |]
          IsIncompleteTypeCheckEnvironment = false
          UseScriptResolutionRules = false
          LoadTime = DateTime.Now
          UnresolvedReferences = None
          OriginalLoadReferences = []
          Stamp = None }

    FSharpReferencedProject.CreateFSharp(
        @"C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project124\Debug\net472\Project124.dll",
        projectOptions
    )

let memoProject124 = memoization Project124

let Project239 _ =
    let projectOptions =
        { ProjectFileName = @"C:\Users\nojaf\Projects\fsharp\tests\service\data\LargeApp\Project239\Project239.fsproj"
          ProjectId = None
          SourceFiles =
            [| @"C:\Users\nojaf\Projects\fsharp\artifacts\obj\Project239\Debug\net472\Project239.AssemblyInfo.fs"
               @"C:\Users\nojaf\Projects\fsharp\tests\service\data\LargeApp\Project239\MyModule.fs" |]
          OtherOptions =
            [| @"-o:C:\Users\nojaf\Projects\fsharp\artifacts\obj\Project239\Debug\net472\Project239.dll"
               @"-g"
               @"--debug:portable"
               @"--noframework"
               @"--define:TRACE"
               @"--define:DEBUG"
               @"--define:ENABLE_MONO_SUPPORT"
               @"--define:NETFRAMEWORK"
               @"--define:NET472"
               @"--define:NET20_OR_GREATER"
               @"--define:NET30_OR_GREATER"
               @"--define:NET35_OR_GREATER"
               @"--define:NET40_OR_GREATER"
               @"--define:NET45_OR_GREATER"
               @"--define:NET451_OR_GREATER"
               @"--define:NET452_OR_GREATER"
               @"--define:NET46_OR_GREATER"
               @"--define:NET461_OR_GREATER"
               @"--define:NET462_OR_GREATER"
               @"--define:NET47_OR_GREATER"
               @"--define:NET471_OR_GREATER"
               @"--define:NET472_OR_GREATER"
               @"--doc:C:\Users\nojaf\Projects\fsharp\artifacts\obj\Project239\Debug\net472\Project239.xml"
               @"--publicsign+"
               @"--optimize-"
               @"--tailcalls-"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\mscorlib.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project140\Debug\net472\Project140.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project142\Debug\net472\Project142.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project149\Debug\net472\Project149.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Core.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Data.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Drawing.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.IO.Compression.FileSystem.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Numerics.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Runtime.Serialization.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Xml.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Xml.Linq.dll"
               @"--target:library"
               @"--nowarn:FS2003,NU5105"
               @"--warn:3"
               @"--warnaserror:3239,1182,0025"
               @"--fullpaths"
               @"--flaterrors"
               @"--subsystemversion:6.00"
               @"--highentropyva+"
               @"--targetprofile:mscorlib"
               @"--nocopyfsharpcore"
               @"--deterministic+"
               @"--simpleresolution"
               @"--nowarn:3384" |]
          ReferencedProjects = [||]
          IsIncompleteTypeCheckEnvironment = false
          UseScriptResolutionRules = false
          LoadTime = DateTime.Now
          UnresolvedReferences = None
          OriginalLoadReferences = []
          Stamp = None }

    FSharpReferencedProject.CreateFSharp(
        @"C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project239\Debug\net472\Project239.dll",
        projectOptions
    )

let memoProject239 = memoization Project239

let Project324 _ =
    let projectOptions =
        { ProjectFileName = @"C:\Users\nojaf\Projects\fsharp\tests\service\data\LargeApp\Project324\Project324.fsproj"
          ProjectId = None
          SourceFiles =
            [| @"C:\Users\nojaf\Projects\fsharp\artifacts\obj\Project324\Debug\net472\Project324.AssemblyInfo.fs"
               @"C:\Users\nojaf\Projects\fsharp\tests\service\data\LargeApp\Project324\MyModule.fs" |]
          OtherOptions =
            [| @"-o:C:\Users\nojaf\Projects\fsharp\artifacts\obj\Project324\Debug\net472\Project324.dll"
               @"-g"
               @"--debug:portable"
               @"--noframework"
               @"--define:TRACE"
               @"--define:DEBUG"
               @"--define:ENABLE_MONO_SUPPORT"
               @"--define:NETFRAMEWORK"
               @"--define:NET472"
               @"--define:NET20_OR_GREATER"
               @"--define:NET30_OR_GREATER"
               @"--define:NET35_OR_GREATER"
               @"--define:NET40_OR_GREATER"
               @"--define:NET45_OR_GREATER"
               @"--define:NET451_OR_GREATER"
               @"--define:NET452_OR_GREATER"
               @"--define:NET46_OR_GREATER"
               @"--define:NET461_OR_GREATER"
               @"--define:NET462_OR_GREATER"
               @"--define:NET47_OR_GREATER"
               @"--define:NET471_OR_GREATER"
               @"--define:NET472_OR_GREATER"
               @"--doc:C:\Users\nojaf\Projects\fsharp\artifacts\obj\Project324\Debug\net472\Project324.xml"
               @"--publicsign+"
               @"--optimize-"
               @"--tailcalls-"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\mscorlib.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project138\Debug\net472\Project138.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project140\Debug\net472\Project140.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project142\Debug\net472\Project142.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project144\Debug\net472\Project144.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project146\Debug\net472\Project146.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project151\Debug\net472\Project151.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project164\Debug\net472\Project164.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project189\Debug\net472\Project189.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project264\Debug\net472\Project264.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project272\Debug\net472\Project272.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project323\Debug\net472\Project323.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Core.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Data.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Drawing.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.IO.Compression.FileSystem.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Numerics.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Runtime.Serialization.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Xml.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Xml.Linq.dll"
               @"--target:library"
               @"--nowarn:FS2003,NU5105"
               @"--warn:3"
               @"--warnaserror:3239,1182,0025"
               @"--fullpaths"
               @"--flaterrors"
               @"--subsystemversion:6.00"
               @"--highentropyva+"
               @"--targetprofile:mscorlib"
               @"--nocopyfsharpcore"
               @"--deterministic+"
               @"--simpleresolution"
               @"--nowarn:3384" |]
          ReferencedProjects =
            [| memoProject151 "Project151"
               memoProject264 "Project264"
               memoProject272 "Project272" |]
          IsIncompleteTypeCheckEnvironment = false
          UseScriptResolutionRules = false
          LoadTime = DateTime.Now
          UnresolvedReferences = None
          OriginalLoadReferences = []
          Stamp = None }

    FSharpReferencedProject.CreateFSharp(
        @"C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project324\Debug\net472\Project324.dll",
        projectOptions
    )

let memoProject324 = memoization Project324

let Project302 _ =
    let projectOptions =
        { ProjectFileName = @"C:\Users\nojaf\Projects\fsharp\tests\service\data\LargeApp\Project302\Project302.fsproj"
          ProjectId = None
          SourceFiles =
            [| @"C:\Users\nojaf\Projects\fsharp\artifacts\obj\Project302\Debug\net472\Project302.AssemblyInfo.fs"
               @"C:\Users\nojaf\Projects\fsharp\tests\service\data\LargeApp\Project302\MyModule.fs" |]
          OtherOptions =
            [| @"-o:C:\Users\nojaf\Projects\fsharp\artifacts\obj\Project302\Debug\net472\Project302.dll"
               @"-g"
               @"--debug:portable"
               @"--noframework"
               @"--define:TRACE"
               @"--define:DEBUG"
               @"--define:ENABLE_MONO_SUPPORT"
               @"--define:NETFRAMEWORK"
               @"--define:NET472"
               @"--define:NET20_OR_GREATER"
               @"--define:NET30_OR_GREATER"
               @"--define:NET35_OR_GREATER"
               @"--define:NET40_OR_GREATER"
               @"--define:NET45_OR_GREATER"
               @"--define:NET451_OR_GREATER"
               @"--define:NET452_OR_GREATER"
               @"--define:NET46_OR_GREATER"
               @"--define:NET461_OR_GREATER"
               @"--define:NET462_OR_GREATER"
               @"--define:NET47_OR_GREATER"
               @"--define:NET471_OR_GREATER"
               @"--define:NET472_OR_GREATER"
               @"--doc:C:\Users\nojaf\Projects\fsharp\artifacts\obj\Project302\Debug\net472\Project302.xml"
               @"--publicsign+"
               @"--optimize-"
               @"--tailcalls-"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\mscorlib.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project122\Debug\net472\Project122.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project138\Debug\net472\Project138.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project140\Debug\net472\Project140.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project146\Debug\net472\Project146.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project151\Debug\net472\Project151.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project164\Debug\net472\Project164.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project172\Debug\net472\Project172.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project189\Debug\net472\Project189.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project192\Debug\net472\Project192.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project214\Debug\net472\Project214.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project243\Debug\net472\Project243.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project264\Debug\net472\Project264.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project272\Debug\net472\Project272.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project300\Debug\net472\Project300.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project305\Debug\net472\Project305.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project323\Debug\net472\Project323.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project324\Debug\net472\Project324.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project326\Debug\net472\Project326.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project329\Debug\net472\Project329.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Core.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Data.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Drawing.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.IO.Compression.FileSystem.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Numerics.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Runtime.Serialization.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Xml.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Xml.Linq.dll"
               @"--target:library"
               @"--nowarn:FS2003,NU5105"
               @"--warn:3"
               @"--warnaserror:3239,1182,0025"
               @"--fullpaths"
               @"--flaterrors"
               @"--subsystemversion:6.00"
               @"--highentropyva+"
               @"--targetprofile:mscorlib"
               @"--nocopyfsharpcore"
               @"--deterministic+"
               @"--simpleresolution"
               @"--nowarn:3384" |]
          ReferencedProjects =
            [| memoProject122 "Project122"
               memoProject151 "Project151"
               memoProject243 "Project243"
               memoProject264 "Project264"
               memoProject272 "Project272"
               memoProject300 "Project300"
               memoProject324 "Project324"
               memoProject329 "Project329" |]
          IsIncompleteTypeCheckEnvironment = false
          UseScriptResolutionRules = false
          LoadTime = DateTime.Now
          UnresolvedReferences = None
          OriginalLoadReferences = []
          Stamp = None }

    FSharpReferencedProject.CreateFSharp(
        @"C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project302\Debug\net472\Project302.dll",
        projectOptions
    )

let memoProject302 = memoization Project302

let Project247 _ =
    let projectOptions =
        { ProjectFileName = @"C:\Users\nojaf\Projects\fsharp\tests\service\data\LargeApp\Project247\Project247.fsproj"
          ProjectId = None
          SourceFiles =
            [| @"C:\Users\nojaf\Projects\fsharp\artifacts\obj\Project247\Debug\net472\Project247.AssemblyInfo.fs"
               @"C:\Users\nojaf\Projects\fsharp\tests\service\data\LargeApp\Project247\MyModule.fs" |]
          OtherOptions =
            [| @"-o:C:\Users\nojaf\Projects\fsharp\artifacts\obj\Project247\Debug\net472\Project247.dll"
               @"-g"
               @"--debug:portable"
               @"--noframework"
               @"--define:TRACE"
               @"--define:DEBUG"
               @"--define:ENABLE_MONO_SUPPORT"
               @"--define:NETFRAMEWORK"
               @"--define:NET472"
               @"--define:NET20_OR_GREATER"
               @"--define:NET30_OR_GREATER"
               @"--define:NET35_OR_GREATER"
               @"--define:NET40_OR_GREATER"
               @"--define:NET45_OR_GREATER"
               @"--define:NET451_OR_GREATER"
               @"--define:NET452_OR_GREATER"
               @"--define:NET46_OR_GREATER"
               @"--define:NET461_OR_GREATER"
               @"--define:NET462_OR_GREATER"
               @"--define:NET47_OR_GREATER"
               @"--define:NET471_OR_GREATER"
               @"--define:NET472_OR_GREATER"
               @"--doc:C:\Users\nojaf\Projects\fsharp\artifacts\obj\Project247\Debug\net472\Project247.xml"
               @"--publicsign+"
               @"--optimize-"
               @"--tailcalls-"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\mscorlib.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Core.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Data.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Drawing.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.IO.Compression.FileSystem.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Numerics.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Runtime.Serialization.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Xml.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Xml.Linq.dll"
               @"--target:library"
               @"--nowarn:FS2003,NU5105"
               @"--warn:3"
               @"--warnaserror:3239,1182,0025"
               @"--fullpaths"
               @"--flaterrors"
               @"--subsystemversion:6.00"
               @"--highentropyva+"
               @"--targetprofile:mscorlib"
               @"--nocopyfsharpcore"
               @"--deterministic+"
               @"--simpleresolution"
               @"--nowarn:3384" |]
          ReferencedProjects = [||]
          IsIncompleteTypeCheckEnvironment = false
          UseScriptResolutionRules = false
          LoadTime = DateTime.Now
          UnresolvedReferences = None
          OriginalLoadReferences = []
          Stamp = None }

    FSharpReferencedProject.CreateFSharp(
        @"C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project247\Debug\net472\Project247.dll",
        projectOptions
    )

let memoProject247 = memoization Project247

let Project294 _ =
    let projectOptions =
        { ProjectFileName = @"C:\Users\nojaf\Projects\fsharp\tests\service\data\LargeApp\Project294\Project294.fsproj"
          ProjectId = None
          SourceFiles =
            [| @"C:\Users\nojaf\Projects\fsharp\artifacts\obj\Project294\Debug\net472\Project294.AssemblyInfo.fs"
               @"C:\Users\nojaf\Projects\fsharp\tests\service\data\LargeApp\Project294\MyModule.fs" |]
          OtherOptions =
            [| @"-o:C:\Users\nojaf\Projects\fsharp\artifacts\obj\Project294\Debug\net472\Project294.dll"
               @"-g"
               @"--debug:portable"
               @"--noframework"
               @"--define:TRACE"
               @"--define:DEBUG"
               @"--define:ENABLE_MONO_SUPPORT"
               @"--define:NETFRAMEWORK"
               @"--define:NET472"
               @"--define:NET20_OR_GREATER"
               @"--define:NET30_OR_GREATER"
               @"--define:NET35_OR_GREATER"
               @"--define:NET40_OR_GREATER"
               @"--define:NET45_OR_GREATER"
               @"--define:NET451_OR_GREATER"
               @"--define:NET452_OR_GREATER"
               @"--define:NET46_OR_GREATER"
               @"--define:NET461_OR_GREATER"
               @"--define:NET462_OR_GREATER"
               @"--define:NET47_OR_GREATER"
               @"--define:NET471_OR_GREATER"
               @"--define:NET472_OR_GREATER"
               @"--doc:C:\Users\nojaf\Projects\fsharp\artifacts\obj\Project294\Debug\net472\Project294.xml"
               @"--publicsign+"
               @"--optimize-"
               @"--tailcalls-"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\mscorlib.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project138\Debug\net472\Project138.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project140\Debug\net472\Project140.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project146\Debug\net472\Project146.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project151\Debug\net472\Project151.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project272\Debug\net472\Project272.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project298\Debug\net472\Project298.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project323\Debug\net472\Project323.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Core.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Data.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Drawing.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.IO.Compression.FileSystem.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Numerics.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Runtime.Serialization.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Xml.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Xml.Linq.dll"
               @"--target:library"
               @"--nowarn:FS2003,NU5105"
               @"--warn:3"
               @"--warnaserror:3239,1182,0025"
               @"--fullpaths"
               @"--flaterrors"
               @"--subsystemversion:6.00"
               @"--highentropyva+"
               @"--targetprofile:mscorlib"
               @"--nocopyfsharpcore"
               @"--deterministic+"
               @"--simpleresolution"
               @"--nowarn:3384" |]
          ReferencedProjects =
            [| memoProject151 "Project151"
               memoProject272 "Project272" |]
          IsIncompleteTypeCheckEnvironment = false
          UseScriptResolutionRules = false
          LoadTime = DateTime.Now
          UnresolvedReferences = None
          OriginalLoadReferences = []
          Stamp = None }

    FSharpReferencedProject.CreateFSharp(
        @"C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project294\Debug\net472\Project294.dll",
        projectOptions
    )

let memoProject294 = memoization Project294

let Project21 _ =
    let projectOptions =
        { ProjectFileName = @"C:\Users\nojaf\Projects\fsharp\tests\service\data\LargeApp\Project21\Project21.fsproj"
          ProjectId = None
          SourceFiles =
            [| @"C:\Users\nojaf\Projects\fsharp\artifacts\obj\Project21\Debug\net472\Project21.AssemblyInfo.fs"
               @"C:\Users\nojaf\Projects\fsharp\tests\service\data\LargeApp\Project21\MyModule.fs" |]
          OtherOptions =
            [| @"-o:C:\Users\nojaf\Projects\fsharp\artifacts\obj\Project21\Debug\net472\Project21.dll"
               @"-g"
               @"--debug:portable"
               @"--noframework"
               @"--define:TRACE"
               @"--define:DEBUG"
               @"--define:ENABLE_MONO_SUPPORT"
               @"--define:NETFRAMEWORK"
               @"--define:NET472"
               @"--define:NET20_OR_GREATER"
               @"--define:NET30_OR_GREATER"
               @"--define:NET35_OR_GREATER"
               @"--define:NET40_OR_GREATER"
               @"--define:NET45_OR_GREATER"
               @"--define:NET451_OR_GREATER"
               @"--define:NET452_OR_GREATER"
               @"--define:NET46_OR_GREATER"
               @"--define:NET461_OR_GREATER"
               @"--define:NET462_OR_GREATER"
               @"--define:NET47_OR_GREATER"
               @"--define:NET471_OR_GREATER"
               @"--define:NET472_OR_GREATER"
               @"--doc:C:\Users\nojaf\Projects\fsharp\artifacts\obj\Project21\Debug\net472\Project21.xml"
               @"--publicsign+"
               @"--optimize-"
               @"--tailcalls-"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\mscorlib.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project14\Debug\net472\Project14.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project140\Debug\net472\Project140.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project142\Debug\net472\Project142.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project149\Debug\net472\Project149.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project151\Debug\net472\Project151.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project161\Debug\net472\Project161.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project168\Debug\net472\Project168.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project22\Debug\net472\Project22.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project23\Debug\net472\Project23.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project24\Debug\net472\Project24.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Core.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Data.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Drawing.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.IO.Compression.FileSystem.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Numerics.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Runtime.Serialization.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Xml.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Xml.Linq.dll"
               @"--target:library"
               @"--nowarn:FS2003,NU5105"
               @"--warn:3"
               @"--warnaserror:3239,1182,0025"
               @"--fullpaths"
               @"--flaterrors"
               @"--subsystemversion:6.00"
               @"--highentropyva+"
               @"--targetprofile:mscorlib"
               @"--nocopyfsharpcore"
               @"--deterministic+"
               @"--simpleresolution"
               @"--nowarn:3384" |]
          ReferencedProjects =
            [| memoProject14 "Project14"
               memoProject151 "Project151"
               memoProject22 "Project22"
               memoProject23 "Project23"
               memoProject24 "Project24" |]
          IsIncompleteTypeCheckEnvironment = false
          UseScriptResolutionRules = false
          LoadTime = DateTime.Now
          UnresolvedReferences = None
          OriginalLoadReferences = []
          Stamp = None }

    FSharpReferencedProject.CreateFSharp(
        @"C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project21\Debug\net472\Project21.dll",
        projectOptions
    )

let memoProject21 = memoization Project21

let Project118 _ =
    let projectOptions =
        { ProjectFileName = @"C:\Users\nojaf\Projects\fsharp\tests\service\data\LargeApp\Project118\Project118.fsproj"
          ProjectId = None
          SourceFiles =
            [| @"C:\Users\nojaf\Projects\fsharp\artifacts\obj\Project118\Debug\net472\Project118.AssemblyInfo.fs"
               @"C:\Users\nojaf\Projects\fsharp\tests\service\data\LargeApp\Project118\MyModule.fs" |]
          OtherOptions =
            [| @"-o:C:\Users\nojaf\Projects\fsharp\artifacts\obj\Project118\Debug\net472\Project118.dll"
               @"-g"
               @"--debug:portable"
               @"--noframework"
               @"--define:TRACE"
               @"--define:DEBUG"
               @"--define:ENABLE_MONO_SUPPORT"
               @"--define:NETFRAMEWORK"
               @"--define:NET472"
               @"--define:NET20_OR_GREATER"
               @"--define:NET30_OR_GREATER"
               @"--define:NET35_OR_GREATER"
               @"--define:NET40_OR_GREATER"
               @"--define:NET45_OR_GREATER"
               @"--define:NET451_OR_GREATER"
               @"--define:NET452_OR_GREATER"
               @"--define:NET46_OR_GREATER"
               @"--define:NET461_OR_GREATER"
               @"--define:NET462_OR_GREATER"
               @"--define:NET47_OR_GREATER"
               @"--define:NET471_OR_GREATER"
               @"--define:NET472_OR_GREATER"
               @"--doc:C:\Users\nojaf\Projects\fsharp\artifacts\obj\Project118\Debug\net472\Project118.xml"
               @"--publicsign+"
               @"--optimize-"
               @"--tailcalls-"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\mscorlib.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project114\Debug\net472\Project114.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project116\Debug\net472\Project116.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project122\Debug\net472\Project122.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project138\Debug\net472\Project138.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project140\Debug\net472\Project140.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project142\Debug\net472\Project142.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project146\Debug\net472\Project146.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project149\Debug\net472\Project149.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project151\Debug\net472\Project151.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project16\Debug\net472\Project16.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project161\Debug\net472\Project161.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project164\Debug\net472\Project164.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project168\Debug\net472\Project168.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project17\Debug\net472\Project17.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project184\Debug\net472\Project184.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project189\Debug\net472\Project189.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project190\Debug\net472\Project190.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project213\Debug\net472\Project213.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project22\Debug\net472\Project22.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project23\Debug\net472\Project23.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project243\Debug\net472\Project243.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project264\Debug\net472\Project264.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project272\Debug\net472\Project272.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project323\Debug\net472\Project323.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project326\Debug\net472\Project326.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project327\Debug\net472\Project327.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project329\Debug\net472\Project329.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project40\Debug\net472\Project40.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project44\Debug\net472\Project44.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project47\Debug\net472\Project47.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Core.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Data.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Drawing.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.IO.Compression.FileSystem.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Numerics.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Runtime.Serialization.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Xml.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Xml.Linq.dll"
               @"--target:library"
               @"--nowarn:FS2003,NU5105"
               @"--warn:3"
               @"--warnaserror:3239,1182,0025"
               @"--fullpaths"
               @"--flaterrors"
               @"--subsystemversion:6.00"
               @"--highentropyva+"
               @"--targetprofile:mscorlib"
               @"--nocopyfsharpcore"
               @"--deterministic+"
               @"--simpleresolution"
               @"--nowarn:3384" |]
          ReferencedProjects =
            [| memoProject114 "Project114"
               memoProject116 "Project116"
               memoProject122 "Project122"
               memoProject151 "Project151"
               memoProject16 "Project16"
               memoProject17 "Project17"
               memoProject213 "Project213"
               memoProject22 "Project22"
               memoProject23 "Project23"
               memoProject243 "Project243"
               memoProject264 "Project264"
               memoProject272 "Project272"
               memoProject329 "Project329"
               memoProject40 "Project40"
               memoProject44 "Project44"
               memoProject47 "Project47" |]
          IsIncompleteTypeCheckEnvironment = false
          UseScriptResolutionRules = false
          LoadTime = DateTime.Now
          UnresolvedReferences = None
          OriginalLoadReferences = []
          Stamp = None }

    FSharpReferencedProject.CreateFSharp(
        @"C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project118\Debug\net472\Project118.dll",
        projectOptions
    )

let memoProject118 = memoization Project118

let Project114 _ =
    let projectOptions =
        { ProjectFileName = @"C:\Users\nojaf\Projects\fsharp\tests\service\data\LargeApp\Project114\Project114.fsproj"
          ProjectId = None
          SourceFiles =
            [| @"C:\Users\nojaf\Projects\fsharp\artifacts\obj\Project114\Debug\net472\Project114.AssemblyInfo.fs"
               @"C:\Users\nojaf\Projects\fsharp\tests\service\data\LargeApp\Project114\MyModule.fs" |]
          OtherOptions =
            [| @"-o:C:\Users\nojaf\Projects\fsharp\artifacts\obj\Project114\Debug\net472\Project114.dll"
               @"-g"
               @"--debug:portable"
               @"--noframework"
               @"--define:TRACE"
               @"--define:DEBUG"
               @"--define:ENABLE_MONO_SUPPORT"
               @"--define:NETFRAMEWORK"
               @"--define:NET472"
               @"--define:NET20_OR_GREATER"
               @"--define:NET30_OR_GREATER"
               @"--define:NET35_OR_GREATER"
               @"--define:NET40_OR_GREATER"
               @"--define:NET45_OR_GREATER"
               @"--define:NET451_OR_GREATER"
               @"--define:NET452_OR_GREATER"
               @"--define:NET46_OR_GREATER"
               @"--define:NET461_OR_GREATER"
               @"--define:NET462_OR_GREATER"
               @"--define:NET47_OR_GREATER"
               @"--define:NET471_OR_GREATER"
               @"--define:NET472_OR_GREATER"
               @"--doc:C:\Users\nojaf\Projects\fsharp\artifacts\obj\Project114\Debug\net472\Project114.xml"
               @"--publicsign+"
               @"--optimize-"
               @"--tailcalls-"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\mscorlib.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project115\Debug\net472\Project115.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project116\Debug\net472\Project116.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project117\Debug\net472\Project117.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project14\Debug\net472\Project14.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project140\Debug\net472\Project140.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project142\Debug\net472\Project142.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project146\Debug\net472\Project146.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project149\Debug\net472\Project149.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project151\Debug\net472\Project151.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project161\Debug\net472\Project161.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project164\Debug\net472\Project164.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project168\Debug\net472\Project168.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project17\Debug\net472\Project17.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project19\Debug\net472\Project19.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project21\Debug\net472\Project21.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project214\Debug\net472\Project214.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project22\Debug\net472\Project22.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project23\Debug\net472\Project23.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project24\Debug\net472\Project24.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project243\Debug\net472\Project243.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project264\Debug\net472\Project264.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project272\Debug\net472\Project272.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project295\Debug\net472\Project295.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project298\Debug\net472\Project298.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project323\Debug\net472\Project323.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project326\Debug\net472\Project326.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Core.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Data.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Drawing.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.IO.Compression.FileSystem.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Numerics.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Runtime.Serialization.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Xml.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Xml.Linq.dll"
               @"--target:library"
               @"--nowarn:FS2003,NU5105"
               @"--warn:3"
               @"--warnaserror:3239,1182,0025"
               @"--fullpaths"
               @"--flaterrors"
               @"--subsystemversion:6.00"
               @"--highentropyva+"
               @"--targetprofile:mscorlib"
               @"--nocopyfsharpcore"
               @"--deterministic+"
               @"--simpleresolution"
               @"--nowarn:3384" |]
          ReferencedProjects =
            [| memoProject115 "Project115"
               memoProject116 "Project116"
               memoProject117 "Project117"
               memoProject14 "Project14"
               memoProject151 "Project151"
               memoProject17 "Project17"
               memoProject19 "Project19"
               memoProject21 "Project21"
               memoProject22 "Project22"
               memoProject23 "Project23"
               memoProject243 "Project243"
               memoProject24 "Project24"
               memoProject264 "Project264"
               memoProject272 "Project272" |]
          IsIncompleteTypeCheckEnvironment = false
          UseScriptResolutionRules = false
          LoadTime = DateTime.Now
          UnresolvedReferences = None
          OriginalLoadReferences = []
          Stamp = None }

    FSharpReferencedProject.CreateFSharp(
        @"C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project114\Debug\net472\Project114.dll",
        projectOptions
    )

let memoProject114 = memoization Project114

let Project115 _ =
    let projectOptions =
        { ProjectFileName = @"C:\Users\nojaf\Projects\fsharp\tests\service\data\LargeApp\Project115\Project115.fsproj"
          ProjectId = None
          SourceFiles =
            [| @"C:\Users\nojaf\Projects\fsharp\artifacts\obj\Project115\Debug\net472\Project115.AssemblyInfo.fs"
               @"C:\Users\nojaf\Projects\fsharp\tests\service\data\LargeApp\Project115\MyModule.fs" |]
          OtherOptions =
            [| @"-o:C:\Users\nojaf\Projects\fsharp\artifacts\obj\Project115\Debug\net472\Project115.dll"
               @"-g"
               @"--debug:portable"
               @"--noframework"
               @"--define:TRACE"
               @"--define:DEBUG"
               @"--define:ENABLE_MONO_SUPPORT"
               @"--define:NETFRAMEWORK"
               @"--define:NET472"
               @"--define:NET20_OR_GREATER"
               @"--define:NET30_OR_GREATER"
               @"--define:NET35_OR_GREATER"
               @"--define:NET40_OR_GREATER"
               @"--define:NET45_OR_GREATER"
               @"--define:NET451_OR_GREATER"
               @"--define:NET452_OR_GREATER"
               @"--define:NET46_OR_GREATER"
               @"--define:NET461_OR_GREATER"
               @"--define:NET462_OR_GREATER"
               @"--define:NET47_OR_GREATER"
               @"--define:NET471_OR_GREATER"
               @"--define:NET472_OR_GREATER"
               @"--doc:C:\Users\nojaf\Projects\fsharp\artifacts\obj\Project115\Debug\net472\Project115.xml"
               @"--publicsign+"
               @"--optimize-"
               @"--tailcalls-"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\mscorlib.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project117\Debug\net472\Project117.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project14\Debug\net472\Project14.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project149\Debug\net472\Project149.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project24\Debug\net472\Project24.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Core.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Data.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Drawing.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.IO.Compression.FileSystem.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Numerics.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Runtime.Serialization.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Xml.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Xml.Linq.dll"
               @"--target:library"
               @"--nowarn:FS2003,NU5105"
               @"--warn:3"
               @"--warnaserror:3239,1182,0025"
               @"--fullpaths"
               @"--flaterrors"
               @"--subsystemversion:6.00"
               @"--highentropyva+"
               @"--targetprofile:mscorlib"
               @"--nocopyfsharpcore"
               @"--deterministic+"
               @"--simpleresolution"
               @"--nowarn:3384" |]
          ReferencedProjects =
            [| memoProject117 "Project117"
               memoProject14 "Project14"
               memoProject24 "Project24" |]
          IsIncompleteTypeCheckEnvironment = false
          UseScriptResolutionRules = false
          LoadTime = DateTime.Now
          UnresolvedReferences = None
          OriginalLoadReferences = []
          Stamp = None }

    FSharpReferencedProject.CreateFSharp(
        @"C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project115\Debug\net472\Project115.dll",
        projectOptions
    )

let memoProject115 = memoization Project115

let Project19 _ =
    let projectOptions =
        { ProjectFileName = @"C:\Users\nojaf\Projects\fsharp\tests\service\data\LargeApp\Project19\Project19.fsproj"
          ProjectId = None
          SourceFiles =
            [| @"C:\Users\nojaf\Projects\fsharp\artifacts\obj\Project19\Debug\net472\Project19.AssemblyInfo.fs"
               @"C:\Users\nojaf\Projects\fsharp\tests\service\data\LargeApp\Project19\MyModule.fs" |]
          OtherOptions =
            [| @"-o:C:\Users\nojaf\Projects\fsharp\artifacts\obj\Project19\Debug\net472\Project19.dll"
               @"-g"
               @"--debug:portable"
               @"--noframework"
               @"--define:TRACE"
               @"--define:DEBUG"
               @"--define:ENABLE_MONO_SUPPORT"
               @"--define:NETFRAMEWORK"
               @"--define:NET472"
               @"--define:NET20_OR_GREATER"
               @"--define:NET30_OR_GREATER"
               @"--define:NET35_OR_GREATER"
               @"--define:NET40_OR_GREATER"
               @"--define:NET45_OR_GREATER"
               @"--define:NET451_OR_GREATER"
               @"--define:NET452_OR_GREATER"
               @"--define:NET46_OR_GREATER"
               @"--define:NET461_OR_GREATER"
               @"--define:NET462_OR_GREATER"
               @"--define:NET47_OR_GREATER"
               @"--define:NET471_OR_GREATER"
               @"--define:NET472_OR_GREATER"
               @"--doc:C:\Users\nojaf\Projects\fsharp\artifacts\obj\Project19\Debug\net472\Project19.xml"
               @"--publicsign+"
               @"--optimize-"
               @"--tailcalls-"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\mscorlib.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project11\Debug\net472\Project11.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project14\Debug\net472\Project14.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project140\Debug\net472\Project140.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project142\Debug\net472\Project142.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project146\Debug\net472\Project146.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project149\Debug\net472\Project149.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project151\Debug\net472\Project151.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project161\Debug\net472\Project161.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project168\Debug\net472\Project168.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project189\Debug\net472\Project189.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project21\Debug\net472\Project21.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project213\Debug\net472\Project213.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project22\Debug\net472\Project22.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project23\Debug\net472\Project23.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project24\Debug\net472\Project24.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project243\Debug\net472\Project243.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project264\Debug\net472\Project264.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project272\Debug\net472\Project272.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project329\Debug\net472\Project329.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Core.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Data.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Drawing.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.IO.Compression.FileSystem.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Numerics.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Runtime.Serialization.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Xml.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Xml.Linq.dll"
               @"--target:library"
               @"--nowarn:FS2003,NU5105"
               @"--warn:3"
               @"--warnaserror:3239,1182,0025"
               @"--fullpaths"
               @"--flaterrors"
               @"--subsystemversion:6.00"
               @"--highentropyva+"
               @"--targetprofile:mscorlib"
               @"--nocopyfsharpcore"
               @"--deterministic+"
               @"--simpleresolution"
               @"--nowarn:3384" |]
          ReferencedProjects =
            [| memoProject11 "Project11"
               memoProject14 "Project14"
               memoProject151 "Project151"
               memoProject213 "Project213"
               memoProject21 "Project21"
               memoProject22 "Project22"
               memoProject23 "Project23"
               memoProject243 "Project243"
               memoProject24 "Project24"
               memoProject264 "Project264"
               memoProject272 "Project272"
               memoProject329 "Project329" |]
          IsIncompleteTypeCheckEnvironment = false
          UseScriptResolutionRules = false
          LoadTime = DateTime.Now
          UnresolvedReferences = None
          OriginalLoadReferences = []
          Stamp = None }

    FSharpReferencedProject.CreateFSharp(
        @"C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project19\Debug\net472\Project19.dll",
        projectOptions
    )

let memoProject19 = memoization Project19

let Project16 _ =
    let projectOptions =
        { ProjectFileName = @"C:\Users\nojaf\Projects\fsharp\tests\service\data\LargeApp\Project16\Project16.fsproj"
          ProjectId = None
          SourceFiles =
            [| @"C:\Users\nojaf\Projects\fsharp\artifacts\obj\Project16\Debug\net472\Project16.AssemblyInfo.fs"
               @"C:\Users\nojaf\Projects\fsharp\tests\service\data\LargeApp\Project16\MyModule.fs" |]
          OtherOptions =
            [| @"-o:C:\Users\nojaf\Projects\fsharp\artifacts\obj\Project16\Debug\net472\Project16.dll"
               @"-g"
               @"--debug:portable"
               @"--noframework"
               @"--define:TRACE"
               @"--define:DEBUG"
               @"--define:ENABLE_MONO_SUPPORT"
               @"--define:NETFRAMEWORK"
               @"--define:NET472"
               @"--define:NET20_OR_GREATER"
               @"--define:NET30_OR_GREATER"
               @"--define:NET35_OR_GREATER"
               @"--define:NET40_OR_GREATER"
               @"--define:NET45_OR_GREATER"
               @"--define:NET451_OR_GREATER"
               @"--define:NET452_OR_GREATER"
               @"--define:NET46_OR_GREATER"
               @"--define:NET461_OR_GREATER"
               @"--define:NET462_OR_GREATER"
               @"--define:NET47_OR_GREATER"
               @"--define:NET471_OR_GREATER"
               @"--define:NET472_OR_GREATER"
               @"--doc:C:\Users\nojaf\Projects\fsharp\artifacts\obj\Project16\Debug\net472\Project16.xml"
               @"--publicsign+"
               @"--optimize-"
               @"--tailcalls-"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\mscorlib.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project151\Debug\net472\Project151.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project264\Debug\net472\Project264.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project326\Debug\net472\Project326.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Core.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Data.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Drawing.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.IO.Compression.FileSystem.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Numerics.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Runtime.Serialization.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Xml.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Xml.Linq.dll"
               @"--target:library"
               @"--nowarn:FS2003,NU5105"
               @"--warn:3"
               @"--warnaserror:3239,1182,0025"
               @"--fullpaths"
               @"--flaterrors"
               @"--subsystemversion:6.00"
               @"--highentropyva+"
               @"--targetprofile:mscorlib"
               @"--nocopyfsharpcore"
               @"--deterministic+"
               @"--simpleresolution"
               @"--nowarn:3384" |]
          ReferencedProjects =
            [| memoProject151 "Project151"
               memoProject264 "Project264" |]
          IsIncompleteTypeCheckEnvironment = false
          UseScriptResolutionRules = false
          LoadTime = DateTime.Now
          UnresolvedReferences = None
          OriginalLoadReferences = []
          Stamp = None }

    FSharpReferencedProject.CreateFSharp(
        @"C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project16\Debug\net472\Project16.dll",
        projectOptions
    )

let memoProject16 = memoization Project16

let Project40 _ =
    let projectOptions =
        { ProjectFileName = @"C:\Users\nojaf\Projects\fsharp\tests\service\data\LargeApp\Project40\Project40.fsproj"
          ProjectId = None
          SourceFiles =
            [| @"C:\Users\nojaf\Projects\fsharp\artifacts\obj\Project40\Debug\net472\Project40.AssemblyInfo.fs"
               @"C:\Users\nojaf\Projects\fsharp\tests\service\data\LargeApp\Project40\MyModule.fs" |]
          OtherOptions =
            [| @"-o:C:\Users\nojaf\Projects\fsharp\artifacts\obj\Project40\Debug\net472\Project40.dll"
               @"-g"
               @"--debug:portable"
               @"--noframework"
               @"--define:TRACE"
               @"--define:DEBUG"
               @"--define:ENABLE_MONO_SUPPORT"
               @"--define:NETFRAMEWORK"
               @"--define:NET472"
               @"--define:NET20_OR_GREATER"
               @"--define:NET30_OR_GREATER"
               @"--define:NET35_OR_GREATER"
               @"--define:NET40_OR_GREATER"
               @"--define:NET45_OR_GREATER"
               @"--define:NET451_OR_GREATER"
               @"--define:NET452_OR_GREATER"
               @"--define:NET46_OR_GREATER"
               @"--define:NET461_OR_GREATER"
               @"--define:NET462_OR_GREATER"
               @"--define:NET47_OR_GREATER"
               @"--define:NET471_OR_GREATER"
               @"--define:NET472_OR_GREATER"
               @"--doc:C:\Users\nojaf\Projects\fsharp\artifacts\obj\Project40\Debug\net472\Project40.xml"
               @"--publicsign+"
               @"--optimize-"
               @"--tailcalls-"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\mscorlib.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project11\Debug\net472\Project11.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project116\Debug\net472\Project116.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project12\Debug\net472\Project12.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project122\Debug\net472\Project122.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project138\Debug\net472\Project138.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project14\Debug\net472\Project14.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project140\Debug\net472\Project140.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project142\Debug\net472\Project142.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project144\Debug\net472\Project144.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project146\Debug\net472\Project146.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project149\Debug\net472\Project149.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project15\Debug\net472\Project15.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project151\Debug\net472\Project151.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project16\Debug\net472\Project16.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project161\Debug\net472\Project161.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project164\Debug\net472\Project164.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project168\Debug\net472\Project168.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project17\Debug\net472\Project17.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project172\Debug\net472\Project172.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project184\Debug\net472\Project184.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project189\Debug\net472\Project189.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project19\Debug\net472\Project19.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project190\Debug\net472\Project190.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project206\Debug\net472\Project206.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project213\Debug\net472\Project213.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project214\Debug\net472\Project214.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project22\Debug\net472\Project22.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project222\Debug\net472\Project222.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project23\Debug\net472\Project23.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project24\Debug\net472\Project24.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project243\Debug\net472\Project243.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project264\Debug\net472\Project264.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project272\Debug\net472\Project272.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project295\Debug\net472\Project295.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project297\Debug\net472\Project297.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project323\Debug\net472\Project323.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project326\Debug\net472\Project326.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project327\Debug\net472\Project327.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project329\Debug\net472\Project329.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project338\Debug\net472\Project338.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project339\Debug\net472\Project339.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project344\Debug\net472\Project344.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project346\Debug\net472\Project346.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project41\Debug\net472\Project41.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project43\Debug\net472\Project43.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project44\Debug\net472\Project44.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project45\Debug\net472\Project45.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project47\Debug\net472\Project47.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project98\Debug\net472\Project98.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Core.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Data.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Drawing.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.IO.Compression.FileSystem.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Numerics.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Runtime.Serialization.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Xml.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Xml.Linq.dll"
               @"--target:library"
               @"--nowarn:FS2003,NU5105"
               @"--warn:3"
               @"--warnaserror:3239,1182,0025"
               @"--fullpaths"
               @"--flaterrors"
               @"--subsystemversion:6.00"
               @"--highentropyva+"
               @"--targetprofile:mscorlib"
               @"--nocopyfsharpcore"
               @"--deterministic+"
               @"--simpleresolution"
               @"--nowarn:3384" |]
          ReferencedProjects =
            [| memoProject116 "Project116"
               memoProject11 "Project11"
               memoProject122 "Project122"
               memoProject12 "Project12"
               memoProject14 "Project14"
               memoProject151 "Project151"
               memoProject15 "Project15"
               memoProject16 "Project16"
               memoProject17 "Project17"
               memoProject19 "Project19"
               memoProject206 "Project206"
               memoProject213 "Project213"
               memoProject222 "Project222"
               memoProject22 "Project22"
               memoProject23 "Project23"
               memoProject243 "Project243"
               memoProject24 "Project24"
               memoProject264 "Project264"
               memoProject272 "Project272"
               memoProject329 "Project329"
               memoProject338 "Project338"
               memoProject339 "Project339"
               memoProject346 "Project346"
               memoProject43 "Project43"
               memoProject44 "Project44"
               memoProject45 "Project45"
               memoProject47 "Project47"
               memoProject98 "Project98" |]
          IsIncompleteTypeCheckEnvironment = false
          UseScriptResolutionRules = false
          LoadTime = DateTime.Now
          UnresolvedReferences = None
          OriginalLoadReferences = []
          Stamp = None }

    FSharpReferencedProject.CreateFSharp(
        @"C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project40\Debug\net472\Project40.dll",
        projectOptions
    )

let memoProject40 = memoization Project40

let Project12 _ =
    let projectOptions =
        { ProjectFileName = @"C:\Users\nojaf\Projects\fsharp\tests\service\data\LargeApp\Project12\Project12.fsproj"
          ProjectId = None
          SourceFiles =
            [| @"C:\Users\nojaf\Projects\fsharp\artifacts\obj\Project12\Debug\net472\Project12.AssemblyInfo.fs"
               @"C:\Users\nojaf\Projects\fsharp\tests\service\data\LargeApp\Project12\MyModule.fs" |]
          OtherOptions =
            [| @"-o:C:\Users\nojaf\Projects\fsharp\artifacts\obj\Project12\Debug\net472\Project12.dll"
               @"-g"
               @"--debug:portable"
               @"--noframework"
               @"--define:TRACE"
               @"--define:DEBUG"
               @"--define:ENABLE_MONO_SUPPORT"
               @"--define:NETFRAMEWORK"
               @"--define:NET472"
               @"--define:NET20_OR_GREATER"
               @"--define:NET30_OR_GREATER"
               @"--define:NET35_OR_GREATER"
               @"--define:NET40_OR_GREATER"
               @"--define:NET45_OR_GREATER"
               @"--define:NET451_OR_GREATER"
               @"--define:NET452_OR_GREATER"
               @"--define:NET46_OR_GREATER"
               @"--define:NET461_OR_GREATER"
               @"--define:NET462_OR_GREATER"
               @"--define:NET47_OR_GREATER"
               @"--define:NET471_OR_GREATER"
               @"--define:NET472_OR_GREATER"
               @"--doc:C:\Users\nojaf\Projects\fsharp\artifacts\obj\Project12\Debug\net472\Project12.xml"
               @"--publicsign+"
               @"--optimize-"
               @"--tailcalls-"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\mscorlib.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project11\Debug\net472\Project11.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project138\Debug\net472\Project138.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project140\Debug\net472\Project140.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project142\Debug\net472\Project142.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project144\Debug\net472\Project144.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project146\Debug\net472\Project146.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project149\Debug\net472\Project149.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project151\Debug\net472\Project151.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project161\Debug\net472\Project161.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project164\Debug\net472\Project164.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project168\Debug\net472\Project168.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project17\Debug\net472\Project17.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project172\Debug\net472\Project172.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project189\Debug\net472\Project189.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project192\Debug\net472\Project192.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project213\Debug\net472\Project213.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project214\Debug\net472\Project214.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project22\Debug\net472\Project22.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project222\Debug\net472\Project222.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project232\Debug\net472\Project232.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project243\Debug\net472\Project243.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project250\Debug\net472\Project250.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project252\Debug\net472\Project252.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project257\Debug\net472\Project257.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project264\Debug\net472\Project264.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project272\Debug\net472\Project272.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project295\Debug\net472\Project295.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project297\Debug\net472\Project297.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project319\Debug\net472\Project319.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project323\Debug\net472\Project323.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project326\Debug\net472\Project326.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project329\Debug\net472\Project329.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project331\Debug\net472\Project331.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project339\Debug\net472\Project339.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project340\Debug\net472\Project340.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project344\Debug\net472\Project344.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project346\Debug\net472\Project346.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project45\Debug\net472\Project45.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project47\Debug\net472\Project47.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project70\Debug\net472\Project70.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project71\Debug\net472\Project71.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project85\Debug\net472\Project85.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Core.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Data.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Drawing.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.IO.Compression.FileSystem.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Numerics.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Runtime.Serialization.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Xml.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Xml.Linq.dll"
               @"--target:library"
               @"--nowarn:FS2003,NU5105"
               @"--warn:3"
               @"--warnaserror:3239,1182,0025"
               @"--fullpaths"
               @"--flaterrors"
               @"--subsystemversion:6.00"
               @"--highentropyva+"
               @"--targetprofile:mscorlib"
               @"--nocopyfsharpcore"
               @"--deterministic+"
               @"--simpleresolution"
               @"--nowarn:3384" |]
          ReferencedProjects =
            [| memoProject11 "Project11"
               memoProject151 "Project151"
               memoProject17 "Project17"
               memoProject213 "Project213"
               memoProject222 "Project222"
               memoProject22 "Project22"
               memoProject232 "Project232"
               memoProject243 "Project243"
               memoProject250 "Project250"
               memoProject252 "Project252"
               memoProject257 "Project257"
               memoProject264 "Project264"
               memoProject272 "Project272"
               memoProject319 "Project319"
               memoProject329 "Project329"
               memoProject331 "Project331"
               memoProject339 "Project339"
               memoProject346 "Project346"
               memoProject45 "Project45"
               memoProject47 "Project47"
               memoProject70 "Project70"
               memoProject71 "Project71"
               memoProject85 "Project85" |]
          IsIncompleteTypeCheckEnvironment = false
          UseScriptResolutionRules = false
          LoadTime = DateTime.Now
          UnresolvedReferences = None
          OriginalLoadReferences = []
          Stamp = None }

    FSharpReferencedProject.CreateFSharp(
        @"C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project12\Debug\net472\Project12.dll",
        projectOptions
    )

let memoProject12 = memoization Project12

let Project232 _ =
    let projectOptions =
        { ProjectFileName = @"C:\Users\nojaf\Projects\fsharp\tests\service\data\LargeApp\Project232\Project232.fsproj"
          ProjectId = None
          SourceFiles =
            [| @"C:\Users\nojaf\Projects\fsharp\artifacts\obj\Project232\Debug\net472\Project232.AssemblyInfo.fs"
               @"C:\Users\nojaf\Projects\fsharp\tests\service\data\LargeApp\Project232\MyModule.fs" |]
          OtherOptions =
            [| @"-o:C:\Users\nojaf\Projects\fsharp\artifacts\obj\Project232\Debug\net472\Project232.dll"
               @"-g"
               @"--debug:portable"
               @"--noframework"
               @"--define:TRACE"
               @"--define:DEBUG"
               @"--define:ENABLE_MONO_SUPPORT"
               @"--define:NETFRAMEWORK"
               @"--define:NET472"
               @"--define:NET20_OR_GREATER"
               @"--define:NET30_OR_GREATER"
               @"--define:NET35_OR_GREATER"
               @"--define:NET40_OR_GREATER"
               @"--define:NET45_OR_GREATER"
               @"--define:NET451_OR_GREATER"
               @"--define:NET452_OR_GREATER"
               @"--define:NET46_OR_GREATER"
               @"--define:NET461_OR_GREATER"
               @"--define:NET462_OR_GREATER"
               @"--define:NET47_OR_GREATER"
               @"--define:NET471_OR_GREATER"
               @"--define:NET472_OR_GREATER"
               @"--doc:C:\Users\nojaf\Projects\fsharp\artifacts\obj\Project232\Debug\net472\Project232.xml"
               @"--publicsign+"
               @"--optimize-"
               @"--tailcalls-"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\mscorlib.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project138\Debug\net472\Project138.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project140\Debug\net472\Project140.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project142\Debug\net472\Project142.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project144\Debug\net472\Project144.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project146\Debug\net472\Project146.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project149\Debug\net472\Project149.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project151\Debug\net472\Project151.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project157\Debug\net472\Project157.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project161\Debug\net472\Project161.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project164\Debug\net472\Project164.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project168\Debug\net472\Project168.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project172\Debug\net472\Project172.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project184\Debug\net472\Project184.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project189\Debug\net472\Project189.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project192\Debug\net472\Project192.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project213\Debug\net472\Project213.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project214\Debug\net472\Project214.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project222\Debug\net472\Project222.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project224\Debug\net472\Project224.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project243\Debug\net472\Project243.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project250\Debug\net472\Project250.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project252\Debug\net472\Project252.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project255\Debug\net472\Project255.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project264\Debug\net472\Project264.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project268\Debug\net472\Project268.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project272\Debug\net472\Project272.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project273\Debug\net472\Project273.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project283\Debug\net472\Project283.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project292\Debug\net472\Project292.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project295\Debug\net472\Project295.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project297\Debug\net472\Project297.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project302\Debug\net472\Project302.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project305\Debug\net472\Project305.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project310\Debug\net472\Project310.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project317\Debug\net472\Project317.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project323\Debug\net472\Project323.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project326\Debug\net472\Project326.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project329\Debug\net472\Project329.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project331\Debug\net472\Project331.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project338\Debug\net472\Project338.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project339\Debug\net472\Project339.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project340\Debug\net472\Project340.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project341\Debug\net472\Project341.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project342\Debug\net472\Project342.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project344\Debug\net472\Project344.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project346\Debug\net472\Project346.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project84\Debug\net472\Project84.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project87\Debug\net472\Project87.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Core.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Data.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Drawing.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.IO.Compression.FileSystem.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Numerics.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Runtime.Serialization.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Xml.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Xml.Linq.dll"
               @"--target:library"
               @"--nowarn:FS2003,NU5105"
               @"--warn:3"
               @"--warnaserror:3239,1182,0025"
               @"--fullpaths"
               @"--flaterrors"
               @"--subsystemversion:6.00"
               @"--highentropyva+"
               @"--targetprofile:mscorlib"
               @"--nocopyfsharpcore"
               @"--deterministic+"
               @"--simpleresolution"
               @"--nowarn:3384" |]
          ReferencedProjects =
            [| memoProject151 "Project151"
               memoProject213 "Project213"
               memoProject222 "Project222"
               memoProject243 "Project243"
               memoProject250 "Project250"
               memoProject252 "Project252"
               memoProject255 "Project255"
               memoProject264 "Project264"
               memoProject268 "Project268"
               memoProject272 "Project272"
               memoProject273 "Project273"
               memoProject283 "Project283"
               memoProject292 "Project292"
               memoProject302 "Project302"
               memoProject310 "Project310"
               memoProject329 "Project329"
               memoProject331 "Project331"
               memoProject338 "Project338"
               memoProject339 "Project339"
               memoProject346 "Project346"
               memoProject84 "Project84" |]
          IsIncompleteTypeCheckEnvironment = false
          UseScriptResolutionRules = false
          LoadTime = DateTime.Now
          UnresolvedReferences = None
          OriginalLoadReferences = []
          Stamp = None }

    FSharpReferencedProject.CreateFSharp(
        @"C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project232\Debug\net472\Project232.dll",
        projectOptions
    )

let memoProject232 = memoization Project232

let Project252 _ =
    let projectOptions =
        { ProjectFileName = @"C:\Users\nojaf\Projects\fsharp\tests\service\data\LargeApp\Project252\Project252.fsproj"
          ProjectId = None
          SourceFiles =
            [| @"C:\Users\nojaf\Projects\fsharp\artifacts\obj\Project252\Debug\net472\Project252.AssemblyInfo.fs"
               @"C:\Users\nojaf\Projects\fsharp\tests\service\data\LargeApp\Project252\MyModule.fs" |]
          OtherOptions =
            [| @"-o:C:\Users\nojaf\Projects\fsharp\artifacts\obj\Project252\Debug\net472\Project252.dll"
               @"-g"
               @"--debug:portable"
               @"--noframework"
               @"--define:TRACE"
               @"--define:DEBUG"
               @"--define:ENABLE_MONO_SUPPORT"
               @"--define:NETFRAMEWORK"
               @"--define:NET472"
               @"--define:NET20_OR_GREATER"
               @"--define:NET30_OR_GREATER"
               @"--define:NET35_OR_GREATER"
               @"--define:NET40_OR_GREATER"
               @"--define:NET45_OR_GREATER"
               @"--define:NET451_OR_GREATER"
               @"--define:NET452_OR_GREATER"
               @"--define:NET46_OR_GREATER"
               @"--define:NET461_OR_GREATER"
               @"--define:NET462_OR_GREATER"
               @"--define:NET47_OR_GREATER"
               @"--define:NET471_OR_GREATER"
               @"--define:NET472_OR_GREATER"
               @"--doc:C:\Users\nojaf\Projects\fsharp\artifacts\obj\Project252\Debug\net472\Project252.xml"
               @"--publicsign+"
               @"--optimize-"
               @"--tailcalls-"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\mscorlib.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project142\Debug\net472\Project142.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project190\Debug\net472\Project190.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project264\Debug\net472\Project264.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project319\Debug\net472\Project319.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project326\Debug\net472\Project326.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project346\Debug\net472\Project346.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Core.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Data.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Drawing.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.IO.Compression.FileSystem.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Numerics.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Runtime.Serialization.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Xml.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Xml.Linq.dll"
               @"--target:library"
               @"--nowarn:FS2003,NU5105"
               @"--warn:3"
               @"--warnaserror:3239,1182,0025"
               @"--fullpaths"
               @"--flaterrors"
               @"--subsystemversion:6.00"
               @"--highentropyva+"
               @"--targetprofile:mscorlib"
               @"--nocopyfsharpcore"
               @"--deterministic+"
               @"--simpleresolution"
               @"--nowarn:3384" |]
          ReferencedProjects =
            [| memoProject264 "Project264"
               memoProject319 "Project319"
               memoProject346 "Project346" |]
          IsIncompleteTypeCheckEnvironment = false
          UseScriptResolutionRules = false
          LoadTime = DateTime.Now
          UnresolvedReferences = None
          OriginalLoadReferences = []
          Stamp = None }

    FSharpReferencedProject.CreateFSharp(
        @"C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project252\Debug\net472\Project252.dll",
        projectOptions
    )

let memoProject252 = memoization Project252

let Project255 _ =
    let projectOptions =
        { ProjectFileName = @"C:\Users\nojaf\Projects\fsharp\tests\service\data\LargeApp\Project255\Project255.fsproj"
          ProjectId = None
          SourceFiles =
            [| @"C:\Users\nojaf\Projects\fsharp\artifacts\obj\Project255\Debug\net472\Project255.AssemblyInfo.fs"
               @"C:\Users\nojaf\Projects\fsharp\tests\service\data\LargeApp\Project255\MyModule.fs" |]
          OtherOptions =
            [| @"-o:C:\Users\nojaf\Projects\fsharp\artifacts\obj\Project255\Debug\net472\Project255.dll"
               @"-g"
               @"--debug:portable"
               @"--noframework"
               @"--define:TRACE"
               @"--define:DEBUG"
               @"--define:ENABLE_MONO_SUPPORT"
               @"--define:NETFRAMEWORK"
               @"--define:NET472"
               @"--define:NET20_OR_GREATER"
               @"--define:NET30_OR_GREATER"
               @"--define:NET35_OR_GREATER"
               @"--define:NET40_OR_GREATER"
               @"--define:NET45_OR_GREATER"
               @"--define:NET451_OR_GREATER"
               @"--define:NET452_OR_GREATER"
               @"--define:NET46_OR_GREATER"
               @"--define:NET461_OR_GREATER"
               @"--define:NET462_OR_GREATER"
               @"--define:NET47_OR_GREATER"
               @"--define:NET471_OR_GREATER"
               @"--define:NET472_OR_GREATER"
               @"--doc:C:\Users\nojaf\Projects\fsharp\artifacts\obj\Project255\Debug\net472\Project255.xml"
               @"--publicsign+"
               @"--optimize-"
               @"--tailcalls-"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\mscorlib.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project146\Debug\net472\Project146.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project257\Debug\net472\Project257.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Core.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Data.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Drawing.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.IO.Compression.FileSystem.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Numerics.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Runtime.Serialization.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Xml.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Xml.Linq.dll"
               @"--target:library"
               @"--nowarn:FS2003,NU5105"
               @"--warn:3"
               @"--warnaserror:3239,1182,0025"
               @"--fullpaths"
               @"--flaterrors"
               @"--subsystemversion:6.00"
               @"--highentropyva+"
               @"--targetprofile:mscorlib"
               @"--nocopyfsharpcore"
               @"--deterministic+"
               @"--simpleresolution"
               @"--nowarn:3384" |]
          ReferencedProjects = [| memoProject257 "Project257" |]
          IsIncompleteTypeCheckEnvironment = false
          UseScriptResolutionRules = false
          LoadTime = DateTime.Now
          UnresolvedReferences = None
          OriginalLoadReferences = []
          Stamp = None }

    FSharpReferencedProject.CreateFSharp(
        @"C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project255\Debug\net472\Project255.dll",
        projectOptions
    )

let memoProject255 = memoization Project255

let Project257 _ =
    let projectOptions =
        { ProjectFileName = @"C:\Users\nojaf\Projects\fsharp\tests\service\data\LargeApp\Project257\Project257.fsproj"
          ProjectId = None
          SourceFiles =
            [| @"C:\Users\nojaf\Projects\fsharp\artifacts\obj\Project257\Debug\net472\Project257.AssemblyInfo.fs"
               @"C:\Users\nojaf\Projects\fsharp\tests\service\data\LargeApp\Project257\MyModule.fs" |]
          OtherOptions =
            [| @"-o:C:\Users\nojaf\Projects\fsharp\artifacts\obj\Project257\Debug\net472\Project257.dll"
               @"-g"
               @"--debug:portable"
               @"--noframework"
               @"--define:TRACE"
               @"--define:DEBUG"
               @"--define:ENABLE_MONO_SUPPORT"
               @"--define:NETFRAMEWORK"
               @"--define:NET472"
               @"--define:NET20_OR_GREATER"
               @"--define:NET30_OR_GREATER"
               @"--define:NET35_OR_GREATER"
               @"--define:NET40_OR_GREATER"
               @"--define:NET45_OR_GREATER"
               @"--define:NET451_OR_GREATER"
               @"--define:NET452_OR_GREATER"
               @"--define:NET46_OR_GREATER"
               @"--define:NET461_OR_GREATER"
               @"--define:NET462_OR_GREATER"
               @"--define:NET47_OR_GREATER"
               @"--define:NET471_OR_GREATER"
               @"--define:NET472_OR_GREATER"
               @"--doc:C:\Users\nojaf\Projects\fsharp\artifacts\obj\Project257\Debug\net472\Project257.xml"
               @"--publicsign+"
               @"--optimize-"
               @"--tailcalls-"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\mscorlib.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project140\Debug\net472\Project140.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project142\Debug\net472\Project142.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project149\Debug\net472\Project149.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project161\Debug\net472\Project161.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project164\Debug\net472\Project164.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project168\Debug\net472\Project168.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project189\Debug\net472\Project189.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project190\Debug\net472\Project190.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project192\Debug\net472\Project192.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project214\Debug\net472\Project214.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project252\Debug\net472\Project252.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project253\Debug\net472\Project253.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project256\Debug\net472\Project256.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project264\Debug\net472\Project264.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project295\Debug\net472\Project295.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project297\Debug\net472\Project297.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project319\Debug\net472\Project319.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project326\Debug\net472\Project326.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project331\Debug\net472\Project331.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project346\Debug\net472\Project346.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Core.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Data.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Drawing.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.IO.Compression.FileSystem.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Numerics.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Runtime.Serialization.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Xml.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Xml.Linq.dll"
               @"--target:library"
               @"--nowarn:FS2003,NU5105"
               @"--warn:3"
               @"--warnaserror:3239,1182,0025"
               @"--fullpaths"
               @"--flaterrors"
               @"--subsystemversion:6.00"
               @"--highentropyva+"
               @"--targetprofile:mscorlib"
               @"--nocopyfsharpcore"
               @"--deterministic+"
               @"--simpleresolution"
               @"--nowarn:3384" |]
          ReferencedProjects =
            [| memoProject252 "Project252"
               memoProject256 "Project256"
               memoProject264 "Project264"
               memoProject319 "Project319"
               memoProject331 "Project331"
               memoProject346 "Project346" |]
          IsIncompleteTypeCheckEnvironment = false
          UseScriptResolutionRules = false
          LoadTime = DateTime.Now
          UnresolvedReferences = None
          OriginalLoadReferences = []
          Stamp = None }

    FSharpReferencedProject.CreateFSharp(
        @"C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project257\Debug\net472\Project257.dll",
        projectOptions
    )

let memoProject257 = memoization Project257

let Project256 _ =
    let projectOptions =
        { ProjectFileName = @"C:\Users\nojaf\Projects\fsharp\tests\service\data\LargeApp\Project256\Project256.fsproj"
          ProjectId = None
          SourceFiles =
            [| @"C:\Users\nojaf\Projects\fsharp\artifacts\obj\Project256\Debug\net472\Project256.AssemblyInfo.fs"
               @"C:\Users\nojaf\Projects\fsharp\tests\service\data\LargeApp\Project256\MyModule.fs" |]
          OtherOptions =
            [| @"-o:C:\Users\nojaf\Projects\fsharp\artifacts\obj\Project256\Debug\net472\Project256.dll"
               @"-g"
               @"--debug:portable"
               @"--noframework"
               @"--define:TRACE"
               @"--define:DEBUG"
               @"--define:ENABLE_MONO_SUPPORT"
               @"--define:NETFRAMEWORK"
               @"--define:NET472"
               @"--define:NET20_OR_GREATER"
               @"--define:NET30_OR_GREATER"
               @"--define:NET35_OR_GREATER"
               @"--define:NET40_OR_GREATER"
               @"--define:NET45_OR_GREATER"
               @"--define:NET451_OR_GREATER"
               @"--define:NET452_OR_GREATER"
               @"--define:NET46_OR_GREATER"
               @"--define:NET461_OR_GREATER"
               @"--define:NET462_OR_GREATER"
               @"--define:NET47_OR_GREATER"
               @"--define:NET471_OR_GREATER"
               @"--define:NET472_OR_GREATER"
               @"--doc:C:\Users\nojaf\Projects\fsharp\artifacts\obj\Project256\Debug\net472\Project256.xml"
               @"--publicsign+"
               @"--optimize-"
               @"--tailcalls-"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\mscorlib.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project142\Debug\net472\Project142.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project190\Debug\net472\Project190.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project252\Debug\net472\Project252.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project264\Debug\net472\Project264.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project319\Debug\net472\Project319.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project326\Debug\net472\Project326.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Core.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Data.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Drawing.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.IO.Compression.FileSystem.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Numerics.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Runtime.Serialization.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Xml.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Xml.Linq.dll"
               @"--target:library"
               @"--nowarn:FS2003,NU5105"
               @"--warn:3"
               @"--warnaserror:3239,1182,0025"
               @"--fullpaths"
               @"--flaterrors"
               @"--subsystemversion:6.00"
               @"--highentropyva+"
               @"--targetprofile:mscorlib"
               @"--nocopyfsharpcore"
               @"--deterministic+"
               @"--simpleresolution"
               @"--nowarn:3384" |]
          ReferencedProjects =
            [| memoProject252 "Project252"
               memoProject264 "Project264"
               memoProject319 "Project319" |]
          IsIncompleteTypeCheckEnvironment = false
          UseScriptResolutionRules = false
          LoadTime = DateTime.Now
          UnresolvedReferences = None
          OriginalLoadReferences = []
          Stamp = None }

    FSharpReferencedProject.CreateFSharp(
        @"C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project256\Debug\net472\Project256.dll",
        projectOptions
    )

let memoProject256 = memoization Project256

let Project331 _ =
    let projectOptions =
        { ProjectFileName = @"C:\Users\nojaf\Projects\fsharp\tests\service\data\LargeApp\Project331\Project331.fsproj"
          ProjectId = None
          SourceFiles =
            [| @"C:\Users\nojaf\Projects\fsharp\artifacts\obj\Project331\Debug\net472\Project331.AssemblyInfo.fs"
               @"C:\Users\nojaf\Projects\fsharp\tests\service\data\LargeApp\Project331\MyModule.fs" |]
          OtherOptions =
            [| @"-o:C:\Users\nojaf\Projects\fsharp\artifacts\obj\Project331\Debug\net472\Project331.dll"
               @"-g"
               @"--debug:portable"
               @"--noframework"
               @"--define:TRACE"
               @"--define:DEBUG"
               @"--define:ENABLE_MONO_SUPPORT"
               @"--define:NETFRAMEWORK"
               @"--define:NET472"
               @"--define:NET20_OR_GREATER"
               @"--define:NET30_OR_GREATER"
               @"--define:NET35_OR_GREATER"
               @"--define:NET40_OR_GREATER"
               @"--define:NET45_OR_GREATER"
               @"--define:NET451_OR_GREATER"
               @"--define:NET452_OR_GREATER"
               @"--define:NET46_OR_GREATER"
               @"--define:NET461_OR_GREATER"
               @"--define:NET462_OR_GREATER"
               @"--define:NET47_OR_GREATER"
               @"--define:NET471_OR_GREATER"
               @"--define:NET472_OR_GREATER"
               @"--doc:C:\Users\nojaf\Projects\fsharp\artifacts\obj\Project331\Debug\net472\Project331.xml"
               @"--publicsign+"
               @"--optimize-"
               @"--tailcalls-"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\mscorlib.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project140\Debug\net472\Project140.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project142\Debug\net472\Project142.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project149\Debug\net472\Project149.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project161\Debug\net472\Project161.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project164\Debug\net472\Project164.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project168\Debug\net472\Project168.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project190\Debug\net472\Project190.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project214\Debug\net472\Project214.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project252\Debug\net472\Project252.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project295\Debug\net472\Project295.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project297\Debug\net472\Project297.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project301\Debug\net472\Project301.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project319\Debug\net472\Project319.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project326\Debug\net472\Project326.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project346\Debug\net472\Project346.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Core.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Data.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Drawing.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.IO.Compression.FileSystem.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Numerics.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Runtime.Serialization.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Xml.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Xml.Linq.dll"
               @"--target:library"
               @"--nowarn:FS2003,NU5105"
               @"--warn:3"
               @"--warnaserror:3239,1182,0025"
               @"--fullpaths"
               @"--flaterrors"
               @"--subsystemversion:6.00"
               @"--highentropyva+"
               @"--targetprofile:mscorlib"
               @"--nocopyfsharpcore"
               @"--deterministic+"
               @"--simpleresolution"
               @"--nowarn:3384" |]
          ReferencedProjects =
            [| memoProject252 "Project252"
               memoProject319 "Project319"
               memoProject346 "Project346" |]
          IsIncompleteTypeCheckEnvironment = false
          UseScriptResolutionRules = false
          LoadTime = DateTime.Now
          UnresolvedReferences = None
          OriginalLoadReferences = []
          Stamp = None }

    FSharpReferencedProject.CreateFSharp(
        @"C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project331\Debug\net472\Project331.dll",
        projectOptions
    )

let memoProject331 = memoization Project331

let Project283 _ =
    let projectOptions =
        { ProjectFileName = @"C:\Users\nojaf\Projects\fsharp\tests\service\data\LargeApp\Project283\Project283.fsproj"
          ProjectId = None
          SourceFiles =
            [| @"C:\Users\nojaf\Projects\fsharp\artifacts\obj\Project283\Debug\net472\Project283.AssemblyInfo.fs"
               @"C:\Users\nojaf\Projects\fsharp\tests\service\data\LargeApp\Project283\MyModule.fs" |]
          OtherOptions =
            [| @"-o:C:\Users\nojaf\Projects\fsharp\artifacts\obj\Project283\Debug\net472\Project283.dll"
               @"-g"
               @"--debug:portable"
               @"--noframework"
               @"--define:TRACE"
               @"--define:DEBUG"
               @"--define:ENABLE_MONO_SUPPORT"
               @"--define:NETFRAMEWORK"
               @"--define:NET472"
               @"--define:NET20_OR_GREATER"
               @"--define:NET30_OR_GREATER"
               @"--define:NET35_OR_GREATER"
               @"--define:NET40_OR_GREATER"
               @"--define:NET45_OR_GREATER"
               @"--define:NET451_OR_GREATER"
               @"--define:NET452_OR_GREATER"
               @"--define:NET46_OR_GREATER"
               @"--define:NET461_OR_GREATER"
               @"--define:NET462_OR_GREATER"
               @"--define:NET47_OR_GREATER"
               @"--define:NET471_OR_GREATER"
               @"--define:NET472_OR_GREATER"
               @"--doc:C:\Users\nojaf\Projects\fsharp\artifacts\obj\Project283\Debug\net472\Project283.xml"
               @"--publicsign+"
               @"--optimize-"
               @"--tailcalls-"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\mscorlib.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project140\Debug\net472\Project140.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project142\Debug\net472\Project142.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project144\Debug\net472\Project144.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project164\Debug\net472\Project164.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project172\Debug\net472\Project172.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project213\Debug\net472\Project213.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project342\Debug\net472\Project342.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Core.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Data.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Drawing.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.IO.Compression.FileSystem.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Numerics.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Runtime.Serialization.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Xml.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Xml.Linq.dll"
               @"--target:library"
               @"--nowarn:FS2003,NU5105"
               @"--warn:3"
               @"--warnaserror:3239,1182,0025"
               @"--fullpaths"
               @"--flaterrors"
               @"--subsystemversion:6.00"
               @"--highentropyva+"
               @"--targetprofile:mscorlib"
               @"--nocopyfsharpcore"
               @"--deterministic+"
               @"--simpleresolution"
               @"--nowarn:3384" |]
          ReferencedProjects = [| memoProject213 "Project213" |]
          IsIncompleteTypeCheckEnvironment = false
          UseScriptResolutionRules = false
          LoadTime = DateTime.Now
          UnresolvedReferences = None
          OriginalLoadReferences = []
          Stamp = None }

    FSharpReferencedProject.CreateFSharp(
        @"C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project283\Debug\net472\Project283.dll",
        projectOptions
    )

let memoProject283 = memoization Project283

let Project292 _ =
    let projectOptions =
        { ProjectFileName = @"C:\Users\nojaf\Projects\fsharp\tests\service\data\LargeApp\Project292\Project292.fsproj"
          ProjectId = None
          SourceFiles =
            [| @"C:\Users\nojaf\Projects\fsharp\artifacts\obj\Project292\Debug\net472\Project292.AssemblyInfo.fs"
               @"C:\Users\nojaf\Projects\fsharp\tests\service\data\LargeApp\Project292\MyModule.fs" |]
          OtherOptions =
            [| @"-o:C:\Users\nojaf\Projects\fsharp\artifacts\obj\Project292\Debug\net472\Project292.dll"
               @"-g"
               @"--debug:portable"
               @"--noframework"
               @"--define:TRACE"
               @"--define:DEBUG"
               @"--define:ENABLE_MONO_SUPPORT"
               @"--define:NETFRAMEWORK"
               @"--define:NET472"
               @"--define:NET20_OR_GREATER"
               @"--define:NET30_OR_GREATER"
               @"--define:NET35_OR_GREATER"
               @"--define:NET40_OR_GREATER"
               @"--define:NET45_OR_GREATER"
               @"--define:NET451_OR_GREATER"
               @"--define:NET452_OR_GREATER"
               @"--define:NET46_OR_GREATER"
               @"--define:NET461_OR_GREATER"
               @"--define:NET462_OR_GREATER"
               @"--define:NET47_OR_GREATER"
               @"--define:NET471_OR_GREATER"
               @"--define:NET472_OR_GREATER"
               @"--doc:C:\Users\nojaf\Projects\fsharp\artifacts\obj\Project292\Debug\net472\Project292.xml"
               @"--publicsign+"
               @"--optimize-"
               @"--tailcalls-"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\mscorlib.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project138\Debug\net472\Project138.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project140\Debug\net472\Project140.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project144\Debug\net472\Project144.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project146\Debug\net472\Project146.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project149\Debug\net472\Project149.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project151\Debug\net472\Project151.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project157\Debug\net472\Project157.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project161\Debug\net472\Project161.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project164\Debug\net472\Project164.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project168\Debug\net472\Project168.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project189\Debug\net472\Project189.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project192\Debug\net472\Project192.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project214\Debug\net472\Project214.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project222\Debug\net472\Project222.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project243\Debug\net472\Project243.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project272\Debug\net472\Project272.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project329\Debug\net472\Project329.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Core.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Data.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Drawing.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.IO.Compression.FileSystem.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Numerics.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Runtime.Serialization.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Xml.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Xml.Linq.dll"
               @"--target:library"
               @"--nowarn:FS2003,NU5105"
               @"--warn:3"
               @"--warnaserror:3239,1182,0025"
               @"--fullpaths"
               @"--flaterrors"
               @"--subsystemversion:6.00"
               @"--highentropyva+"
               @"--targetprofile:mscorlib"
               @"--nocopyfsharpcore"
               @"--deterministic+"
               @"--simpleresolution"
               @"--nowarn:3384" |]
          ReferencedProjects =
            [| memoProject151 "Project151"
               memoProject222 "Project222"
               memoProject243 "Project243"
               memoProject272 "Project272"
               memoProject329 "Project329" |]
          IsIncompleteTypeCheckEnvironment = false
          UseScriptResolutionRules = false
          LoadTime = DateTime.Now
          UnresolvedReferences = None
          OriginalLoadReferences = []
          Stamp = None }

    FSharpReferencedProject.CreateFSharp(
        @"C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project292\Debug\net472\Project292.dll",
        projectOptions
    )

let memoProject292 = memoization Project292

let Project338 _ =
    let projectOptions =
        { ProjectFileName = @"C:\Users\nojaf\Projects\fsharp\tests\service\data\LargeApp\Project338\Project338.fsproj"
          ProjectId = None
          SourceFiles =
            [| @"C:\Users\nojaf\Projects\fsharp\artifacts\obj\Project338\Debug\net472\Project338.AssemblyInfo.fs"
               @"C:\Users\nojaf\Projects\fsharp\tests\service\data\LargeApp\Project338\MyModule.fs" |]
          OtherOptions =
            [| @"-o:C:\Users\nojaf\Projects\fsharp\artifacts\obj\Project338\Debug\net472\Project338.dll"
               @"-g"
               @"--debug:portable"
               @"--noframework"
               @"--define:TRACE"
               @"--define:DEBUG"
               @"--define:ENABLE_MONO_SUPPORT"
               @"--define:NETFRAMEWORK"
               @"--define:NET472"
               @"--define:NET20_OR_GREATER"
               @"--define:NET30_OR_GREATER"
               @"--define:NET35_OR_GREATER"
               @"--define:NET40_OR_GREATER"
               @"--define:NET45_OR_GREATER"
               @"--define:NET451_OR_GREATER"
               @"--define:NET452_OR_GREATER"
               @"--define:NET46_OR_GREATER"
               @"--define:NET461_OR_GREATER"
               @"--define:NET462_OR_GREATER"
               @"--define:NET47_OR_GREATER"
               @"--define:NET471_OR_GREATER"
               @"--define:NET472_OR_GREATER"
               @"--doc:C:\Users\nojaf\Projects\fsharp\artifacts\obj\Project338\Debug\net472\Project338.xml"
               @"--publicsign+"
               @"--optimize-"
               @"--tailcalls-"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\mscorlib.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project138\Debug\net472\Project138.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project140\Debug\net472\Project140.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project142\Debug\net472\Project142.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project144\Debug\net472\Project144.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project146\Debug\net472\Project146.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project149\Debug\net472\Project149.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project151\Debug\net472\Project151.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project157\Debug\net472\Project157.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project161\Debug\net472\Project161.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project164\Debug\net472\Project164.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project168\Debug\net472\Project168.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project214\Debug\net472\Project214.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project222\Debug\net472\Project222.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project272\Debug\net472\Project272.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project273\Debug\net472\Project273.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project326\Debug\net472\Project326.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project342\Debug\net472\Project342.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project344\Debug\net472\Project344.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Core.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Data.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Drawing.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.IO.Compression.FileSystem.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Numerics.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Runtime.Serialization.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Xml.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Xml.Linq.dll"
               @"--target:library"
               @"--nowarn:FS2003,NU5105"
               @"--warn:3"
               @"--warnaserror:3239,1182,0025"
               @"--fullpaths"
               @"--flaterrors"
               @"--subsystemversion:6.00"
               @"--highentropyva+"
               @"--targetprofile:mscorlib"
               @"--nocopyfsharpcore"
               @"--deterministic+"
               @"--simpleresolution"
               @"--nowarn:3384" |]
          ReferencedProjects =
            [| memoProject151 "Project151"
               memoProject222 "Project222"
               memoProject272 "Project272"
               memoProject273 "Project273" |]
          IsIncompleteTypeCheckEnvironment = false
          UseScriptResolutionRules = false
          LoadTime = DateTime.Now
          UnresolvedReferences = None
          OriginalLoadReferences = []
          Stamp = None }

    FSharpReferencedProject.CreateFSharp(
        @"C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project338\Debug\net472\Project338.dll",
        projectOptions
    )

let memoProject338 = memoization Project338

let Project339 _ =
    let projectOptions =
        { ProjectFileName = @"C:\Users\nojaf\Projects\fsharp\tests\service\data\LargeApp\Project339\Project339.fsproj"
          ProjectId = None
          SourceFiles =
            [| @"C:\Users\nojaf\Projects\fsharp\artifacts\obj\Project339\Debug\net472\Project339.AssemblyInfo.fs"
               @"C:\Users\nojaf\Projects\fsharp\tests\service\data\LargeApp\Project339\MyModule.fs" |]
          OtherOptions =
            [| @"-o:C:\Users\nojaf\Projects\fsharp\artifacts\obj\Project339\Debug\net472\Project339.dll"
               @"-g"
               @"--debug:portable"
               @"--noframework"
               @"--define:TRACE"
               @"--define:DEBUG"
               @"--define:ENABLE_MONO_SUPPORT"
               @"--define:NETFRAMEWORK"
               @"--define:NET472"
               @"--define:NET20_OR_GREATER"
               @"--define:NET30_OR_GREATER"
               @"--define:NET35_OR_GREATER"
               @"--define:NET40_OR_GREATER"
               @"--define:NET45_OR_GREATER"
               @"--define:NET451_OR_GREATER"
               @"--define:NET452_OR_GREATER"
               @"--define:NET46_OR_GREATER"
               @"--define:NET461_OR_GREATER"
               @"--define:NET462_OR_GREATER"
               @"--define:NET47_OR_GREATER"
               @"--define:NET471_OR_GREATER"
               @"--define:NET472_OR_GREATER"
               @"--doc:C:\Users\nojaf\Projects\fsharp\artifacts\obj\Project339\Debug\net472\Project339.xml"
               @"--publicsign+"
               @"--optimize-"
               @"--tailcalls-"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\mscorlib.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project138\Debug\net472\Project138.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project140\Debug\net472\Project140.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project142\Debug\net472\Project142.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project144\Debug\net472\Project144.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project146\Debug\net472\Project146.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project149\Debug\net472\Project149.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project151\Debug\net472\Project151.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project161\Debug\net472\Project161.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project164\Debug\net472\Project164.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project168\Debug\net472\Project168.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project172\Debug\net472\Project172.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project189\Debug\net472\Project189.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project192\Debug\net472\Project192.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project204\Debug\net472\Project204.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project209\Debug\net472\Project209.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project210\Debug\net472\Project210.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project214\Debug\net472\Project214.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project222\Debug\net472\Project222.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project243\Debug\net472\Project243.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project264\Debug\net472\Project264.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project272\Debug\net472\Project272.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project297\Debug\net472\Project297.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project300\Debug\net472\Project300.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project302\Debug\net472\Project302.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project305\Debug\net472\Project305.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project307\Debug\net472\Project307.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project326\Debug\net472\Project326.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project327\Debug\net472\Project327.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project329\Debug\net472\Project329.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project338\Debug\net472\Project338.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project340\Debug\net472\Project340.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project341\Debug\net472\Project341.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project342\Debug\net472\Project342.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project344\Debug\net472\Project344.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Core.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Data.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Drawing.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.IO.Compression.FileSystem.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Numerics.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Runtime.Serialization.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Xml.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Xml.Linq.dll"
               @"--target:library"
               @"--nowarn:FS2003,NU5105"
               @"--warn:3"
               @"--warnaserror:3239,1182,0025"
               @"--fullpaths"
               @"--flaterrors"
               @"--subsystemversion:6.00"
               @"--highentropyva+"
               @"--targetprofile:mscorlib"
               @"--nocopyfsharpcore"
               @"--deterministic+"
               @"--simpleresolution"
               @"--nowarn:3384" |]
          ReferencedProjects =
            [| memoProject151 "Project151"
               memoProject222 "Project222"
               memoProject243 "Project243"
               memoProject264 "Project264"
               memoProject272 "Project272"
               memoProject300 "Project300"
               memoProject302 "Project302"
               memoProject329 "Project329"
               memoProject338 "Project338" |]
          IsIncompleteTypeCheckEnvironment = false
          UseScriptResolutionRules = false
          LoadTime = DateTime.Now
          UnresolvedReferences = None
          OriginalLoadReferences = []
          Stamp = None }

    FSharpReferencedProject.CreateFSharp(
        @"C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project339\Debug\net472\Project339.dll",
        projectOptions
    )

let memoProject339 = memoization Project339

let Project84 _ =
    let projectOptions =
        { ProjectFileName = @"C:\Users\nojaf\Projects\fsharp\tests\service\data\LargeApp\Project84\Project84.fsproj"
          ProjectId = None
          SourceFiles =
            [| @"C:\Users\nojaf\Projects\fsharp\artifacts\obj\Project84\Debug\net472\Project84.AssemblyInfo.fs"
               @"C:\Users\nojaf\Projects\fsharp\tests\service\data\LargeApp\Project84\MyModule.fs" |]
          OtherOptions =
            [| @"-o:C:\Users\nojaf\Projects\fsharp\artifacts\obj\Project84\Debug\net472\Project84.dll"
               @"-g"
               @"--debug:portable"
               @"--noframework"
               @"--define:TRACE"
               @"--define:DEBUG"
               @"--define:ENABLE_MONO_SUPPORT"
               @"--define:NETFRAMEWORK"
               @"--define:NET472"
               @"--define:NET20_OR_GREATER"
               @"--define:NET30_OR_GREATER"
               @"--define:NET35_OR_GREATER"
               @"--define:NET40_OR_GREATER"
               @"--define:NET45_OR_GREATER"
               @"--define:NET451_OR_GREATER"
               @"--define:NET452_OR_GREATER"
               @"--define:NET46_OR_GREATER"
               @"--define:NET461_OR_GREATER"
               @"--define:NET462_OR_GREATER"
               @"--define:NET47_OR_GREATER"
               @"--define:NET471_OR_GREATER"
               @"--define:NET472_OR_GREATER"
               @"--doc:C:\Users\nojaf\Projects\fsharp\artifacts\obj\Project84\Debug\net472\Project84.xml"
               @"--publicsign+"
               @"--optimize-"
               @"--tailcalls-"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\mscorlib.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project138\Debug\net472\Project138.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project140\Debug\net472\Project140.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project142\Debug\net472\Project142.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project144\Debug\net472\Project144.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project146\Debug\net472\Project146.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project149\Debug\net472\Project149.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project151\Debug\net472\Project151.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project161\Debug\net472\Project161.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project164\Debug\net472\Project164.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project172\Debug\net472\Project172.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project190\Debug\net472\Project190.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project192\Debug\net472\Project192.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project214\Debug\net472\Project214.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project243\Debug\net472\Project243.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project250\Debug\net472\Project250.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project264\Debug\net472\Project264.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project272\Debug\net472\Project272.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project273\Debug\net472\Project273.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project293\Debug\net472\Project293.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project294\Debug\net472\Project294.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project295\Debug\net472\Project295.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project297\Debug\net472\Project297.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project298\Debug\net472\Project298.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project301\Debug\net472\Project301.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project302\Debug\net472\Project302.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project305\Debug\net472\Project305.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project311\Debug\net472\Project311.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project318\Debug\net472\Project318.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project319\Debug\net472\Project319.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project320\Debug\net472\Project320.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project321\Debug\net472\Project321.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project323\Debug\net472\Project323.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project324\Debug\net472\Project324.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project326\Debug\net472\Project326.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project329\Debug\net472\Project329.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project87\Debug\net472\Project87.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Core.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Data.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Drawing.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.IO.Compression.FileSystem.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Numerics.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Runtime.Serialization.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Xml.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Xml.Linq.dll"
               @"--target:library"
               @"--nowarn:FS2003,NU5105"
               @"--warn:3"
               @"--warnaserror:3239,1182,0025"
               @"--fullpaths"
               @"--flaterrors"
               @"--subsystemversion:6.00"
               @"--highentropyva+"
               @"--targetprofile:mscorlib"
               @"--nocopyfsharpcore"
               @"--deterministic+"
               @"--simpleresolution"
               @"--nowarn:3384" |]
          ReferencedProjects =
            [| memoProject151 "Project151"
               memoProject243 "Project243"
               memoProject250 "Project250"
               memoProject264 "Project264"
               memoProject272 "Project272"
               memoProject273 "Project273"
               memoProject293 "Project293"
               memoProject294 "Project294"
               memoProject302 "Project302"
               memoProject311 "Project311"
               memoProject318 "Project318"
               memoProject319 "Project319"
               memoProject320 "Project320"
               memoProject321 "Project321"
               memoProject324 "Project324"
               memoProject329 "Project329" |]
          IsIncompleteTypeCheckEnvironment = false
          UseScriptResolutionRules = false
          LoadTime = DateTime.Now
          UnresolvedReferences = None
          OriginalLoadReferences = []
          Stamp = None }

    FSharpReferencedProject.CreateFSharp(
        @"C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project84\Debug\net472\Project84.dll",
        projectOptions
    )

let memoProject84 = memoization Project84

let Project293 _ =
    let projectOptions =
        { ProjectFileName = @"C:\Users\nojaf\Projects\fsharp\tests\service\data\LargeApp\Project293\Project293.fsproj"
          ProjectId = None
          SourceFiles =
            [| @"C:\Users\nojaf\Projects\fsharp\artifacts\obj\Project293\Debug\net472\Project293.AssemblyInfo.fs"
               @"C:\Users\nojaf\Projects\fsharp\tests\service\data\LargeApp\Project293\MyModule.fs" |]
          OtherOptions =
            [| @"-o:C:\Users\nojaf\Projects\fsharp\artifacts\obj\Project293\Debug\net472\Project293.dll"
               @"-g"
               @"--debug:portable"
               @"--noframework"
               @"--define:TRACE"
               @"--define:DEBUG"
               @"--define:ENABLE_MONO_SUPPORT"
               @"--define:NETFRAMEWORK"
               @"--define:NET472"
               @"--define:NET20_OR_GREATER"
               @"--define:NET30_OR_GREATER"
               @"--define:NET35_OR_GREATER"
               @"--define:NET40_OR_GREATER"
               @"--define:NET45_OR_GREATER"
               @"--define:NET451_OR_GREATER"
               @"--define:NET452_OR_GREATER"
               @"--define:NET46_OR_GREATER"
               @"--define:NET461_OR_GREATER"
               @"--define:NET462_OR_GREATER"
               @"--define:NET47_OR_GREATER"
               @"--define:NET471_OR_GREATER"
               @"--define:NET472_OR_GREATER"
               @"--doc:C:\Users\nojaf\Projects\fsharp\artifacts\obj\Project293\Debug\net472\Project293.xml"
               @"--publicsign+"
               @"--optimize-"
               @"--tailcalls-"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\mscorlib.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project138\Debug\net472\Project138.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project140\Debug\net472\Project140.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project142\Debug\net472\Project142.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project146\Debug\net472\Project146.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project149\Debug\net472\Project149.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project151\Debug\net472\Project151.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project161\Debug\net472\Project161.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project164\Debug\net472\Project164.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project168\Debug\net472\Project168.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project214\Debug\net472\Project214.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project272\Debug\net472\Project272.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project295\Debug\net472\Project295.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project297\Debug\net472\Project297.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project301\Debug\net472\Project301.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Core.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Data.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Drawing.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.IO.Compression.FileSystem.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Numerics.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Runtime.Serialization.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Xml.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Xml.Linq.dll"
               @"--target:library"
               @"--nowarn:FS2003,NU5105"
               @"--warn:3"
               @"--warnaserror:3239,1182,0025"
               @"--fullpaths"
               @"--flaterrors"
               @"--subsystemversion:6.00"
               @"--highentropyva+"
               @"--targetprofile:mscorlib"
               @"--nocopyfsharpcore"
               @"--deterministic+"
               @"--simpleresolution"
               @"--nowarn:3384" |]
          ReferencedProjects =
            [| memoProject151 "Project151"
               memoProject272 "Project272" |]
          IsIncompleteTypeCheckEnvironment = false
          UseScriptResolutionRules = false
          LoadTime = DateTime.Now
          UnresolvedReferences = None
          OriginalLoadReferences = []
          Stamp = None }

    FSharpReferencedProject.CreateFSharp(
        @"C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project293\Debug\net472\Project293.dll",
        projectOptions
    )

let memoProject293 = memoization Project293

let Project318 _ =
    let projectOptions =
        { ProjectFileName = @"C:\Users\nojaf\Projects\fsharp\tests\service\data\LargeApp\Project318\Project318.fsproj"
          ProjectId = None
          SourceFiles =
            [| @"C:\Users\nojaf\Projects\fsharp\artifacts\obj\Project318\Debug\net472\Project318.AssemblyInfo.fs"
               @"C:\Users\nojaf\Projects\fsharp\tests\service\data\LargeApp\Project318\MyModule.fs" |]
          OtherOptions =
            [| @"-o:C:\Users\nojaf\Projects\fsharp\artifacts\obj\Project318\Debug\net472\Project318.dll"
               @"-g"
               @"--debug:portable"
               @"--noframework"
               @"--define:TRACE"
               @"--define:DEBUG"
               @"--define:ENABLE_MONO_SUPPORT"
               @"--define:NETFRAMEWORK"
               @"--define:NET472"
               @"--define:NET20_OR_GREATER"
               @"--define:NET30_OR_GREATER"
               @"--define:NET35_OR_GREATER"
               @"--define:NET40_OR_GREATER"
               @"--define:NET45_OR_GREATER"
               @"--define:NET451_OR_GREATER"
               @"--define:NET452_OR_GREATER"
               @"--define:NET46_OR_GREATER"
               @"--define:NET461_OR_GREATER"
               @"--define:NET462_OR_GREATER"
               @"--define:NET47_OR_GREATER"
               @"--define:NET471_OR_GREATER"
               @"--define:NET472_OR_GREATER"
               @"--doc:C:\Users\nojaf\Projects\fsharp\artifacts\obj\Project318\Debug\net472\Project318.xml"
               @"--publicsign+"
               @"--optimize-"
               @"--tailcalls-"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\mscorlib.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project189\Debug\net472\Project189.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project319\Debug\net472\Project319.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Core.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Data.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Drawing.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.IO.Compression.FileSystem.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Numerics.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Runtime.Serialization.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Xml.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Xml.Linq.dll"
               @"--target:library"
               @"--nowarn:FS2003,NU5105"
               @"--warn:3"
               @"--warnaserror:3239,1182,0025"
               @"--fullpaths"
               @"--flaterrors"
               @"--subsystemversion:6.00"
               @"--highentropyva+"
               @"--targetprofile:mscorlib"
               @"--nocopyfsharpcore"
               @"--deterministic+"
               @"--simpleresolution"
               @"--nowarn:3384" |]
          ReferencedProjects = [| memoProject319 "Project319" |]
          IsIncompleteTypeCheckEnvironment = false
          UseScriptResolutionRules = false
          LoadTime = DateTime.Now
          UnresolvedReferences = None
          OriginalLoadReferences = []
          Stamp = None }

    FSharpReferencedProject.CreateFSharp(
        @"C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project318\Debug\net472\Project318.dll",
        projectOptions
    )

let memoProject318 = memoization Project318

let Project320 _ =
    let projectOptions =
        { ProjectFileName = @"C:\Users\nojaf\Projects\fsharp\tests\service\data\LargeApp\Project320\Project320.fsproj"
          ProjectId = None
          SourceFiles =
            [| @"C:\Users\nojaf\Projects\fsharp\artifacts\obj\Project320\Debug\net472\Project320.AssemblyInfo.fs"
               @"C:\Users\nojaf\Projects\fsharp\tests\service\data\LargeApp\Project320\MyModule.fs" |]
          OtherOptions =
            [| @"-o:C:\Users\nojaf\Projects\fsharp\artifacts\obj\Project320\Debug\net472\Project320.dll"
               @"-g"
               @"--debug:portable"
               @"--noframework"
               @"--define:TRACE"
               @"--define:DEBUG"
               @"--define:ENABLE_MONO_SUPPORT"
               @"--define:NETFRAMEWORK"
               @"--define:NET472"
               @"--define:NET20_OR_GREATER"
               @"--define:NET30_OR_GREATER"
               @"--define:NET35_OR_GREATER"
               @"--define:NET40_OR_GREATER"
               @"--define:NET45_OR_GREATER"
               @"--define:NET451_OR_GREATER"
               @"--define:NET452_OR_GREATER"
               @"--define:NET46_OR_GREATER"
               @"--define:NET461_OR_GREATER"
               @"--define:NET462_OR_GREATER"
               @"--define:NET47_OR_GREATER"
               @"--define:NET471_OR_GREATER"
               @"--define:NET472_OR_GREATER"
               @"--doc:C:\Users\nojaf\Projects\fsharp\artifacts\obj\Project320\Debug\net472\Project320.xml"
               @"--publicsign+"
               @"--optimize-"
               @"--tailcalls-"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\mscorlib.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project140\Debug\net472\Project140.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project142\Debug\net472\Project142.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project149\Debug\net472\Project149.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project164\Debug\net472\Project164.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project189\Debug\net472\Project189.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project190\Debug\net472\Project190.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project240\Debug\net472\Project240.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project264\Debug\net472\Project264.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project318\Debug\net472\Project318.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project319\Debug\net472\Project319.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project321\Debug\net472\Project321.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project323\Debug\net472\Project323.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project326\Debug\net472\Project326.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Core.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Data.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Drawing.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.IO.Compression.FileSystem.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Numerics.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Runtime.Serialization.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Xml.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Xml.Linq.dll"
               @"--target:library"
               @"--nowarn:FS2003,NU5105"
               @"--warn:3"
               @"--warnaserror:3239,1182,0025"
               @"--fullpaths"
               @"--flaterrors"
               @"--subsystemversion:6.00"
               @"--highentropyva+"
               @"--targetprofile:mscorlib"
               @"--nocopyfsharpcore"
               @"--deterministic+"
               @"--simpleresolution"
               @"--nowarn:3384" |]
          ReferencedProjects =
            [| memoProject240 "Project240"
               memoProject264 "Project264"
               memoProject318 "Project318"
               memoProject319 "Project319"
               memoProject321 "Project321" |]
          IsIncompleteTypeCheckEnvironment = false
          UseScriptResolutionRules = false
          LoadTime = DateTime.Now
          UnresolvedReferences = None
          OriginalLoadReferences = []
          Stamp = None }

    FSharpReferencedProject.CreateFSharp(
        @"C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project320\Debug\net472\Project320.dll",
        projectOptions
    )

let memoProject320 = memoization Project320

let Project321 _ =
    let projectOptions =
        { ProjectFileName = @"C:\Users\nojaf\Projects\fsharp\tests\service\data\LargeApp\Project321\Project321.fsproj"
          ProjectId = None
          SourceFiles =
            [| @"C:\Users\nojaf\Projects\fsharp\artifacts\obj\Project321\Debug\net472\Project321.AssemblyInfo.fs"
               @"C:\Users\nojaf\Projects\fsharp\tests\service\data\LargeApp\Project321\MyModule.fs" |]
          OtherOptions =
            [| @"-o:C:\Users\nojaf\Projects\fsharp\artifacts\obj\Project321\Debug\net472\Project321.dll"
               @"-g"
               @"--debug:portable"
               @"--noframework"
               @"--define:TRACE"
               @"--define:DEBUG"
               @"--define:ENABLE_MONO_SUPPORT"
               @"--define:NETFRAMEWORK"
               @"--define:NET472"
               @"--define:NET20_OR_GREATER"
               @"--define:NET30_OR_GREATER"
               @"--define:NET35_OR_GREATER"
               @"--define:NET40_OR_GREATER"
               @"--define:NET45_OR_GREATER"
               @"--define:NET451_OR_GREATER"
               @"--define:NET452_OR_GREATER"
               @"--define:NET46_OR_GREATER"
               @"--define:NET461_OR_GREATER"
               @"--define:NET462_OR_GREATER"
               @"--define:NET47_OR_GREATER"
               @"--define:NET471_OR_GREATER"
               @"--define:NET472_OR_GREATER"
               @"--doc:C:\Users\nojaf\Projects\fsharp\artifacts\obj\Project321\Debug\net472\Project321.xml"
               @"--publicsign+"
               @"--optimize-"
               @"--tailcalls-"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\mscorlib.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project140\Debug\net472\Project140.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project142\Debug\net472\Project142.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project161\Debug\net472\Project161.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project164\Debug\net472\Project164.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project168\Debug\net472\Project168.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project189\Debug\net472\Project189.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project240\Debug\net472\Project240.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project264\Debug\net472\Project264.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project318\Debug\net472\Project318.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project319\Debug\net472\Project319.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Core.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Data.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Drawing.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.IO.Compression.FileSystem.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Numerics.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Runtime.Serialization.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Xml.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Xml.Linq.dll"
               @"--target:library"
               @"--nowarn:FS2003,NU5105"
               @"--warn:3"
               @"--warnaserror:3239,1182,0025"
               @"--fullpaths"
               @"--flaterrors"
               @"--subsystemversion:6.00"
               @"--highentropyva+"
               @"--targetprofile:mscorlib"
               @"--nocopyfsharpcore"
               @"--deterministic+"
               @"--simpleresolution"
               @"--nowarn:3384" |]
          ReferencedProjects =
            [| memoProject240 "Project240"
               memoProject264 "Project264"
               memoProject318 "Project318"
               memoProject319 "Project319" |]
          IsIncompleteTypeCheckEnvironment = false
          UseScriptResolutionRules = false
          LoadTime = DateTime.Now
          UnresolvedReferences = None
          OriginalLoadReferences = []
          Stamp = None }

    FSharpReferencedProject.CreateFSharp(
        @"C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project321\Debug\net472\Project321.dll",
        projectOptions
    )

let memoProject321 = memoization Project321

let Project45 _ =
    let projectOptions =
        { ProjectFileName = @"C:\Users\nojaf\Projects\fsharp\tests\service\data\LargeApp\Project45\Project45.fsproj"
          ProjectId = None
          SourceFiles =
            [| @"C:\Users\nojaf\Projects\fsharp\artifacts\obj\Project45\Debug\net472\Project45.AssemblyInfo.fs"
               @"C:\Users\nojaf\Projects\fsharp\tests\service\data\LargeApp\Project45\MyModule.fs" |]
          OtherOptions =
            [| @"-o:C:\Users\nojaf\Projects\fsharp\artifacts\obj\Project45\Debug\net472\Project45.dll"
               @"-g"
               @"--debug:portable"
               @"--noframework"
               @"--define:TRACE"
               @"--define:DEBUG"
               @"--define:ENABLE_MONO_SUPPORT"
               @"--define:NETFRAMEWORK"
               @"--define:NET472"
               @"--define:NET20_OR_GREATER"
               @"--define:NET30_OR_GREATER"
               @"--define:NET35_OR_GREATER"
               @"--define:NET40_OR_GREATER"
               @"--define:NET45_OR_GREATER"
               @"--define:NET451_OR_GREATER"
               @"--define:NET452_OR_GREATER"
               @"--define:NET46_OR_GREATER"
               @"--define:NET461_OR_GREATER"
               @"--define:NET462_OR_GREATER"
               @"--define:NET47_OR_GREATER"
               @"--define:NET471_OR_GREATER"
               @"--define:NET472_OR_GREATER"
               @"--doc:C:\Users\nojaf\Projects\fsharp\artifacts\obj\Project45\Debug\net472\Project45.xml"
               @"--publicsign+"
               @"--optimize-"
               @"--tailcalls-"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\mscorlib.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project116\Debug\net472\Project116.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project140\Debug\net472\Project140.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project142\Debug\net472\Project142.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project149\Debug\net472\Project149.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project164\Debug\net472\Project164.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project17\Debug\net472\Project17.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project22\Debug\net472\Project22.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project42\Debug\net472\Project42.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project47\Debug\net472\Project47.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Core.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Data.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Drawing.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.IO.Compression.FileSystem.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Numerics.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Runtime.Serialization.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Xml.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Xml.Linq.dll"
               @"--target:library"
               @"--nowarn:FS2003,NU5105"
               @"--warn:3"
               @"--warnaserror:3239,1182,0025"
               @"--fullpaths"
               @"--flaterrors"
               @"--subsystemversion:6.00"
               @"--highentropyva+"
               @"--targetprofile:mscorlib"
               @"--nocopyfsharpcore"
               @"--deterministic+"
               @"--simpleresolution"
               @"--nowarn:3384" |]
          ReferencedProjects =
            [| memoProject116 "Project116"
               memoProject17 "Project17"
               memoProject22 "Project22"
               memoProject42 "Project42"
               memoProject47 "Project47" |]
          IsIncompleteTypeCheckEnvironment = false
          UseScriptResolutionRules = false
          LoadTime = DateTime.Now
          UnresolvedReferences = None
          OriginalLoadReferences = []
          Stamp = None }

    FSharpReferencedProject.CreateFSharp(
        @"C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project45\Debug\net472\Project45.dll",
        projectOptions
    )

let memoProject45 = memoization Project45

let Project42 _ =
    let projectOptions =
        { ProjectFileName = @"C:\Users\nojaf\Projects\fsharp\tests\service\data\LargeApp\Project42\Project42.fsproj"
          ProjectId = None
          SourceFiles =
            [| @"C:\Users\nojaf\Projects\fsharp\artifacts\obj\Project42\Debug\net472\Project42.AssemblyInfo.fs"
               @"C:\Users\nojaf\Projects\fsharp\tests\service\data\LargeApp\Project42\MyModule.fs" |]
          OtherOptions =
            [| @"-o:C:\Users\nojaf\Projects\fsharp\artifacts\obj\Project42\Debug\net472\Project42.dll"
               @"-g"
               @"--debug:portable"
               @"--noframework"
               @"--define:TRACE"
               @"--define:DEBUG"
               @"--define:ENABLE_MONO_SUPPORT"
               @"--define:NETFRAMEWORK"
               @"--define:NET472"
               @"--define:NET20_OR_GREATER"
               @"--define:NET30_OR_GREATER"
               @"--define:NET35_OR_GREATER"
               @"--define:NET40_OR_GREATER"
               @"--define:NET45_OR_GREATER"
               @"--define:NET451_OR_GREATER"
               @"--define:NET452_OR_GREATER"
               @"--define:NET46_OR_GREATER"
               @"--define:NET461_OR_GREATER"
               @"--define:NET462_OR_GREATER"
               @"--define:NET47_OR_GREATER"
               @"--define:NET471_OR_GREATER"
               @"--define:NET472_OR_GREATER"
               @"--doc:C:\Users\nojaf\Projects\fsharp\artifacts\obj\Project42\Debug\net472\Project42.xml"
               @"--publicsign+"
               @"--optimize-"
               @"--tailcalls-"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\mscorlib.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project122\Debug\net472\Project122.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project138\Debug\net472\Project138.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project140\Debug\net472\Project140.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project142\Debug\net472\Project142.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project149\Debug\net472\Project149.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project164\Debug\net472\Project164.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project17\Debug\net472\Project17.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project22\Debug\net472\Project22.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project298\Debug\net472\Project298.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project47\Debug\net472\Project47.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Core.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Data.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Drawing.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.IO.Compression.FileSystem.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Numerics.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Runtime.Serialization.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Xml.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Xml.Linq.dll"
               @"--target:library"
               @"--nowarn:FS2003,NU5105"
               @"--warn:3"
               @"--warnaserror:3239,1182,0025"
               @"--fullpaths"
               @"--flaterrors"
               @"--subsystemversion:6.00"
               @"--highentropyva+"
               @"--targetprofile:mscorlib"
               @"--nocopyfsharpcore"
               @"--deterministic+"
               @"--simpleresolution"
               @"--nowarn:3384" |]
          ReferencedProjects =
            [| memoProject122 "Project122"
               memoProject17 "Project17"
               memoProject22 "Project22"
               memoProject47 "Project47" |]
          IsIncompleteTypeCheckEnvironment = false
          UseScriptResolutionRules = false
          LoadTime = DateTime.Now
          UnresolvedReferences = None
          OriginalLoadReferences = []
          Stamp = None }

    FSharpReferencedProject.CreateFSharp(
        @"C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project42\Debug\net472\Project42.dll",
        projectOptions
    )

let memoProject42 = memoization Project42

let Project47 _ =
    let projectOptions =
        { ProjectFileName = @"C:\Users\nojaf\Projects\fsharp\tests\service\data\LargeApp\Project47\Project47.fsproj"
          ProjectId = None
          SourceFiles =
            [| @"C:\Users\nojaf\Projects\fsharp\artifacts\obj\Project47\Debug\net472\Project47.AssemblyInfo.fs"
               @"C:\Users\nojaf\Projects\fsharp\tests\service\data\LargeApp\Project47\MyModule.fs" |]
          OtherOptions =
            [| @"-o:C:\Users\nojaf\Projects\fsharp\artifacts\obj\Project47\Debug\net472\Project47.dll"
               @"-g"
               @"--debug:portable"
               @"--noframework"
               @"--define:TRACE"
               @"--define:DEBUG"
               @"--define:ENABLE_MONO_SUPPORT"
               @"--define:NETFRAMEWORK"
               @"--define:NET472"
               @"--define:NET20_OR_GREATER"
               @"--define:NET30_OR_GREATER"
               @"--define:NET35_OR_GREATER"
               @"--define:NET40_OR_GREATER"
               @"--define:NET45_OR_GREATER"
               @"--define:NET451_OR_GREATER"
               @"--define:NET452_OR_GREATER"
               @"--define:NET46_OR_GREATER"
               @"--define:NET461_OR_GREATER"
               @"--define:NET462_OR_GREATER"
               @"--define:NET47_OR_GREATER"
               @"--define:NET471_OR_GREATER"
               @"--define:NET472_OR_GREATER"
               @"--doc:C:\Users\nojaf\Projects\fsharp\artifacts\obj\Project47\Debug\net472\Project47.xml"
               @"--publicsign+"
               @"--optimize-"
               @"--tailcalls-"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\mscorlib.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project142\Debug\net472\Project142.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project144\Debug\net472\Project144.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project146\Debug\net472\Project146.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project151\Debug\net472\Project151.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project164\Debug\net472\Project164.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project172\Debug\net472\Project172.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project184\Debug\net472\Project184.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project189\Debug\net472\Project189.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project206\Debug\net472\Project206.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project213\Debug\net472\Project213.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project22\Debug\net472\Project22.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project272\Debug\net472\Project272.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Core.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Data.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Drawing.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.IO.Compression.FileSystem.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Numerics.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Runtime.Serialization.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Xml.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Xml.Linq.dll"
               @"--target:library"
               @"--nowarn:FS2003,NU5105"
               @"--warn:3"
               @"--warnaserror:3239,1182,0025"
               @"--fullpaths"
               @"--flaterrors"
               @"--subsystemversion:6.00"
               @"--highentropyva+"
               @"--targetprofile:mscorlib"
               @"--nocopyfsharpcore"
               @"--deterministic+"
               @"--simpleresolution"
               @"--nowarn:3384" |]
          ReferencedProjects =
            [| memoProject151 "Project151"
               memoProject206 "Project206"
               memoProject213 "Project213"
               memoProject22 "Project22"
               memoProject272 "Project272" |]
          IsIncompleteTypeCheckEnvironment = false
          UseScriptResolutionRules = false
          LoadTime = DateTime.Now
          UnresolvedReferences = None
          OriginalLoadReferences = []
          Stamp = None }

    FSharpReferencedProject.CreateFSharp(
        @"C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project47\Debug\net472\Project47.dll",
        projectOptions
    )

let memoProject47 = memoization Project47

let Project70 _ =
    let projectOptions =
        { ProjectFileName = @"C:\Users\nojaf\Projects\fsharp\tests\service\data\LargeApp\Project70\Project70.fsproj"
          ProjectId = None
          SourceFiles =
            [| @"C:\Users\nojaf\Projects\fsharp\artifacts\obj\Project70\Debug\net472\Project70.AssemblyInfo.fs"
               @"C:\Users\nojaf\Projects\fsharp\tests\service\data\LargeApp\Project70\MyModule.fs" |]
          OtherOptions =
            [| @"-o:C:\Users\nojaf\Projects\fsharp\artifacts\obj\Project70\Debug\net472\Project70.dll"
               @"-g"
               @"--debug:portable"
               @"--noframework"
               @"--define:TRACE"
               @"--define:DEBUG"
               @"--define:ENABLE_MONO_SUPPORT"
               @"--define:NETFRAMEWORK"
               @"--define:NET472"
               @"--define:NET20_OR_GREATER"
               @"--define:NET30_OR_GREATER"
               @"--define:NET35_OR_GREATER"
               @"--define:NET40_OR_GREATER"
               @"--define:NET45_OR_GREATER"
               @"--define:NET451_OR_GREATER"
               @"--define:NET452_OR_GREATER"
               @"--define:NET46_OR_GREATER"
               @"--define:NET461_OR_GREATER"
               @"--define:NET462_OR_GREATER"
               @"--define:NET47_OR_GREATER"
               @"--define:NET471_OR_GREATER"
               @"--define:NET472_OR_GREATER"
               @"--doc:C:\Users\nojaf\Projects\fsharp\artifacts\obj\Project70\Debug\net472\Project70.xml"
               @"--publicsign+"
               @"--optimize-"
               @"--tailcalls-"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\mscorlib.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project142\Debug\net472\Project142.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project252\Debug\net472\Project252.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project319\Debug\net472\Project319.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project331\Debug\net472\Project331.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project346\Debug\net472\Project346.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Core.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Data.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Drawing.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.IO.Compression.FileSystem.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Numerics.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Runtime.Serialization.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Xml.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Xml.Linq.dll"
               @"--target:library"
               @"--nowarn:FS2003,NU5105"
               @"--warn:3"
               @"--warnaserror:3239,1182,0025"
               @"--fullpaths"
               @"--flaterrors"
               @"--subsystemversion:6.00"
               @"--highentropyva+"
               @"--targetprofile:mscorlib"
               @"--nocopyfsharpcore"
               @"--deterministic+"
               @"--simpleresolution"
               @"--nowarn:3384" |]
          ReferencedProjects =
            [| memoProject252 "Project252"
               memoProject319 "Project319"
               memoProject331 "Project331"
               memoProject346 "Project346" |]
          IsIncompleteTypeCheckEnvironment = false
          UseScriptResolutionRules = false
          LoadTime = DateTime.Now
          UnresolvedReferences = None
          OriginalLoadReferences = []
          Stamp = None }

    FSharpReferencedProject.CreateFSharp(
        @"C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project70\Debug\net472\Project70.dll",
        projectOptions
    )

let memoProject70 = memoization Project70

let Project71 _ =
    let projectOptions =
        { ProjectFileName = @"C:\Users\nojaf\Projects\fsharp\tests\service\data\LargeApp\Project71\Project71.fsproj"
          ProjectId = None
          SourceFiles =
            [| @"C:\Users\nojaf\Projects\fsharp\artifacts\obj\Project71\Debug\net472\Project71.AssemblyInfo.fs"
               @"C:\Users\nojaf\Projects\fsharp\tests\service\data\LargeApp\Project71\MyModule.fs" |]
          OtherOptions =
            [| @"-o:C:\Users\nojaf\Projects\fsharp\artifacts\obj\Project71\Debug\net472\Project71.dll"
               @"-g"
               @"--debug:portable"
               @"--noframework"
               @"--define:TRACE"
               @"--define:DEBUG"
               @"--define:ENABLE_MONO_SUPPORT"
               @"--define:NETFRAMEWORK"
               @"--define:NET472"
               @"--define:NET20_OR_GREATER"
               @"--define:NET30_OR_GREATER"
               @"--define:NET35_OR_GREATER"
               @"--define:NET40_OR_GREATER"
               @"--define:NET45_OR_GREATER"
               @"--define:NET451_OR_GREATER"
               @"--define:NET452_OR_GREATER"
               @"--define:NET46_OR_GREATER"
               @"--define:NET461_OR_GREATER"
               @"--define:NET462_OR_GREATER"
               @"--define:NET47_OR_GREATER"
               @"--define:NET471_OR_GREATER"
               @"--define:NET472_OR_GREATER"
               @"--doc:C:\Users\nojaf\Projects\fsharp\artifacts\obj\Project71\Debug\net472\Project71.xml"
               @"--publicsign+"
               @"--optimize-"
               @"--tailcalls-"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\mscorlib.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project140\Debug\net472\Project140.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project142\Debug\net472\Project142.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project149\Debug\net472\Project149.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project161\Debug\net472\Project161.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project164\Debug\net472\Project164.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project172\Debug\net472\Project172.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project189\Debug\net472\Project189.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project190\Debug\net472\Project190.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project192\Debug\net472\Project192.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project238\Debug\net472\Project238.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project252\Debug\net472\Project252.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project253\Debug\net472\Project253.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project257\Debug\net472\Project257.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project264\Debug\net472\Project264.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project295\Debug\net472\Project295.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project319\Debug\net472\Project319.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project331\Debug\net472\Project331.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project340\Debug\net472\Project340.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project341\Debug\net472\Project341.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project346\Debug\net472\Project346.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project70\Debug\net472\Project70.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project72\Debug\net472\Project72.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project74\Debug\net472\Project74.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project84\Debug\net472\Project84.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project87\Debug\net472\Project87.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Core.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Data.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Drawing.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.IO.Compression.FileSystem.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Numerics.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Runtime.Serialization.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Xml.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Xml.Linq.dll"
               @"--target:library"
               @"--nowarn:FS2003,NU5105"
               @"--warn:3"
               @"--warnaserror:3239,1182,0025"
               @"--fullpaths"
               @"--flaterrors"
               @"--subsystemversion:6.00"
               @"--highentropyva+"
               @"--targetprofile:mscorlib"
               @"--nocopyfsharpcore"
               @"--deterministic+"
               @"--simpleresolution"
               @"--nowarn:3384" |]
          ReferencedProjects =
            [| memoProject252 "Project252"
               memoProject257 "Project257"
               memoProject264 "Project264"
               memoProject319 "Project319"
               memoProject331 "Project331"
               memoProject346 "Project346"
               memoProject70 "Project70"
               memoProject84 "Project84" |]
          IsIncompleteTypeCheckEnvironment = false
          UseScriptResolutionRules = false
          LoadTime = DateTime.Now
          UnresolvedReferences = None
          OriginalLoadReferences = []
          Stamp = None }

    FSharpReferencedProject.CreateFSharp(
        @"C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project71\Debug\net472\Project71.dll",
        projectOptions
    )

let memoProject71 = memoization Project71

let Project281 _ =
    let projectOptions =
        { ProjectFileName = @"C:\Users\nojaf\Projects\fsharp\tests\service\data\LargeApp\Project281\Project281.fsproj"
          ProjectId = None
          SourceFiles =
            [| @"C:\Users\nojaf\Projects\fsharp\artifacts\obj\Project281\Debug\net472\Project281.AssemblyInfo.fs"
               @"C:\Users\nojaf\Projects\fsharp\tests\service\data\LargeApp\Project281\MyModule.fs" |]
          OtherOptions =
            [| @"-o:C:\Users\nojaf\Projects\fsharp\artifacts\obj\Project281\Debug\net472\Project281.dll"
               @"-g"
               @"--debug:portable"
               @"--noframework"
               @"--define:TRACE"
               @"--define:DEBUG"
               @"--define:ENABLE_MONO_SUPPORT"
               @"--define:NETFRAMEWORK"
               @"--define:NET472"
               @"--define:NET20_OR_GREATER"
               @"--define:NET30_OR_GREATER"
               @"--define:NET35_OR_GREATER"
               @"--define:NET40_OR_GREATER"
               @"--define:NET45_OR_GREATER"
               @"--define:NET451_OR_GREATER"
               @"--define:NET452_OR_GREATER"
               @"--define:NET46_OR_GREATER"
               @"--define:NET461_OR_GREATER"
               @"--define:NET462_OR_GREATER"
               @"--define:NET47_OR_GREATER"
               @"--define:NET471_OR_GREATER"
               @"--define:NET472_OR_GREATER"
               @"--doc:C:\Users\nojaf\Projects\fsharp\artifacts\obj\Project281\Debug\net472\Project281.xml"
               @"--publicsign+"
               @"--optimize-"
               @"--tailcalls-"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\mscorlib.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project250\Debug\net472\Project250.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project272\Debug\net472\Project272.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Core.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Data.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Drawing.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.IO.Compression.FileSystem.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Numerics.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Runtime.Serialization.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Xml.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Xml.Linq.dll"
               @"--target:library"
               @"--nowarn:FS2003,NU5105"
               @"--warn:3"
               @"--warnaserror:3239,1182,0025"
               @"--fullpaths"
               @"--flaterrors"
               @"--subsystemversion:6.00"
               @"--highentropyva+"
               @"--targetprofile:mscorlib"
               @"--nocopyfsharpcore"
               @"--deterministic+"
               @"--simpleresolution"
               @"--nowarn:3384" |]
          ReferencedProjects =
            [| memoProject250 "Project250"
               memoProject272 "Project272" |]
          IsIncompleteTypeCheckEnvironment = false
          UseScriptResolutionRules = false
          LoadTime = DateTime.Now
          UnresolvedReferences = None
          OriginalLoadReferences = []
          Stamp = None }

    FSharpReferencedProject.CreateFSharp(
        @"C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project281\Debug\net472\Project281.dll",
        projectOptions
    )

let memoProject281 = memoization Project281

let Project69 _ =
    let projectOptions =
        { ProjectFileName = @"C:\Users\nojaf\Projects\fsharp\tests\service\data\LargeApp\Project69\Project69.fsproj"
          ProjectId = None
          SourceFiles =
            [| @"C:\Users\nojaf\Projects\fsharp\artifacts\obj\Project69\Debug\net472\Project69.AssemblyInfo.fs"
               @"C:\Users\nojaf\Projects\fsharp\tests\service\data\LargeApp\Project69\MyModule.fs" |]
          OtherOptions =
            [| @"-o:C:\Users\nojaf\Projects\fsharp\artifacts\obj\Project69\Debug\net472\Project69.dll"
               @"-g"
               @"--debug:portable"
               @"--noframework"
               @"--define:TRACE"
               @"--define:DEBUG"
               @"--define:ENABLE_MONO_SUPPORT"
               @"--define:NETFRAMEWORK"
               @"--define:NET472"
               @"--define:NET20_OR_GREATER"
               @"--define:NET30_OR_GREATER"
               @"--define:NET35_OR_GREATER"
               @"--define:NET40_OR_GREATER"
               @"--define:NET45_OR_GREATER"
               @"--define:NET451_OR_GREATER"
               @"--define:NET452_OR_GREATER"
               @"--define:NET46_OR_GREATER"
               @"--define:NET461_OR_GREATER"
               @"--define:NET462_OR_GREATER"
               @"--define:NET47_OR_GREATER"
               @"--define:NET471_OR_GREATER"
               @"--define:NET472_OR_GREATER"
               @"--doc:C:\Users\nojaf\Projects\fsharp\artifacts\obj\Project69\Debug\net472\Project69.xml"
               @"--publicsign+"
               @"--optimize-"
               @"--tailcalls-"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\mscorlib.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project140\Debug\net472\Project140.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project142\Debug\net472\Project142.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project151\Debug\net472\Project151.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project161\Debug\net472\Project161.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project164\Debug\net472\Project164.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project168\Debug\net472\Project168.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project189\Debug\net472\Project189.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project192\Debug\net472\Project192.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project214\Debug\net472\Project214.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project264\Debug\net472\Project264.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project281\Debug\net472\Project281.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project297\Debug\net472\Project297.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project298\Debug\net472\Project298.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project323\Debug\net472\Project323.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project326\Debug\net472\Project326.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project331\Debug\net472\Project331.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project340\Debug\net472\Project340.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project341\Debug\net472\Project341.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project68\Debug\net472\Project68.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project84\Debug\net472\Project84.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Core.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Data.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Drawing.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.IO.Compression.FileSystem.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Numerics.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Runtime.Serialization.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Xml.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Xml.Linq.dll"
               @"--target:library"
               @"--nowarn:FS2003,NU5105"
               @"--warn:3"
               @"--warnaserror:3239,1182,0025"
               @"--fullpaths"
               @"--flaterrors"
               @"--subsystemversion:6.00"
               @"--highentropyva+"
               @"--targetprofile:mscorlib"
               @"--nocopyfsharpcore"
               @"--deterministic+"
               @"--simpleresolution"
               @"--nowarn:3384" |]
          ReferencedProjects =
            [| memoProject151 "Project151"
               memoProject264 "Project264"
               memoProject281 "Project281"
               memoProject331 "Project331"
               memoProject84 "Project84" |]
          IsIncompleteTypeCheckEnvironment = false
          UseScriptResolutionRules = false
          LoadTime = DateTime.Now
          UnresolvedReferences = None
          OriginalLoadReferences = []
          Stamp = None }

    FSharpReferencedProject.CreateFSharp(
        @"C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project69\Debug\net472\Project69.dll",
        projectOptions
    )

let memoProject69 = memoization Project69

let Project85 _ =
    let projectOptions =
        { ProjectFileName = @"C:\Users\nojaf\Projects\fsharp\tests\service\data\LargeApp\Project85\Project85.fsproj"
          ProjectId = None
          SourceFiles =
            [| @"C:\Users\nojaf\Projects\fsharp\artifacts\obj\Project85\Debug\net472\Project85.AssemblyInfo.fs"
               @"C:\Users\nojaf\Projects\fsharp\tests\service\data\LargeApp\Project85\MyModule.fs" |]
          OtherOptions =
            [| @"-o:C:\Users\nojaf\Projects\fsharp\artifacts\obj\Project85\Debug\net472\Project85.dll"
               @"-g"
               @"--debug:portable"
               @"--noframework"
               @"--define:TRACE"
               @"--define:DEBUG"
               @"--define:ENABLE_MONO_SUPPORT"
               @"--define:NETFRAMEWORK"
               @"--define:NET472"
               @"--define:NET20_OR_GREATER"
               @"--define:NET30_OR_GREATER"
               @"--define:NET35_OR_GREATER"
               @"--define:NET40_OR_GREATER"
               @"--define:NET45_OR_GREATER"
               @"--define:NET451_OR_GREATER"
               @"--define:NET452_OR_GREATER"
               @"--define:NET46_OR_GREATER"
               @"--define:NET461_OR_GREATER"
               @"--define:NET462_OR_GREATER"
               @"--define:NET47_OR_GREATER"
               @"--define:NET471_OR_GREATER"
               @"--define:NET472_OR_GREATER"
               @"--doc:C:\Users\nojaf\Projects\fsharp\artifacts\obj\Project85\Debug\net472\Project85.xml"
               @"--publicsign+"
               @"--optimize-"
               @"--tailcalls-"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\mscorlib.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project138\Debug\net472\Project138.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project140\Debug\net472\Project140.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project142\Debug\net472\Project142.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project144\Debug\net472\Project144.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project146\Debug\net472\Project146.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project149\Debug\net472\Project149.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project151\Debug\net472\Project151.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project161\Debug\net472\Project161.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project164\Debug\net472\Project164.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project168\Debug\net472\Project168.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project172\Debug\net472\Project172.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project189\Debug\net472\Project189.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project190\Debug\net472\Project190.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project192\Debug\net472\Project192.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project213\Debug\net472\Project213.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project214\Debug\net472\Project214.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project222\Debug\net472\Project222.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project232\Debug\net472\Project232.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project236\Debug\net472\Project236.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project243\Debug\net472\Project243.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project250\Debug\net472\Project250.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project252\Debug\net472\Project252.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project257\Debug\net472\Project257.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project264\Debug\net472\Project264.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project268\Debug\net472\Project268.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project272\Debug\net472\Project272.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project282\Debug\net472\Project282.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project292\Debug\net472\Project292.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project295\Debug\net472\Project295.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project297\Debug\net472\Project297.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project301\Debug\net472\Project301.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project302\Debug\net472\Project302.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project303\Debug\net472\Project303.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project319\Debug\net472\Project319.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project323\Debug\net472\Project323.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project326\Debug\net472\Project326.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project329\Debug\net472\Project329.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project331\Debug\net472\Project331.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project339\Debug\net472\Project339.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project344\Debug\net472\Project344.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project346\Debug\net472\Project346.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project84\Debug\net472\Project84.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project87\Debug\net472\Project87.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Core.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Data.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Drawing.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.IO.Compression.FileSystem.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Numerics.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Runtime.Serialization.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Xml.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Xml.Linq.dll"
               @"--target:library"
               @"--nowarn:FS2003,NU5105"
               @"--warn:3"
               @"--warnaserror:3239,1182,0025"
               @"--fullpaths"
               @"--flaterrors"
               @"--subsystemversion:6.00"
               @"--highentropyva+"
               @"--targetprofile:mscorlib"
               @"--nocopyfsharpcore"
               @"--deterministic+"
               @"--simpleresolution"
               @"--nowarn:3384" |]
          ReferencedProjects =
            [| memoProject151 "Project151"
               memoProject213 "Project213"
               memoProject222 "Project222"
               memoProject232 "Project232"
               memoProject236 "Project236"
               memoProject243 "Project243"
               memoProject250 "Project250"
               memoProject252 "Project252"
               memoProject257 "Project257"
               memoProject264 "Project264"
               memoProject268 "Project268"
               memoProject272 "Project272"
               memoProject282 "Project282"
               memoProject292 "Project292"
               memoProject302 "Project302"
               memoProject303 "Project303"
               memoProject319 "Project319"
               memoProject329 "Project329"
               memoProject331 "Project331"
               memoProject339 "Project339"
               memoProject346 "Project346"
               memoProject84 "Project84" |]
          IsIncompleteTypeCheckEnvironment = false
          UseScriptResolutionRules = false
          LoadTime = DateTime.Now
          UnresolvedReferences = None
          OriginalLoadReferences = []
          Stamp = None }

    FSharpReferencedProject.CreateFSharp(
        @"C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project85\Debug\net472\Project85.dll",
        projectOptions
    )

let memoProject85 = memoization Project85

let Project282 _ =
    let projectOptions =
        { ProjectFileName = @"C:\Users\nojaf\Projects\fsharp\tests\service\data\LargeApp\Project282\Project282.fsproj"
          ProjectId = None
          SourceFiles =
            [| @"C:\Users\nojaf\Projects\fsharp\artifacts\obj\Project282\Debug\net472\Project282.AssemblyInfo.fs"
               @"C:\Users\nojaf\Projects\fsharp\tests\service\data\LargeApp\Project282\MyModule.fs" |]
          OtherOptions =
            [| @"-o:C:\Users\nojaf\Projects\fsharp\artifacts\obj\Project282\Debug\net472\Project282.dll"
               @"-g"
               @"--debug:portable"
               @"--noframework"
               @"--define:TRACE"
               @"--define:DEBUG"
               @"--define:ENABLE_MONO_SUPPORT"
               @"--define:NETFRAMEWORK"
               @"--define:NET472"
               @"--define:NET20_OR_GREATER"
               @"--define:NET30_OR_GREATER"
               @"--define:NET35_OR_GREATER"
               @"--define:NET40_OR_GREATER"
               @"--define:NET45_OR_GREATER"
               @"--define:NET451_OR_GREATER"
               @"--define:NET452_OR_GREATER"
               @"--define:NET46_OR_GREATER"
               @"--define:NET461_OR_GREATER"
               @"--define:NET462_OR_GREATER"
               @"--define:NET47_OR_GREATER"
               @"--define:NET471_OR_GREATER"
               @"--define:NET472_OR_GREATER"
               @"--doc:C:\Users\nojaf\Projects\fsharp\artifacts\obj\Project282\Debug\net472\Project282.xml"
               @"--publicsign+"
               @"--optimize-"
               @"--tailcalls-"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\mscorlib.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project138\Debug\net472\Project138.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project140\Debug\net472\Project140.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project142\Debug\net472\Project142.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project144\Debug\net472\Project144.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project146\Debug\net472\Project146.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project149\Debug\net472\Project149.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project151\Debug\net472\Project151.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project157\Debug\net472\Project157.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project164\Debug\net472\Project164.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project168\Debug\net472\Project168.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project172\Debug\net472\Project172.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project189\Debug\net472\Project189.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project192\Debug\net472\Project192.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project213\Debug\net472\Project213.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project214\Debug\net472\Project214.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project222\Debug\net472\Project222.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project232\Debug\net472\Project232.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project243\Debug\net472\Project243.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project264\Debug\net472\Project264.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project268\Debug\net472\Project268.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project272\Debug\net472\Project272.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project292\Debug\net472\Project292.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project323\Debug\net472\Project323.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project326\Debug\net472\Project326.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project329\Debug\net472\Project329.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project337\Debug\net472\Project337.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project339\Debug\net472\Project339.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project346\Debug\net472\Project346.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Core.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Data.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Drawing.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.IO.Compression.FileSystem.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Numerics.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Runtime.Serialization.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Xml.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Xml.Linq.dll"
               @"--target:library"
               @"--nowarn:FS2003,NU5105"
               @"--warn:3"
               @"--warnaserror:3239,1182,0025"
               @"--fullpaths"
               @"--flaterrors"
               @"--subsystemversion:6.00"
               @"--highentropyva+"
               @"--targetprofile:mscorlib"
               @"--nocopyfsharpcore"
               @"--deterministic+"
               @"--simpleresolution"
               @"--nowarn:3384" |]
          ReferencedProjects =
            [| memoProject151 "Project151"
               memoProject213 "Project213"
               memoProject222 "Project222"
               memoProject232 "Project232"
               memoProject243 "Project243"
               memoProject264 "Project264"
               memoProject268 "Project268"
               memoProject272 "Project272"
               memoProject292 "Project292"
               memoProject329 "Project329"
               memoProject337 "Project337"
               memoProject339 "Project339"
               memoProject346 "Project346" |]
          IsIncompleteTypeCheckEnvironment = false
          UseScriptResolutionRules = false
          LoadTime = DateTime.Now
          UnresolvedReferences = None
          OriginalLoadReferences = []
          Stamp = None }

    FSharpReferencedProject.CreateFSharp(
        @"C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project282\Debug\net472\Project282.dll",
        projectOptions
    )

let memoProject282 = memoization Project282

let Project337 _ =
    let projectOptions =
        { ProjectFileName = @"C:\Users\nojaf\Projects\fsharp\tests\service\data\LargeApp\Project337\Project337.fsproj"
          ProjectId = None
          SourceFiles =
            [| @"C:\Users\nojaf\Projects\fsharp\artifacts\obj\Project337\Debug\net472\Project337.AssemblyInfo.fs"
               @"C:\Users\nojaf\Projects\fsharp\tests\service\data\LargeApp\Project337\MyModule.fs" |]
          OtherOptions =
            [| @"-o:C:\Users\nojaf\Projects\fsharp\artifacts\obj\Project337\Debug\net472\Project337.dll"
               @"-g"
               @"--debug:portable"
               @"--noframework"
               @"--define:TRACE"
               @"--define:DEBUG"
               @"--define:ENABLE_MONO_SUPPORT"
               @"--define:NETFRAMEWORK"
               @"--define:NET472"
               @"--define:NET20_OR_GREATER"
               @"--define:NET30_OR_GREATER"
               @"--define:NET35_OR_GREATER"
               @"--define:NET40_OR_GREATER"
               @"--define:NET45_OR_GREATER"
               @"--define:NET451_OR_GREATER"
               @"--define:NET452_OR_GREATER"
               @"--define:NET46_OR_GREATER"
               @"--define:NET461_OR_GREATER"
               @"--define:NET462_OR_GREATER"
               @"--define:NET47_OR_GREATER"
               @"--define:NET471_OR_GREATER"
               @"--define:NET472_OR_GREATER"
               @"--doc:C:\Users\nojaf\Projects\fsharp\artifacts\obj\Project337\Debug\net472\Project337.xml"
               @"--publicsign+"
               @"--optimize-"
               @"--tailcalls-"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\mscorlib.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project138\Debug\net472\Project138.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project140\Debug\net472\Project140.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project142\Debug\net472\Project142.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project151\Debug\net472\Project151.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project161\Debug\net472\Project161.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project164\Debug\net472\Project164.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project168\Debug\net472\Project168.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project200\Debug\net472\Project200.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project214\Debug\net472\Project214.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project260\Debug\net472\Project260.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project264\Debug\net472\Project264.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project305\Debug\net472\Project305.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project326\Debug\net472\Project326.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project342\Debug\net472\Project342.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project344\Debug\net472\Project344.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Core.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Data.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Drawing.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.IO.Compression.FileSystem.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Numerics.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Runtime.Serialization.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Xml.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Xml.Linq.dll"
               @"--target:library"
               @"--nowarn:FS2003,NU5105"
               @"--warn:3"
               @"--warnaserror:3239,1182,0025"
               @"--fullpaths"
               @"--flaterrors"
               @"--subsystemversion:6.00"
               @"--highentropyva+"
               @"--targetprofile:mscorlib"
               @"--nocopyfsharpcore"
               @"--deterministic+"
               @"--simpleresolution"
               @"--nowarn:3384" |]
          ReferencedProjects =
            [| memoProject151 "Project151"
               memoProject264 "Project264" |]
          IsIncompleteTypeCheckEnvironment = false
          UseScriptResolutionRules = false
          LoadTime = DateTime.Now
          UnresolvedReferences = None
          OriginalLoadReferences = []
          Stamp = None }

    FSharpReferencedProject.CreateFSharp(
        @"C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project337\Debug\net472\Project337.dll",
        projectOptions
    )

let memoProject337 = memoization Project337

let Project303 _ =
    let projectOptions =
        { ProjectFileName = @"C:\Users\nojaf\Projects\fsharp\tests\service\data\LargeApp\Project303\Project303.fsproj"
          ProjectId = None
          SourceFiles =
            [| @"C:\Users\nojaf\Projects\fsharp\artifacts\obj\Project303\Debug\net472\Project303.AssemblyInfo.fs"
               @"C:\Users\nojaf\Projects\fsharp\tests\service\data\LargeApp\Project303\MyModule.fs" |]
          OtherOptions =
            [| @"-o:C:\Users\nojaf\Projects\fsharp\artifacts\obj\Project303\Debug\net472\Project303.dll"
               @"-g"
               @"--debug:portable"
               @"--noframework"
               @"--define:TRACE"
               @"--define:DEBUG"
               @"--define:ENABLE_MONO_SUPPORT"
               @"--define:NETFRAMEWORK"
               @"--define:NET472"
               @"--define:NET20_OR_GREATER"
               @"--define:NET30_OR_GREATER"
               @"--define:NET35_OR_GREATER"
               @"--define:NET40_OR_GREATER"
               @"--define:NET45_OR_GREATER"
               @"--define:NET451_OR_GREATER"
               @"--define:NET452_OR_GREATER"
               @"--define:NET46_OR_GREATER"
               @"--define:NET461_OR_GREATER"
               @"--define:NET462_OR_GREATER"
               @"--define:NET47_OR_GREATER"
               @"--define:NET471_OR_GREATER"
               @"--define:NET472_OR_GREATER"
               @"--doc:C:\Users\nojaf\Projects\fsharp\artifacts\obj\Project303\Debug\net472\Project303.xml"
               @"--publicsign+"
               @"--optimize-"
               @"--tailcalls-"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\mscorlib.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project138\Debug\net472\Project138.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project140\Debug\net472\Project140.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project142\Debug\net472\Project142.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project146\Debug\net472\Project146.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project149\Debug\net472\Project149.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project151\Debug\net472\Project151.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project164\Debug\net472\Project164.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project168\Debug\net472\Project168.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project172\Debug\net472\Project172.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project189\Debug\net472\Project189.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project190\Debug\net472\Project190.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project192\Debug\net472\Project192.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project213\Debug\net472\Project213.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project222\Debug\net472\Project222.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project236\Debug\net472\Project236.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project243\Debug\net472\Project243.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project252\Debug\net472\Project252.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project257\Debug\net472\Project257.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project264\Debug\net472\Project264.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project268\Debug\net472\Project268.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project272\Debug\net472\Project272.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project302\Debug\net472\Project302.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project305\Debug\net472\Project305.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project319\Debug\net472\Project319.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project323\Debug\net472\Project323.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project324\Debug\net472\Project324.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project326\Debug\net472\Project326.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project329\Debug\net472\Project329.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project87\Debug\net472\Project87.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Core.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Data.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Drawing.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.IO.Compression.FileSystem.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Numerics.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Runtime.Serialization.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Xml.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Xml.Linq.dll"
               @"--target:library"
               @"--nowarn:FS2003,NU5105"
               @"--warn:3"
               @"--warnaserror:3239,1182,0025"
               @"--fullpaths"
               @"--flaterrors"
               @"--subsystemversion:6.00"
               @"--highentropyva+"
               @"--targetprofile:mscorlib"
               @"--nocopyfsharpcore"
               @"--deterministic+"
               @"--simpleresolution"
               @"--nowarn:3384" |]
          ReferencedProjects =
            [| memoProject151 "Project151"
               memoProject213 "Project213"
               memoProject222 "Project222"
               memoProject236 "Project236"
               memoProject243 "Project243"
               memoProject252 "Project252"
               memoProject257 "Project257"
               memoProject264 "Project264"
               memoProject268 "Project268"
               memoProject272 "Project272"
               memoProject302 "Project302"
               memoProject319 "Project319"
               memoProject324 "Project324"
               memoProject329 "Project329" |]
          IsIncompleteTypeCheckEnvironment = false
          UseScriptResolutionRules = false
          LoadTime = DateTime.Now
          UnresolvedReferences = None
          OriginalLoadReferences = []
          Stamp = None }

    FSharpReferencedProject.CreateFSharp(
        @"C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project303\Debug\net472\Project303.dll",
        projectOptions
    )

let memoProject303 = memoization Project303

let Project43 _ =
    let projectOptions =
        { ProjectFileName = @"C:\Users\nojaf\Projects\fsharp\tests\service\data\LargeApp\Project43\Project43.fsproj"
          ProjectId = None
          SourceFiles =
            [| @"C:\Users\nojaf\Projects\fsharp\artifacts\obj\Project43\Debug\net472\Project43.AssemblyInfo.fs"
               @"C:\Users\nojaf\Projects\fsharp\tests\service\data\LargeApp\Project43\MyModule.fs" |]
          OtherOptions =
            [| @"-o:C:\Users\nojaf\Projects\fsharp\artifacts\obj\Project43\Debug\net472\Project43.dll"
               @"-g"
               @"--debug:portable"
               @"--noframework"
               @"--define:TRACE"
               @"--define:DEBUG"
               @"--define:ENABLE_MONO_SUPPORT"
               @"--define:NETFRAMEWORK"
               @"--define:NET472"
               @"--define:NET20_OR_GREATER"
               @"--define:NET30_OR_GREATER"
               @"--define:NET35_OR_GREATER"
               @"--define:NET40_OR_GREATER"
               @"--define:NET45_OR_GREATER"
               @"--define:NET451_OR_GREATER"
               @"--define:NET452_OR_GREATER"
               @"--define:NET46_OR_GREATER"
               @"--define:NET461_OR_GREATER"
               @"--define:NET462_OR_GREATER"
               @"--define:NET47_OR_GREATER"
               @"--define:NET471_OR_GREATER"
               @"--define:NET472_OR_GREATER"
               @"--doc:C:\Users\nojaf\Projects\fsharp\artifacts\obj\Project43\Debug\net472\Project43.xml"
               @"--publicsign+"
               @"--optimize-"
               @"--tailcalls-"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\mscorlib.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project140\Debug\net472\Project140.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project142\Debug\net472\Project142.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project149\Debug\net472\Project149.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project161\Debug\net472\Project161.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project164\Debug\net472\Project164.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project168\Debug\net472\Project168.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project21\Debug\net472\Project21.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project22\Debug\net472\Project22.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project24\Debug\net472\Project24.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project47\Debug\net472\Project47.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Core.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Data.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Drawing.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.IO.Compression.FileSystem.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Numerics.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Runtime.Serialization.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Xml.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Xml.Linq.dll"
               @"--target:library"
               @"--nowarn:FS2003,NU5105"
               @"--warn:3"
               @"--warnaserror:3239,1182,0025"
               @"--fullpaths"
               @"--flaterrors"
               @"--subsystemversion:6.00"
               @"--highentropyva+"
               @"--targetprofile:mscorlib"
               @"--nocopyfsharpcore"
               @"--deterministic+"
               @"--simpleresolution"
               @"--nowarn:3384" |]
          ReferencedProjects =
            [| memoProject21 "Project21"
               memoProject22 "Project22"
               memoProject24 "Project24"
               memoProject47 "Project47" |]
          IsIncompleteTypeCheckEnvironment = false
          UseScriptResolutionRules = false
          LoadTime = DateTime.Now
          UnresolvedReferences = None
          OriginalLoadReferences = []
          Stamp = None }

    FSharpReferencedProject.CreateFSharp(
        @"C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project43\Debug\net472\Project43.dll",
        projectOptions
    )

let memoProject43 = memoization Project43

let Project44 _ =
    let projectOptions =
        { ProjectFileName = @"C:\Users\nojaf\Projects\fsharp\tests\service\data\LargeApp\Project44\Project44.fsproj"
          ProjectId = None
          SourceFiles =
            [| @"C:\Users\nojaf\Projects\fsharp\artifacts\obj\Project44\Debug\net472\Project44.AssemblyInfo.fs"
               @"C:\Users\nojaf\Projects\fsharp\tests\service\data\LargeApp\Project44\MyModule.fs" |]
          OtherOptions =
            [| @"-o:C:\Users\nojaf\Projects\fsharp\artifacts\obj\Project44\Debug\net472\Project44.dll"
               @"-g"
               @"--debug:portable"
               @"--noframework"
               @"--define:TRACE"
               @"--define:DEBUG"
               @"--define:ENABLE_MONO_SUPPORT"
               @"--define:NETFRAMEWORK"
               @"--define:NET472"
               @"--define:NET20_OR_GREATER"
               @"--define:NET30_OR_GREATER"
               @"--define:NET35_OR_GREATER"
               @"--define:NET40_OR_GREATER"
               @"--define:NET45_OR_GREATER"
               @"--define:NET451_OR_GREATER"
               @"--define:NET452_OR_GREATER"
               @"--define:NET46_OR_GREATER"
               @"--define:NET461_OR_GREATER"
               @"--define:NET462_OR_GREATER"
               @"--define:NET47_OR_GREATER"
               @"--define:NET471_OR_GREATER"
               @"--define:NET472_OR_GREATER"
               @"--doc:C:\Users\nojaf\Projects\fsharp\artifacts\obj\Project44\Debug\net472\Project44.xml"
               @"--publicsign+"
               @"--optimize-"
               @"--tailcalls-"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\mscorlib.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project140\Debug\net472\Project140.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project142\Debug\net472\Project142.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project149\Debug\net472\Project149.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project164\Debug\net472\Project164.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project17\Debug\net472\Project17.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project22\Debug\net472\Project22.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project23\Debug\net472\Project23.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project45\Debug\net472\Project45.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project46\Debug\net472\Project46.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Core.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Data.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Drawing.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.IO.Compression.FileSystem.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Numerics.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Runtime.Serialization.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Xml.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Xml.Linq.dll"
               @"--target:library"
               @"--nowarn:FS2003,NU5105"
               @"--warn:3"
               @"--warnaserror:3239,1182,0025"
               @"--fullpaths"
               @"--flaterrors"
               @"--subsystemversion:6.00"
               @"--highentropyva+"
               @"--targetprofile:mscorlib"
               @"--nocopyfsharpcore"
               @"--deterministic+"
               @"--simpleresolution"
               @"--nowarn:3384" |]
          ReferencedProjects =
            [| memoProject17 "Project17"
               memoProject22 "Project22"
               memoProject23 "Project23"
               memoProject45 "Project45"
               memoProject46 "Project46" |]
          IsIncompleteTypeCheckEnvironment = false
          UseScriptResolutionRules = false
          LoadTime = DateTime.Now
          UnresolvedReferences = None
          OriginalLoadReferences = []
          Stamp = None }

    FSharpReferencedProject.CreateFSharp(
        @"C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project44\Debug\net472\Project44.dll",
        projectOptions
    )

let memoProject44 = memoization Project44

let Project46 _ =
    let projectOptions =
        { ProjectFileName = @"C:\Users\nojaf\Projects\fsharp\tests\service\data\LargeApp\Project46\Project46.fsproj"
          ProjectId = None
          SourceFiles =
            [| @"C:\Users\nojaf\Projects\fsharp\artifacts\obj\Project46\Debug\net472\Project46.AssemblyInfo.fs"
               @"C:\Users\nojaf\Projects\fsharp\tests\service\data\LargeApp\Project46\MyModule.fs" |]
          OtherOptions =
            [| @"-o:C:\Users\nojaf\Projects\fsharp\artifacts\obj\Project46\Debug\net472\Project46.dll"
               @"-g"
               @"--debug:portable"
               @"--noframework"
               @"--define:TRACE"
               @"--define:DEBUG"
               @"--define:ENABLE_MONO_SUPPORT"
               @"--define:NETFRAMEWORK"
               @"--define:NET472"
               @"--define:NET20_OR_GREATER"
               @"--define:NET30_OR_GREATER"
               @"--define:NET35_OR_GREATER"
               @"--define:NET40_OR_GREATER"
               @"--define:NET45_OR_GREATER"
               @"--define:NET451_OR_GREATER"
               @"--define:NET452_OR_GREATER"
               @"--define:NET46_OR_GREATER"
               @"--define:NET461_OR_GREATER"
               @"--define:NET462_OR_GREATER"
               @"--define:NET47_OR_GREATER"
               @"--define:NET471_OR_GREATER"
               @"--define:NET472_OR_GREATER"
               @"--doc:C:\Users\nojaf\Projects\fsharp\artifacts\obj\Project46\Debug\net472\Project46.xml"
               @"--publicsign+"
               @"--optimize-"
               @"--tailcalls-"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\mscorlib.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project140\Debug\net472\Project140.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project142\Debug\net472\Project142.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project149\Debug\net472\Project149.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project161\Debug\net472\Project161.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project164\Debug\net472\Project164.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project17\Debug\net472\Project17.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project22\Debug\net472\Project22.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Core.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Data.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Drawing.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.IO.Compression.FileSystem.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Numerics.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Runtime.Serialization.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Xml.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Xml.Linq.dll"
               @"--target:library"
               @"--nowarn:FS2003,NU5105"
               @"--warn:3"
               @"--warnaserror:3239,1182,0025"
               @"--fullpaths"
               @"--flaterrors"
               @"--subsystemversion:6.00"
               @"--highentropyva+"
               @"--targetprofile:mscorlib"
               @"--nocopyfsharpcore"
               @"--deterministic+"
               @"--simpleresolution"
               @"--nowarn:3384" |]
          ReferencedProjects =
            [| memoProject17 "Project17"
               memoProject22 "Project22" |]
          IsIncompleteTypeCheckEnvironment = false
          UseScriptResolutionRules = false
          LoadTime = DateTime.Now
          UnresolvedReferences = None
          OriginalLoadReferences = []
          Stamp = None }

    FSharpReferencedProject.CreateFSharp(
        @"C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project46\Debug\net472\Project46.dll",
        projectOptions
    )

let memoProject46 = memoization Project46

let Project98 _ =
    let projectOptions =
        { ProjectFileName = @"C:\Users\nojaf\Projects\fsharp\tests\service\data\LargeApp\Project98\Project98.fsproj"
          ProjectId = None
          SourceFiles =
            [| @"C:\Users\nojaf\Projects\fsharp\artifacts\obj\Project98\Debug\net472\Project98.AssemblyInfo.fs"
               @"C:\Users\nojaf\Projects\fsharp\tests\service\data\LargeApp\Project98\MyModule.fs" |]
          OtherOptions =
            [| @"-o:C:\Users\nojaf\Projects\fsharp\artifacts\obj\Project98\Debug\net472\Project98.dll"
               @"-g"
               @"--debug:portable"
               @"--noframework"
               @"--define:TRACE"
               @"--define:DEBUG"
               @"--define:ENABLE_MONO_SUPPORT"
               @"--define:NETFRAMEWORK"
               @"--define:NET472"
               @"--define:NET20_OR_GREATER"
               @"--define:NET30_OR_GREATER"
               @"--define:NET35_OR_GREATER"
               @"--define:NET40_OR_GREATER"
               @"--define:NET45_OR_GREATER"
               @"--define:NET451_OR_GREATER"
               @"--define:NET452_OR_GREATER"
               @"--define:NET46_OR_GREATER"
               @"--define:NET461_OR_GREATER"
               @"--define:NET462_OR_GREATER"
               @"--define:NET47_OR_GREATER"
               @"--define:NET471_OR_GREATER"
               @"--define:NET472_OR_GREATER"
               @"--doc:C:\Users\nojaf\Projects\fsharp\artifacts\obj\Project98\Debug\net472\Project98.xml"
               @"--publicsign+"
               @"--optimize-"
               @"--tailcalls-"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\mscorlib.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project122\Debug\net472\Project122.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project138\Debug\net472\Project138.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project140\Debug\net472\Project140.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project142\Debug\net472\Project142.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project144\Debug\net472\Project144.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project146\Debug\net472\Project146.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project149\Debug\net472\Project149.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project151\Debug\net472\Project151.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project161\Debug\net472\Project161.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project164\Debug\net472\Project164.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project172\Debug\net472\Project172.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project184\Debug\net472\Project184.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project189\Debug\net472\Project189.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project206\Debug\net472\Project206.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project209\Debug\net472\Project209.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project214\Debug\net472\Project214.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project23\Debug\net472\Project23.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project236\Debug\net472\Project236.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project243\Debug\net472\Project243.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project250\Debug\net472\Project250.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project261\Debug\net472\Project261.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project264\Debug\net472\Project264.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project272\Debug\net472\Project272.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project3\Debug\net472\Project3.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project300\Debug\net472\Project300.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project302\Debug\net472\Project302.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project5\Debug\net472\Project5.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project87\Debug\net472\Project87.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Core.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Data.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Drawing.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.IO.Compression.FileSystem.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Numerics.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Runtime.Serialization.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Xml.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Xml.Linq.dll"
               @"--target:library"
               @"--nowarn:FS2003,NU5105"
               @"--warn:3"
               @"--warnaserror:3239,1182,0025"
               @"--fullpaths"
               @"--flaterrors"
               @"--subsystemversion:6.00"
               @"--highentropyva+"
               @"--targetprofile:mscorlib"
               @"--nocopyfsharpcore"
               @"--deterministic+"
               @"--simpleresolution"
               @"--nowarn:3384" |]
          ReferencedProjects =
            [| memoProject122 "Project122"
               memoProject151 "Project151"
               memoProject206 "Project206"
               memoProject236 "Project236"
               memoProject23 "Project23"
               memoProject243 "Project243"
               memoProject250 "Project250"
               memoProject261 "Project261"
               memoProject264 "Project264"
               memoProject272 "Project272"
               memoProject300 "Project300"
               memoProject302 "Project302"
               memoProject3 "Project3"
               memoProject5 "Project5" |]
          IsIncompleteTypeCheckEnvironment = false
          UseScriptResolutionRules = false
          LoadTime = DateTime.Now
          UnresolvedReferences = None
          OriginalLoadReferences = []
          Stamp = None }

    FSharpReferencedProject.CreateFSharp(
        @"C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project98\Debug\net472\Project98.dll",
        projectOptions
    )

let memoProject98 = memoization Project98

let Project3 _ =
    let projectOptions =
        { ProjectFileName = @"C:\Users\nojaf\Projects\fsharp\tests\service\data\LargeApp\Project3\Project3.fsproj"
          ProjectId = None
          SourceFiles =
            [| @"C:\Users\nojaf\Projects\fsharp\artifacts\obj\Project3\Debug\net472\Project3.AssemblyInfo.fs"
               @"C:\Users\nojaf\Projects\fsharp\tests\service\data\LargeApp\Project3\MyModule.fs" |]
          OtherOptions =
            [| @"-o:C:\Users\nojaf\Projects\fsharp\artifacts\obj\Project3\Debug\net472\Project3.dll"
               @"-g"
               @"--debug:portable"
               @"--noframework"
               @"--define:TRACE"
               @"--define:DEBUG"
               @"--define:ENABLE_MONO_SUPPORT"
               @"--define:NETFRAMEWORK"
               @"--define:NET472"
               @"--define:NET20_OR_GREATER"
               @"--define:NET30_OR_GREATER"
               @"--define:NET35_OR_GREATER"
               @"--define:NET40_OR_GREATER"
               @"--define:NET45_OR_GREATER"
               @"--define:NET451_OR_GREATER"
               @"--define:NET452_OR_GREATER"
               @"--define:NET46_OR_GREATER"
               @"--define:NET461_OR_GREATER"
               @"--define:NET462_OR_GREATER"
               @"--define:NET47_OR_GREATER"
               @"--define:NET471_OR_GREATER"
               @"--define:NET472_OR_GREATER"
               @"--doc:C:\Users\nojaf\Projects\fsharp\artifacts\obj\Project3\Debug\net472\Project3.xml"
               @"--publicsign+"
               @"--optimize-"
               @"--tailcalls-"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\mscorlib.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project138\Debug\net472\Project138.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project140\Debug\net472\Project140.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project142\Debug\net472\Project142.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project144\Debug\net472\Project144.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project146\Debug\net472\Project146.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project149\Debug\net472\Project149.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project151\Debug\net472\Project151.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project161\Debug\net472\Project161.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project164\Debug\net472\Project164.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project189\Debug\net472\Project189.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project206\Debug\net472\Project206.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project209\Debug\net472\Project209.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project214\Debug\net472\Project214.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project236\Debug\net472\Project236.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project243\Debug\net472\Project243.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project250\Debug\net472\Project250.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project261\Debug\net472\Project261.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project264\Debug\net472\Project264.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project272\Debug\net472\Project272.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project295\Debug\net472\Project295.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project311\Debug\net472\Project311.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Core.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Data.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Drawing.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.IO.Compression.FileSystem.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Numerics.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Runtime.Serialization.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Xml.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Xml.Linq.dll"
               @"--target:library"
               @"--nowarn:FS2003,NU5105"
               @"--warn:3"
               @"--warnaserror:3239,1182,0025"
               @"--fullpaths"
               @"--flaterrors"
               @"--subsystemversion:6.00"
               @"--highentropyva+"
               @"--targetprofile:mscorlib"
               @"--nocopyfsharpcore"
               @"--deterministic+"
               @"--simpleresolution"
               @"--nowarn:3384" |]
          ReferencedProjects =
            [| memoProject151 "Project151"
               memoProject206 "Project206"
               memoProject236 "Project236"
               memoProject243 "Project243"
               memoProject250 "Project250"
               memoProject261 "Project261"
               memoProject264 "Project264"
               memoProject272 "Project272"
               memoProject311 "Project311" |]
          IsIncompleteTypeCheckEnvironment = false
          UseScriptResolutionRules = false
          LoadTime = DateTime.Now
          UnresolvedReferences = None
          OriginalLoadReferences = []
          Stamp = None }

    FSharpReferencedProject.CreateFSharp(
        @"C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project3\Debug\net472\Project3.dll",
        projectOptions
    )

let memoProject3 = memoization Project3

let Project5 _ =
    let projectOptions =
        { ProjectFileName = @"C:\Users\nojaf\Projects\fsharp\tests\service\data\LargeApp\Project5\Project5.fsproj"
          ProjectId = None
          SourceFiles =
            [| @"C:\Users\nojaf\Projects\fsharp\artifacts\obj\Project5\Debug\net472\Project5.AssemblyInfo.fs"
               @"C:\Users\nojaf\Projects\fsharp\tests\service\data\LargeApp\Project5\MyModule.fs" |]
          OtherOptions =
            [| @"-o:C:\Users\nojaf\Projects\fsharp\artifacts\obj\Project5\Debug\net472\Project5.dll"
               @"-g"
               @"--debug:portable"
               @"--noframework"
               @"--define:TRACE"
               @"--define:DEBUG"
               @"--define:ENABLE_MONO_SUPPORT"
               @"--define:NETFRAMEWORK"
               @"--define:NET472"
               @"--define:NET20_OR_GREATER"
               @"--define:NET30_OR_GREATER"
               @"--define:NET35_OR_GREATER"
               @"--define:NET40_OR_GREATER"
               @"--define:NET45_OR_GREATER"
               @"--define:NET451_OR_GREATER"
               @"--define:NET452_OR_GREATER"
               @"--define:NET46_OR_GREATER"
               @"--define:NET461_OR_GREATER"
               @"--define:NET462_OR_GREATER"
               @"--define:NET47_OR_GREATER"
               @"--define:NET471_OR_GREATER"
               @"--define:NET472_OR_GREATER"
               @"--doc:C:\Users\nojaf\Projects\fsharp\artifacts\obj\Project5\Debug\net472\Project5.xml"
               @"--publicsign+"
               @"--optimize-"
               @"--tailcalls-"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\mscorlib.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project138\Debug\net472\Project138.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project140\Debug\net472\Project140.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project142\Debug\net472\Project142.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project144\Debug\net472\Project144.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project146\Debug\net472\Project146.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project149\Debug\net472\Project149.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project151\Debug\net472\Project151.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project161\Debug\net472\Project161.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project164\Debug\net472\Project164.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project168\Debug\net472\Project168.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project172\Debug\net472\Project172.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project184\Debug\net472\Project184.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project189\Debug\net472\Project189.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project190\Debug\net472\Project190.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project192\Debug\net472\Project192.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project206\Debug\net472\Project206.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project213\Debug\net472\Project213.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project214\Debug\net472\Project214.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project222\Debug\net472\Project222.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project232\Debug\net472\Project232.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project243\Debug\net472\Project243.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project250\Debug\net472\Project250.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project261\Debug\net472\Project261.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project264\Debug\net472\Project264.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project272\Debug\net472\Project272.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project273\Debug\net472\Project273.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project281\Debug\net472\Project281.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project295\Debug\net472\Project295.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project297\Debug\net472\Project297.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project3\Debug\net472\Project3.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project323\Debug\net472\Project323.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project324\Debug\net472\Project324.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project326\Debug\net472\Project326.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project329\Debug\net472\Project329.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project331\Debug\net472\Project331.dll"
               @"-r:C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project346\Debug\net472\Project346.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Core.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Data.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Drawing.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.IO.Compression.FileSystem.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Numerics.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Runtime.Serialization.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Xml.dll"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\System.Xml.Linq.dll"
               @"--target:library"
               @"--nowarn:FS2003,NU5105"
               @"--warn:3"
               @"--warnaserror:3239,1182,0025"
               @"--fullpaths"
               @"--flaterrors"
               @"--subsystemversion:6.00"
               @"--highentropyva+"
               @"--targetprofile:mscorlib"
               @"--nocopyfsharpcore"
               @"--deterministic+"
               @"--simpleresolution"
               @"--nowarn:3384" |]
          ReferencedProjects =
            [| memoProject151 "Project151"
               memoProject206 "Project206"
               memoProject213 "Project213"
               memoProject222 "Project222"
               memoProject232 "Project232"
               memoProject243 "Project243"
               memoProject250 "Project250"
               memoProject261 "Project261"
               memoProject264 "Project264"
               memoProject272 "Project272"
               memoProject273 "Project273"
               memoProject281 "Project281"
               memoProject324 "Project324"
               memoProject329 "Project329"
               memoProject331 "Project331"
               memoProject346 "Project346"
               memoProject3 "Project3" |]
          IsIncompleteTypeCheckEnvironment = false
          UseScriptResolutionRules = false
          LoadTime = DateTime.Now
          UnresolvedReferences = None
          OriginalLoadReferences = []
          Stamp = None }

    FSharpReferencedProject.CreateFSharp(
        @"C:\Users\nojaf\Projects\fsharp\artifacts\bin\Project5\Debug\net472\Project5.dll",
        projectOptions
    )

let memoProject5 = memoization Project5
