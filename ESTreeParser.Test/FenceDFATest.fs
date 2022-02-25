namespace ESTreeParser

open FslexFsyacc.Fslex

open System.IO

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
        
    [<Fact>]
    member _.``0 = compiler test``() =
        let hdr,dfs,rls = FslexCompiler.parseToStructuralData text
        show hdr
        show dfs
        show rls
        
    [<Fact>]
    member _.``1 = verify``() =
        let y = fslex.verify()

        Assert.True(y.undeclared.IsEmpty)
        Assert.True(y.unused.IsEmpty)

    [<Fact>]
    member _.``2 = universal characters``() =
        let res = fslex.getRegularExpressions()

        let y = 
            res
            |> Array.collect(fun re -> re.getCharacters())
            |> Set.ofArray

        show y

    [<Fact(Skip="once and for all!")>] //
    member _.``3 = generate DFA``() =

        let name = "FenceDFA" // **input**
        let moduleName = $"ESTreeParser.{name}" // **namespace**

        let y = fslex.toFslexDFAFile()
        let result = y.generate(moduleName)

        let outputDir = Path.Combine(PathUtils.estreeParserPath, $"{name}.fs")
        File.WriteAllText(outputDir, result)
        output.WriteLine("dfa output path:" + outputDir)

    [<Fact>]
    member _.``4 = valid DFA``() =
        let y = fslex.toFslexDFAFile()

        Should.equal y.nextStates FenceDFA.nextStates
        Should.equal y.header     FenceDFA.header
        Should.equal y.rules      FenceDFA.rules

