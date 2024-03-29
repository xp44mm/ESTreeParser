﻿namespace ESTreeParser

open FslexFsyacc.Fslex

open System.IO
open System.Text

open Xunit
open Xunit.Abstractions

open FSharp.Literals
open FSharp.xUnit

type FenceDFATest(output:ITestOutputHelper) =
    let show res =
        res
        |> Literal.stringify
        |> output.WriteLine

    let filePath = Path.Combine(PathUtils.estreeParserPath, @"fence.fslex") // **input**
    let text = File.ReadAllText(filePath)
    let fslex = FslexFile.parse text
        
    //[<Fact>]
    //member _.``01 - compiler test``() =
    //    let hdr,dfs,rls = FslexCompiler.parseToStructuralData text
    //    show hdr
    //    show dfs
    //    show rls
        
    [<Fact>]
    member _.``02 - verify``() =
        let y = fslex.verify()

        Assert.True(y.undeclared.IsEmpty)
        Assert.True(y.unused.IsEmpty)

    [<Fact>]
    member _.``03 - universal characters``() =
        let res = fslex.getRegularExpressions()

        let y = 
            res
            |> List.collect(fun re -> re.getCharacters())
            |> Set.ofList

        show y

    [<Fact()>] // Skip="once and for all!"
    member _.``04 - generate DFA``() =

        let name = "FenceDFA" // **input**
        let moduleName = $"ESTreeParser.{name}" // **namespace**

        let y = fslex.toFslexDFAFile()
        let result = y.generate(moduleName)

        let outputDir = Path.Combine(PathUtils.estreeParserPath, $"{name}.fs")
        File.WriteAllText(outputDir, result,System.Text.Encoding.UTF8)
        output.WriteLine("dfa output path:" + outputDir)

    [<Fact>]
    member _.``10 - valid DFA``() =
        let src = fslex.toFslexDFAFile()
        Should.equal src.nextStates FenceDFA.nextStates

        let headerFslex =
            FSharp.Compiler.SyntaxTreeX.Parser.getDecls("header.fsx",src.header)

        let semansFslex =
            let mappers = src.generateMappers()
            FSharp.Compiler.SyntaxTreeX.SourceCodeParser.semansFromMappers mappers

        let header,semans =
            let filePath = Path.Combine(PathUtils.estreeParserPath, "FenceDFA.fs")
            File.ReadAllText(filePath, Encoding.UTF8)
            |> FSharp.Compiler.SyntaxTreeX.SourceCodeParser.getHeaderSemansFromFSharp 1

        Should.equal headerFslex header
        Should.equal semansFslex semans


