namespace ESTreeParser

open Xunit
open Xunit.Abstractions
open System
open System.IO
open System.Text
open System.Text.RegularExpressions

open FSharp.Literals
open FSharp.xUnit
open FslexFsyacc.Fsyacc
open FslexFsyacc.Yacc
open FSharp.Idioms

type ESTreeParseTableTest(output:ITestOutputHelper) =
    let show res =
        res
        |> Render.stringify
        |> output.WriteLine

    ///符号在输入文件中的表示
    let symbolRender sym =
        if Regex.IsMatch(sym,@"^\w+$") then
            sym
        else JsonString.quote sym

    let projPath = PathUtils.estreeParserPath
    let filePath = Path.Combine(projPath, "estree.fsyacc") // **input**
    let text = File.ReadAllText(filePath)

    //let rawFsyacc = RawFsyaccFile.parse text
    //let fsyacc = FlatFsyaccFile.fromRaw rawFsyacc

    let name = "ESTreeParseTable" // **input**
    let parseTblModule = $"ESTreeParser.{name}"// **input**
    let modulePath = Path.Combine(projPath, "ESTreeParseTable.fs")

    let grammar text =
        text
        |> FlatFsyaccFileUtils.parse
        |> FlatFsyaccFileUtils.toGrammar

    let ambiguousCollection text =
        text
        |> FlatFsyaccFileUtils.parse
        |> FlatFsyaccFileUtils.toAmbiguousCollection

    //解析表数据
    let parseTbl text = 
        text
        |> FlatFsyaccFileUtils.parse
        |> FlatFsyaccFileUtils.toFsyaccParseTableFile

    [<Fact()>] //Skip="once and for all!"
    member _.``5 = generate parsing table``() =
        let parseTbl = parseTbl text
        let fsharpCode = parseTbl.generateModule(parseTblModule)
        File.WriteAllText(modulePath,fsharpCode,System.Text.Encoding.UTF8)
        output.WriteLine("module path:"+modulePath)

    [<Fact>]
    member _.``9 - valid ParseTable``() =
        let src =  parseTbl text

        Should.equal src.actions ESTreeParseTable.actions
        Should.equal src.closures ESTreeParseTable.closures

        let headerFromFsyacc =
            FSharp.Compiler.SyntaxTreeX.Parser.getDecls("header.fsx",src.header)

        let semansFsyacc =
            let mappers = src.generateMappers()
            FSharp.Compiler.SyntaxTreeX.SourceCodeParser.semansFromMappers mappers

        let header,semans =
            File.ReadAllText(modulePath, Encoding.UTF8)
            |> FSharp.Compiler.SyntaxTreeX.SourceCodeParser.getHeaderSemansFromFSharp 2

        Should.equal headerFromFsyacc header
        Should.equal semansFsyacc semans

