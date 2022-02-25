namespace ESTreeParser

open Xunit
open Xunit.Abstractions

open System.IO
open FSharp.Literals

type EstreeTokenUtilsTest(output:ITestOutputHelper) =
    let show res =
        res
        |> Literal.stringify
        |> output.WriteLine

    [<Theory>]    
    [<InlineData("5"   )>]
    [<InlineData("2015")>]
    [<InlineData("2016")>]
    [<InlineData("2017")>]
    [<InlineData("2018")>]
    [<InlineData("2019")>]
    [<InlineData("2020")>]
    [<InlineData("2021")>]
    [<InlineData("2022")>]
    member _.``1 = tokenize from tscodes``(v) =
        let filePath = Path.Combine(PathUtils.tscodesPath,$"es{v}.ts")
        if File.Exists filePath then
            let text = File.ReadAllText(filePath)
            let tokens = 
                ESTreeTokenUtils.tokenize text
                |> List.ofSeq
            show tokens

        
