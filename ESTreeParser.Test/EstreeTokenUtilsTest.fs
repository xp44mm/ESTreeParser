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

    [<Theory>]    
    [<MemberData(nameof DataSource.filesForMemberData, MemberType=typeof<DataSource>)>]
    member _.``1 = tokenize from codes``(v) =
        let filePath = Path.Combine(PathUtils.codesPath,$"es{v}.estree")
        if File.Exists filePath then
            let text = File.ReadAllText(filePath)
            let tokens = 
                ESTreeTokenUtils.tokenize text
                |> List.ofSeq
            show tokens

        
