namespace ESTreeParser

open Xunit
open Xunit.Abstractions

open FSharp.Literals
open FSharp.xUnit
open System
open System.IO


open System.Threading.Tasks
open System.Reactive.Linq
open FSharp.Control.Tasks.V2

open ESTreeParser.Ast

type ParserTest(output:ITestOutputHelper) =
    let show res =
        res
        |> Literal.stringify
        |> output.WriteLine
                
    [<Fact>]
    member _.``2 = parser``() =
        let source = PathUtils.codesPath
        let filePath = Path.Combine(source,"es2015.ts")
        if File.Exists filePath then
            let text = File.ReadAllText(filePath)
            let y = 
                Parser.parse text
                |> JSON.read
                |> UnquotedJson.JSON.stringifyNormalJson

            output.WriteLine(y)

    [<Fact>]
    member _.``3 = parse union``() =

        let text = """
        interface Literal <: Expression {
            type: "Literal";
            value: string | boolean | null | number | RegExp;
        }
        """
        let y = 
            Parser.parse text
            |> JSON.read
            |> UnquotedJson.JSON.stringifyNormalJson

        output.WriteLine(y)
    [<Theory>]    
    [<MemberData(nameof DataSource.filesForMemberData, MemberType=typeof<DataSource>)>]
    member _.``1 = parser from codes``(v) =
        let filePath = Path.Combine(PathUtils.codesPath,$"es{v}.ts")
        if File.Exists filePath then
            let text = File.ReadAllText(filePath)
            let json =
                text
                |> Parser.parse
                |> JSON.read
                |> UnquotedJson.JSON.stringifyNormalJson

            output.WriteLine(json)
