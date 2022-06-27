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
        else Quotation.quote sym

    let projPath = PathUtils.estreeParserPath
    let filePath = Path.Combine(projPath, "estree.fsyacc") // **input**
    let text = File.ReadAllText(filePath)
    let rawFsyacc = FsyaccFile.parse text
    let fsyacc = NormFsyaccFile.fromRaw rawFsyacc

    [<Fact>]
    member _.``1 = 显示冲突状态的冲突项目``() =
        let collection =
            AmbiguousCollection.create <| fsyacc.getMainProductions()
        let conflicts =
            collection.filterConflictedClosures()
        show conflicts
        let y = Map[
            7,Map["|",set[
                {production=["annotation";"annotation";"|";"annotation"];dot=1};
                {production=["annotation";"annotation";"|";"annotation"];dot=3}]]]

        Should.equal y conflicts

    [<Fact>]
    member _.``2 = 汇总冲突的产生式``() =
        let collection =
            AmbiguousCollection.create <| fsyacc.getMainProductions()
        let conflicts =
            collection.filterConflictedClosures()

        let productions =
            AmbiguousCollection.gatherProductions conflicts
        // production -> %prec
        let pprods =
            ProductionUtils.precedenceOfProductions collection.grammar.terminals productions
            |> List.ofArray

        //优先级应该据此结果给出，不能少，也不应该多。
        let y = [["annotation";"annotation";"|";"annotation"],"|"]

        Should.equal y pprods

    [<Fact>]
    member _.``3 = list all tokens``() =
        let grammar = Grammar.from <| fsyacc.getMainProductions()
        let tokens =
            grammar.symbols - grammar.nonterminals
        show tokens


    [<Fact>]
    member _.``4 = print the template of type annotaitions``() =
        let grammar = Grammar.from <| fsyacc.getMainProductions()

        let symbols =
            grammar.nonterminals
            |> Set.map(symbolRender)

        let sourceCode =
            [
                for i in symbols do
                    $"{i}: \"string\""
            ] |> String.concat "\r\n"
        output.WriteLine(sourceCode)

    [<Fact(Skip="once and for all!")>] //
    member _.``5 = generate parsing table``() =
        let name = "ESTreeParseTable" // **input**
        let moduleName = $"ESTreeParser.{name}"// **input**

        let parseTbl = fsyacc.toFsyaccParseTableFile()
        let fsharpCode = parseTbl.generate(moduleName)
        let outputDir = Path.Combine(projPath, $"{name}.fs")

        File.WriteAllText(outputDir,fsharpCode,System.Text.Encoding.UTF8)
        output.WriteLine("output path:"+outputDir)

    [<Fact>]
    member _.``9 - valid ParseTable``() =
        let src = fsyacc.toFsyaccParseTableFile()

        Should.equal src.actions ESTreeParseTable.actions
        Should.equal src.closures ESTreeParseTable.closures

        let headerFromFsyacc =
            FSharp.Compiler.SyntaxTreeX.Parser.getDecls("header.fsx",src.header)

        let semansFsyacc =
            let mappers = src.generateMappers()
            FSharp.Compiler.SyntaxTreeX.SourceCodeParser.semansFromMappers mappers

        let header,semans =
            let filePath = Path.Combine(projPath, "ESTreeParseTable.fs")
            File.ReadAllText(filePath, Encoding.UTF8)
            |> FSharp.Compiler.SyntaxTreeX.SourceCodeParser.getHeaderSemansFromFSharp 2

        Should.equal headerFromFsyacc header
        Should.equal semansFsyacc semans

