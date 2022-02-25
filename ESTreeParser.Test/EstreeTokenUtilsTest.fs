namespace ESTreeParser

open Xunit
open Xunit.Abstractions

open System.IO
open FSharp.Literals

type ESTreeTokenUtilsTest(output:ITestOutputHelper) =
    let show res =
        res
        |> Literal.stringify
        |> output.WriteLine

    static member files = 
        PathUtils.files
        |> Seq.map(box>>Array.singleton)

    [<Theory>]    
    [<MemberData(nameof ESTreeTokenUtilsTest.files)>]
    member _.``1 = tokenize from codes``(v) =
        let filePath = Path.Combine(PathUtils.codesPath,$"es{v}.ts")
        if File.Exists filePath then
            let text = File.ReadAllText(filePath)
            let tokens = 
                ESTreeTokenUtils.tokenize text
                |> List.ofSeq
            show tokens

        
