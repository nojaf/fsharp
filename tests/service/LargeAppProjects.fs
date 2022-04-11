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
        { ProjectFileName = __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project0\Project0.fsproj"
          ProjectId = None
          SourceFiles =
            [| __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project0\obj\Debug\net472\Project0.AssemblyInfo.fs"
               __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project0\MyModule.fs" |]
          OtherOptions =
            [| "-o:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project0\obj\Debug\net472\Project0.dll"
               @"-g"
               @"--debug:portable"
               @"--noframework"
               @"--define:TRACE"
               @"--define:DEBUG"
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
               @"--optimize-"
               @"--tailcalls-"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\mscorlib.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project116\bin\Debug\net472\Project116.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project118\bin\Debug\net472\Project118.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project140\bin\Debug\net472\Project140.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project142\bin\Debug\net472\Project142.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project144\bin\Debug\net472\Project144.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project146\bin\Debug\net472\Project146.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project149\bin\Debug\net472\Project149.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project151\bin\Debug\net472\Project151.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project161\bin\Debug\net472\Project161.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project164\bin\Debug\net472\Project164.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project168\bin\Debug\net472\Project168.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project17\bin\Debug\net472\Project17.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project189\bin\Debug\net472\Project189.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project190\bin\Debug\net472\Project190.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project22\bin\Debug\net472\Project22.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project23\bin\Debug\net472\Project23.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project243\bin\Debug\net472\Project243.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project264\bin\Debug\net472\Project264.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project272\bin\Debug\net472\Project272.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project281\bin\Debug\net472\Project281.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project323\bin\Debug\net472\Project323.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project326\bin\Debug\net472\Project326.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project327\bin\Debug\net472\Project327.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project329\bin\Debug\net472\Project329.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project346\bin\Debug\net472\Project346.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project40\bin\Debug\net472\Project40.dll"
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
               @"--warn:3"
               @"--warnaserror:3239"
               @"--fullpaths"
               @"--flaterrors"
               @"--subsystemversion:6.00"
               @"--highentropyva+"
               @"--targetprofile:mscorlib"
               @"--nocopyfsharpcore"
               @"--deterministic+"
               @"--simpleresolution" |]
          ReferencedProjects =
            [| memoProject116 "Project116"
               memoProject118 "Project118"
               Project140
               Project142
               Project144
               Project146
               Project149
               memoProject151 "Project151"
               Project161
               Project164
               Project168
               memoProject17 "Project17"
               Project189
               Project190
               memoProject22 "Project22"
               memoProject23 "Project23"
               memoProject243 "Project243"
               memoProject264 "Project264"
               memoProject272 "Project272"
               memoProject281 "Project281"
               Project323
               Project326
               Project327
               memoProject329 "Project329"
               memoProject346 "Project346"
               memoProject40 "Project40" |]
          IsIncompleteTypeCheckEnvironment = false
          UseScriptResolutionRules = false
          LoadTime = DateTime.Now
          UnresolvedReferences = None
          OriginalLoadReferences = []
          Stamp = Some 0L }

    FSharpReferencedProject.CreateFSharp(
        __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project0\bin\Debug\net472\Project0.dll",
        projectOptions
    )

let memoProject0 = memoization Project0

let Project116 _ =
    let projectOptions =
        { ProjectFileName = __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project116\Project116.fsproj"
          ProjectId = None
          SourceFiles =
            [| __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project116\obj\Debug\net472\Project116.AssemblyInfo.fs"
               __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project116\MyModule.fs" |]
          OtherOptions =
            [| "-o:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project116\obj\Debug\net472\Project116.dll"
               @"-g"
               @"--debug:portable"
               @"--noframework"
               @"--define:TRACE"
               @"--define:DEBUG"
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
               @"--optimize-"
               @"--tailcalls-"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\mscorlib.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project11\bin\Debug\net472\Project11.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project117\bin\Debug\net472\Project117.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project14\bin\Debug\net472\Project14.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project140\bin\Debug\net472\Project140.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project142\bin\Debug\net472\Project142.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project146\bin\Debug\net472\Project146.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project149\bin\Debug\net472\Project149.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project151\bin\Debug\net472\Project151.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project161\bin\Debug\net472\Project161.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project164\bin\Debug\net472\Project164.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project168\bin\Debug\net472\Project168.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project17\bin\Debug\net472\Project17.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project21\bin\Debug\net472\Project21.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project22\bin\Debug\net472\Project22.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project23\bin\Debug\net472\Project23.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project24\bin\Debug\net472\Project24.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project243\bin\Debug\net472\Project243.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project272\bin\Debug\net472\Project272.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project298\bin\Debug\net472\Project298.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project326\bin\Debug\net472\Project326.dll"
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
               @"--warn:3"
               @"--warnaserror:3239"
               @"--fullpaths"
               @"--flaterrors"
               @"--subsystemversion:6.00"
               @"--highentropyva+"
               @"--targetprofile:mscorlib"
               @"--nocopyfsharpcore"
               @"--deterministic+"
               @"--simpleresolution" |]
          ReferencedProjects =
            [| memoProject117 "Project117"
               memoProject11 "Project11"
               Project140
               Project142
               Project146
               Project149
               memoProject14 "Project14"
               memoProject151 "Project151"
               Project161
               Project164
               Project168
               memoProject17 "Project17"
               memoProject21 "Project21"
               memoProject22 "Project22"
               memoProject23 "Project23"
               memoProject243 "Project243"
               memoProject24 "Project24"
               memoProject272 "Project272"
               Project298
               Project326 |]
          IsIncompleteTypeCheckEnvironment = false
          UseScriptResolutionRules = false
          LoadTime = DateTime.Now
          UnresolvedReferences = None
          OriginalLoadReferences = []
          Stamp = Some 116L }

    FSharpReferencedProject.CreateFSharp(
        __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project116\bin\Debug\net472\Project116.dll",
        projectOptions
    )

let memoProject116 = memoization Project116

let Project117 _ =
    let projectOptions =
        { ProjectFileName = __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project117\Project117.fsproj"
          ProjectId = None
          SourceFiles =
            [| __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project117\obj\Debug\net472\Project117.AssemblyInfo.fs"
               __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project117\MyModule.fs" |]
          OtherOptions =
            [| "-o:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project117\obj\Debug\net472\Project117.dll"
               @"-g"
               @"--debug:portable"
               @"--noframework"
               @"--define:TRACE"
               @"--define:DEBUG"
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
               @"--optimize-"
               @"--tailcalls-"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\mscorlib.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project14\bin\Debug\net472\Project14.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project140\bin\Debug\net472\Project140.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project142\bin\Debug\net472\Project142.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project149\bin\Debug\net472\Project149.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project151\bin\Debug\net472\Project151.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project161\bin\Debug\net472\Project161.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project164\bin\Debug\net472\Project164.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project168\bin\Debug\net472\Project168.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project17\bin\Debug\net472\Project17.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project21\bin\Debug\net472\Project21.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project214\bin\Debug\net472\Project214.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project22\bin\Debug\net472\Project22.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project24\bin\Debug\net472\Project24.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project295\bin\Debug\net472\Project295.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project298\bin\Debug\net472\Project298.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project326\bin\Debug\net472\Project326.dll"
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
               @"--warn:3"
               @"--warnaserror:3239"
               @"--fullpaths"
               @"--flaterrors"
               @"--subsystemversion:6.00"
               @"--highentropyva+"
               @"--targetprofile:mscorlib"
               @"--nocopyfsharpcore"
               @"--deterministic+"
               @"--simpleresolution" |]
          ReferencedProjects =
            [| Project140
               Project142
               Project149
               memoProject14 "Project14"
               memoProject151 "Project151"
               Project161
               Project164
               Project168
               memoProject17 "Project17"
               Project214
               memoProject21 "Project21"
               memoProject22 "Project22"
               memoProject24 "Project24"
               Project295
               Project298
               Project326 |]
          IsIncompleteTypeCheckEnvironment = false
          UseScriptResolutionRules = false
          LoadTime = DateTime.Now
          UnresolvedReferences = None
          OriginalLoadReferences = []
          Stamp = Some 117L }

    FSharpReferencedProject.CreateFSharp(
        __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project117\bin\Debug\net472\Project117.dll",
        projectOptions
    )

let memoProject117 = memoization Project117

let Project140 =
    FSharpReferencedProject.CreatePortableExecutable(
        __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project140\bin\Debug\net472\Project140.dll",
        getTimeStamp,
        loader
    )

let Project147 =
    FSharpReferencedProject.CreatePortableExecutable(
        __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project147\bin\Debug\net472\Project147.dll",
        getTimeStamp,
        loader
    )

let Project204 =
    FSharpReferencedProject.CreatePortableExecutable(
        __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project204\bin\Debug\net472\Project204.dll",
        getTimeStamp,
        loader
    )

let Project138 =
    FSharpReferencedProject.CreatePortableExecutable(
        __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project138\bin\Debug\net472\Project138.dll",
        getTimeStamp,
        loader
    )

let Project189 =
    FSharpReferencedProject.CreatePortableExecutable(
        __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project189\bin\Debug\net472\Project189.dll",
        getTimeStamp,
        loader
    )

let Project172 =
    FSharpReferencedProject.CreatePortableExecutable(
        __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project172\bin\Debug\net472\Project172.dll",
        getTimeStamp,
        loader
    )

let Project164 =
    FSharpReferencedProject.CreatePortableExecutable(
        __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project164\bin\Debug\net472\Project164.dll",
        getTimeStamp,
        loader
    )

let Project144 =
    FSharpReferencedProject.CreatePortableExecutable(
        __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project144\bin\Debug\net472\Project144.dll",
        getTimeStamp,
        loader
    )

let Project146 =
    FSharpReferencedProject.CreatePortableExecutable(
        __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project146\bin\Debug\net472\Project146.dll",
        getTimeStamp,
        loader
    )

let Project142 =
    FSharpReferencedProject.CreatePortableExecutable(
        __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project142\bin\Debug\net472\Project142.dll",
        getTimeStamp,
        loader
    )

let Project336 =
    FSharpReferencedProject.CreatePortableExecutable(
        __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project336\bin\Debug\net472\Project336.dll",
        getTimeStamp,
        loader
    )

let Project175 =
    FSharpReferencedProject.CreatePortableExecutable(
        __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project175\bin\Debug\net472\Project175.dll",
        getTimeStamp,
        loader
    )

let Project161 =
    FSharpReferencedProject.CreatePortableExecutable(
        __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project161\bin\Debug\net472\Project161.dll",
        getTimeStamp,
        loader
    )

let Project264 _ =
    let projectOptions =
        { ProjectFileName = __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project264\Project264.fsproj"
          ProjectId = None
          SourceFiles =
            [| __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project264\obj\Debug\net472\Project264.AssemblyInfo.fs"
               __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project264\MyModule.fs" |]
          OtherOptions =
            [| "-o:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project264\obj\Debug\net472\Project264.dll"
               @"-g"
               @"--debug:portable"
               @"--noframework"
               @"--define:TRACE"
               @"--define:DEBUG"
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
               @"--warn:3"
               @"--warnaserror:3239"
               @"--fullpaths"
               @"--flaterrors"
               @"--subsystemversion:6.00"
               @"--highentropyva+"
               @"--targetprofile:mscorlib"
               @"--nocopyfsharpcore"
               @"--deterministic+"
               @"--simpleresolution" |]
          ReferencedProjects = [||]
          IsIncompleteTypeCheckEnvironment = false
          UseScriptResolutionRules = false
          LoadTime = DateTime.Now
          UnresolvedReferences = None
          OriginalLoadReferences = []
          Stamp = Some 264L }

    FSharpReferencedProject.CreateFSharp(
        __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project264\bin\Debug\net472\Project264.dll",
        projectOptions
    )

let memoProject264 = memoization Project264

let Project210 =
    FSharpReferencedProject.CreatePortableExecutable(
        __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project210\bin\Debug\net472\Project210.dll",
        getTimeStamp,
        loader
    )

let Project214 =
    FSharpReferencedProject.CreatePortableExecutable(
        __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project214\bin\Debug\net472\Project214.dll",
        getTimeStamp,
        loader
    )

let Project168 =
    FSharpReferencedProject.CreatePortableExecutable(
        __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project168\bin\Debug\net472\Project168.dll",
        getTimeStamp,
        loader
    )

let Project157 =
    FSharpReferencedProject.CreatePortableExecutable(
        __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project157\bin\Debug\net472\Project157.dll",
        getTimeStamp,
        loader
    )

let Project179 =
    FSharpReferencedProject.CreatePortableExecutable(
        __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project179\bin\Debug\net472\Project179.dll",
        getTimeStamp,
        loader
    )

let Project177 =
    FSharpReferencedProject.CreatePortableExecutable(
        __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project177\bin\Debug\net472\Project177.dll",
        getTimeStamp,
        loader
    )

let Project149 =
    FSharpReferencedProject.CreatePortableExecutable(
        __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project149\bin\Debug\net472\Project149.dll",
        getTimeStamp,
        loader
    )

let Project209 =
    FSharpReferencedProject.CreatePortableExecutable(
        __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project209\bin\Debug\net472\Project209.dll",
        getTimeStamp,
        loader
    )

let Project216 =
    FSharpReferencedProject.CreatePortableExecutable(
        __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project216\bin\Debug\net472\Project216.dll",
        getTimeStamp,
        loader
    )

let Project173 =
    FSharpReferencedProject.CreatePortableExecutable(
        __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project173\bin\Debug\net472\Project173.dll",
        getTimeStamp,
        loader
    )

let Project221 =
    FSharpReferencedProject.CreatePortableExecutable(
        __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project221\bin\Debug\net472\Project221.dll",
        getTimeStamp,
        loader
    )

let Project206 _ =
    let projectOptions =
        { ProjectFileName = __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project206\Project206.fsproj"
          ProjectId = None
          SourceFiles =
            [| __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project206\obj\Debug\net472\Project206.AssemblyInfo.fs"
               __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project206\MyModule.fs" |]
          OtherOptions =
            [| "-o:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project206\obj\Debug\net472\Project206.dll"
               @"-g"
               @"--debug:portable"
               @"--noframework"
               @"--define:TRACE"
               @"--define:DEBUG"
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
               @"--optimize-"
               @"--tailcalls-"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\mscorlib.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project144\bin\Debug\net472\Project144.dll"
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
               @"--warn:3"
               @"--warnaserror:3239"
               @"--fullpaths"
               @"--flaterrors"
               @"--subsystemversion:6.00"
               @"--highentropyva+"
               @"--targetprofile:mscorlib"
               @"--nocopyfsharpcore"
               @"--deterministic+"
               @"--simpleresolution" |]
          ReferencedProjects = [| Project144 |]
          IsIncompleteTypeCheckEnvironment = false
          UseScriptResolutionRules = false
          LoadTime = DateTime.Now
          UnresolvedReferences = None
          OriginalLoadReferences = []
          Stamp = Some 206L }

    FSharpReferencedProject.CreateFSharp(
        __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project206\bin\Debug\net472\Project206.dll",
        projectOptions
    )

let memoProject206 = memoization Project206

let Project207 =
    FSharpReferencedProject.CreatePortableExecutable(
        __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project207\bin\Debug\net472\Project207.dll",
        getTimeStamp,
        loader
    )

let Project217 =
    FSharpReferencedProject.CreatePortableExecutable(
        __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project217\bin\Debug\net472\Project217.dll",
        getTimeStamp,
        loader
    )

let Project166 =
    FSharpReferencedProject.CreatePortableExecutable(
        __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project166\bin\Debug\net472\Project166.dll",
        getTimeStamp,
        loader
    )

let Project278 =
    FSharpReferencedProject.CreatePortableExecutable(
        __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project278\bin\Debug\net472\Project278.dll",
        getTimeStamp,
        loader
    )

let Project317 =
    FSharpReferencedProject.CreatePortableExecutable(
        __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project317\bin\Debug\net472\Project317.dll",
        getTimeStamp,
        loader
    )

let Project310 _ =
    let projectOptions =
        { ProjectFileName = __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project310\Project310.fsproj"
          ProjectId = None
          SourceFiles =
            [| __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project310\obj\Debug\net472\Project310.AssemblyInfo.fs"
               __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project310\MyModule.fs" |]
          OtherOptions =
            [| "-o:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project310\obj\Debug\net472\Project310.dll"
               @"-g"
               @"--debug:portable"
               @"--noframework"
               @"--define:TRACE"
               @"--define:DEBUG"
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
               @"--optimize-"
               @"--tailcalls-"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\mscorlib.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project264\bin\Debug\net472\Project264.dll"
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
               @"--warn:3"
               @"--warnaserror:3239"
               @"--fullpaths"
               @"--flaterrors"
               @"--subsystemversion:6.00"
               @"--highentropyva+"
               @"--targetprofile:mscorlib"
               @"--nocopyfsharpcore"
               @"--deterministic+"
               @"--simpleresolution" |]
          ReferencedProjects = [| memoProject264 "Project264" |]
          IsIncompleteTypeCheckEnvironment = false
          UseScriptResolutionRules = false
          LoadTime = DateTime.Now
          UnresolvedReferences = None
          OriginalLoadReferences = []
          Stamp = Some 310L }

    FSharpReferencedProject.CreateFSharp(
        __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project310\bin\Debug\net472\Project310.dll",
        projectOptions
    )

let memoProject310 = memoization Project310

let Project14 _ =
    let projectOptions =
        { ProjectFileName = __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project14\Project14.fsproj"
          ProjectId = None
          SourceFiles =
            [| __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project14\obj\Debug\net472\Project14.AssemblyInfo.fs"
               __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project14\MyModule.fs" |]
          OtherOptions =
            [| "-o:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project14\obj\Debug\net472\Project14.dll"
               @"-g"
               @"--debug:portable"
               @"--noframework"
               @"--define:TRACE"
               @"--define:DEBUG"
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
               @"--optimize-"
               @"--tailcalls-"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\mscorlib.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project138\bin\Debug\net472\Project138.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project140\bin\Debug\net472\Project140.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project144\bin\Debug\net472\Project144.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project146\bin\Debug\net472\Project146.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project151\bin\Debug\net472\Project151.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project164\bin\Debug\net472\Project164.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project22\bin\Debug\net472\Project22.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project243\bin\Debug\net472\Project243.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project272\bin\Debug\net472\Project272.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project289\bin\Debug\net472\Project289.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project295\bin\Debug\net472\Project295.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project298\bin\Debug\net472\Project298.dll"
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
               @"--warn:3"
               @"--warnaserror:3239"
               @"--fullpaths"
               @"--flaterrors"
               @"--subsystemversion:6.00"
               @"--highentropyva+"
               @"--targetprofile:mscorlib"
               @"--nocopyfsharpcore"
               @"--deterministic+"
               @"--simpleresolution" |]
          ReferencedProjects =
            [| Project138
               Project140
               Project144
               Project146
               memoProject151 "Project151"
               Project164
               memoProject22 "Project22"
               memoProject243 "Project243"
               memoProject272 "Project272"
               memoProject289 "Project289"
               Project295
               Project298 |]
          IsIncompleteTypeCheckEnvironment = false
          UseScriptResolutionRules = false
          LoadTime = DateTime.Now
          UnresolvedReferences = None
          OriginalLoadReferences = []
          Stamp = Some 14L }

    FSharpReferencedProject.CreateFSharp(
        __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project14\bin\Debug\net472\Project14.dll",
        projectOptions
    )

let memoProject14 = memoization Project14

let Project151 _ =
    let projectOptions =
        { ProjectFileName = __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project151\Project151.fsproj"
          ProjectId = None
          SourceFiles =
            [| __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project151\obj\Debug\net472\Project151.AssemblyInfo.fs"
               __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project151\MyModule.fs" |]
          OtherOptions =
            [| "-o:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project151\obj\Debug\net472\Project151.dll"
               @"-g"
               @"--debug:portable"
               @"--noframework"
               @"--define:TRACE"
               @"--define:DEBUG"
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
               @"--optimize-"
               @"--tailcalls-"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\mscorlib.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project222\bin\Debug\net472\Project222.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project272\bin\Debug\net472\Project272.dll"
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
               @"--warn:3"
               @"--warnaserror:3239"
               @"--fullpaths"
               @"--flaterrors"
               @"--subsystemversion:6.00"
               @"--highentropyva+"
               @"--targetprofile:mscorlib"
               @"--nocopyfsharpcore"
               @"--deterministic+"
               @"--simpleresolution" |]
          ReferencedProjects =
            [| memoProject222 "Project222"
               memoProject272 "Project272" |]
          IsIncompleteTypeCheckEnvironment = false
          UseScriptResolutionRules = false
          LoadTime = DateTime.Now
          UnresolvedReferences = None
          OriginalLoadReferences = []
          Stamp = Some 151L }

    FSharpReferencedProject.CreateFSharp(
        __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project151\bin\Debug\net472\Project151.dll",
        projectOptions
    )

let memoProject151 = memoization Project151

let Project222 _ =
    let projectOptions =
        { ProjectFileName = __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project222\Project222.fsproj"
          ProjectId = None
          SourceFiles =
            [| __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project222\obj\Debug\net472\Project222.AssemblyInfo.fs"
               __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project222\MyModule.fs" |]
          OtherOptions =
            [| "-o:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project222\obj\Debug\net472\Project222.dll"
               @"-g"
               @"--debug:portable"
               @"--noframework"
               @"--define:TRACE"
               @"--define:DEBUG"
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
               @"--optimize-"
               @"--tailcalls-"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\mscorlib.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project193\bin\Debug\net472\Project193.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project224\bin\Debug\net472\Project224.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project273\bin\Debug\net472\Project273.dll"
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
               @"--warn:3"
               @"--warnaserror:3239"
               @"--fullpaths"
               @"--flaterrors"
               @"--subsystemversion:6.00"
               @"--highentropyva+"
               @"--targetprofile:mscorlib"
               @"--nocopyfsharpcore"
               @"--deterministic+"
               @"--simpleresolution" |]
          ReferencedProjects =
            [| Project193
               Project224
               memoProject273 "Project273" |]
          IsIncompleteTypeCheckEnvironment = false
          UseScriptResolutionRules = false
          LoadTime = DateTime.Now
          UnresolvedReferences = None
          OriginalLoadReferences = []
          Stamp = Some 222L }

    FSharpReferencedProject.CreateFSharp(
        __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project222\bin\Debug\net472\Project222.dll",
        projectOptions
    )

let memoProject222 = memoization Project222

let Project193 =
    FSharpReferencedProject.CreatePortableExecutable(
        __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project193\bin\Debug\net472\Project193.dll",
        getTimeStamp,
        loader
    )

let Project224 =
    FSharpReferencedProject.CreatePortableExecutable(
        __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project224\bin\Debug\net472\Project224.dll",
        getTimeStamp,
        loader
    )

let Project162 =
    FSharpReferencedProject.CreatePortableExecutable(
        __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project162\bin\Debug\net472\Project162.dll",
        getTimeStamp,
        loader
    )

let Project199 =
    FSharpReferencedProject.CreatePortableExecutable(
        __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project199\bin\Debug\net472\Project199.dll",
        getTimeStamp,
        loader
    )

let Project192 =
    FSharpReferencedProject.CreatePortableExecutable(
        __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project192\bin\Debug\net472\Project192.dll",
        getTimeStamp,
        loader
    )

let Project127 =
    FSharpReferencedProject.CreatePortableExecutable(
        __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project127\bin\Debug\net472\Project127.dll",
        getTimeStamp,
        loader
    )

let Project130 =
    FSharpReferencedProject.CreatePortableExecutable(
        __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project130\bin\Debug\net472\Project130.dll",
        getTimeStamp,
        loader
    )

let Project126 =
    FSharpReferencedProject.CreatePortableExecutable(
        __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project126\bin\Debug\net472\Project126.dll",
        getTimeStamp,
        loader
    )

let Project131 =
    FSharpReferencedProject.CreatePortableExecutable(
        __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project131\bin\Debug\net472\Project131.dll",
        getTimeStamp,
        loader
    )

let Project134 =
    FSharpReferencedProject.CreatePortableExecutable(
        __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project134\bin\Debug\net472\Project134.dll",
        getTimeStamp,
        loader
    )

let Project123 =
    FSharpReferencedProject.CreatePortableExecutable(
        __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project123\bin\Debug\net472\Project123.dll",
        getTimeStamp,
        loader
    )

let Project184 =
    FSharpReferencedProject.CreatePortableExecutable(
        __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project184\bin\Debug\net472\Project184.dll",
        getTimeStamp,
        loader
    )

let Project187 =
    FSharpReferencedProject.CreatePortableExecutable(
        __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project187\bin\Debug\net472\Project187.dll",
        getTimeStamp,
        loader
    )

let Project195 =
    FSharpReferencedProject.CreatePortableExecutable(
        __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project195\bin\Debug\net472\Project195.dll",
        getTimeStamp,
        loader
    )

let Project314 =
    FSharpReferencedProject.CreatePortableExecutable(
        __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project314\bin\Debug\net472\Project314.dll",
        getTimeStamp,
        loader
    )

let Project315 =
    FSharpReferencedProject.CreatePortableExecutable(
        __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project315\bin\Debug\net472\Project315.dll",
        getTimeStamp,
        loader
    )

let Project273 _ =
    let projectOptions =
        { ProjectFileName = __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project273\Project273.fsproj"
          ProjectId = None
          SourceFiles =
            [| __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project273\obj\Debug\net472\Project273.AssemblyInfo.fs"
               __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project273\MyModule.fs" |]
          OtherOptions =
            [| "-o:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project273\obj\Debug\net472\Project273.dll"
               @"-g"
               @"--debug:portable"
               @"--noframework"
               @"--define:TRACE"
               @"--define:DEBUG"
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
               @"--optimize-"
               @"--tailcalls-"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\mscorlib.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project138\bin\Debug\net472\Project138.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project164\bin\Debug\net472\Project164.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project209\bin\Debug\net472\Project209.dll"
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
               @"--warn:3"
               @"--warnaserror:3239"
               @"--fullpaths"
               @"--flaterrors"
               @"--subsystemversion:6.00"
               @"--highentropyva+"
               @"--targetprofile:mscorlib"
               @"--nocopyfsharpcore"
               @"--deterministic+"
               @"--simpleresolution" |]
          ReferencedProjects = [| Project138; Project164; Project209 |]
          IsIncompleteTypeCheckEnvironment = false
          UseScriptResolutionRules = false
          LoadTime = DateTime.Now
          UnresolvedReferences = None
          OriginalLoadReferences = []
          Stamp = Some 273L }

    FSharpReferencedProject.CreateFSharp(
        __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project273\bin\Debug\net472\Project273.dll",
        projectOptions
    )

let memoProject273 = memoization Project273

let Project272 _ =
    let projectOptions =
        { ProjectFileName = __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project272\Project272.fsproj"
          ProjectId = None
          SourceFiles =
            [| __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project272\obj\Debug\net472\Project272.AssemblyInfo.fs"
               __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project272\MyModule.fs" |]
          OtherOptions =
            [| "-o:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project272\obj\Debug\net472\Project272.dll"
               @"-g"
               @"--debug:portable"
               @"--noframework"
               @"--define:TRACE"
               @"--define:DEBUG"
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
               @"--optimize-"
               @"--tailcalls-"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\mscorlib.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project146\bin\Debug\net472\Project146.dll"
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
               @"--warn:3"
               @"--warnaserror:3239"
               @"--fullpaths"
               @"--flaterrors"
               @"--subsystemversion:6.00"
               @"--highentropyva+"
               @"--targetprofile:mscorlib"
               @"--nocopyfsharpcore"
               @"--deterministic+"
               @"--simpleresolution" |]
          ReferencedProjects = [| Project146 |]
          IsIncompleteTypeCheckEnvironment = false
          UseScriptResolutionRules = false
          LoadTime = DateTime.Now
          UnresolvedReferences = None
          OriginalLoadReferences = []
          Stamp = Some 272L }

    FSharpReferencedProject.CreateFSharp(
        __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project272\bin\Debug\net472\Project272.dll",
        projectOptions
    )

let memoProject272 = memoization Project272

let Project22 _ =
    let projectOptions =
        { ProjectFileName = __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project22\Project22.fsproj"
          ProjectId = None
          SourceFiles =
            [| __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project22\obj\Debug\net472\Project22.AssemblyInfo.fs"
               __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project22\MyModule.fs" |]
          OtherOptions =
            [| "-o:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project22\obj\Debug\net472\Project22.dll"
               @"-g"
               @"--debug:portable"
               @"--noframework"
               @"--define:TRACE"
               @"--define:DEBUG"
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
               @"--optimize-"
               @"--tailcalls-"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\mscorlib.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project138\bin\Debug\net472\Project138.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project140\bin\Debug\net472\Project140.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project142\bin\Debug\net472\Project142.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project144\bin\Debug\net472\Project144.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project146\bin\Debug\net472\Project146.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project149\bin\Debug\net472\Project149.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project151\bin\Debug\net472\Project151.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project161\bin\Debug\net472\Project161.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project164\bin\Debug\net472\Project164.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project172\bin\Debug\net472\Project172.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project189\bin\Debug\net472\Project189.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project192\bin\Debug\net472\Project192.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project213\bin\Debug\net472\Project213.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project214\bin\Debug\net472\Project214.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project243\bin\Debug\net472\Project243.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project250\bin\Debug\net472\Project250.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project262\bin\Debug\net472\Project262.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project264\bin\Debug\net472\Project264.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project272\bin\Debug\net472\Project272.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project298\bin\Debug\net472\Project298.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project323\bin\Debug\net472\Project323.dll"
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
               @"--warn:3"
               @"--warnaserror:3239"
               @"--fullpaths"
               @"--flaterrors"
               @"--subsystemversion:6.00"
               @"--highentropyva+"
               @"--targetprofile:mscorlib"
               @"--nocopyfsharpcore"
               @"--deterministic+"
               @"--simpleresolution" |]
          ReferencedProjects =
            [| Project138
               Project140
               Project142
               Project144
               Project146
               Project149
               memoProject151 "Project151"
               Project161
               Project164
               Project172
               Project189
               Project192
               memoProject213 "Project213"
               Project214
               memoProject243 "Project243"
               memoProject250 "Project250"
               memoProject262 "Project262"
               memoProject264 "Project264"
               memoProject272 "Project272"
               Project298
               Project323 |]
          IsIncompleteTypeCheckEnvironment = false
          UseScriptResolutionRules = false
          LoadTime = DateTime.Now
          UnresolvedReferences = None
          OriginalLoadReferences = []
          Stamp = Some 22L }

    FSharpReferencedProject.CreateFSharp(
        __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project22\bin\Debug\net472\Project22.dll",
        projectOptions
    )

let memoProject22 = memoization Project22

let Project213 _ =
    let projectOptions =
        { ProjectFileName = __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project213\Project213.fsproj"
          ProjectId = None
          SourceFiles =
            [| __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project213\obj\Debug\net472\Project213.AssemblyInfo.fs"
               __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project213\MyModule.fs" |]
          OtherOptions =
            [| "-o:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project213\obj\Debug\net472\Project213.dll"
               @"-g"
               @"--debug:portable"
               @"--noframework"
               @"--define:TRACE"
               @"--define:DEBUG"
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
               @"--optimize-"
               @"--tailcalls-"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\mscorlib.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project243\bin\Debug\net472\Project243.dll"
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
               @"--warn:3"
               @"--warnaserror:3239"
               @"--fullpaths"
               @"--flaterrors"
               @"--subsystemversion:6.00"
               @"--highentropyva+"
               @"--targetprofile:mscorlib"
               @"--nocopyfsharpcore"
               @"--deterministic+"
               @"--simpleresolution" |]
          ReferencedProjects = [| memoProject243 "Project243" |]
          IsIncompleteTypeCheckEnvironment = false
          UseScriptResolutionRules = false
          LoadTime = DateTime.Now
          UnresolvedReferences = None
          OriginalLoadReferences = []
          Stamp = Some 213L }

    FSharpReferencedProject.CreateFSharp(
        __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project213\bin\Debug\net472\Project213.dll",
        projectOptions
    )

let memoProject213 = memoization Project213

let Project243 _ =
    let projectOptions =
        { ProjectFileName = __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project243\Project243.fsproj"
          ProjectId = None
          SourceFiles =
            [| __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project243\obj\Debug\net472\Project243.AssemblyInfo.fs"
               __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project243\MyModule.fs" |]
          OtherOptions =
            [| "-o:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project243\obj\Debug\net472\Project243.dll"
               @"-g"
               @"--debug:portable"
               @"--noframework"
               @"--define:TRACE"
               @"--define:DEBUG"
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
               @"--optimize-"
               @"--tailcalls-"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\mscorlib.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project236\bin\Debug\net472\Project236.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project311\bin\Debug\net472\Project311.dll"
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
               @"--warn:3"
               @"--warnaserror:3239"
               @"--fullpaths"
               @"--flaterrors"
               @"--subsystemversion:6.00"
               @"--highentropyva+"
               @"--targetprofile:mscorlib"
               @"--nocopyfsharpcore"
               @"--deterministic+"
               @"--simpleresolution" |]
          ReferencedProjects =
            [| memoProject236 "Project236"
               memoProject311 "Project311" |]
          IsIncompleteTypeCheckEnvironment = false
          UseScriptResolutionRules = false
          LoadTime = DateTime.Now
          UnresolvedReferences = None
          OriginalLoadReferences = []
          Stamp = Some 243L }

    FSharpReferencedProject.CreateFSharp(
        __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project243\bin\Debug\net472\Project243.dll",
        projectOptions
    )

let memoProject243 = memoization Project243

let Project236 _ =
    let projectOptions =
        { ProjectFileName = __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project236\Project236.fsproj"
          ProjectId = None
          SourceFiles =
            [| __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project236\obj\Debug\net472\Project236.AssemblyInfo.fs"
               __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project236\MyModule.fs" |]
          OtherOptions =
            [| "-o:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project236\obj\Debug\net472\Project236.dll"
               @"-g"
               @"--debug:portable"
               @"--noframework"
               @"--define:TRACE"
               @"--define:DEBUG"
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
               @"--optimize-"
               @"--tailcalls-"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\mscorlib.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project250\bin\Debug\net472\Project250.dll"
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
               @"--warn:3"
               @"--warnaserror:3239"
               @"--fullpaths"
               @"--flaterrors"
               @"--subsystemversion:6.00"
               @"--highentropyva+"
               @"--targetprofile:mscorlib"
               @"--nocopyfsharpcore"
               @"--deterministic+"
               @"--simpleresolution" |]
          ReferencedProjects = [| memoProject250 "Project250" |]
          IsIncompleteTypeCheckEnvironment = false
          UseScriptResolutionRules = false
          LoadTime = DateTime.Now
          UnresolvedReferences = None
          OriginalLoadReferences = []
          Stamp = Some 236L }

    FSharpReferencedProject.CreateFSharp(
        __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project236\bin\Debug\net472\Project236.dll",
        projectOptions
    )

let memoProject236 = memoization Project236

let Project250 _ =
    let projectOptions =
        { ProjectFileName = __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project250\Project250.fsproj"
          ProjectId = None
          SourceFiles =
            [| __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project250\obj\Debug\net472\Project250.AssemblyInfo.fs"
               __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project250\MyModule.fs" |]
          OtherOptions =
            [| "-o:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project250\obj\Debug\net472\Project250.dll"
               @"-g"
               @"--debug:portable"
               @"--noframework"
               @"--define:TRACE"
               @"--define:DEBUG"
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
               @"--optimize-"
               @"--tailcalls-"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\mscorlib.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project151\bin\Debug\net472\Project151.dll"
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
               @"--warn:3"
               @"--warnaserror:3239"
               @"--fullpaths"
               @"--flaterrors"
               @"--subsystemversion:6.00"
               @"--highentropyva+"
               @"--targetprofile:mscorlib"
               @"--nocopyfsharpcore"
               @"--deterministic+"
               @"--simpleresolution" |]
          ReferencedProjects = [| memoProject151 "Project151" |]
          IsIncompleteTypeCheckEnvironment = false
          UseScriptResolutionRules = false
          LoadTime = DateTime.Now
          UnresolvedReferences = None
          OriginalLoadReferences = []
          Stamp = Some 250L }

    FSharpReferencedProject.CreateFSharp(
        __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project250\bin\Debug\net472\Project250.dll",
        projectOptions
    )

let memoProject250 = memoization Project250

let Project311 _ =
    let projectOptions =
        { ProjectFileName = __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project311\Project311.fsproj"
          ProjectId = None
          SourceFiles =
            [| __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project311\obj\Debug\net472\Project311.AssemblyInfo.fs"
               __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project311\MyModule.fs" |]
          OtherOptions =
            [| "-o:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project311\obj\Debug\net472\Project311.dll"
               @"-g"
               @"--debug:portable"
               @"--noframework"
               @"--define:TRACE"
               @"--define:DEBUG"
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
               @"--optimize-"
               @"--tailcalls-"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\mscorlib.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project250\bin\Debug\net472\Project250.dll"
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
               @"--warn:3"
               @"--warnaserror:3239"
               @"--fullpaths"
               @"--flaterrors"
               @"--subsystemversion:6.00"
               @"--highentropyva+"
               @"--targetprofile:mscorlib"
               @"--nocopyfsharpcore"
               @"--deterministic+"
               @"--simpleresolution" |]
          ReferencedProjects = [| memoProject250 "Project250" |]
          IsIncompleteTypeCheckEnvironment = false
          UseScriptResolutionRules = false
          LoadTime = DateTime.Now
          UnresolvedReferences = None
          OriginalLoadReferences = []
          Stamp = Some 311L }

    FSharpReferencedProject.CreateFSharp(
        __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project311\bin\Debug\net472\Project311.dll",
        projectOptions
    )

let memoProject311 = memoization Project311

let Project262 _ =
    let projectOptions =
        { ProjectFileName = __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project262\Project262.fsproj"
          ProjectId = None
          SourceFiles =
            [| __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project262\obj\Debug\net472\Project262.AssemblyInfo.fs"
               __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project262\MyModule.fs" |]
          OtherOptions =
            [| "-o:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project262\obj\Debug\net472\Project262.dll"
               @"-g"
               @"--debug:portable"
               @"--noframework"
               @"--define:TRACE"
               @"--define:DEBUG"
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
               @"--optimize-"
               @"--tailcalls-"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\mscorlib.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project264\bin\Debug\net472\Project264.dll"
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
               @"--warn:3"
               @"--warnaserror:3239"
               @"--fullpaths"
               @"--flaterrors"
               @"--subsystemversion:6.00"
               @"--highentropyva+"
               @"--targetprofile:mscorlib"
               @"--nocopyfsharpcore"
               @"--deterministic+"
               @"--simpleresolution" |]
          ReferencedProjects = [| memoProject264 "Project264" |]
          IsIncompleteTypeCheckEnvironment = false
          UseScriptResolutionRules = false
          LoadTime = DateTime.Now
          UnresolvedReferences = None
          OriginalLoadReferences = []
          Stamp = Some 262L }

    FSharpReferencedProject.CreateFSharp(
        __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project262\bin\Debug\net472\Project262.dll",
        projectOptions
    )

let memoProject262 = memoization Project262

let Project298 =
    FSharpReferencedProject.CreatePortableExecutable(
        __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project298\bin\Debug\net472\Project298.dll",
        getTimeStamp,
        loader
    )

let Project323 =
    FSharpReferencedProject.CreatePortableExecutable(
        __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project323\bin\Debug\net472\Project323.dll",
        getTimeStamp,
        loader
    )

let Project289 _ =
    let projectOptions =
        { ProjectFileName = __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project289\Project289.fsproj"
          ProjectId = None
          SourceFiles =
            [| __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project289\obj\Debug\net472\Project289.AssemblyInfo.fs"
               __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project289\MyModule.fs" |]
          OtherOptions =
            [| "-o:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project289\obj\Debug\net472\Project289.dll"
               @"-g"
               @"--debug:portable"
               @"--noframework"
               @"--define:TRACE"
               @"--define:DEBUG"
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
               @"--optimize-"
               @"--tailcalls-"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\mscorlib.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project138\bin\Debug\net472\Project138.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project140\bin\Debug\net472\Project140.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project144\bin\Debug\net472\Project144.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project146\bin\Debug\net472\Project146.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project151\bin\Debug\net472\Project151.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project164\bin\Debug\net472\Project164.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project192\bin\Debug\net472\Project192.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project243\bin\Debug\net472\Project243.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project272\bin\Debug\net472\Project272.dll"
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
               @"--warn:3"
               @"--warnaserror:3239"
               @"--fullpaths"
               @"--flaterrors"
               @"--subsystemversion:6.00"
               @"--highentropyva+"
               @"--targetprofile:mscorlib"
               @"--nocopyfsharpcore"
               @"--deterministic+"
               @"--simpleresolution" |]
          ReferencedProjects =
            [| Project138
               Project140
               Project144
               Project146
               memoProject151 "Project151"
               Project164
               Project192
               memoProject243 "Project243"
               memoProject272 "Project272" |]
          IsIncompleteTypeCheckEnvironment = false
          UseScriptResolutionRules = false
          LoadTime = DateTime.Now
          UnresolvedReferences = None
          OriginalLoadReferences = []
          Stamp = Some 289L }

    FSharpReferencedProject.CreateFSharp(
        __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project289\bin\Debug\net472\Project289.dll",
        projectOptions
    )

let memoProject289 = memoization Project289

let Project295 =
    FSharpReferencedProject.CreatePortableExecutable(
        __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project295\bin\Debug\net472\Project295.dll",
        getTimeStamp,
        loader
    )

let Project190 =
    FSharpReferencedProject.CreatePortableExecutable(
        __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project190\bin\Debug\net472\Project190.dll",
        getTimeStamp,
        loader
    )

let Project226 =
    FSharpReferencedProject.CreatePortableExecutable(
        __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project226\bin\Debug\net472\Project226.dll",
        getTimeStamp,
        loader
    )

let Project265 _ =
    let projectOptions =
        { ProjectFileName = __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project265\Project265.fsproj"
          ProjectId = None
          SourceFiles =
            [| __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project265\obj\Debug\net472\Project265.AssemblyInfo.fs"
               __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project265\MyModule.fs" |]
          OtherOptions =
            [| "-o:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project265\obj\Debug\net472\Project265.dll"
               @"-g"
               @"--debug:portable"
               @"--noframework"
               @"--define:TRACE"
               @"--define:DEBUG"
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
               @"--warn:3"
               @"--warnaserror:3239"
               @"--fullpaths"
               @"--flaterrors"
               @"--subsystemversion:6.00"
               @"--highentropyva+"
               @"--targetprofile:mscorlib"
               @"--nocopyfsharpcore"
               @"--deterministic+"
               @"--simpleresolution" |]
          ReferencedProjects = [||]
          IsIncompleteTypeCheckEnvironment = false
          UseScriptResolutionRules = false
          LoadTime = DateTime.Now
          UnresolvedReferences = None
          OriginalLoadReferences = []
          Stamp = Some 265L }

    FSharpReferencedProject.CreateFSharp(
        __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project265\bin\Debug\net472\Project265.dll",
        projectOptions
    )

let memoProject265 = memoization Project265

let Project297 =
    FSharpReferencedProject.CreatePortableExecutable(
        __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project297\bin\Debug\net472\Project297.dll",
        getTimeStamp,
        loader
    )

let Project300 _ =
    let projectOptions =
        { ProjectFileName = __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project300\Project300.fsproj"
          ProjectId = None
          SourceFiles =
            [| __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project300\obj\Debug\net472\Project300.AssemblyInfo.fs"
               __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project300\MyModule.fs" |]
          OtherOptions =
            [| "-o:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project300\obj\Debug\net472\Project300.dll"
               @"-g"
               @"--debug:portable"
               @"--noframework"
               @"--define:TRACE"
               @"--define:DEBUG"
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
               @"--optimize-"
               @"--tailcalls-"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\mscorlib.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project144\bin\Debug\net472\Project144.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project164\bin\Debug\net472\Project164.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project172\bin\Debug\net472\Project172.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project184\bin\Debug\net472\Project184.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project189\bin\Debug\net472\Project189.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project206\bin\Debug\net472\Project206.dll"
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
               @"--warn:3"
               @"--warnaserror:3239"
               @"--fullpaths"
               @"--flaterrors"
               @"--subsystemversion:6.00"
               @"--highentropyva+"
               @"--targetprofile:mscorlib"
               @"--nocopyfsharpcore"
               @"--deterministic+"
               @"--simpleresolution" |]
          ReferencedProjects =
            [| Project144
               Project164
               Project172
               Project184
               Project189
               memoProject206 "Project206" |]
          IsIncompleteTypeCheckEnvironment = false
          UseScriptResolutionRules = false
          LoadTime = DateTime.Now
          UnresolvedReferences = None
          OriginalLoadReferences = []
          Stamp = Some 300L }

    FSharpReferencedProject.CreateFSharp(
        __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project300\bin\Debug\net472\Project300.dll",
        projectOptions
    )

let memoProject300 = memoization Project300

let Project301 =
    FSharpReferencedProject.CreatePortableExecutable(
        __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project301\bin\Debug\net472\Project301.dll",
        getTimeStamp,
        loader
    )

let Project326 =
    FSharpReferencedProject.CreatePortableExecutable(
        __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project326\bin\Debug\net472\Project326.dll",
        getTimeStamp,
        loader
    )

let Project319 _ =
    let projectOptions =
        { ProjectFileName = __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project319\Project319.fsproj"
          ProjectId = None
          SourceFiles =
            [| __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project319\obj\Debug\net472\Project319.AssemblyInfo.fs"
               __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project319\MyModule.fs" |]
          OtherOptions =
            [| "-o:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project319\obj\Debug\net472\Project319.dll"
               @"-g"
               @"--debug:portable"
               @"--noframework"
               @"--define:TRACE"
               @"--define:DEBUG"
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
               @"--optimize-"
               @"--tailcalls-"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\mscorlib.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project142\bin\Debug\net472\Project142.dll"
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
               @"--warn:3"
               @"--warnaserror:3239"
               @"--fullpaths"
               @"--flaterrors"
               @"--subsystemversion:6.00"
               @"--highentropyva+"
               @"--targetprofile:mscorlib"
               @"--nocopyfsharpcore"
               @"--deterministic+"
               @"--simpleresolution" |]
          ReferencedProjects = [| Project142 |]
          IsIncompleteTypeCheckEnvironment = false
          UseScriptResolutionRules = false
          LoadTime = DateTime.Now
          UnresolvedReferences = None
          OriginalLoadReferences = []
          Stamp = Some 319L }

    FSharpReferencedProject.CreateFSharp(
        __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project319\bin\Debug\net472\Project319.dll",
        projectOptions
    )

let memoProject319 = memoization Project319

let Project334 _ =
    let projectOptions =
        { ProjectFileName = __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project334\Project334.fsproj"
          ProjectId = None
          SourceFiles =
            [| __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project334\obj\Debug\net472\Project334.AssemblyInfo.fs"
               __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project334\MyModule.fs" |]
          OtherOptions =
            [| "-o:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project334\obj\Debug\net472\Project334.dll"
               @"-g"
               @"--debug:portable"
               @"--noframework"
               @"--define:TRACE"
               @"--define:DEBUG"
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
               @"--optimize-"
               @"--tailcalls-"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\mscorlib.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project122\bin\Debug\net472\Project122.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project138\bin\Debug\net472\Project138.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project140\bin\Debug\net472\Project140.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project142\bin\Debug\net472\Project142.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project144\bin\Debug\net472\Project144.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project149\bin\Debug\net472\Project149.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project161\bin\Debug\net472\Project161.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project164\bin\Debug\net472\Project164.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project168\bin\Debug\net472\Project168.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project172\bin\Debug\net472\Project172.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project184\bin\Debug\net472\Project184.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project189\bin\Debug\net472\Project189.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project209\bin\Debug\net472\Project209.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project214\bin\Debug\net472\Project214.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project240\bin\Debug\net472\Project240.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project264\bin\Debug\net472\Project264.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project265\bin\Debug\net472\Project265.dll"
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
               @"--warn:3"
               @"--warnaserror:3239"
               @"--fullpaths"
               @"--flaterrors"
               @"--subsystemversion:6.00"
               @"--highentropyva+"
               @"--targetprofile:mscorlib"
               @"--nocopyfsharpcore"
               @"--deterministic+"
               @"--simpleresolution" |]
          ReferencedProjects =
            [| memoProject122 "Project122"
               Project138
               Project140
               Project142
               Project144
               Project149
               Project161
               Project164
               Project168
               Project172
               Project184
               Project189
               Project209
               Project214
               memoProject240 "Project240"
               memoProject264 "Project264"
               memoProject265 "Project265" |]
          IsIncompleteTypeCheckEnvironment = false
          UseScriptResolutionRules = false
          LoadTime = DateTime.Now
          UnresolvedReferences = None
          OriginalLoadReferences = []
          Stamp = Some 334L }

    FSharpReferencedProject.CreateFSharp(
        __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project334\bin\Debug\net472\Project334.dll",
        projectOptions
    )

let memoProject334 = memoization Project334

let Project122 _ =
    let projectOptions =
        { ProjectFileName = __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project122\Project122.fsproj"
          ProjectId = None
          SourceFiles =
            [| __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project122\obj\Debug\net472\Project122.AssemblyInfo.fs"
               __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project122\MyModule.fs" |]
          OtherOptions =
            [| "-o:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project122\obj\Debug\net472\Project122.dll"
               @"-g"
               @"--debug:portable"
               @"--noframework"
               @"--define:TRACE"
               @"--define:DEBUG"
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
               @"--optimize-"
               @"--tailcalls-"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\mscorlib.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project126\bin\Debug\net472\Project126.dll"
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
               @"--warn:3"
               @"--warnaserror:3239"
               @"--fullpaths"
               @"--flaterrors"
               @"--subsystemversion:6.00"
               @"--highentropyva+"
               @"--targetprofile:mscorlib"
               @"--nocopyfsharpcore"
               @"--deterministic+"
               @"--simpleresolution" |]
          ReferencedProjects = [| Project126 |]
          IsIncompleteTypeCheckEnvironment = false
          UseScriptResolutionRules = false
          LoadTime = DateTime.Now
          UnresolvedReferences = None
          OriginalLoadReferences = []
          Stamp = Some 122L }

    FSharpReferencedProject.CreateFSharp(
        __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project122\bin\Debug\net472\Project122.dll",
        projectOptions
    )

let memoProject122 = memoization Project122

let Project240 _ =
    let projectOptions =
        { ProjectFileName = __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project240\Project240.fsproj"
          ProjectId = None
          SourceFiles =
            [| __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project240\obj\Debug\net472\Project240.AssemblyInfo.fs"
               __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project240\MyModule.fs" |]
          OtherOptions =
            [| "-o:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project240\obj\Debug\net472\Project240.dll"
               @"-g"
               @"--debug:portable"
               @"--noframework"
               @"--define:TRACE"
               @"--define:DEBUG"
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
               @"--optimize-"
               @"--tailcalls-"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\mscorlib.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project140\bin\Debug\net472\Project140.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project142\bin\Debug\net472\Project142.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project149\bin\Debug\net472\Project149.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project161\bin\Debug\net472\Project161.dll"
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
               @"--warn:3"
               @"--warnaserror:3239"
               @"--fullpaths"
               @"--flaterrors"
               @"--subsystemversion:6.00"
               @"--highentropyva+"
               @"--targetprofile:mscorlib"
               @"--nocopyfsharpcore"
               @"--deterministic+"
               @"--simpleresolution" |]
          ReferencedProjects =
            [| Project140
               Project142
               Project149
               Project161 |]
          IsIncompleteTypeCheckEnvironment = false
          UseScriptResolutionRules = false
          LoadTime = DateTime.Now
          UnresolvedReferences = None
          OriginalLoadReferences = []
          Stamp = Some 240L }

    FSharpReferencedProject.CreateFSharp(
        __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project240\bin\Debug\net472\Project240.dll",
        projectOptions
    )

let memoProject240 = memoization Project240

let Project17 _ =
    let projectOptions =
        { ProjectFileName = __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project17\Project17.fsproj"
          ProjectId = None
          SourceFiles =
            [| __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project17\obj\Debug\net472\Project17.AssemblyInfo.fs"
               __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project17\MyModule.fs" |]
          OtherOptions =
            [| "-o:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project17\obj\Debug\net472\Project17.dll"
               @"-g"
               @"--debug:portable"
               @"--noframework"
               @"--define:TRACE"
               @"--define:DEBUG"
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
               @"--optimize-"
               @"--tailcalls-"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\mscorlib.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project122\bin\Debug\net472\Project122.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project138\bin\Debug\net472\Project138.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project140\bin\Debug\net472\Project140.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project142\bin\Debug\net472\Project142.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project146\bin\Debug\net472\Project146.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project149\bin\Debug\net472\Project149.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project151\bin\Debug\net472\Project151.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project161\bin\Debug\net472\Project161.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project164\bin\Debug\net472\Project164.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project168\bin\Debug\net472\Project168.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project214\bin\Debug\net472\Project214.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project22\bin\Debug\net472\Project22.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project23\bin\Debug\net472\Project23.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project240\bin\Debug\net472\Project240.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project272\bin\Debug\net472\Project272.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project295\bin\Debug\net472\Project295.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project297\bin\Debug\net472\Project297.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project298\bin\Debug\net472\Project298.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project326\bin\Debug\net472\Project326.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project344\bin\Debug\net472\Project344.dll"
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
               @"--warn:3"
               @"--warnaserror:3239"
               @"--fullpaths"
               @"--flaterrors"
               @"--subsystemversion:6.00"
               @"--highentropyva+"
               @"--targetprofile:mscorlib"
               @"--nocopyfsharpcore"
               @"--deterministic+"
               @"--simpleresolution" |]
          ReferencedProjects =
            [| memoProject122 "Project122"
               Project138
               Project140
               Project142
               Project146
               Project149
               memoProject151 "Project151"
               Project161
               Project164
               Project168
               Project214
               memoProject22 "Project22"
               memoProject23 "Project23"
               memoProject240 "Project240"
               memoProject272 "Project272"
               Project295
               Project297
               Project298
               Project326
               Project344 |]
          IsIncompleteTypeCheckEnvironment = false
          UseScriptResolutionRules = false
          LoadTime = DateTime.Now
          UnresolvedReferences = None
          OriginalLoadReferences = []
          Stamp = Some 17L }

    FSharpReferencedProject.CreateFSharp(
        __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project17\bin\Debug\net472\Project17.dll",
        projectOptions
    )

let memoProject17 = memoization Project17

let Project23 _ =
    let projectOptions =
        { ProjectFileName = __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project23\Project23.fsproj"
          ProjectId = None
          SourceFiles =
            [| __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project23\obj\Debug\net472\Project23.AssemblyInfo.fs"
               __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project23\MyModule.fs" |]
          OtherOptions =
            [| "-o:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project23\obj\Debug\net472\Project23.dll"
               @"-g"
               @"--debug:portable"
               @"--noframework"
               @"--define:TRACE"
               @"--define:DEBUG"
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
               @"--optimize-"
               @"--tailcalls-"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\mscorlib.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project11\bin\Debug\net472\Project11.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project138\bin\Debug\net472\Project138.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project14\bin\Debug\net472\Project14.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project140\bin\Debug\net472\Project140.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project142\bin\Debug\net472\Project142.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project146\bin\Debug\net472\Project146.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project149\bin\Debug\net472\Project149.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project151\bin\Debug\net472\Project151.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project161\bin\Debug\net472\Project161.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project164\bin\Debug\net472\Project164.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project168\bin\Debug\net472\Project168.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project189\bin\Debug\net472\Project189.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project213\bin\Debug\net472\Project213.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project22\bin\Debug\net472\Project22.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project239\bin\Debug\net472\Project239.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project24\bin\Debug\net472\Project24.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project243\bin\Debug\net472\Project243.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project247\bin\Debug\net472\Project247.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project264\bin\Debug\net472\Project264.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project272\bin\Debug\net472\Project272.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project326\bin\Debug\net472\Project326.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project329\bin\Debug\net472\Project329.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project334\bin\Debug\net472\Project334.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project87\bin\Debug\net472\Project87.dll"
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
               @"--warn:3"
               @"--warnaserror:3239"
               @"--fullpaths"
               @"--flaterrors"
               @"--subsystemversion:6.00"
               @"--highentropyva+"
               @"--targetprofile:mscorlib"
               @"--nocopyfsharpcore"
               @"--deterministic+"
               @"--simpleresolution" |]
          ReferencedProjects =
            [| memoProject11 "Project11"
               Project138
               Project140
               Project142
               Project146
               Project149
               memoProject14 "Project14"
               memoProject151 "Project151"
               Project161
               Project164
               Project168
               Project189
               memoProject213 "Project213"
               memoProject22 "Project22"
               memoProject239 "Project239"
               memoProject243 "Project243"
               memoProject247 "Project247"
               memoProject24 "Project24"
               memoProject264 "Project264"
               memoProject272 "Project272"
               Project326
               memoProject329 "Project329"
               memoProject334 "Project334"
               Project87 |]
          IsIncompleteTypeCheckEnvironment = false
          UseScriptResolutionRules = false
          LoadTime = DateTime.Now
          UnresolvedReferences = None
          OriginalLoadReferences = []
          Stamp = Some 23L }

    FSharpReferencedProject.CreateFSharp(
        __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project23\bin\Debug\net472\Project23.dll",
        projectOptions
    )

let memoProject23 = memoization Project23

let Project11 _ =
    let projectOptions =
        { ProjectFileName = __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project11\Project11.fsproj"
          ProjectId = None
          SourceFiles =
            [| __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project11\obj\Debug\net472\Project11.AssemblyInfo.fs"
               __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project11\MyModule.fs" |]
          OtherOptions =
            [| "-o:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project11\obj\Debug\net472\Project11.dll"
               @"-g"
               @"--debug:portable"
               @"--noframework"
               @"--define:TRACE"
               @"--define:DEBUG"
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
               @"--optimize-"
               @"--tailcalls-"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\mscorlib.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project14\bin\Debug\net472\Project14.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project140\bin\Debug\net472\Project140.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project142\bin\Debug\net472\Project142.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project146\bin\Debug\net472\Project146.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project15\bin\Debug\net472\Project15.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project151\bin\Debug\net472\Project151.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project164\bin\Debug\net472\Project164.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project192\bin\Debug\net472\Project192.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project22\bin\Debug\net472\Project22.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project24\bin\Debug\net472\Project24.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project243\bin\Debug\net472\Project243.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project250\bin\Debug\net472\Project250.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project261\bin\Debug\net472\Project261.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project272\bin\Debug\net472\Project272.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project289\bin\Debug\net472\Project289.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project346\bin\Debug\net472\Project346.dll"
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
               @"--warn:3"
               @"--warnaserror:3239"
               @"--fullpaths"
               @"--flaterrors"
               @"--subsystemversion:6.00"
               @"--highentropyva+"
               @"--targetprofile:mscorlib"
               @"--nocopyfsharpcore"
               @"--deterministic+"
               @"--simpleresolution" |]
          ReferencedProjects =
            [| Project140
               Project142
               Project146
               memoProject14 "Project14"
               memoProject151 "Project151"
               memoProject15 "Project15"
               Project164
               Project192
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
          Stamp = Some 11L }

    FSharpReferencedProject.CreateFSharp(
        __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project11\bin\Debug\net472\Project11.dll",
        projectOptions
    )

let memoProject11 = memoization Project11

let Project15 _ =
    let projectOptions =
        { ProjectFileName = __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project15\Project15.fsproj"
          ProjectId = None
          SourceFiles =
            [| __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project15\obj\Debug\net472\Project15.AssemblyInfo.fs"
               __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project15\MyModule.fs" |]
          OtherOptions =
            [| "-o:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project15\obj\Debug\net472\Project15.dll"
               @"-g"
               @"--debug:portable"
               @"--noframework"
               @"--define:TRACE"
               @"--define:DEBUG"
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
               @"--optimize-"
               @"--tailcalls-"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\mscorlib.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project146\bin\Debug\net472\Project146.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project151\bin\Debug\net472\Project151.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project22\bin\Debug\net472\Project22.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project272\bin\Debug\net472\Project272.dll"
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
               @"--warn:3"
               @"--warnaserror:3239"
               @"--fullpaths"
               @"--flaterrors"
               @"--subsystemversion:6.00"
               @"--highentropyva+"
               @"--targetprofile:mscorlib"
               @"--nocopyfsharpcore"
               @"--deterministic+"
               @"--simpleresolution" |]
          ReferencedProjects =
            [| Project146
               memoProject151 "Project151"
               memoProject22 "Project22"
               memoProject272 "Project272" |]
          IsIncompleteTypeCheckEnvironment = false
          UseScriptResolutionRules = false
          LoadTime = DateTime.Now
          UnresolvedReferences = None
          OriginalLoadReferences = []
          Stamp = Some 15L }

    FSharpReferencedProject.CreateFSharp(
        __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project15\bin\Debug\net472\Project15.dll",
        projectOptions
    )

let memoProject15 = memoization Project15

let Project24 _ =
    let projectOptions =
        { ProjectFileName = __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project24\Project24.fsproj"
          ProjectId = None
          SourceFiles =
            [| __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project24\obj\Debug\net472\Project24.AssemblyInfo.fs"
               __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project24\MyModule.fs" |]
          OtherOptions =
            [| "-o:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project24\obj\Debug\net472\Project24.dll"
               @"-g"
               @"--debug:portable"
               @"--noframework"
               @"--define:TRACE"
               @"--define:DEBUG"
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
               @"--optimize-"
               @"--tailcalls-"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\mscorlib.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project10\bin\Debug\net472\Project10.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project140\bin\Debug\net472\Project140.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project144\bin\Debug\net472\Project144.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project146\bin\Debug\net472\Project146.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project15\bin\Debug\net472\Project15.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project151\bin\Debug\net472\Project151.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project164\bin\Debug\net472\Project164.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project22\bin\Debug\net472\Project22.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project243\bin\Debug\net472\Project243.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project272\bin\Debug\net472\Project272.dll"
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
               @"--warn:3"
               @"--warnaserror:3239"
               @"--fullpaths"
               @"--flaterrors"
               @"--subsystemversion:6.00"
               @"--highentropyva+"
               @"--targetprofile:mscorlib"
               @"--nocopyfsharpcore"
               @"--deterministic+"
               @"--simpleresolution" |]
          ReferencedProjects =
            [| memoProject10 "Project10"
               Project140
               Project144
               Project146
               memoProject151 "Project151"
               memoProject15 "Project15"
               Project164
               memoProject22 "Project22"
               memoProject243 "Project243"
               memoProject272 "Project272" |]
          IsIncompleteTypeCheckEnvironment = false
          UseScriptResolutionRules = false
          LoadTime = DateTime.Now
          UnresolvedReferences = None
          OriginalLoadReferences = []
          Stamp = Some 24L }

    FSharpReferencedProject.CreateFSharp(
        __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project24\bin\Debug\net472\Project24.dll",
        projectOptions
    )

let memoProject24 = memoization Project24

let Project10 _ =
    let projectOptions =
        { ProjectFileName = __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project10\Project10.fsproj"
          ProjectId = None
          SourceFiles =
            [| __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project10\obj\Debug\net472\Project10.AssemblyInfo.fs"
               __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project10\MyModule.fs" |]
          OtherOptions =
            [| "-o:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project10\obj\Debug\net472\Project10.dll"
               @"-g"
               @"--debug:portable"
               @"--noframework"
               @"--define:TRACE"
               @"--define:DEBUG"
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
               @"--optimize-"
               @"--tailcalls-"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\mscorlib.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project15\bin\Debug\net472\Project15.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project22\bin\Debug\net472\Project22.dll"
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
               @"--warn:3"
               @"--warnaserror:3239"
               @"--fullpaths"
               @"--flaterrors"
               @"--subsystemversion:6.00"
               @"--highentropyva+"
               @"--targetprofile:mscorlib"
               @"--nocopyfsharpcore"
               @"--deterministic+"
               @"--simpleresolution" |]
          ReferencedProjects =
            [| memoProject15 "Project15"
               memoProject22 "Project22" |]
          IsIncompleteTypeCheckEnvironment = false
          UseScriptResolutionRules = false
          LoadTime = DateTime.Now
          UnresolvedReferences = None
          OriginalLoadReferences = []
          Stamp = Some 10L }

    FSharpReferencedProject.CreateFSharp(
        __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project10\bin\Debug\net472\Project10.dll",
        projectOptions
    )

let memoProject10 = memoization Project10

let Project261 _ =
    let projectOptions =
        { ProjectFileName = __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project261\Project261.fsproj"
          ProjectId = None
          SourceFiles =
            [| __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project261\obj\Debug\net472\Project261.AssemblyInfo.fs"
               __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project261\MyModule.fs" |]
          OtherOptions =
            [| "-o:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project261\obj\Debug\net472\Project261.dll"
               @"-g"
               @"--debug:portable"
               @"--noframework"
               @"--define:TRACE"
               @"--define:DEBUG"
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
               @"--optimize-"
               @"--tailcalls-"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\mscorlib.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project140\bin\Debug\net472\Project140.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project144\bin\Debug\net472\Project144.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project146\bin\Debug\net472\Project146.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project151\bin\Debug\net472\Project151.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project164\bin\Debug\net472\Project164.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project243\bin\Debug\net472\Project243.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project272\bin\Debug\net472\Project272.dll"
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
               @"--warn:3"
               @"--warnaserror:3239"
               @"--fullpaths"
               @"--flaterrors"
               @"--subsystemversion:6.00"
               @"--highentropyva+"
               @"--targetprofile:mscorlib"
               @"--nocopyfsharpcore"
               @"--deterministic+"
               @"--simpleresolution" |]
          ReferencedProjects =
            [| Project140
               Project144
               Project146
               memoProject151 "Project151"
               Project164
               memoProject243 "Project243"
               memoProject272 "Project272" |]
          IsIncompleteTypeCheckEnvironment = false
          UseScriptResolutionRules = false
          LoadTime = DateTime.Now
          UnresolvedReferences = None
          OriginalLoadReferences = []
          Stamp = Some 261L }

    FSharpReferencedProject.CreateFSharp(
        __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project261\bin\Debug\net472\Project261.dll",
        projectOptions
    )

let memoProject261 = memoization Project261

let Project346 _ =
    let projectOptions =
        { ProjectFileName = __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project346\Project346.fsproj"
          ProjectId = None
          SourceFiles =
            [| __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project346\obj\Debug\net472\Project346.AssemblyInfo.fs"
               __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project346\MyModule.fs" |]
          OtherOptions =
            [| "-o:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project346\obj\Debug\net472\Project346.dll"
               @"-g"
               @"--debug:portable"
               @"--noframework"
               @"--define:TRACE"
               @"--define:DEBUG"
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
               @"--optimize-"
               @"--tailcalls-"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\mscorlib.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project138\bin\Debug\net472\Project138.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project140\bin\Debug\net472\Project140.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project142\bin\Debug\net472\Project142.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project146\bin\Debug\net472\Project146.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project149\bin\Debug\net472\Project149.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project151\bin\Debug\net472\Project151.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project161\bin\Debug\net472\Project161.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project164\bin\Debug\net472\Project164.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project168\bin\Debug\net472\Project168.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project172\bin\Debug\net472\Project172.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project189\bin\Debug\net472\Project189.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project190\bin\Debug\net472\Project190.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project192\bin\Debug\net472\Project192.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project213\bin\Debug\net472\Project213.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project214\bin\Debug\net472\Project214.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project222\bin\Debug\net472\Project222.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project243\bin\Debug\net472\Project243.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project250\bin\Debug\net472\Project250.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project264\bin\Debug\net472\Project264.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project268\bin\Debug\net472\Project268.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project272\bin\Debug\net472\Project272.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project295\bin\Debug\net472\Project295.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project297\bin\Debug\net472\Project297.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project301\bin\Debug\net472\Project301.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project326\bin\Debug\net472\Project326.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project329\bin\Debug\net472\Project329.dll"
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
               @"--warn:3"
               @"--warnaserror:3239"
               @"--fullpaths"
               @"--flaterrors"
               @"--subsystemversion:6.00"
               @"--highentropyva+"
               @"--targetprofile:mscorlib"
               @"--nocopyfsharpcore"
               @"--deterministic+"
               @"--simpleresolution" |]
          ReferencedProjects =
            [| Project138
               Project140
               Project142
               Project146
               Project149
               memoProject151 "Project151"
               Project161
               Project164
               Project168
               Project172
               Project189
               Project190
               Project192
               memoProject213 "Project213"
               Project214
               memoProject222 "Project222"
               memoProject243 "Project243"
               memoProject250 "Project250"
               memoProject264 "Project264"
               memoProject268 "Project268"
               memoProject272 "Project272"
               Project295
               Project297
               Project301
               Project326
               memoProject329 "Project329" |]
          IsIncompleteTypeCheckEnvironment = false
          UseScriptResolutionRules = false
          LoadTime = DateTime.Now
          UnresolvedReferences = None
          OriginalLoadReferences = []
          Stamp = Some 346L }

    FSharpReferencedProject.CreateFSharp(
        __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project346\bin\Debug\net472\Project346.dll",
        projectOptions
    )

let memoProject346 = memoization Project346

let Project268 _ =
    let projectOptions =
        { ProjectFileName = __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project268\Project268.fsproj"
          ProjectId = None
          SourceFiles =
            [| __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project268\obj\Debug\net472\Project268.AssemblyInfo.fs"
               __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project268\MyModule.fs" |]
          OtherOptions =
            [| "-o:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project268\obj\Debug\net472\Project268.dll"
               @"-g"
               @"--debug:portable"
               @"--noframework"
               @"--define:TRACE"
               @"--define:DEBUG"
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
               @"--optimize-"
               @"--tailcalls-"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\mscorlib.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project138\bin\Debug\net472\Project138.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project140\bin\Debug\net472\Project140.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project142\bin\Debug\net472\Project142.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project144\bin\Debug\net472\Project144.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project149\bin\Debug\net472\Project149.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project164\bin\Debug\net472\Project164.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project168\bin\Debug\net472\Project168.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project172\bin\Debug\net472\Project172.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project184\bin\Debug\net472\Project184.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project189\bin\Debug\net472\Project189.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project192\bin\Debug\net472\Project192.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project213\bin\Debug\net472\Project213.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project222\bin\Debug\net472\Project222.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project243\bin\Debug\net472\Project243.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project264\bin\Debug\net472\Project264.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project265\bin\Debug\net472\Project265.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project300\bin\Debug\net472\Project300.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project310\bin\Debug\net472\Project310.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project326\bin\Debug\net472\Project326.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project329\bin\Debug\net472\Project329.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project334\bin\Debug\net472\Project334.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project344\bin\Debug\net472\Project344.dll"
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
               @"--warn:3"
               @"--warnaserror:3239"
               @"--fullpaths"
               @"--flaterrors"
               @"--subsystemversion:6.00"
               @"--highentropyva+"
               @"--targetprofile:mscorlib"
               @"--nocopyfsharpcore"
               @"--deterministic+"
               @"--simpleresolution" |]
          ReferencedProjects =
            [| Project138
               Project140
               Project142
               Project144
               Project149
               Project164
               Project168
               Project172
               Project184
               Project189
               Project192
               memoProject213 "Project213"
               memoProject222 "Project222"
               memoProject243 "Project243"
               memoProject264 "Project264"
               memoProject265 "Project265"
               memoProject300 "Project300"
               memoProject310 "Project310"
               Project326
               memoProject329 "Project329"
               memoProject334 "Project334"
               Project344 |]
          IsIncompleteTypeCheckEnvironment = false
          UseScriptResolutionRules = false
          LoadTime = DateTime.Now
          UnresolvedReferences = None
          OriginalLoadReferences = []
          Stamp = Some 268L }

    FSharpReferencedProject.CreateFSharp(
        __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project268\bin\Debug\net472\Project268.dll",
        projectOptions
    )

let memoProject268 = memoization Project268

let Project329 _ =
    let projectOptions =
        { ProjectFileName = __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project329\Project329.fsproj"
          ProjectId = None
          SourceFiles =
            [| __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project329\obj\Debug\net472\Project329.AssemblyInfo.fs"
               __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project329\MyModule.fs" |]
          OtherOptions =
            [| "-o:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project329\obj\Debug\net472\Project329.dll"
               @"-g"
               @"--debug:portable"
               @"--noframework"
               @"--define:TRACE"
               @"--define:DEBUG"
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
               @"--optimize-"
               @"--tailcalls-"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\mscorlib.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project124\bin\Debug\net472\Project124.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project127\bin\Debug\net472\Project127.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project138\bin\Debug\net472\Project138.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project140\bin\Debug\net472\Project140.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project142\bin\Debug\net472\Project142.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project144\bin\Debug\net472\Project144.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project146\bin\Debug\net472\Project146.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project149\bin\Debug\net472\Project149.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project151\bin\Debug\net472\Project151.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project161\bin\Debug\net472\Project161.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project164\bin\Debug\net472\Project164.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project168\bin\Debug\net472\Project168.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project172\bin\Debug\net472\Project172.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project184\bin\Debug\net472\Project184.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project189\bin\Debug\net472\Project189.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project192\bin\Debug\net472\Project192.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project193\bin\Debug\net472\Project193.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project213\bin\Debug\net472\Project213.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project214\bin\Debug\net472\Project214.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project222\bin\Debug\net472\Project222.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project226\bin\Debug\net472\Project226.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project239\bin\Debug\net472\Project239.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project243\bin\Debug\net472\Project243.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project250\bin\Debug\net472\Project250.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project264\bin\Debug\net472\Project264.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project272\bin\Debug\net472\Project272.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project300\bin\Debug\net472\Project300.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project310\bin\Debug\net472\Project310.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project323\bin\Debug\net472\Project323.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project324\bin\Debug\net472\Project324.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project326\bin\Debug\net472\Project326.dll"
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
               @"--warn:3"
               @"--warnaserror:3239"
               @"--fullpaths"
               @"--flaterrors"
               @"--subsystemversion:6.00"
               @"--highentropyva+"
               @"--targetprofile:mscorlib"
               @"--nocopyfsharpcore"
               @"--deterministic+"
               @"--simpleresolution" |]
          ReferencedProjects =
            [| memoProject124 "Project124"
               Project127
               Project138
               Project140
               Project142
               Project144
               Project146
               Project149
               memoProject151 "Project151"
               Project161
               Project164
               Project168
               Project172
               Project184
               Project189
               Project192
               Project193
               memoProject213 "Project213"
               Project214
               memoProject222 "Project222"
               Project226
               memoProject239 "Project239"
               memoProject243 "Project243"
               memoProject250 "Project250"
               memoProject264 "Project264"
               memoProject272 "Project272"
               memoProject300 "Project300"
               memoProject310 "Project310"
               Project323
               memoProject324 "Project324"
               Project326 |]
          IsIncompleteTypeCheckEnvironment = false
          UseScriptResolutionRules = false
          LoadTime = DateTime.Now
          UnresolvedReferences = None
          OriginalLoadReferences = []
          Stamp = Some 329L }

    FSharpReferencedProject.CreateFSharp(
        __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project329\bin\Debug\net472\Project329.dll",
        projectOptions
    )

let memoProject329 = memoization Project329

let Project124 _ =
    let projectOptions =
        { ProjectFileName = __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project124\Project124.fsproj"
          ProjectId = None
          SourceFiles =
            [| __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project124\obj\Debug\net472\Project124.AssemblyInfo.fs"
               __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project124\MyModule.fs" |]
          OtherOptions =
            [| "-o:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project124\obj\Debug\net472\Project124.dll"
               @"-g"
               @"--debug:portable"
               @"--noframework"
               @"--define:TRACE"
               @"--define:DEBUG"
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
               @"--optimize-"
               @"--tailcalls-"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\mscorlib.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project122\bin\Debug\net472\Project122.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project125\bin\Debug\net472\Project125.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project213\bin\Debug\net472\Project213.dll"
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
               @"--warn:3"
               @"--warnaserror:3239"
               @"--fullpaths"
               @"--flaterrors"
               @"--subsystemversion:6.00"
               @"--highentropyva+"
               @"--targetprofile:mscorlib"
               @"--nocopyfsharpcore"
               @"--deterministic+"
               @"--simpleresolution" |]
          ReferencedProjects =
            [| memoProject122 "Project122"
               Project125
               memoProject213 "Project213" |]
          IsIncompleteTypeCheckEnvironment = false
          UseScriptResolutionRules = false
          LoadTime = DateTime.Now
          UnresolvedReferences = None
          OriginalLoadReferences = []
          Stamp = Some 124L }

    FSharpReferencedProject.CreateFSharp(
        __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project124\bin\Debug\net472\Project124.dll",
        projectOptions
    )

let memoProject124 = memoization Project124

let Project125 =
    FSharpReferencedProject.CreatePortableExecutable(
        __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project125\bin\Debug\net472\Project125.dll",
        getTimeStamp,
        loader
    )

let Project165 =
    FSharpReferencedProject.CreatePortableExecutable(
        __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project165\bin\Debug\net472\Project165.dll",
        getTimeStamp,
        loader
    )

let Project276 =
    FSharpReferencedProject.CreatePortableExecutable(
        __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project276\bin\Debug\net472\Project276.dll",
        getTimeStamp,
        loader
    )

let Project239 _ =
    let projectOptions =
        { ProjectFileName = __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project239\Project239.fsproj"
          ProjectId = None
          SourceFiles =
            [| __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project239\obj\Debug\net472\Project239.AssemblyInfo.fs"
               __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project239\MyModule.fs" |]
          OtherOptions =
            [| "-o:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project239\obj\Debug\net472\Project239.dll"
               @"-g"
               @"--debug:portable"
               @"--noframework"
               @"--define:TRACE"
               @"--define:DEBUG"
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
               @"--optimize-"
               @"--tailcalls-"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\mscorlib.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project140\bin\Debug\net472\Project140.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project142\bin\Debug\net472\Project142.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project149\bin\Debug\net472\Project149.dll"
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
               @"--warn:3"
               @"--warnaserror:3239"
               @"--fullpaths"
               @"--flaterrors"
               @"--subsystemversion:6.00"
               @"--highentropyva+"
               @"--targetprofile:mscorlib"
               @"--nocopyfsharpcore"
               @"--deterministic+"
               @"--simpleresolution" |]
          ReferencedProjects = [| Project140; Project142; Project149 |]
          IsIncompleteTypeCheckEnvironment = false
          UseScriptResolutionRules = false
          LoadTime = DateTime.Now
          UnresolvedReferences = None
          OriginalLoadReferences = []
          Stamp = Some 239L }

    FSharpReferencedProject.CreateFSharp(
        __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project239\bin\Debug\net472\Project239.dll",
        projectOptions
    )

let memoProject239 = memoization Project239

let Project324 _ =
    let projectOptions =
        { ProjectFileName = __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project324\Project324.fsproj"
          ProjectId = None
          SourceFiles =
            [| __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project324\obj\Debug\net472\Project324.AssemblyInfo.fs"
               __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project324\MyModule.fs" |]
          OtherOptions =
            [| "-o:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project324\obj\Debug\net472\Project324.dll"
               @"-g"
               @"--debug:portable"
               @"--noframework"
               @"--define:TRACE"
               @"--define:DEBUG"
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
               @"--optimize-"
               @"--tailcalls-"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\mscorlib.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project138\bin\Debug\net472\Project138.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project140\bin\Debug\net472\Project140.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project142\bin\Debug\net472\Project142.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project144\bin\Debug\net472\Project144.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project146\bin\Debug\net472\Project146.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project151\bin\Debug\net472\Project151.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project164\bin\Debug\net472\Project164.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project189\bin\Debug\net472\Project189.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project264\bin\Debug\net472\Project264.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project272\bin\Debug\net472\Project272.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project323\bin\Debug\net472\Project323.dll"
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
               @"--warn:3"
               @"--warnaserror:3239"
               @"--fullpaths"
               @"--flaterrors"
               @"--subsystemversion:6.00"
               @"--highentropyva+"
               @"--targetprofile:mscorlib"
               @"--nocopyfsharpcore"
               @"--deterministic+"
               @"--simpleresolution" |]
          ReferencedProjects =
            [| Project138
               Project140
               Project142
               Project144
               Project146
               memoProject151 "Project151"
               Project164
               Project189
               memoProject264 "Project264"
               memoProject272 "Project272"
               Project323 |]
          IsIncompleteTypeCheckEnvironment = false
          UseScriptResolutionRules = false
          LoadTime = DateTime.Now
          UnresolvedReferences = None
          OriginalLoadReferences = []
          Stamp = Some 324L }

    FSharpReferencedProject.CreateFSharp(
        __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project324\bin\Debug\net472\Project324.dll",
        projectOptions
    )

let memoProject324 = memoization Project324

let Project344 =
    FSharpReferencedProject.CreatePortableExecutable(
        __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project344\bin\Debug\net472\Project344.dll",
        getTimeStamp,
        loader
    )

let Project153 =
    FSharpReferencedProject.CreatePortableExecutable(
        __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project153\bin\Debug\net472\Project153.dll",
        getTimeStamp,
        loader
    )

let Project200 =
    FSharpReferencedProject.CreatePortableExecutable(
        __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project200\bin\Debug\net472\Project200.dll",
        getTimeStamp,
        loader
    )

let Project258 =
    FSharpReferencedProject.CreatePortableExecutable(
        __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project258\bin\Debug\net472\Project258.dll",
        getTimeStamp,
        loader
    )

let Project260 =
    FSharpReferencedProject.CreatePortableExecutable(
        __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project260\bin\Debug\net472\Project260.dll",
        getTimeStamp,
        loader
    )

let Project341 =
    FSharpReferencedProject.CreatePortableExecutable(
        __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project341\bin\Debug\net472\Project341.dll",
        getTimeStamp,
        loader
    )

let Project340 =
    FSharpReferencedProject.CreatePortableExecutable(
        __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project340\bin\Debug\net472\Project340.dll",
        getTimeStamp,
        loader
    )

let Project291 =
    FSharpReferencedProject.CreatePortableExecutable(
        __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project291\bin\Debug\net472\Project291.dll",
        getTimeStamp,
        loader
    )

let Project305 =
    FSharpReferencedProject.CreatePortableExecutable(
        __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project305\bin\Debug\net472\Project305.dll",
        getTimeStamp,
        loader
    )

let Project307 =
    FSharpReferencedProject.CreatePortableExecutable(
        __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project307\bin\Debug\net472\Project307.dll",
        getTimeStamp,
        loader
    )

let Project327 =
    FSharpReferencedProject.CreatePortableExecutable(
        __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project327\bin\Debug\net472\Project327.dll",
        getTimeStamp,
        loader
    )

let Project302 _ =
    let projectOptions =
        { ProjectFileName = __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project302\Project302.fsproj"
          ProjectId = None
          SourceFiles =
            [| __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project302\obj\Debug\net472\Project302.AssemblyInfo.fs"
               __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project302\MyModule.fs" |]
          OtherOptions =
            [| "-o:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project302\obj\Debug\net472\Project302.dll"
               @"-g"
               @"--debug:portable"
               @"--noframework"
               @"--define:TRACE"
               @"--define:DEBUG"
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
               @"--optimize-"
               @"--tailcalls-"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\mscorlib.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project122\bin\Debug\net472\Project122.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project138\bin\Debug\net472\Project138.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project140\bin\Debug\net472\Project140.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project146\bin\Debug\net472\Project146.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project151\bin\Debug\net472\Project151.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project164\bin\Debug\net472\Project164.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project172\bin\Debug\net472\Project172.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project189\bin\Debug\net472\Project189.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project192\bin\Debug\net472\Project192.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project214\bin\Debug\net472\Project214.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project243\bin\Debug\net472\Project243.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project264\bin\Debug\net472\Project264.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project272\bin\Debug\net472\Project272.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project300\bin\Debug\net472\Project300.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project305\bin\Debug\net472\Project305.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project323\bin\Debug\net472\Project323.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project324\bin\Debug\net472\Project324.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project326\bin\Debug\net472\Project326.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project329\bin\Debug\net472\Project329.dll"
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
               @"--warn:3"
               @"--warnaserror:3239"
               @"--fullpaths"
               @"--flaterrors"
               @"--subsystemversion:6.00"
               @"--highentropyva+"
               @"--targetprofile:mscorlib"
               @"--nocopyfsharpcore"
               @"--deterministic+"
               @"--simpleresolution" |]
          ReferencedProjects =
            [| memoProject122 "Project122"
               Project138
               Project140
               Project146
               memoProject151 "Project151"
               Project164
               Project172
               Project189
               Project192
               Project214
               memoProject243 "Project243"
               memoProject264 "Project264"
               memoProject272 "Project272"
               memoProject300 "Project300"
               Project305
               Project323
               memoProject324 "Project324"
               Project326
               memoProject329 "Project329" |]
          IsIncompleteTypeCheckEnvironment = false
          UseScriptResolutionRules = false
          LoadTime = DateTime.Now
          UnresolvedReferences = None
          OriginalLoadReferences = []
          Stamp = Some 302L }

    FSharpReferencedProject.CreateFSharp(
        __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project302\bin\Debug\net472\Project302.dll",
        projectOptions
    )

let memoProject302 = memoization Project302

let Project342 =
    FSharpReferencedProject.CreatePortableExecutable(
        __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project342\bin\Debug\net472\Project342.dll",
        getTimeStamp,
        loader
    )

let Project247 _ =
    let projectOptions =
        { ProjectFileName = __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project247\Project247.fsproj"
          ProjectId = None
          SourceFiles =
            [| __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project247\obj\Debug\net472\Project247.AssemblyInfo.fs"
               __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project247\MyModule.fs" |]
          OtherOptions =
            [| "-o:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project247\obj\Debug\net472\Project247.dll"
               @"-g"
               @"--debug:portable"
               @"--noframework"
               @"--define:TRACE"
               @"--define:DEBUG"
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
               @"--warn:3"
               @"--warnaserror:3239"
               @"--fullpaths"
               @"--flaterrors"
               @"--subsystemversion:6.00"
               @"--highentropyva+"
               @"--targetprofile:mscorlib"
               @"--nocopyfsharpcore"
               @"--deterministic+"
               @"--simpleresolution" |]
          ReferencedProjects = [||]
          IsIncompleteTypeCheckEnvironment = false
          UseScriptResolutionRules = false
          LoadTime = DateTime.Now
          UnresolvedReferences = None
          OriginalLoadReferences = []
          Stamp = Some 247L }

    FSharpReferencedProject.CreateFSharp(
        __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project247\bin\Debug\net472\Project247.dll",
        projectOptions
    )

let memoProject247 = memoization Project247

let Project87 =
    FSharpReferencedProject.CreatePortableExecutable(
        __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project87\bin\Debug\net472\Project87.dll",
        getTimeStamp,
        loader
    )

let Project294 _ =
    let projectOptions =
        { ProjectFileName = __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project294\Project294.fsproj"
          ProjectId = None
          SourceFiles =
            [| __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project294\obj\Debug\net472\Project294.AssemblyInfo.fs"
               __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project294\MyModule.fs" |]
          OtherOptions =
            [| "-o:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project294\obj\Debug\net472\Project294.dll"
               @"-g"
               @"--debug:portable"
               @"--noframework"
               @"--define:TRACE"
               @"--define:DEBUG"
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
               @"--optimize-"
               @"--tailcalls-"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\mscorlib.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project138\bin\Debug\net472\Project138.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project140\bin\Debug\net472\Project140.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project146\bin\Debug\net472\Project146.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project151\bin\Debug\net472\Project151.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project272\bin\Debug\net472\Project272.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project298\bin\Debug\net472\Project298.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project323\bin\Debug\net472\Project323.dll"
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
               @"--warn:3"
               @"--warnaserror:3239"
               @"--fullpaths"
               @"--flaterrors"
               @"--subsystemversion:6.00"
               @"--highentropyva+"
               @"--targetprofile:mscorlib"
               @"--nocopyfsharpcore"
               @"--deterministic+"
               @"--simpleresolution" |]
          ReferencedProjects =
            [| Project138
               Project140
               Project146
               memoProject151 "Project151"
               memoProject272 "Project272"
               Project298
               Project323 |]
          IsIncompleteTypeCheckEnvironment = false
          UseScriptResolutionRules = false
          LoadTime = DateTime.Now
          UnresolvedReferences = None
          OriginalLoadReferences = []
          Stamp = Some 294L }

    FSharpReferencedProject.CreateFSharp(
        __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project294\bin\Debug\net472\Project294.dll",
        projectOptions
    )

let memoProject294 = memoization Project294

let Project21 _ =
    let projectOptions =
        { ProjectFileName = __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project21\Project21.fsproj"
          ProjectId = None
          SourceFiles =
            [| __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project21\obj\Debug\net472\Project21.AssemblyInfo.fs"
               __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project21\MyModule.fs" |]
          OtherOptions =
            [| "-o:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project21\obj\Debug\net472\Project21.dll"
               @"-g"
               @"--debug:portable"
               @"--noframework"
               @"--define:TRACE"
               @"--define:DEBUG"
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
               @"--optimize-"
               @"--tailcalls-"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\mscorlib.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project14\bin\Debug\net472\Project14.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project140\bin\Debug\net472\Project140.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project142\bin\Debug\net472\Project142.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project149\bin\Debug\net472\Project149.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project151\bin\Debug\net472\Project151.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project161\bin\Debug\net472\Project161.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project168\bin\Debug\net472\Project168.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project22\bin\Debug\net472\Project22.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project23\bin\Debug\net472\Project23.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project24\bin\Debug\net472\Project24.dll"
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
               @"--warn:3"
               @"--warnaserror:3239"
               @"--fullpaths"
               @"--flaterrors"
               @"--subsystemversion:6.00"
               @"--highentropyva+"
               @"--targetprofile:mscorlib"
               @"--nocopyfsharpcore"
               @"--deterministic+"
               @"--simpleresolution" |]
          ReferencedProjects =
            [| Project140
               Project142
               Project149
               memoProject14 "Project14"
               memoProject151 "Project151"
               Project161
               Project168
               memoProject22 "Project22"
               memoProject23 "Project23"
               memoProject24 "Project24" |]
          IsIncompleteTypeCheckEnvironment = false
          UseScriptResolutionRules = false
          LoadTime = DateTime.Now
          UnresolvedReferences = None
          OriginalLoadReferences = []
          Stamp = Some 21L }

    FSharpReferencedProject.CreateFSharp(
        __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project21\bin\Debug\net472\Project21.dll",
        projectOptions
    )

let memoProject21 = memoization Project21

let Project118 _ =
    let projectOptions =
        { ProjectFileName = __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project118\Project118.fsproj"
          ProjectId = None
          SourceFiles =
            [| __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project118\obj\Debug\net472\Project118.AssemblyInfo.fs"
               __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project118\MyModule.fs" |]
          OtherOptions =
            [| "-o:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project118\obj\Debug\net472\Project118.dll"
               @"-g"
               @"--debug:portable"
               @"--noframework"
               @"--define:TRACE"
               @"--define:DEBUG"
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
               @"--optimize-"
               @"--tailcalls-"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\mscorlib.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project114\bin\Debug\net472\Project114.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project116\bin\Debug\net472\Project116.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project122\bin\Debug\net472\Project122.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project138\bin\Debug\net472\Project138.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project140\bin\Debug\net472\Project140.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project142\bin\Debug\net472\Project142.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project146\bin\Debug\net472\Project146.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project149\bin\Debug\net472\Project149.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project151\bin\Debug\net472\Project151.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project16\bin\Debug\net472\Project16.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project161\bin\Debug\net472\Project161.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project164\bin\Debug\net472\Project164.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project168\bin\Debug\net472\Project168.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project17\bin\Debug\net472\Project17.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project184\bin\Debug\net472\Project184.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project189\bin\Debug\net472\Project189.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project190\bin\Debug\net472\Project190.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project213\bin\Debug\net472\Project213.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project22\bin\Debug\net472\Project22.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project23\bin\Debug\net472\Project23.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project243\bin\Debug\net472\Project243.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project264\bin\Debug\net472\Project264.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project272\bin\Debug\net472\Project272.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project323\bin\Debug\net472\Project323.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project326\bin\Debug\net472\Project326.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project327\bin\Debug\net472\Project327.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project329\bin\Debug\net472\Project329.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project40\bin\Debug\net472\Project40.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project44\bin\Debug\net472\Project44.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project47\bin\Debug\net472\Project47.dll"
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
               @"--warn:3"
               @"--warnaserror:3239"
               @"--fullpaths"
               @"--flaterrors"
               @"--subsystemversion:6.00"
               @"--highentropyva+"
               @"--targetprofile:mscorlib"
               @"--nocopyfsharpcore"
               @"--deterministic+"
               @"--simpleresolution" |]
          ReferencedProjects =
            [| memoProject114 "Project114"
               memoProject116 "Project116"
               memoProject122 "Project122"
               Project138
               Project140
               Project142
               Project146
               Project149
               memoProject151 "Project151"
               Project161
               Project164
               Project168
               memoProject16 "Project16"
               memoProject17 "Project17"
               Project184
               Project189
               Project190
               memoProject213 "Project213"
               memoProject22 "Project22"
               memoProject23 "Project23"
               memoProject243 "Project243"
               memoProject264 "Project264"
               memoProject272 "Project272"
               Project323
               Project326
               Project327
               memoProject329 "Project329"
               memoProject40 "Project40"
               memoProject44 "Project44"
               memoProject47 "Project47" |]
          IsIncompleteTypeCheckEnvironment = false
          UseScriptResolutionRules = false
          LoadTime = DateTime.Now
          UnresolvedReferences = None
          OriginalLoadReferences = []
          Stamp = Some 118L }

    FSharpReferencedProject.CreateFSharp(
        __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project118\bin\Debug\net472\Project118.dll",
        projectOptions
    )

let memoProject118 = memoization Project118

let Project114 _ =
    let projectOptions =
        { ProjectFileName = __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project114\Project114.fsproj"
          ProjectId = None
          SourceFiles =
            [| __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project114\obj\Debug\net472\Project114.AssemblyInfo.fs"
               __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project114\MyModule.fs" |]
          OtherOptions =
            [| "-o:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project114\obj\Debug\net472\Project114.dll"
               @"-g"
               @"--debug:portable"
               @"--noframework"
               @"--define:TRACE"
               @"--define:DEBUG"
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
               @"--optimize-"
               @"--tailcalls-"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\mscorlib.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project115\bin\Debug\net472\Project115.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project116\bin\Debug\net472\Project116.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project117\bin\Debug\net472\Project117.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project14\bin\Debug\net472\Project14.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project140\bin\Debug\net472\Project140.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project142\bin\Debug\net472\Project142.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project146\bin\Debug\net472\Project146.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project149\bin\Debug\net472\Project149.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project151\bin\Debug\net472\Project151.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project161\bin\Debug\net472\Project161.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project164\bin\Debug\net472\Project164.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project168\bin\Debug\net472\Project168.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project17\bin\Debug\net472\Project17.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project19\bin\Debug\net472\Project19.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project21\bin\Debug\net472\Project21.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project214\bin\Debug\net472\Project214.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project22\bin\Debug\net472\Project22.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project23\bin\Debug\net472\Project23.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project24\bin\Debug\net472\Project24.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project243\bin\Debug\net472\Project243.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project264\bin\Debug\net472\Project264.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project272\bin\Debug\net472\Project272.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project295\bin\Debug\net472\Project295.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project298\bin\Debug\net472\Project298.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project323\bin\Debug\net472\Project323.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project326\bin\Debug\net472\Project326.dll"
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
               @"--warn:3"
               @"--warnaserror:3239"
               @"--fullpaths"
               @"--flaterrors"
               @"--subsystemversion:6.00"
               @"--highentropyva+"
               @"--targetprofile:mscorlib"
               @"--nocopyfsharpcore"
               @"--deterministic+"
               @"--simpleresolution" |]
          ReferencedProjects =
            [| memoProject115 "Project115"
               memoProject116 "Project116"
               memoProject117 "Project117"
               Project140
               Project142
               Project146
               Project149
               memoProject14 "Project14"
               memoProject151 "Project151"
               Project161
               Project164
               Project168
               memoProject17 "Project17"
               memoProject19 "Project19"
               Project214
               memoProject21 "Project21"
               memoProject22 "Project22"
               memoProject23 "Project23"
               memoProject243 "Project243"
               memoProject24 "Project24"
               memoProject264 "Project264"
               memoProject272 "Project272"
               Project295
               Project298
               Project323
               Project326 |]
          IsIncompleteTypeCheckEnvironment = false
          UseScriptResolutionRules = false
          LoadTime = DateTime.Now
          UnresolvedReferences = None
          OriginalLoadReferences = []
          Stamp = Some 114L }

    FSharpReferencedProject.CreateFSharp(
        __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project114\bin\Debug\net472\Project114.dll",
        projectOptions
    )

let memoProject114 = memoization Project114

let Project115 _ =
    let projectOptions =
        { ProjectFileName = __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project115\Project115.fsproj"
          ProjectId = None
          SourceFiles =
            [| __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project115\obj\Debug\net472\Project115.AssemblyInfo.fs"
               __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project115\MyModule.fs" |]
          OtherOptions =
            [| "-o:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project115\obj\Debug\net472\Project115.dll"
               @"-g"
               @"--debug:portable"
               @"--noframework"
               @"--define:TRACE"
               @"--define:DEBUG"
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
               @"--optimize-"
               @"--tailcalls-"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\mscorlib.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project117\bin\Debug\net472\Project117.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project14\bin\Debug\net472\Project14.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project149\bin\Debug\net472\Project149.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project24\bin\Debug\net472\Project24.dll"
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
               @"--warn:3"
               @"--warnaserror:3239"
               @"--fullpaths"
               @"--flaterrors"
               @"--subsystemversion:6.00"
               @"--highentropyva+"
               @"--targetprofile:mscorlib"
               @"--nocopyfsharpcore"
               @"--deterministic+"
               @"--simpleresolution" |]
          ReferencedProjects =
            [| memoProject117 "Project117"
               Project149
               memoProject14 "Project14"
               memoProject24 "Project24" |]
          IsIncompleteTypeCheckEnvironment = false
          UseScriptResolutionRules = false
          LoadTime = DateTime.Now
          UnresolvedReferences = None
          OriginalLoadReferences = []
          Stamp = Some 115L }

    FSharpReferencedProject.CreateFSharp(
        __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project115\bin\Debug\net472\Project115.dll",
        projectOptions
    )

let memoProject115 = memoization Project115

let Project19 _ =
    let projectOptions =
        { ProjectFileName = __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project19\Project19.fsproj"
          ProjectId = None
          SourceFiles =
            [| __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project19\obj\Debug\net472\Project19.AssemblyInfo.fs"
               __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project19\MyModule.fs" |]
          OtherOptions =
            [| "-o:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project19\obj\Debug\net472\Project19.dll"
               @"-g"
               @"--debug:portable"
               @"--noframework"
               @"--define:TRACE"
               @"--define:DEBUG"
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
               @"--optimize-"
               @"--tailcalls-"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\mscorlib.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project11\bin\Debug\net472\Project11.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project14\bin\Debug\net472\Project14.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project140\bin\Debug\net472\Project140.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project142\bin\Debug\net472\Project142.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project146\bin\Debug\net472\Project146.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project149\bin\Debug\net472\Project149.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project151\bin\Debug\net472\Project151.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project161\bin\Debug\net472\Project161.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project168\bin\Debug\net472\Project168.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project189\bin\Debug\net472\Project189.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project21\bin\Debug\net472\Project21.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project213\bin\Debug\net472\Project213.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project22\bin\Debug\net472\Project22.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project23\bin\Debug\net472\Project23.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project24\bin\Debug\net472\Project24.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project243\bin\Debug\net472\Project243.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project264\bin\Debug\net472\Project264.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project272\bin\Debug\net472\Project272.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project329\bin\Debug\net472\Project329.dll"
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
               @"--warn:3"
               @"--warnaserror:3239"
               @"--fullpaths"
               @"--flaterrors"
               @"--subsystemversion:6.00"
               @"--highentropyva+"
               @"--targetprofile:mscorlib"
               @"--nocopyfsharpcore"
               @"--deterministic+"
               @"--simpleresolution" |]
          ReferencedProjects =
            [| memoProject11 "Project11"
               Project140
               Project142
               Project146
               Project149
               memoProject14 "Project14"
               memoProject151 "Project151"
               Project161
               Project168
               Project189
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
          Stamp = Some 19L }

    FSharpReferencedProject.CreateFSharp(
        __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project19\bin\Debug\net472\Project19.dll",
        projectOptions
    )

let memoProject19 = memoization Project19

let Project16 _ =
    let projectOptions =
        { ProjectFileName = __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project16\Project16.fsproj"
          ProjectId = None
          SourceFiles =
            [| __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project16\obj\Debug\net472\Project16.AssemblyInfo.fs"
               __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project16\MyModule.fs" |]
          OtherOptions =
            [| "-o:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project16\obj\Debug\net472\Project16.dll"
               @"-g"
               @"--debug:portable"
               @"--noframework"
               @"--define:TRACE"
               @"--define:DEBUG"
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
               @"--optimize-"
               @"--tailcalls-"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\mscorlib.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project151\bin\Debug\net472\Project151.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project264\bin\Debug\net472\Project264.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project326\bin\Debug\net472\Project326.dll"
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
               @"--warn:3"
               @"--warnaserror:3239"
               @"--fullpaths"
               @"--flaterrors"
               @"--subsystemversion:6.00"
               @"--highentropyva+"
               @"--targetprofile:mscorlib"
               @"--nocopyfsharpcore"
               @"--deterministic+"
               @"--simpleresolution" |]
          ReferencedProjects =
            [| memoProject151 "Project151"
               memoProject264 "Project264"
               Project326 |]
          IsIncompleteTypeCheckEnvironment = false
          UseScriptResolutionRules = false
          LoadTime = DateTime.Now
          UnresolvedReferences = None
          OriginalLoadReferences = []
          Stamp = Some 16L }

    FSharpReferencedProject.CreateFSharp(
        __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project16\bin\Debug\net472\Project16.dll",
        projectOptions
    )

let memoProject16 = memoization Project16

let Project40 _ =
    let projectOptions =
        { ProjectFileName = __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project40\Project40.fsproj"
          ProjectId = None
          SourceFiles =
            [| __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project40\obj\Debug\net472\Project40.AssemblyInfo.fs"
               __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project40\MyModule.fs" |]
          OtherOptions =
            [| "-o:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project40\obj\Debug\net472\Project40.dll"
               @"-g"
               @"--debug:portable"
               @"--noframework"
               @"--define:TRACE"
               @"--define:DEBUG"
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
               @"--optimize-"
               @"--tailcalls-"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\mscorlib.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project11\bin\Debug\net472\Project11.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project116\bin\Debug\net472\Project116.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project12\bin\Debug\net472\Project12.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project122\bin\Debug\net472\Project122.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project138\bin\Debug\net472\Project138.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project14\bin\Debug\net472\Project14.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project140\bin\Debug\net472\Project140.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project142\bin\Debug\net472\Project142.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project144\bin\Debug\net472\Project144.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project146\bin\Debug\net472\Project146.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project149\bin\Debug\net472\Project149.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project15\bin\Debug\net472\Project15.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project151\bin\Debug\net472\Project151.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project16\bin\Debug\net472\Project16.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project161\bin\Debug\net472\Project161.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project164\bin\Debug\net472\Project164.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project168\bin\Debug\net472\Project168.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project17\bin\Debug\net472\Project17.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project172\bin\Debug\net472\Project172.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project184\bin\Debug\net472\Project184.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project189\bin\Debug\net472\Project189.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project19\bin\Debug\net472\Project19.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project190\bin\Debug\net472\Project190.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project206\bin\Debug\net472\Project206.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project213\bin\Debug\net472\Project213.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project214\bin\Debug\net472\Project214.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project22\bin\Debug\net472\Project22.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project222\bin\Debug\net472\Project222.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project23\bin\Debug\net472\Project23.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project24\bin\Debug\net472\Project24.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project243\bin\Debug\net472\Project243.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project264\bin\Debug\net472\Project264.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project272\bin\Debug\net472\Project272.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project295\bin\Debug\net472\Project295.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project297\bin\Debug\net472\Project297.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project323\bin\Debug\net472\Project323.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project326\bin\Debug\net472\Project326.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project327\bin\Debug\net472\Project327.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project329\bin\Debug\net472\Project329.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project338\bin\Debug\net472\Project338.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project339\bin\Debug\net472\Project339.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project344\bin\Debug\net472\Project344.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project346\bin\Debug\net472\Project346.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project41\bin\Debug\net472\Project41.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project43\bin\Debug\net472\Project43.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project44\bin\Debug\net472\Project44.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project45\bin\Debug\net472\Project45.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project47\bin\Debug\net472\Project47.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project98\bin\Debug\net472\Project98.dll"
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
               @"--warn:3"
               @"--warnaserror:3239"
               @"--fullpaths"
               @"--flaterrors"
               @"--subsystemversion:6.00"
               @"--highentropyva+"
               @"--targetprofile:mscorlib"
               @"--nocopyfsharpcore"
               @"--deterministic+"
               @"--simpleresolution" |]
          ReferencedProjects =
            [| memoProject116 "Project116"
               memoProject11 "Project11"
               memoProject122 "Project122"
               memoProject12 "Project12"
               Project138
               Project140
               Project142
               Project144
               Project146
               Project149
               memoProject14 "Project14"
               memoProject151 "Project151"
               memoProject15 "Project15"
               Project161
               Project164
               Project168
               memoProject16 "Project16"
               Project172
               memoProject17 "Project17"
               Project184
               Project189
               Project190
               memoProject19 "Project19"
               memoProject206 "Project206"
               memoProject213 "Project213"
               Project214
               memoProject222 "Project222"
               memoProject22 "Project22"
               memoProject23 "Project23"
               memoProject243 "Project243"
               memoProject24 "Project24"
               memoProject264 "Project264"
               memoProject272 "Project272"
               Project295
               Project297
               Project323
               Project326
               Project327
               memoProject329 "Project329"
               memoProject338 "Project338"
               memoProject339 "Project339"
               Project344
               memoProject346 "Project346"
               Project41
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
          Stamp = Some 40L }

    FSharpReferencedProject.CreateFSharp(
        __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project40\bin\Debug\net472\Project40.dll",
        projectOptions
    )

let memoProject40 = memoization Project40

let Project12 _ =
    let projectOptions =
        { ProjectFileName = __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project12\Project12.fsproj"
          ProjectId = None
          SourceFiles =
            [| __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project12\obj\Debug\net472\Project12.AssemblyInfo.fs"
               __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project12\MyModule.fs" |]
          OtherOptions =
            [| "-o:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project12\obj\Debug\net472\Project12.dll"
               @"-g"
               @"--debug:portable"
               @"--noframework"
               @"--define:TRACE"
               @"--define:DEBUG"
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
               @"--optimize-"
               @"--tailcalls-"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\mscorlib.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project11\bin\Debug\net472\Project11.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project138\bin\Debug\net472\Project138.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project140\bin\Debug\net472\Project140.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project142\bin\Debug\net472\Project142.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project144\bin\Debug\net472\Project144.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project146\bin\Debug\net472\Project146.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project149\bin\Debug\net472\Project149.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project151\bin\Debug\net472\Project151.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project161\bin\Debug\net472\Project161.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project164\bin\Debug\net472\Project164.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project168\bin\Debug\net472\Project168.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project17\bin\Debug\net472\Project17.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project172\bin\Debug\net472\Project172.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project189\bin\Debug\net472\Project189.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project192\bin\Debug\net472\Project192.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project213\bin\Debug\net472\Project213.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project214\bin\Debug\net472\Project214.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project22\bin\Debug\net472\Project22.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project222\bin\Debug\net472\Project222.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project232\bin\Debug\net472\Project232.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project243\bin\Debug\net472\Project243.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project250\bin\Debug\net472\Project250.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project252\bin\Debug\net472\Project252.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project257\bin\Debug\net472\Project257.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project264\bin\Debug\net472\Project264.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project272\bin\Debug\net472\Project272.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project295\bin\Debug\net472\Project295.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project297\bin\Debug\net472\Project297.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project319\bin\Debug\net472\Project319.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project323\bin\Debug\net472\Project323.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project326\bin\Debug\net472\Project326.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project329\bin\Debug\net472\Project329.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project331\bin\Debug\net472\Project331.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project339\bin\Debug\net472\Project339.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project340\bin\Debug\net472\Project340.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project344\bin\Debug\net472\Project344.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project346\bin\Debug\net472\Project346.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project45\bin\Debug\net472\Project45.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project47\bin\Debug\net472\Project47.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project70\bin\Debug\net472\Project70.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project71\bin\Debug\net472\Project71.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project85\bin\Debug\net472\Project85.dll"
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
               @"--warn:3"
               @"--warnaserror:3239"
               @"--fullpaths"
               @"--flaterrors"
               @"--subsystemversion:6.00"
               @"--highentropyva+"
               @"--targetprofile:mscorlib"
               @"--nocopyfsharpcore"
               @"--deterministic+"
               @"--simpleresolution" |]
          ReferencedProjects =
            [| memoProject11 "Project11"
               Project138
               Project140
               Project142
               Project144
               Project146
               Project149
               memoProject151 "Project151"
               Project161
               Project164
               Project168
               Project172
               memoProject17 "Project17"
               Project189
               Project192
               memoProject213 "Project213"
               Project214
               memoProject222 "Project222"
               memoProject22 "Project22"
               memoProject232 "Project232"
               memoProject243 "Project243"
               memoProject250 "Project250"
               memoProject252 "Project252"
               memoProject257 "Project257"
               memoProject264 "Project264"
               memoProject272 "Project272"
               Project295
               Project297
               memoProject319 "Project319"
               Project323
               Project326
               memoProject329 "Project329"
               memoProject331 "Project331"
               memoProject339 "Project339"
               Project340
               Project344
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
          Stamp = Some 12L }

    FSharpReferencedProject.CreateFSharp(
        __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project12\bin\Debug\net472\Project12.dll",
        projectOptions
    )

let memoProject12 = memoization Project12

let Project232 _ =
    let projectOptions =
        { ProjectFileName = __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project232\Project232.fsproj"
          ProjectId = None
          SourceFiles =
            [| __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project232\obj\Debug\net472\Project232.AssemblyInfo.fs"
               __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project232\MyModule.fs" |]
          OtherOptions =
            [| "-o:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project232\obj\Debug\net472\Project232.dll"
               @"-g"
               @"--debug:portable"
               @"--noframework"
               @"--define:TRACE"
               @"--define:DEBUG"
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
               @"--optimize-"
               @"--tailcalls-"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\mscorlib.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project138\bin\Debug\net472\Project138.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project140\bin\Debug\net472\Project140.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project142\bin\Debug\net472\Project142.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project144\bin\Debug\net472\Project144.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project146\bin\Debug\net472\Project146.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project149\bin\Debug\net472\Project149.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project151\bin\Debug\net472\Project151.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project157\bin\Debug\net472\Project157.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project161\bin\Debug\net472\Project161.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project164\bin\Debug\net472\Project164.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project168\bin\Debug\net472\Project168.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project172\bin\Debug\net472\Project172.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project184\bin\Debug\net472\Project184.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project189\bin\Debug\net472\Project189.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project192\bin\Debug\net472\Project192.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project213\bin\Debug\net472\Project213.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project214\bin\Debug\net472\Project214.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project222\bin\Debug\net472\Project222.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project224\bin\Debug\net472\Project224.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project243\bin\Debug\net472\Project243.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project250\bin\Debug\net472\Project250.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project252\bin\Debug\net472\Project252.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project255\bin\Debug\net472\Project255.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project264\bin\Debug\net472\Project264.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project268\bin\Debug\net472\Project268.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project272\bin\Debug\net472\Project272.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project273\bin\Debug\net472\Project273.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project283\bin\Debug\net472\Project283.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project292\bin\Debug\net472\Project292.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project295\bin\Debug\net472\Project295.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project297\bin\Debug\net472\Project297.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project302\bin\Debug\net472\Project302.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project305\bin\Debug\net472\Project305.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project310\bin\Debug\net472\Project310.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project317\bin\Debug\net472\Project317.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project323\bin\Debug\net472\Project323.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project326\bin\Debug\net472\Project326.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project329\bin\Debug\net472\Project329.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project331\bin\Debug\net472\Project331.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project338\bin\Debug\net472\Project338.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project339\bin\Debug\net472\Project339.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project340\bin\Debug\net472\Project340.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project341\bin\Debug\net472\Project341.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project342\bin\Debug\net472\Project342.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project344\bin\Debug\net472\Project344.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project346\bin\Debug\net472\Project346.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project84\bin\Debug\net472\Project84.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project87\bin\Debug\net472\Project87.dll"
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
               @"--warn:3"
               @"--warnaserror:3239"
               @"--fullpaths"
               @"--flaterrors"
               @"--subsystemversion:6.00"
               @"--highentropyva+"
               @"--targetprofile:mscorlib"
               @"--nocopyfsharpcore"
               @"--deterministic+"
               @"--simpleresolution" |]
          ReferencedProjects =
            [| Project138
               Project140
               Project142
               Project144
               Project146
               Project149
               memoProject151 "Project151"
               Project157
               Project161
               Project164
               Project168
               Project172
               Project184
               Project189
               Project192
               memoProject213 "Project213"
               Project214
               memoProject222 "Project222"
               Project224
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
               Project295
               Project297
               memoProject302 "Project302"
               Project305
               memoProject310 "Project310"
               Project317
               Project323
               Project326
               memoProject329 "Project329"
               memoProject331 "Project331"
               memoProject338 "Project338"
               memoProject339 "Project339"
               Project340
               Project341
               Project342
               Project344
               memoProject346 "Project346"
               memoProject84 "Project84"
               Project87 |]
          IsIncompleteTypeCheckEnvironment = false
          UseScriptResolutionRules = false
          LoadTime = DateTime.Now
          UnresolvedReferences = None
          OriginalLoadReferences = []
          Stamp = Some 232L }

    FSharpReferencedProject.CreateFSharp(
        __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project232\bin\Debug\net472\Project232.dll",
        projectOptions
    )

let memoProject232 = memoization Project232

let Project252 _ =
    let projectOptions =
        { ProjectFileName = __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project252\Project252.fsproj"
          ProjectId = None
          SourceFiles =
            [| __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project252\obj\Debug\net472\Project252.AssemblyInfo.fs"
               __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project252\MyModule.fs" |]
          OtherOptions =
            [| "-o:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project252\obj\Debug\net472\Project252.dll"
               @"-g"
               @"--debug:portable"
               @"--noframework"
               @"--define:TRACE"
               @"--define:DEBUG"
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
               @"--optimize-"
               @"--tailcalls-"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\mscorlib.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project142\bin\Debug\net472\Project142.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project190\bin\Debug\net472\Project190.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project264\bin\Debug\net472\Project264.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project319\bin\Debug\net472\Project319.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project326\bin\Debug\net472\Project326.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project346\bin\Debug\net472\Project346.dll"
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
               @"--warn:3"
               @"--warnaserror:3239"
               @"--fullpaths"
               @"--flaterrors"
               @"--subsystemversion:6.00"
               @"--highentropyva+"
               @"--targetprofile:mscorlib"
               @"--nocopyfsharpcore"
               @"--deterministic+"
               @"--simpleresolution" |]
          ReferencedProjects =
            [| Project142
               Project190
               memoProject264 "Project264"
               memoProject319 "Project319"
               Project326
               memoProject346 "Project346" |]
          IsIncompleteTypeCheckEnvironment = false
          UseScriptResolutionRules = false
          LoadTime = DateTime.Now
          UnresolvedReferences = None
          OriginalLoadReferences = []
          Stamp = Some 252L }

    FSharpReferencedProject.CreateFSharp(
        __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project252\bin\Debug\net472\Project252.dll",
        projectOptions
    )

let memoProject252 = memoization Project252

let Project255 _ =
    let projectOptions =
        { ProjectFileName = __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project255\Project255.fsproj"
          ProjectId = None
          SourceFiles =
            [| __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project255\obj\Debug\net472\Project255.AssemblyInfo.fs"
               __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project255\MyModule.fs" |]
          OtherOptions =
            [| "-o:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project255\obj\Debug\net472\Project255.dll"
               @"-g"
               @"--debug:portable"
               @"--noframework"
               @"--define:TRACE"
               @"--define:DEBUG"
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
               @"--optimize-"
               @"--tailcalls-"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\mscorlib.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project146\bin\Debug\net472\Project146.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project257\bin\Debug\net472\Project257.dll"
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
               @"--warn:3"
               @"--warnaserror:3239"
               @"--fullpaths"
               @"--flaterrors"
               @"--subsystemversion:6.00"
               @"--highentropyva+"
               @"--targetprofile:mscorlib"
               @"--nocopyfsharpcore"
               @"--deterministic+"
               @"--simpleresolution" |]
          ReferencedProjects =
            [| Project146
               memoProject257 "Project257" |]
          IsIncompleteTypeCheckEnvironment = false
          UseScriptResolutionRules = false
          LoadTime = DateTime.Now
          UnresolvedReferences = None
          OriginalLoadReferences = []
          Stamp = Some 255L }

    FSharpReferencedProject.CreateFSharp(
        __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project255\bin\Debug\net472\Project255.dll",
        projectOptions
    )

let memoProject255 = memoization Project255

let Project257 _ =
    let projectOptions =
        { ProjectFileName = __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project257\Project257.fsproj"
          ProjectId = None
          SourceFiles =
            [| __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project257\obj\Debug\net472\Project257.AssemblyInfo.fs"
               __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project257\MyModule.fs" |]
          OtherOptions =
            [| "-o:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project257\obj\Debug\net472\Project257.dll"
               @"-g"
               @"--debug:portable"
               @"--noframework"
               @"--define:TRACE"
               @"--define:DEBUG"
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
               @"--optimize-"
               @"--tailcalls-"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\mscorlib.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project140\bin\Debug\net472\Project140.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project142\bin\Debug\net472\Project142.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project149\bin\Debug\net472\Project149.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project161\bin\Debug\net472\Project161.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project164\bin\Debug\net472\Project164.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project168\bin\Debug\net472\Project168.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project189\bin\Debug\net472\Project189.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project190\bin\Debug\net472\Project190.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project192\bin\Debug\net472\Project192.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project214\bin\Debug\net472\Project214.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project252\bin\Debug\net472\Project252.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project253\bin\Debug\net472\Project253.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project256\bin\Debug\net472\Project256.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project264\bin\Debug\net472\Project264.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project295\bin\Debug\net472\Project295.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project297\bin\Debug\net472\Project297.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project319\bin\Debug\net472\Project319.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project326\bin\Debug\net472\Project326.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project331\bin\Debug\net472\Project331.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project346\bin\Debug\net472\Project346.dll"
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
               @"--warn:3"
               @"--warnaserror:3239"
               @"--fullpaths"
               @"--flaterrors"
               @"--subsystemversion:6.00"
               @"--highentropyva+"
               @"--targetprofile:mscorlib"
               @"--nocopyfsharpcore"
               @"--deterministic+"
               @"--simpleresolution" |]
          ReferencedProjects =
            [| Project140
               Project142
               Project149
               Project161
               Project164
               Project168
               Project189
               Project190
               Project192
               Project214
               memoProject252 "Project252"
               Project253
               memoProject256 "Project256"
               memoProject264 "Project264"
               Project295
               Project297
               memoProject319 "Project319"
               Project326
               memoProject331 "Project331"
               memoProject346 "Project346" |]
          IsIncompleteTypeCheckEnvironment = false
          UseScriptResolutionRules = false
          LoadTime = DateTime.Now
          UnresolvedReferences = None
          OriginalLoadReferences = []
          Stamp = Some 257L }

    FSharpReferencedProject.CreateFSharp(
        __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project257\bin\Debug\net472\Project257.dll",
        projectOptions
    )

let memoProject257 = memoization Project257

let Project253 =
    FSharpReferencedProject.CreatePortableExecutable(
        __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project253\bin\Debug\net472\Project253.dll",
        getTimeStamp,
        loader
    )

let Project256 _ =
    let projectOptions =
        { ProjectFileName = __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project256\Project256.fsproj"
          ProjectId = None
          SourceFiles =
            [| __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project256\obj\Debug\net472\Project256.AssemblyInfo.fs"
               __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project256\MyModule.fs" |]
          OtherOptions =
            [| "-o:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project256\obj\Debug\net472\Project256.dll"
               @"-g"
               @"--debug:portable"
               @"--noframework"
               @"--define:TRACE"
               @"--define:DEBUG"
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
               @"--optimize-"
               @"--tailcalls-"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\mscorlib.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project142\bin\Debug\net472\Project142.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project190\bin\Debug\net472\Project190.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project252\bin\Debug\net472\Project252.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project264\bin\Debug\net472\Project264.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project319\bin\Debug\net472\Project319.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project326\bin\Debug\net472\Project326.dll"
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
               @"--warn:3"
               @"--warnaserror:3239"
               @"--fullpaths"
               @"--flaterrors"
               @"--subsystemversion:6.00"
               @"--highentropyva+"
               @"--targetprofile:mscorlib"
               @"--nocopyfsharpcore"
               @"--deterministic+"
               @"--simpleresolution" |]
          ReferencedProjects =
            [| Project142
               Project190
               memoProject252 "Project252"
               memoProject264 "Project264"
               memoProject319 "Project319"
               Project326 |]
          IsIncompleteTypeCheckEnvironment = false
          UseScriptResolutionRules = false
          LoadTime = DateTime.Now
          UnresolvedReferences = None
          OriginalLoadReferences = []
          Stamp = Some 256L }

    FSharpReferencedProject.CreateFSharp(
        __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project256\bin\Debug\net472\Project256.dll",
        projectOptions
    )

let memoProject256 = memoization Project256

let Project331 _ =
    let projectOptions =
        { ProjectFileName = __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project331\Project331.fsproj"
          ProjectId = None
          SourceFiles =
            [| __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project331\obj\Debug\net472\Project331.AssemblyInfo.fs"
               __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project331\MyModule.fs" |]
          OtherOptions =
            [| "-o:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project331\obj\Debug\net472\Project331.dll"
               @"-g"
               @"--debug:portable"
               @"--noframework"
               @"--define:TRACE"
               @"--define:DEBUG"
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
               @"--optimize-"
               @"--tailcalls-"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\mscorlib.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project140\bin\Debug\net472\Project140.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project142\bin\Debug\net472\Project142.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project149\bin\Debug\net472\Project149.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project161\bin\Debug\net472\Project161.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project164\bin\Debug\net472\Project164.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project168\bin\Debug\net472\Project168.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project190\bin\Debug\net472\Project190.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project214\bin\Debug\net472\Project214.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project252\bin\Debug\net472\Project252.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project295\bin\Debug\net472\Project295.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project297\bin\Debug\net472\Project297.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project301\bin\Debug\net472\Project301.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project319\bin\Debug\net472\Project319.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project326\bin\Debug\net472\Project326.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project346\bin\Debug\net472\Project346.dll"
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
               @"--warn:3"
               @"--warnaserror:3239"
               @"--fullpaths"
               @"--flaterrors"
               @"--subsystemversion:6.00"
               @"--highentropyva+"
               @"--targetprofile:mscorlib"
               @"--nocopyfsharpcore"
               @"--deterministic+"
               @"--simpleresolution" |]
          ReferencedProjects =
            [| Project140
               Project142
               Project149
               Project161
               Project164
               Project168
               Project190
               Project214
               memoProject252 "Project252"
               Project295
               Project297
               Project301
               memoProject319 "Project319"
               Project326
               memoProject346 "Project346" |]
          IsIncompleteTypeCheckEnvironment = false
          UseScriptResolutionRules = false
          LoadTime = DateTime.Now
          UnresolvedReferences = None
          OriginalLoadReferences = []
          Stamp = Some 331L }

    FSharpReferencedProject.CreateFSharp(
        __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project331\bin\Debug\net472\Project331.dll",
        projectOptions
    )

let memoProject331 = memoization Project331

let Project283 _ =
    let projectOptions =
        { ProjectFileName = __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project283\Project283.fsproj"
          ProjectId = None
          SourceFiles =
            [| __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project283\obj\Debug\net472\Project283.AssemblyInfo.fs"
               __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project283\MyModule.fs" |]
          OtherOptions =
            [| "-o:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project283\obj\Debug\net472\Project283.dll"
               @"-g"
               @"--debug:portable"
               @"--noframework"
               @"--define:TRACE"
               @"--define:DEBUG"
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
               @"--optimize-"
               @"--tailcalls-"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\mscorlib.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project140\bin\Debug\net472\Project140.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project142\bin\Debug\net472\Project142.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project144\bin\Debug\net472\Project144.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project164\bin\Debug\net472\Project164.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project172\bin\Debug\net472\Project172.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project213\bin\Debug\net472\Project213.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project342\bin\Debug\net472\Project342.dll"
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
               @"--warn:3"
               @"--warnaserror:3239"
               @"--fullpaths"
               @"--flaterrors"
               @"--subsystemversion:6.00"
               @"--highentropyva+"
               @"--targetprofile:mscorlib"
               @"--nocopyfsharpcore"
               @"--deterministic+"
               @"--simpleresolution" |]
          ReferencedProjects =
            [| Project140
               Project142
               Project144
               Project164
               Project172
               memoProject213 "Project213"
               Project342 |]
          IsIncompleteTypeCheckEnvironment = false
          UseScriptResolutionRules = false
          LoadTime = DateTime.Now
          UnresolvedReferences = None
          OriginalLoadReferences = []
          Stamp = Some 283L }

    FSharpReferencedProject.CreateFSharp(
        __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project283\bin\Debug\net472\Project283.dll",
        projectOptions
    )

let memoProject283 = memoization Project283

let Project292 _ =
    let projectOptions =
        { ProjectFileName = __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project292\Project292.fsproj"
          ProjectId = None
          SourceFiles =
            [| __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project292\obj\Debug\net472\Project292.AssemblyInfo.fs"
               __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project292\MyModule.fs" |]
          OtherOptions =
            [| "-o:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project292\obj\Debug\net472\Project292.dll"
               @"-g"
               @"--debug:portable"
               @"--noframework"
               @"--define:TRACE"
               @"--define:DEBUG"
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
               @"--optimize-"
               @"--tailcalls-"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\mscorlib.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project138\bin\Debug\net472\Project138.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project140\bin\Debug\net472\Project140.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project144\bin\Debug\net472\Project144.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project146\bin\Debug\net472\Project146.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project149\bin\Debug\net472\Project149.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project151\bin\Debug\net472\Project151.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project157\bin\Debug\net472\Project157.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project161\bin\Debug\net472\Project161.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project164\bin\Debug\net472\Project164.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project168\bin\Debug\net472\Project168.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project189\bin\Debug\net472\Project189.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project192\bin\Debug\net472\Project192.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project214\bin\Debug\net472\Project214.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project222\bin\Debug\net472\Project222.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project243\bin\Debug\net472\Project243.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project272\bin\Debug\net472\Project272.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project329\bin\Debug\net472\Project329.dll"
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
               @"--warn:3"
               @"--warnaserror:3239"
               @"--fullpaths"
               @"--flaterrors"
               @"--subsystemversion:6.00"
               @"--highentropyva+"
               @"--targetprofile:mscorlib"
               @"--nocopyfsharpcore"
               @"--deterministic+"
               @"--simpleresolution" |]
          ReferencedProjects =
            [| Project138
               Project140
               Project144
               Project146
               Project149
               memoProject151 "Project151"
               Project157
               Project161
               Project164
               Project168
               Project189
               Project192
               Project214
               memoProject222 "Project222"
               memoProject243 "Project243"
               memoProject272 "Project272"
               memoProject329 "Project329" |]
          IsIncompleteTypeCheckEnvironment = false
          UseScriptResolutionRules = false
          LoadTime = DateTime.Now
          UnresolvedReferences = None
          OriginalLoadReferences = []
          Stamp = Some 292L }

    FSharpReferencedProject.CreateFSharp(
        __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project292\bin\Debug\net472\Project292.dll",
        projectOptions
    )

let memoProject292 = memoization Project292

let Project338 _ =
    let projectOptions =
        { ProjectFileName = __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project338\Project338.fsproj"
          ProjectId = None
          SourceFiles =
            [| __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project338\obj\Debug\net472\Project338.AssemblyInfo.fs"
               __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project338\MyModule.fs" |]
          OtherOptions =
            [| "-o:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project338\obj\Debug\net472\Project338.dll"
               @"-g"
               @"--debug:portable"
               @"--noframework"
               @"--define:TRACE"
               @"--define:DEBUG"
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
               @"--optimize-"
               @"--tailcalls-"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\mscorlib.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project138\bin\Debug\net472\Project138.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project140\bin\Debug\net472\Project140.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project142\bin\Debug\net472\Project142.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project144\bin\Debug\net472\Project144.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project146\bin\Debug\net472\Project146.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project149\bin\Debug\net472\Project149.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project151\bin\Debug\net472\Project151.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project157\bin\Debug\net472\Project157.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project161\bin\Debug\net472\Project161.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project164\bin\Debug\net472\Project164.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project168\bin\Debug\net472\Project168.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project214\bin\Debug\net472\Project214.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project222\bin\Debug\net472\Project222.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project272\bin\Debug\net472\Project272.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project273\bin\Debug\net472\Project273.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project326\bin\Debug\net472\Project326.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project342\bin\Debug\net472\Project342.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project344\bin\Debug\net472\Project344.dll"
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
               @"--warn:3"
               @"--warnaserror:3239"
               @"--fullpaths"
               @"--flaterrors"
               @"--subsystemversion:6.00"
               @"--highentropyva+"
               @"--targetprofile:mscorlib"
               @"--nocopyfsharpcore"
               @"--deterministic+"
               @"--simpleresolution" |]
          ReferencedProjects =
            [| Project138
               Project140
               Project142
               Project144
               Project146
               Project149
               memoProject151 "Project151"
               Project157
               Project161
               Project164
               Project168
               Project214
               memoProject222 "Project222"
               memoProject272 "Project272"
               memoProject273 "Project273"
               Project326
               Project342
               Project344 |]
          IsIncompleteTypeCheckEnvironment = false
          UseScriptResolutionRules = false
          LoadTime = DateTime.Now
          UnresolvedReferences = None
          OriginalLoadReferences = []
          Stamp = Some 338L }

    FSharpReferencedProject.CreateFSharp(
        __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project338\bin\Debug\net472\Project338.dll",
        projectOptions
    )

let memoProject338 = memoization Project338

let Project339 _ =
    let projectOptions =
        { ProjectFileName = __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project339\Project339.fsproj"
          ProjectId = None
          SourceFiles =
            [| __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project339\obj\Debug\net472\Project339.AssemblyInfo.fs"
               __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project339\MyModule.fs" |]
          OtherOptions =
            [| "-o:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project339\obj\Debug\net472\Project339.dll"
               @"-g"
               @"--debug:portable"
               @"--noframework"
               @"--define:TRACE"
               @"--define:DEBUG"
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
               @"--optimize-"
               @"--tailcalls-"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\mscorlib.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project138\bin\Debug\net472\Project138.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project140\bin\Debug\net472\Project140.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project142\bin\Debug\net472\Project142.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project144\bin\Debug\net472\Project144.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project146\bin\Debug\net472\Project146.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project149\bin\Debug\net472\Project149.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project151\bin\Debug\net472\Project151.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project161\bin\Debug\net472\Project161.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project164\bin\Debug\net472\Project164.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project168\bin\Debug\net472\Project168.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project172\bin\Debug\net472\Project172.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project189\bin\Debug\net472\Project189.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project192\bin\Debug\net472\Project192.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project204\bin\Debug\net472\Project204.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project209\bin\Debug\net472\Project209.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project210\bin\Debug\net472\Project210.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project214\bin\Debug\net472\Project214.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project222\bin\Debug\net472\Project222.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project243\bin\Debug\net472\Project243.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project264\bin\Debug\net472\Project264.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project272\bin\Debug\net472\Project272.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project297\bin\Debug\net472\Project297.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project300\bin\Debug\net472\Project300.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project302\bin\Debug\net472\Project302.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project305\bin\Debug\net472\Project305.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project307\bin\Debug\net472\Project307.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project326\bin\Debug\net472\Project326.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project327\bin\Debug\net472\Project327.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project329\bin\Debug\net472\Project329.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project338\bin\Debug\net472\Project338.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project340\bin\Debug\net472\Project340.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project341\bin\Debug\net472\Project341.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project342\bin\Debug\net472\Project342.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project344\bin\Debug\net472\Project344.dll"
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
               @"--warn:3"
               @"--warnaserror:3239"
               @"--fullpaths"
               @"--flaterrors"
               @"--subsystemversion:6.00"
               @"--highentropyva+"
               @"--targetprofile:mscorlib"
               @"--nocopyfsharpcore"
               @"--deterministic+"
               @"--simpleresolution" |]
          ReferencedProjects =
            [| Project138
               Project140
               Project142
               Project144
               Project146
               Project149
               memoProject151 "Project151"
               Project161
               Project164
               Project168
               Project172
               Project189
               Project192
               Project204
               Project209
               Project210
               Project214
               memoProject222 "Project222"
               memoProject243 "Project243"
               memoProject264 "Project264"
               memoProject272 "Project272"
               Project297
               memoProject300 "Project300"
               memoProject302 "Project302"
               Project305
               Project307
               Project326
               Project327
               memoProject329 "Project329"
               memoProject338 "Project338"
               Project340
               Project341
               Project342
               Project344 |]
          IsIncompleteTypeCheckEnvironment = false
          UseScriptResolutionRules = false
          LoadTime = DateTime.Now
          UnresolvedReferences = None
          OriginalLoadReferences = []
          Stamp = Some 339L }

    FSharpReferencedProject.CreateFSharp(
        __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project339\bin\Debug\net472\Project339.dll",
        projectOptions
    )

let memoProject339 = memoization Project339

let Project84 _ =
    let projectOptions =
        { ProjectFileName = __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project84\Project84.fsproj"
          ProjectId = None
          SourceFiles =
            [| __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project84\obj\Debug\net472\Project84.AssemblyInfo.fs"
               __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project84\MyModule.fs" |]
          OtherOptions =
            [| "-o:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project84\obj\Debug\net472\Project84.dll"
               @"-g"
               @"--debug:portable"
               @"--noframework"
               @"--define:TRACE"
               @"--define:DEBUG"
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
               @"--optimize-"
               @"--tailcalls-"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\mscorlib.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project138\bin\Debug\net472\Project138.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project140\bin\Debug\net472\Project140.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project142\bin\Debug\net472\Project142.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project144\bin\Debug\net472\Project144.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project146\bin\Debug\net472\Project146.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project149\bin\Debug\net472\Project149.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project151\bin\Debug\net472\Project151.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project161\bin\Debug\net472\Project161.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project164\bin\Debug\net472\Project164.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project172\bin\Debug\net472\Project172.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project190\bin\Debug\net472\Project190.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project192\bin\Debug\net472\Project192.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project214\bin\Debug\net472\Project214.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project243\bin\Debug\net472\Project243.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project250\bin\Debug\net472\Project250.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project264\bin\Debug\net472\Project264.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project272\bin\Debug\net472\Project272.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project273\bin\Debug\net472\Project273.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project293\bin\Debug\net472\Project293.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project294\bin\Debug\net472\Project294.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project295\bin\Debug\net472\Project295.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project297\bin\Debug\net472\Project297.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project298\bin\Debug\net472\Project298.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project301\bin\Debug\net472\Project301.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project302\bin\Debug\net472\Project302.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project305\bin\Debug\net472\Project305.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project311\bin\Debug\net472\Project311.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project318\bin\Debug\net472\Project318.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project319\bin\Debug\net472\Project319.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project320\bin\Debug\net472\Project320.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project321\bin\Debug\net472\Project321.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project323\bin\Debug\net472\Project323.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project324\bin\Debug\net472\Project324.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project326\bin\Debug\net472\Project326.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project329\bin\Debug\net472\Project329.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project87\bin\Debug\net472\Project87.dll"
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
               @"--warn:3"
               @"--warnaserror:3239"
               @"--fullpaths"
               @"--flaterrors"
               @"--subsystemversion:6.00"
               @"--highentropyva+"
               @"--targetprofile:mscorlib"
               @"--nocopyfsharpcore"
               @"--deterministic+"
               @"--simpleresolution" |]
          ReferencedProjects =
            [| Project138
               Project140
               Project142
               Project144
               Project146
               Project149
               memoProject151 "Project151"
               Project161
               Project164
               Project172
               Project190
               Project192
               Project214
               memoProject243 "Project243"
               memoProject250 "Project250"
               memoProject264 "Project264"
               memoProject272 "Project272"
               memoProject273 "Project273"
               memoProject293 "Project293"
               memoProject294 "Project294"
               Project295
               Project297
               Project298
               Project301
               memoProject302 "Project302"
               Project305
               memoProject311 "Project311"
               memoProject318 "Project318"
               memoProject319 "Project319"
               memoProject320 "Project320"
               memoProject321 "Project321"
               Project323
               memoProject324 "Project324"
               Project326
               memoProject329 "Project329"
               Project87 |]
          IsIncompleteTypeCheckEnvironment = false
          UseScriptResolutionRules = false
          LoadTime = DateTime.Now
          UnresolvedReferences = None
          OriginalLoadReferences = []
          Stamp = Some 84L }

    FSharpReferencedProject.CreateFSharp(
        __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project84\bin\Debug\net472\Project84.dll",
        projectOptions
    )

let memoProject84 = memoization Project84

let Project293 _ =
    let projectOptions =
        { ProjectFileName = __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project293\Project293.fsproj"
          ProjectId = None
          SourceFiles =
            [| __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project293\obj\Debug\net472\Project293.AssemblyInfo.fs"
               __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project293\MyModule.fs" |]
          OtherOptions =
            [| "-o:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project293\obj\Debug\net472\Project293.dll"
               @"-g"
               @"--debug:portable"
               @"--noframework"
               @"--define:TRACE"
               @"--define:DEBUG"
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
               @"--optimize-"
               @"--tailcalls-"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\mscorlib.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project138\bin\Debug\net472\Project138.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project140\bin\Debug\net472\Project140.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project142\bin\Debug\net472\Project142.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project146\bin\Debug\net472\Project146.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project149\bin\Debug\net472\Project149.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project151\bin\Debug\net472\Project151.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project161\bin\Debug\net472\Project161.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project164\bin\Debug\net472\Project164.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project168\bin\Debug\net472\Project168.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project214\bin\Debug\net472\Project214.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project272\bin\Debug\net472\Project272.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project295\bin\Debug\net472\Project295.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project297\bin\Debug\net472\Project297.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project301\bin\Debug\net472\Project301.dll"
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
               @"--warn:3"
               @"--warnaserror:3239"
               @"--fullpaths"
               @"--flaterrors"
               @"--subsystemversion:6.00"
               @"--highentropyva+"
               @"--targetprofile:mscorlib"
               @"--nocopyfsharpcore"
               @"--deterministic+"
               @"--simpleresolution" |]
          ReferencedProjects =
            [| Project138
               Project140
               Project142
               Project146
               Project149
               memoProject151 "Project151"
               Project161
               Project164
               Project168
               Project214
               memoProject272 "Project272"
               Project295
               Project297
               Project301 |]
          IsIncompleteTypeCheckEnvironment = false
          UseScriptResolutionRules = false
          LoadTime = DateTime.Now
          UnresolvedReferences = None
          OriginalLoadReferences = []
          Stamp = Some 293L }

    FSharpReferencedProject.CreateFSharp(
        __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project293\bin\Debug\net472\Project293.dll",
        projectOptions
    )

let memoProject293 = memoization Project293

let Project318 _ =
    let projectOptions =
        { ProjectFileName = __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project318\Project318.fsproj"
          ProjectId = None
          SourceFiles =
            [| __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project318\obj\Debug\net472\Project318.AssemblyInfo.fs"
               __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project318\MyModule.fs" |]
          OtherOptions =
            [| "-o:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project318\obj\Debug\net472\Project318.dll"
               @"-g"
               @"--debug:portable"
               @"--noframework"
               @"--define:TRACE"
               @"--define:DEBUG"
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
               @"--optimize-"
               @"--tailcalls-"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\mscorlib.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project189\bin\Debug\net472\Project189.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project319\bin\Debug\net472\Project319.dll"
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
               @"--warn:3"
               @"--warnaserror:3239"
               @"--fullpaths"
               @"--flaterrors"
               @"--subsystemversion:6.00"
               @"--highentropyva+"
               @"--targetprofile:mscorlib"
               @"--nocopyfsharpcore"
               @"--deterministic+"
               @"--simpleresolution" |]
          ReferencedProjects =
            [| Project189
               memoProject319 "Project319" |]
          IsIncompleteTypeCheckEnvironment = false
          UseScriptResolutionRules = false
          LoadTime = DateTime.Now
          UnresolvedReferences = None
          OriginalLoadReferences = []
          Stamp = Some 318L }

    FSharpReferencedProject.CreateFSharp(
        __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project318\bin\Debug\net472\Project318.dll",
        projectOptions
    )

let memoProject318 = memoization Project318

let Project320 _ =
    let projectOptions =
        { ProjectFileName = __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project320\Project320.fsproj"
          ProjectId = None
          SourceFiles =
            [| __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project320\obj\Debug\net472\Project320.AssemblyInfo.fs"
               __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project320\MyModule.fs" |]
          OtherOptions =
            [| "-o:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project320\obj\Debug\net472\Project320.dll"
               @"-g"
               @"--debug:portable"
               @"--noframework"
               @"--define:TRACE"
               @"--define:DEBUG"
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
               @"--optimize-"
               @"--tailcalls-"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\mscorlib.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project140\bin\Debug\net472\Project140.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project142\bin\Debug\net472\Project142.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project149\bin\Debug\net472\Project149.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project164\bin\Debug\net472\Project164.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project189\bin\Debug\net472\Project189.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project190\bin\Debug\net472\Project190.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project240\bin\Debug\net472\Project240.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project264\bin\Debug\net472\Project264.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project318\bin\Debug\net472\Project318.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project319\bin\Debug\net472\Project319.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project321\bin\Debug\net472\Project321.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project323\bin\Debug\net472\Project323.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project326\bin\Debug\net472\Project326.dll"
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
               @"--warn:3"
               @"--warnaserror:3239"
               @"--fullpaths"
               @"--flaterrors"
               @"--subsystemversion:6.00"
               @"--highentropyva+"
               @"--targetprofile:mscorlib"
               @"--nocopyfsharpcore"
               @"--deterministic+"
               @"--simpleresolution" |]
          ReferencedProjects =
            [| Project140
               Project142
               Project149
               Project164
               Project189
               Project190
               memoProject240 "Project240"
               memoProject264 "Project264"
               memoProject318 "Project318"
               memoProject319 "Project319"
               memoProject321 "Project321"
               Project323
               Project326 |]
          IsIncompleteTypeCheckEnvironment = false
          UseScriptResolutionRules = false
          LoadTime = DateTime.Now
          UnresolvedReferences = None
          OriginalLoadReferences = []
          Stamp = Some 320L }

    FSharpReferencedProject.CreateFSharp(
        __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project320\bin\Debug\net472\Project320.dll",
        projectOptions
    )

let memoProject320 = memoization Project320

let Project321 _ =
    let projectOptions =
        { ProjectFileName = __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project321\Project321.fsproj"
          ProjectId = None
          SourceFiles =
            [| __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project321\obj\Debug\net472\Project321.AssemblyInfo.fs"
               __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project321\MyModule.fs" |]
          OtherOptions =
            [| "-o:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project321\obj\Debug\net472\Project321.dll"
               @"-g"
               @"--debug:portable"
               @"--noframework"
               @"--define:TRACE"
               @"--define:DEBUG"
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
               @"--optimize-"
               @"--tailcalls-"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\mscorlib.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project140\bin\Debug\net472\Project140.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project142\bin\Debug\net472\Project142.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project161\bin\Debug\net472\Project161.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project164\bin\Debug\net472\Project164.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project168\bin\Debug\net472\Project168.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project189\bin\Debug\net472\Project189.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project240\bin\Debug\net472\Project240.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project264\bin\Debug\net472\Project264.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project318\bin\Debug\net472\Project318.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project319\bin\Debug\net472\Project319.dll"
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
               @"--warn:3"
               @"--warnaserror:3239"
               @"--fullpaths"
               @"--flaterrors"
               @"--subsystemversion:6.00"
               @"--highentropyva+"
               @"--targetprofile:mscorlib"
               @"--nocopyfsharpcore"
               @"--deterministic+"
               @"--simpleresolution" |]
          ReferencedProjects =
            [| Project140
               Project142
               Project161
               Project164
               Project168
               Project189
               memoProject240 "Project240"
               memoProject264 "Project264"
               memoProject318 "Project318"
               memoProject319 "Project319" |]
          IsIncompleteTypeCheckEnvironment = false
          UseScriptResolutionRules = false
          LoadTime = DateTime.Now
          UnresolvedReferences = None
          OriginalLoadReferences = []
          Stamp = Some 321L }

    FSharpReferencedProject.CreateFSharp(
        __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project321\bin\Debug\net472\Project321.dll",
        projectOptions
    )

let memoProject321 = memoization Project321

let Project45 _ =
    let projectOptions =
        { ProjectFileName = __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project45\Project45.fsproj"
          ProjectId = None
          SourceFiles =
            [| __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project45\obj\Debug\net472\Project45.AssemblyInfo.fs"
               __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project45\MyModule.fs" |]
          OtherOptions =
            [| "-o:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project45\obj\Debug\net472\Project45.dll"
               @"-g"
               @"--debug:portable"
               @"--noframework"
               @"--define:TRACE"
               @"--define:DEBUG"
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
               @"--optimize-"
               @"--tailcalls-"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\mscorlib.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project116\bin\Debug\net472\Project116.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project140\bin\Debug\net472\Project140.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project142\bin\Debug\net472\Project142.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project149\bin\Debug\net472\Project149.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project164\bin\Debug\net472\Project164.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project17\bin\Debug\net472\Project17.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project22\bin\Debug\net472\Project22.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project42\bin\Debug\net472\Project42.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project47\bin\Debug\net472\Project47.dll"
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
               @"--warn:3"
               @"--warnaserror:3239"
               @"--fullpaths"
               @"--flaterrors"
               @"--subsystemversion:6.00"
               @"--highentropyva+"
               @"--targetprofile:mscorlib"
               @"--nocopyfsharpcore"
               @"--deterministic+"
               @"--simpleresolution" |]
          ReferencedProjects =
            [| memoProject116 "Project116"
               Project140
               Project142
               Project149
               Project164
               memoProject17 "Project17"
               memoProject22 "Project22"
               memoProject42 "Project42"
               memoProject47 "Project47" |]
          IsIncompleteTypeCheckEnvironment = false
          UseScriptResolutionRules = false
          LoadTime = DateTime.Now
          UnresolvedReferences = None
          OriginalLoadReferences = []
          Stamp = Some 45L }

    FSharpReferencedProject.CreateFSharp(
        __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project45\bin\Debug\net472\Project45.dll",
        projectOptions
    )

let memoProject45 = memoization Project45

let Project42 _ =
    let projectOptions =
        { ProjectFileName = __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project42\Project42.fsproj"
          ProjectId = None
          SourceFiles =
            [| __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project42\obj\Debug\net472\Project42.AssemblyInfo.fs"
               __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project42\MyModule.fs" |]
          OtherOptions =
            [| "-o:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project42\obj\Debug\net472\Project42.dll"
               @"-g"
               @"--debug:portable"
               @"--noframework"
               @"--define:TRACE"
               @"--define:DEBUG"
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
               @"--optimize-"
               @"--tailcalls-"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\mscorlib.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project122\bin\Debug\net472\Project122.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project138\bin\Debug\net472\Project138.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project140\bin\Debug\net472\Project140.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project142\bin\Debug\net472\Project142.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project149\bin\Debug\net472\Project149.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project164\bin\Debug\net472\Project164.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project17\bin\Debug\net472\Project17.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project22\bin\Debug\net472\Project22.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project298\bin\Debug\net472\Project298.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project47\bin\Debug\net472\Project47.dll"
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
               @"--warn:3"
               @"--warnaserror:3239"
               @"--fullpaths"
               @"--flaterrors"
               @"--subsystemversion:6.00"
               @"--highentropyva+"
               @"--targetprofile:mscorlib"
               @"--nocopyfsharpcore"
               @"--deterministic+"
               @"--simpleresolution" |]
          ReferencedProjects =
            [| memoProject122 "Project122"
               Project138
               Project140
               Project142
               Project149
               Project164
               memoProject17 "Project17"
               memoProject22 "Project22"
               Project298
               memoProject47 "Project47" |]
          IsIncompleteTypeCheckEnvironment = false
          UseScriptResolutionRules = false
          LoadTime = DateTime.Now
          UnresolvedReferences = None
          OriginalLoadReferences = []
          Stamp = Some 42L }

    FSharpReferencedProject.CreateFSharp(
        __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project42\bin\Debug\net472\Project42.dll",
        projectOptions
    )

let memoProject42 = memoization Project42

let Project47 _ =
    let projectOptions =
        { ProjectFileName = __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project47\Project47.fsproj"
          ProjectId = None
          SourceFiles =
            [| __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project47\obj\Debug\net472\Project47.AssemblyInfo.fs"
               __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project47\MyModule.fs" |]
          OtherOptions =
            [| "-o:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project47\obj\Debug\net472\Project47.dll"
               @"-g"
               @"--debug:portable"
               @"--noframework"
               @"--define:TRACE"
               @"--define:DEBUG"
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
               @"--optimize-"
               @"--tailcalls-"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\mscorlib.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project142\bin\Debug\net472\Project142.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project144\bin\Debug\net472\Project144.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project146\bin\Debug\net472\Project146.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project151\bin\Debug\net472\Project151.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project164\bin\Debug\net472\Project164.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project172\bin\Debug\net472\Project172.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project184\bin\Debug\net472\Project184.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project189\bin\Debug\net472\Project189.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project206\bin\Debug\net472\Project206.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project213\bin\Debug\net472\Project213.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project22\bin\Debug\net472\Project22.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project272\bin\Debug\net472\Project272.dll"
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
               @"--warn:3"
               @"--warnaserror:3239"
               @"--fullpaths"
               @"--flaterrors"
               @"--subsystemversion:6.00"
               @"--highentropyva+"
               @"--targetprofile:mscorlib"
               @"--nocopyfsharpcore"
               @"--deterministic+"
               @"--simpleresolution" |]
          ReferencedProjects =
            [| Project142
               Project144
               Project146
               memoProject151 "Project151"
               Project164
               Project172
               Project184
               Project189
               memoProject206 "Project206"
               memoProject213 "Project213"
               memoProject22 "Project22"
               memoProject272 "Project272" |]
          IsIncompleteTypeCheckEnvironment = false
          UseScriptResolutionRules = false
          LoadTime = DateTime.Now
          UnresolvedReferences = None
          OriginalLoadReferences = []
          Stamp = Some 47L }

    FSharpReferencedProject.CreateFSharp(
        __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project47\bin\Debug\net472\Project47.dll",
        projectOptions
    )

let memoProject47 = memoization Project47

let Project70 _ =
    let projectOptions =
        { ProjectFileName = __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project70\Project70.fsproj"
          ProjectId = None
          SourceFiles =
            [| __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project70\obj\Debug\net472\Project70.AssemblyInfo.fs"
               __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project70\MyModule.fs" |]
          OtherOptions =
            [| "-o:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project70\obj\Debug\net472\Project70.dll"
               @"-g"
               @"--debug:portable"
               @"--noframework"
               @"--define:TRACE"
               @"--define:DEBUG"
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
               @"--optimize-"
               @"--tailcalls-"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\mscorlib.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project142\bin\Debug\net472\Project142.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project252\bin\Debug\net472\Project252.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project319\bin\Debug\net472\Project319.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project331\bin\Debug\net472\Project331.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project346\bin\Debug\net472\Project346.dll"
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
               @"--warn:3"
               @"--warnaserror:3239"
               @"--fullpaths"
               @"--flaterrors"
               @"--subsystemversion:6.00"
               @"--highentropyva+"
               @"--targetprofile:mscorlib"
               @"--nocopyfsharpcore"
               @"--deterministic+"
               @"--simpleresolution" |]
          ReferencedProjects =
            [| Project142
               memoProject252 "Project252"
               memoProject319 "Project319"
               memoProject331 "Project331"
               memoProject346 "Project346" |]
          IsIncompleteTypeCheckEnvironment = false
          UseScriptResolutionRules = false
          LoadTime = DateTime.Now
          UnresolvedReferences = None
          OriginalLoadReferences = []
          Stamp = Some 70L }

    FSharpReferencedProject.CreateFSharp(
        __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project70\bin\Debug\net472\Project70.dll",
        projectOptions
    )

let memoProject70 = memoization Project70

let Project71 _ =
    let projectOptions =
        { ProjectFileName = __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project71\Project71.fsproj"
          ProjectId = None
          SourceFiles =
            [| __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project71\obj\Debug\net472\Project71.AssemblyInfo.fs"
               __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project71\MyModule.fs" |]
          OtherOptions =
            [| "-o:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project71\obj\Debug\net472\Project71.dll"
               @"-g"
               @"--debug:portable"
               @"--noframework"
               @"--define:TRACE"
               @"--define:DEBUG"
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
               @"--optimize-"
               @"--tailcalls-"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\mscorlib.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project140\bin\Debug\net472\Project140.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project142\bin\Debug\net472\Project142.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project149\bin\Debug\net472\Project149.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project161\bin\Debug\net472\Project161.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project164\bin\Debug\net472\Project164.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project172\bin\Debug\net472\Project172.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project189\bin\Debug\net472\Project189.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project190\bin\Debug\net472\Project190.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project192\bin\Debug\net472\Project192.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project238\bin\Debug\net472\Project238.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project252\bin\Debug\net472\Project252.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project253\bin\Debug\net472\Project253.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project257\bin\Debug\net472\Project257.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project264\bin\Debug\net472\Project264.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project295\bin\Debug\net472\Project295.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project319\bin\Debug\net472\Project319.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project331\bin\Debug\net472\Project331.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project340\bin\Debug\net472\Project340.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project341\bin\Debug\net472\Project341.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project346\bin\Debug\net472\Project346.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project70\bin\Debug\net472\Project70.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project72\bin\Debug\net472\Project72.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project74\bin\Debug\net472\Project74.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project84\bin\Debug\net472\Project84.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project87\bin\Debug\net472\Project87.dll"
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
               @"--warn:3"
               @"--warnaserror:3239"
               @"--fullpaths"
               @"--flaterrors"
               @"--subsystemversion:6.00"
               @"--highentropyva+"
               @"--targetprofile:mscorlib"
               @"--nocopyfsharpcore"
               @"--deterministic+"
               @"--simpleresolution" |]
          ReferencedProjects =
            [| Project140
               Project142
               Project149
               Project161
               Project164
               Project172
               Project189
               Project190
               Project192
               Project238
               memoProject252 "Project252"
               Project253
               memoProject257 "Project257"
               memoProject264 "Project264"
               Project295
               memoProject319 "Project319"
               memoProject331 "Project331"
               Project340
               Project341
               memoProject346 "Project346"
               memoProject70 "Project70"
               Project72
               Project74
               memoProject84 "Project84"
               Project87 |]
          IsIncompleteTypeCheckEnvironment = false
          UseScriptResolutionRules = false
          LoadTime = DateTime.Now
          UnresolvedReferences = None
          OriginalLoadReferences = []
          Stamp = Some 71L }

    FSharpReferencedProject.CreateFSharp(
        __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project71\bin\Debug\net472\Project71.dll",
        projectOptions
    )

let memoProject71 = memoization Project71

let Project238 =
    FSharpReferencedProject.CreatePortableExecutable(
        __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project238\bin\Debug\net472\Project238.dll",
        getTimeStamp,
        loader
    )

let Project237 =
    FSharpReferencedProject.CreatePortableExecutable(
        __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project237\bin\Debug\net472\Project237.dll",
        getTimeStamp,
        loader
    )

let Project72 =
    FSharpReferencedProject.CreatePortableExecutable(
        __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project72\bin\Debug\net472\Project72.dll",
        getTimeStamp,
        loader
    )

let Project74 =
    FSharpReferencedProject.CreatePortableExecutable(
        __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project74\bin\Debug\net472\Project74.dll",
        getTimeStamp,
        loader
    )

let Project281 _ =
    let projectOptions =
        { ProjectFileName = __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project281\Project281.fsproj"
          ProjectId = None
          SourceFiles =
            [| __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project281\obj\Debug\net472\Project281.AssemblyInfo.fs"
               __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project281\MyModule.fs" |]
          OtherOptions =
            [| "-o:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project281\obj\Debug\net472\Project281.dll"
               @"-g"
               @"--debug:portable"
               @"--noframework"
               @"--define:TRACE"
               @"--define:DEBUG"
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
               @"--optimize-"
               @"--tailcalls-"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\mscorlib.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project250\bin\Debug\net472\Project250.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project272\bin\Debug\net472\Project272.dll"
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
               @"--warn:3"
               @"--warnaserror:3239"
               @"--fullpaths"
               @"--flaterrors"
               @"--subsystemversion:6.00"
               @"--highentropyva+"
               @"--targetprofile:mscorlib"
               @"--nocopyfsharpcore"
               @"--deterministic+"
               @"--simpleresolution" |]
          ReferencedProjects =
            [| memoProject250 "Project250"
               memoProject272 "Project272" |]
          IsIncompleteTypeCheckEnvironment = false
          UseScriptResolutionRules = false
          LoadTime = DateTime.Now
          UnresolvedReferences = None
          OriginalLoadReferences = []
          Stamp = Some 281L }

    FSharpReferencedProject.CreateFSharp(
        __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project281\bin\Debug\net472\Project281.dll",
        projectOptions
    )

let memoProject281 = memoization Project281

let Project69 _ =
    let projectOptions =
        { ProjectFileName = __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project69\Project69.fsproj"
          ProjectId = None
          SourceFiles =
            [| __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project69\obj\Debug\net472\Project69.AssemblyInfo.fs"
               __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project69\MyModule.fs" |]
          OtherOptions =
            [| "-o:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project69\obj\Debug\net472\Project69.dll"
               @"-g"
               @"--debug:portable"
               @"--noframework"
               @"--define:TRACE"
               @"--define:DEBUG"
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
               @"--optimize-"
               @"--tailcalls-"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\mscorlib.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project140\bin\Debug\net472\Project140.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project142\bin\Debug\net472\Project142.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project151\bin\Debug\net472\Project151.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project161\bin\Debug\net472\Project161.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project164\bin\Debug\net472\Project164.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project168\bin\Debug\net472\Project168.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project189\bin\Debug\net472\Project189.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project192\bin\Debug\net472\Project192.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project214\bin\Debug\net472\Project214.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project264\bin\Debug\net472\Project264.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project281\bin\Debug\net472\Project281.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project297\bin\Debug\net472\Project297.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project298\bin\Debug\net472\Project298.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project323\bin\Debug\net472\Project323.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project326\bin\Debug\net472\Project326.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project331\bin\Debug\net472\Project331.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project340\bin\Debug\net472\Project340.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project341\bin\Debug\net472\Project341.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project68\bin\Debug\net472\Project68.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project84\bin\Debug\net472\Project84.dll"
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
               @"--warn:3"
               @"--warnaserror:3239"
               @"--fullpaths"
               @"--flaterrors"
               @"--subsystemversion:6.00"
               @"--highentropyva+"
               @"--targetprofile:mscorlib"
               @"--nocopyfsharpcore"
               @"--deterministic+"
               @"--simpleresolution" |]
          ReferencedProjects =
            [| Project140
               Project142
               memoProject151 "Project151"
               Project161
               Project164
               Project168
               Project189
               Project192
               Project214
               memoProject264 "Project264"
               memoProject281 "Project281"
               Project297
               Project298
               Project323
               Project326
               memoProject331 "Project331"
               Project340
               Project341
               Project68
               memoProject84 "Project84" |]
          IsIncompleteTypeCheckEnvironment = false
          UseScriptResolutionRules = false
          LoadTime = DateTime.Now
          UnresolvedReferences = None
          OriginalLoadReferences = []
          Stamp = Some 69L }

    FSharpReferencedProject.CreateFSharp(
        __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project69\bin\Debug\net472\Project69.dll",
        projectOptions
    )

let memoProject69 = memoization Project69

let Project68 =
    FSharpReferencedProject.CreatePortableExecutable(
        __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project68\bin\Debug\net472\Project68.dll",
        getTimeStamp,
        loader
    )

let Project85 _ =
    let projectOptions =
        { ProjectFileName = __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project85\Project85.fsproj"
          ProjectId = None
          SourceFiles =
            [| __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project85\obj\Debug\net472\Project85.AssemblyInfo.fs"
               __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project85\MyModule.fs" |]
          OtherOptions =
            [| "-o:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project85\obj\Debug\net472\Project85.dll"
               @"-g"
               @"--debug:portable"
               @"--noframework"
               @"--define:TRACE"
               @"--define:DEBUG"
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
               @"--optimize-"
               @"--tailcalls-"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\mscorlib.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project138\bin\Debug\net472\Project138.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project140\bin\Debug\net472\Project140.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project142\bin\Debug\net472\Project142.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project144\bin\Debug\net472\Project144.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project146\bin\Debug\net472\Project146.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project149\bin\Debug\net472\Project149.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project151\bin\Debug\net472\Project151.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project161\bin\Debug\net472\Project161.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project164\bin\Debug\net472\Project164.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project168\bin\Debug\net472\Project168.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project172\bin\Debug\net472\Project172.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project189\bin\Debug\net472\Project189.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project190\bin\Debug\net472\Project190.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project192\bin\Debug\net472\Project192.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project213\bin\Debug\net472\Project213.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project214\bin\Debug\net472\Project214.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project222\bin\Debug\net472\Project222.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project232\bin\Debug\net472\Project232.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project236\bin\Debug\net472\Project236.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project243\bin\Debug\net472\Project243.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project250\bin\Debug\net472\Project250.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project252\bin\Debug\net472\Project252.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project257\bin\Debug\net472\Project257.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project264\bin\Debug\net472\Project264.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project268\bin\Debug\net472\Project268.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project272\bin\Debug\net472\Project272.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project282\bin\Debug\net472\Project282.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project292\bin\Debug\net472\Project292.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project295\bin\Debug\net472\Project295.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project297\bin\Debug\net472\Project297.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project301\bin\Debug\net472\Project301.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project302\bin\Debug\net472\Project302.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project303\bin\Debug\net472\Project303.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project319\bin\Debug\net472\Project319.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project323\bin\Debug\net472\Project323.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project326\bin\Debug\net472\Project326.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project329\bin\Debug\net472\Project329.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project331\bin\Debug\net472\Project331.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project339\bin\Debug\net472\Project339.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project344\bin\Debug\net472\Project344.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project346\bin\Debug\net472\Project346.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project84\bin\Debug\net472\Project84.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project87\bin\Debug\net472\Project87.dll"
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
               @"--warn:3"
               @"--warnaserror:3239"
               @"--fullpaths"
               @"--flaterrors"
               @"--subsystemversion:6.00"
               @"--highentropyva+"
               @"--targetprofile:mscorlib"
               @"--nocopyfsharpcore"
               @"--deterministic+"
               @"--simpleresolution" |]
          ReferencedProjects =
            [| Project138
               Project140
               Project142
               Project144
               Project146
               Project149
               memoProject151 "Project151"
               Project161
               Project164
               Project168
               Project172
               Project189
               Project190
               Project192
               memoProject213 "Project213"
               Project214
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
               Project295
               Project297
               Project301
               memoProject302 "Project302"
               memoProject303 "Project303"
               memoProject319 "Project319"
               Project323
               Project326
               memoProject329 "Project329"
               memoProject331 "Project331"
               memoProject339 "Project339"
               Project344
               memoProject346 "Project346"
               memoProject84 "Project84"
               Project87 |]
          IsIncompleteTypeCheckEnvironment = false
          UseScriptResolutionRules = false
          LoadTime = DateTime.Now
          UnresolvedReferences = None
          OriginalLoadReferences = []
          Stamp = Some 85L }

    FSharpReferencedProject.CreateFSharp(
        __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project85\bin\Debug\net472\Project85.dll",
        projectOptions
    )

let memoProject85 = memoization Project85

let Project282 _ =
    let projectOptions =
        { ProjectFileName = __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project282\Project282.fsproj"
          ProjectId = None
          SourceFiles =
            [| __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project282\obj\Debug\net472\Project282.AssemblyInfo.fs"
               __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project282\MyModule.fs" |]
          OtherOptions =
            [| "-o:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project282\obj\Debug\net472\Project282.dll"
               @"-g"
               @"--debug:portable"
               @"--noframework"
               @"--define:TRACE"
               @"--define:DEBUG"
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
               @"--optimize-"
               @"--tailcalls-"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\mscorlib.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project138\bin\Debug\net472\Project138.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project140\bin\Debug\net472\Project140.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project142\bin\Debug\net472\Project142.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project144\bin\Debug\net472\Project144.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project146\bin\Debug\net472\Project146.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project149\bin\Debug\net472\Project149.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project151\bin\Debug\net472\Project151.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project157\bin\Debug\net472\Project157.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project164\bin\Debug\net472\Project164.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project168\bin\Debug\net472\Project168.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project172\bin\Debug\net472\Project172.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project189\bin\Debug\net472\Project189.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project192\bin\Debug\net472\Project192.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project213\bin\Debug\net472\Project213.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project214\bin\Debug\net472\Project214.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project222\bin\Debug\net472\Project222.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project232\bin\Debug\net472\Project232.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project243\bin\Debug\net472\Project243.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project264\bin\Debug\net472\Project264.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project268\bin\Debug\net472\Project268.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project272\bin\Debug\net472\Project272.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project292\bin\Debug\net472\Project292.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project323\bin\Debug\net472\Project323.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project326\bin\Debug\net472\Project326.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project329\bin\Debug\net472\Project329.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project337\bin\Debug\net472\Project337.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project339\bin\Debug\net472\Project339.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project346\bin\Debug\net472\Project346.dll"
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
               @"--warn:3"
               @"--warnaserror:3239"
               @"--fullpaths"
               @"--flaterrors"
               @"--subsystemversion:6.00"
               @"--highentropyva+"
               @"--targetprofile:mscorlib"
               @"--nocopyfsharpcore"
               @"--deterministic+"
               @"--simpleresolution" |]
          ReferencedProjects =
            [| Project138
               Project140
               Project142
               Project144
               Project146
               Project149
               memoProject151 "Project151"
               Project157
               Project164
               Project168
               Project172
               Project189
               Project192
               memoProject213 "Project213"
               Project214
               memoProject222 "Project222"
               memoProject232 "Project232"
               memoProject243 "Project243"
               memoProject264 "Project264"
               memoProject268 "Project268"
               memoProject272 "Project272"
               memoProject292 "Project292"
               Project323
               Project326
               memoProject329 "Project329"
               memoProject337 "Project337"
               memoProject339 "Project339"
               memoProject346 "Project346" |]
          IsIncompleteTypeCheckEnvironment = false
          UseScriptResolutionRules = false
          LoadTime = DateTime.Now
          UnresolvedReferences = None
          OriginalLoadReferences = []
          Stamp = Some 282L }

    FSharpReferencedProject.CreateFSharp(
        __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project282\bin\Debug\net472\Project282.dll",
        projectOptions
    )

let memoProject282 = memoization Project282

let Project337 _ =
    let projectOptions =
        { ProjectFileName = __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project337\Project337.fsproj"
          ProjectId = None
          SourceFiles =
            [| __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project337\obj\Debug\net472\Project337.AssemblyInfo.fs"
               __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project337\MyModule.fs" |]
          OtherOptions =
            [| "-o:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project337\obj\Debug\net472\Project337.dll"
               @"-g"
               @"--debug:portable"
               @"--noframework"
               @"--define:TRACE"
               @"--define:DEBUG"
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
               @"--optimize-"
               @"--tailcalls-"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\mscorlib.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project138\bin\Debug\net472\Project138.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project140\bin\Debug\net472\Project140.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project142\bin\Debug\net472\Project142.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project151\bin\Debug\net472\Project151.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project161\bin\Debug\net472\Project161.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project164\bin\Debug\net472\Project164.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project168\bin\Debug\net472\Project168.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project200\bin\Debug\net472\Project200.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project214\bin\Debug\net472\Project214.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project260\bin\Debug\net472\Project260.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project264\bin\Debug\net472\Project264.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project305\bin\Debug\net472\Project305.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project326\bin\Debug\net472\Project326.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project342\bin\Debug\net472\Project342.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project344\bin\Debug\net472\Project344.dll"
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
               @"--warn:3"
               @"--warnaserror:3239"
               @"--fullpaths"
               @"--flaterrors"
               @"--subsystemversion:6.00"
               @"--highentropyva+"
               @"--targetprofile:mscorlib"
               @"--nocopyfsharpcore"
               @"--deterministic+"
               @"--simpleresolution" |]
          ReferencedProjects =
            [| Project138
               Project140
               Project142
               memoProject151 "Project151"
               Project161
               Project164
               Project168
               Project200
               Project214
               Project260
               memoProject264 "Project264"
               Project305
               Project326
               Project342
               Project344 |]
          IsIncompleteTypeCheckEnvironment = false
          UseScriptResolutionRules = false
          LoadTime = DateTime.Now
          UnresolvedReferences = None
          OriginalLoadReferences = []
          Stamp = Some 337L }

    FSharpReferencedProject.CreateFSharp(
        __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project337\bin\Debug\net472\Project337.dll",
        projectOptions
    )

let memoProject337 = memoization Project337

let Project303 _ =
    let projectOptions =
        { ProjectFileName = __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project303\Project303.fsproj"
          ProjectId = None
          SourceFiles =
            [| __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project303\obj\Debug\net472\Project303.AssemblyInfo.fs"
               __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project303\MyModule.fs" |]
          OtherOptions =
            [| "-o:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project303\obj\Debug\net472\Project303.dll"
               @"-g"
               @"--debug:portable"
               @"--noframework"
               @"--define:TRACE"
               @"--define:DEBUG"
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
               @"--optimize-"
               @"--tailcalls-"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\mscorlib.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project138\bin\Debug\net472\Project138.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project140\bin\Debug\net472\Project140.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project142\bin\Debug\net472\Project142.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project146\bin\Debug\net472\Project146.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project149\bin\Debug\net472\Project149.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project151\bin\Debug\net472\Project151.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project164\bin\Debug\net472\Project164.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project168\bin\Debug\net472\Project168.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project172\bin\Debug\net472\Project172.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project189\bin\Debug\net472\Project189.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project190\bin\Debug\net472\Project190.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project192\bin\Debug\net472\Project192.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project213\bin\Debug\net472\Project213.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project222\bin\Debug\net472\Project222.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project236\bin\Debug\net472\Project236.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project243\bin\Debug\net472\Project243.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project252\bin\Debug\net472\Project252.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project257\bin\Debug\net472\Project257.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project264\bin\Debug\net472\Project264.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project268\bin\Debug\net472\Project268.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project272\bin\Debug\net472\Project272.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project302\bin\Debug\net472\Project302.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project305\bin\Debug\net472\Project305.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project319\bin\Debug\net472\Project319.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project323\bin\Debug\net472\Project323.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project324\bin\Debug\net472\Project324.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project326\bin\Debug\net472\Project326.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project329\bin\Debug\net472\Project329.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project87\bin\Debug\net472\Project87.dll"
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
               @"--warn:3"
               @"--warnaserror:3239"
               @"--fullpaths"
               @"--flaterrors"
               @"--subsystemversion:6.00"
               @"--highentropyva+"
               @"--targetprofile:mscorlib"
               @"--nocopyfsharpcore"
               @"--deterministic+"
               @"--simpleresolution" |]
          ReferencedProjects =
            [| Project138
               Project140
               Project142
               Project146
               Project149
               memoProject151 "Project151"
               Project164
               Project168
               Project172
               Project189
               Project190
               Project192
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
               Project305
               memoProject319 "Project319"
               Project323
               memoProject324 "Project324"
               Project326
               memoProject329 "Project329"
               Project87 |]
          IsIncompleteTypeCheckEnvironment = false
          UseScriptResolutionRules = false
          LoadTime = DateTime.Now
          UnresolvedReferences = None
          OriginalLoadReferences = []
          Stamp = Some 303L }

    FSharpReferencedProject.CreateFSharp(
        __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project303\bin\Debug\net472\Project303.dll",
        projectOptions
    )

let memoProject303 = memoization Project303

let Project41 =
    FSharpReferencedProject.CreatePortableExecutable(
        __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project41\bin\Debug\net472\Project41.dll",
        getTimeStamp,
        loader
    )

let Project43 _ =
    let projectOptions =
        { ProjectFileName = __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project43\Project43.fsproj"
          ProjectId = None
          SourceFiles =
            [| __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project43\obj\Debug\net472\Project43.AssemblyInfo.fs"
               __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project43\MyModule.fs" |]
          OtherOptions =
            [| "-o:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project43\obj\Debug\net472\Project43.dll"
               @"-g"
               @"--debug:portable"
               @"--noframework"
               @"--define:TRACE"
               @"--define:DEBUG"
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
               @"--optimize-"
               @"--tailcalls-"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\mscorlib.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project140\bin\Debug\net472\Project140.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project142\bin\Debug\net472\Project142.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project149\bin\Debug\net472\Project149.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project161\bin\Debug\net472\Project161.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project164\bin\Debug\net472\Project164.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project168\bin\Debug\net472\Project168.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project21\bin\Debug\net472\Project21.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project22\bin\Debug\net472\Project22.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project24\bin\Debug\net472\Project24.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project47\bin\Debug\net472\Project47.dll"
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
               @"--warn:3"
               @"--warnaserror:3239"
               @"--fullpaths"
               @"--flaterrors"
               @"--subsystemversion:6.00"
               @"--highentropyva+"
               @"--targetprofile:mscorlib"
               @"--nocopyfsharpcore"
               @"--deterministic+"
               @"--simpleresolution" |]
          ReferencedProjects =
            [| Project140
               Project142
               Project149
               Project161
               Project164
               Project168
               memoProject21 "Project21"
               memoProject22 "Project22"
               memoProject24 "Project24"
               memoProject47 "Project47" |]
          IsIncompleteTypeCheckEnvironment = false
          UseScriptResolutionRules = false
          LoadTime = DateTime.Now
          UnresolvedReferences = None
          OriginalLoadReferences = []
          Stamp = Some 43L }

    FSharpReferencedProject.CreateFSharp(
        __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project43\bin\Debug\net472\Project43.dll",
        projectOptions
    )

let memoProject43 = memoization Project43

let Project44 _ =
    let projectOptions =
        { ProjectFileName = __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project44\Project44.fsproj"
          ProjectId = None
          SourceFiles =
            [| __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project44\obj\Debug\net472\Project44.AssemblyInfo.fs"
               __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project44\MyModule.fs" |]
          OtherOptions =
            [| "-o:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project44\obj\Debug\net472\Project44.dll"
               @"-g"
               @"--debug:portable"
               @"--noframework"
               @"--define:TRACE"
               @"--define:DEBUG"
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
               @"--optimize-"
               @"--tailcalls-"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\mscorlib.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project140\bin\Debug\net472\Project140.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project142\bin\Debug\net472\Project142.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project149\bin\Debug\net472\Project149.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project164\bin\Debug\net472\Project164.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project17\bin\Debug\net472\Project17.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project22\bin\Debug\net472\Project22.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project23\bin\Debug\net472\Project23.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project45\bin\Debug\net472\Project45.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project46\bin\Debug\net472\Project46.dll"
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
               @"--warn:3"
               @"--warnaserror:3239"
               @"--fullpaths"
               @"--flaterrors"
               @"--subsystemversion:6.00"
               @"--highentropyva+"
               @"--targetprofile:mscorlib"
               @"--nocopyfsharpcore"
               @"--deterministic+"
               @"--simpleresolution" |]
          ReferencedProjects =
            [| Project140
               Project142
               Project149
               Project164
               memoProject17 "Project17"
               memoProject22 "Project22"
               memoProject23 "Project23"
               memoProject45 "Project45"
               memoProject46 "Project46" |]
          IsIncompleteTypeCheckEnvironment = false
          UseScriptResolutionRules = false
          LoadTime = DateTime.Now
          UnresolvedReferences = None
          OriginalLoadReferences = []
          Stamp = Some 44L }

    FSharpReferencedProject.CreateFSharp(
        __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project44\bin\Debug\net472\Project44.dll",
        projectOptions
    )

let memoProject44 = memoization Project44

let Project46 _ =
    let projectOptions =
        { ProjectFileName = __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project46\Project46.fsproj"
          ProjectId = None
          SourceFiles =
            [| __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project46\obj\Debug\net472\Project46.AssemblyInfo.fs"
               __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project46\MyModule.fs" |]
          OtherOptions =
            [| "-o:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project46\obj\Debug\net472\Project46.dll"
               @"-g"
               @"--debug:portable"
               @"--noframework"
               @"--define:TRACE"
               @"--define:DEBUG"
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
               @"--optimize-"
               @"--tailcalls-"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\mscorlib.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project140\bin\Debug\net472\Project140.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project142\bin\Debug\net472\Project142.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project149\bin\Debug\net472\Project149.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project161\bin\Debug\net472\Project161.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project164\bin\Debug\net472\Project164.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project17\bin\Debug\net472\Project17.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project22\bin\Debug\net472\Project22.dll"
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
               @"--warn:3"
               @"--warnaserror:3239"
               @"--fullpaths"
               @"--flaterrors"
               @"--subsystemversion:6.00"
               @"--highentropyva+"
               @"--targetprofile:mscorlib"
               @"--nocopyfsharpcore"
               @"--deterministic+"
               @"--simpleresolution" |]
          ReferencedProjects =
            [| Project140
               Project142
               Project149
               Project161
               Project164
               memoProject17 "Project17"
               memoProject22 "Project22" |]
          IsIncompleteTypeCheckEnvironment = false
          UseScriptResolutionRules = false
          LoadTime = DateTime.Now
          UnresolvedReferences = None
          OriginalLoadReferences = []
          Stamp = Some 46L }

    FSharpReferencedProject.CreateFSharp(
        __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project46\bin\Debug\net472\Project46.dll",
        projectOptions
    )

let memoProject46 = memoization Project46

let Project98 _ =
    let projectOptions =
        { ProjectFileName = __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project98\Project98.fsproj"
          ProjectId = None
          SourceFiles =
            [| __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project98\obj\Debug\net472\Project98.AssemblyInfo.fs"
               __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project98\MyModule.fs" |]
          OtherOptions =
            [| "-o:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project98\obj\Debug\net472\Project98.dll"
               @"-g"
               @"--debug:portable"
               @"--noframework"
               @"--define:TRACE"
               @"--define:DEBUG"
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
               @"--optimize-"
               @"--tailcalls-"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\mscorlib.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project122\bin\Debug\net472\Project122.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project138\bin\Debug\net472\Project138.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project140\bin\Debug\net472\Project140.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project142\bin\Debug\net472\Project142.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project144\bin\Debug\net472\Project144.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project146\bin\Debug\net472\Project146.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project149\bin\Debug\net472\Project149.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project151\bin\Debug\net472\Project151.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project161\bin\Debug\net472\Project161.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project164\bin\Debug\net472\Project164.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project172\bin\Debug\net472\Project172.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project184\bin\Debug\net472\Project184.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project189\bin\Debug\net472\Project189.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project206\bin\Debug\net472\Project206.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project209\bin\Debug\net472\Project209.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project214\bin\Debug\net472\Project214.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project23\bin\Debug\net472\Project23.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project236\bin\Debug\net472\Project236.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project243\bin\Debug\net472\Project243.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project250\bin\Debug\net472\Project250.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project261\bin\Debug\net472\Project261.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project264\bin\Debug\net472\Project264.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project272\bin\Debug\net472\Project272.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project3\bin\Debug\net472\Project3.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project300\bin\Debug\net472\Project300.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project302\bin\Debug\net472\Project302.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project5\bin\Debug\net472\Project5.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project87\bin\Debug\net472\Project87.dll"
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
               @"--warn:3"
               @"--warnaserror:3239"
               @"--fullpaths"
               @"--flaterrors"
               @"--subsystemversion:6.00"
               @"--highentropyva+"
               @"--targetprofile:mscorlib"
               @"--nocopyfsharpcore"
               @"--deterministic+"
               @"--simpleresolution" |]
          ReferencedProjects =
            [| memoProject122 "Project122"
               Project138
               Project140
               Project142
               Project144
               Project146
               Project149
               memoProject151 "Project151"
               Project161
               Project164
               Project172
               Project184
               Project189
               memoProject206 "Project206"
               Project209
               Project214
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
               memoProject5 "Project5"
               Project87 |]
          IsIncompleteTypeCheckEnvironment = false
          UseScriptResolutionRules = false
          LoadTime = DateTime.Now
          UnresolvedReferences = None
          OriginalLoadReferences = []
          Stamp = Some 98L }

    FSharpReferencedProject.CreateFSharp(
        __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project98\bin\Debug\net472\Project98.dll",
        projectOptions
    )

let memoProject98 = memoization Project98

let Project3 _ =
    let projectOptions =
        { ProjectFileName = __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project3\Project3.fsproj"
          ProjectId = None
          SourceFiles =
            [| __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project3\obj\Debug\net472\Project3.AssemblyInfo.fs"
               __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project3\MyModule.fs" |]
          OtherOptions =
            [| "-o:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project3\obj\Debug\net472\Project3.dll"
               @"-g"
               @"--debug:portable"
               @"--noframework"
               @"--define:TRACE"
               @"--define:DEBUG"
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
               @"--optimize-"
               @"--tailcalls-"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\mscorlib.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project138\bin\Debug\net472\Project138.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project140\bin\Debug\net472\Project140.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project142\bin\Debug\net472\Project142.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project144\bin\Debug\net472\Project144.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project146\bin\Debug\net472\Project146.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project149\bin\Debug\net472\Project149.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project151\bin\Debug\net472\Project151.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project161\bin\Debug\net472\Project161.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project164\bin\Debug\net472\Project164.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project189\bin\Debug\net472\Project189.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project206\bin\Debug\net472\Project206.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project209\bin\Debug\net472\Project209.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project214\bin\Debug\net472\Project214.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project236\bin\Debug\net472\Project236.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project243\bin\Debug\net472\Project243.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project250\bin\Debug\net472\Project250.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project261\bin\Debug\net472\Project261.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project264\bin\Debug\net472\Project264.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project272\bin\Debug\net472\Project272.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project295\bin\Debug\net472\Project295.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project311\bin\Debug\net472\Project311.dll"
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
               @"--warn:3"
               @"--warnaserror:3239"
               @"--fullpaths"
               @"--flaterrors"
               @"--subsystemversion:6.00"
               @"--highentropyva+"
               @"--targetprofile:mscorlib"
               @"--nocopyfsharpcore"
               @"--deterministic+"
               @"--simpleresolution" |]
          ReferencedProjects =
            [| Project138
               Project140
               Project142
               Project144
               Project146
               Project149
               memoProject151 "Project151"
               Project161
               Project164
               Project189
               memoProject206 "Project206"
               Project209
               Project214
               memoProject236 "Project236"
               memoProject243 "Project243"
               memoProject250 "Project250"
               memoProject261 "Project261"
               memoProject264 "Project264"
               memoProject272 "Project272"
               Project295
               memoProject311 "Project311" |]
          IsIncompleteTypeCheckEnvironment = false
          UseScriptResolutionRules = false
          LoadTime = DateTime.Now
          UnresolvedReferences = None
          OriginalLoadReferences = []
          Stamp = Some 3L }

    FSharpReferencedProject.CreateFSharp(
        __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project3\bin\Debug\net472\Project3.dll",
        projectOptions
    )

let memoProject3 = memoization Project3

let Project5 _ =
    let projectOptions =
        { ProjectFileName = __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project5\Project5.fsproj"
          ProjectId = None
          SourceFiles =
            [| __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project5\obj\Debug\net472\Project5.AssemblyInfo.fs"
               __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project5\MyModule.fs" |]
          OtherOptions =
            [| "-o:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project5\obj\Debug\net472\Project5.dll"
               @"-g"
               @"--debug:portable"
               @"--noframework"
               @"--define:TRACE"
               @"--define:DEBUG"
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
               @"--optimize-"
               @"--tailcalls-"
               @"-r:C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\mscorlib.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project138\bin\Debug\net472\Project138.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project140\bin\Debug\net472\Project140.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project142\bin\Debug\net472\Project142.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project144\bin\Debug\net472\Project144.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project146\bin\Debug\net472\Project146.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project149\bin\Debug\net472\Project149.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project151\bin\Debug\net472\Project151.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project161\bin\Debug\net472\Project161.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project164\bin\Debug\net472\Project164.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project168\bin\Debug\net472\Project168.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project172\bin\Debug\net472\Project172.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project184\bin\Debug\net472\Project184.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project189\bin\Debug\net472\Project189.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project190\bin\Debug\net472\Project190.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project192\bin\Debug\net472\Project192.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project206\bin\Debug\net472\Project206.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project213\bin\Debug\net472\Project213.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project214\bin\Debug\net472\Project214.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project222\bin\Debug\net472\Project222.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project232\bin\Debug\net472\Project232.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project243\bin\Debug\net472\Project243.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project250\bin\Debug\net472\Project250.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project261\bin\Debug\net472\Project261.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project264\bin\Debug\net472\Project264.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project272\bin\Debug\net472\Project272.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project273\bin\Debug\net472\Project273.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project281\bin\Debug\net472\Project281.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project295\bin\Debug\net472\Project295.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project297\bin\Debug\net472\Project297.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project3\bin\Debug\net472\Project3.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project323\bin\Debug\net472\Project323.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project324\bin\Debug\net472\Project324.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project326\bin\Debug\net472\Project326.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project329\bin\Debug\net472\Project329.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project331\bin\Debug\net472\Project331.dll"
               "-r:" + __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project346\bin\Debug\net472\Project346.dll"
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
               @"--warn:3"
               @"--warnaserror:3239"
               @"--fullpaths"
               @"--flaterrors"
               @"--subsystemversion:6.00"
               @"--highentropyva+"
               @"--targetprofile:mscorlib"
               @"--nocopyfsharpcore"
               @"--deterministic+"
               @"--simpleresolution" |]
          ReferencedProjects =
            [| Project138
               Project140
               Project142
               Project144
               Project146
               Project149
               memoProject151 "Project151"
               Project161
               Project164
               Project168
               Project172
               Project184
               Project189
               Project190
               Project192
               memoProject206 "Project206"
               memoProject213 "Project213"
               Project214
               memoProject222 "Project222"
               memoProject232 "Project232"
               memoProject243 "Project243"
               memoProject250 "Project250"
               memoProject261 "Project261"
               memoProject264 "Project264"
               memoProject272 "Project272"
               memoProject273 "Project273"
               memoProject281 "Project281"
               Project295
               Project297
               Project323
               memoProject324 "Project324"
               Project326
               memoProject329 "Project329"
               memoProject331 "Project331"
               memoProject346 "Project346"
               memoProject3 "Project3" |]
          IsIncompleteTypeCheckEnvironment = false
          UseScriptResolutionRules = false
          LoadTime = DateTime.Now
          UnresolvedReferences = None
          OriginalLoadReferences = []
          Stamp = Some 5L }

    FSharpReferencedProject.CreateFSharp(
        __SOURCE_DIRECTORY__ + @"\data\LargeApp\Project5\bin\Debug\net472\Project5.dll",
        projectOptions
    )

let memoProject5 = memoization Project5
