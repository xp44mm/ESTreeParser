namespace ESTreeParser

open Xunit
open Xunit.Abstractions

open System.IO
open FSharp.Literals
open ESTreeParser.Ast

type DefinitionFileTest(output:ITestOutputHelper) =
    let show res =
        res
        |> Literal.stringify
        |> output.WriteLine

    let getDefinition path file =
        let filePath = Path.Combine(path,file)
        let text = 
            try
                File.ReadAllText(filePath)
            with :? FileNotFoundException as e -> 
                failwith e.Message
        Parser.parse text
        
    let getBasDefs file =
        getDefinition PathUtils.extendedPath file 

    let getExtDefs file =
        getDefinition PathUtils.codesPath file 

    [<Theory>]
    [<MemberData(nameof DataSource.filesForMemberData, MemberType=typeof<DataSource>)>]
    member _.``1 = no duplication``(version) =
        let definitions = 
            getDefinition PathUtils.codesPath $"es{version}.estree"

        let duplications =
            definitions
            |> List.groupBy(fun d -> d.name)
            |> List.filter(fun(nm,ls)-> not ls.Tail.IsEmpty)
        // 不能重名
        Assert.Empty(duplications)

    [<Fact>]
    member _.``2 = es5 for all is extended ``() =
        let definitions = 
            getDefinition PathUtils.codesPath "es5.estree"

        let extendings =
            definitions
            |> List.filter(fun d -> d.extend)

        // 不能有extend=true的定义
        Assert.Empty(extendings)

    [<Fact>]
    member _.``3 = verify base es5 before extending``() =
        let basDefs = []
        let extDefs = getExtDefs "es5.estree"
        let errors = DefinitionExtension.mergeError basDefs extDefs
        // 不能有错误
        Assert.Empty(errors)

    [<Fact>]
    member _.``4 = generate extended es5``() =
        let newfile = "es5.estree"
        let basDefs = []
        let extDefs = getExtDefs newfile

        let newEs = DefinitionExtension.merge basDefs extDefs
        let y =
            newEs
            |> DefinitionExtension.sortDefinitions
            |> List.map Render.renderDefinition
            |> String.concat "\r\n"
        let outputPath = Path.Combine(PathUtils.extendedPath, newfile)
        //output.WriteLine(y)
        File.WriteAllText(outputPath,y,System.Text.Encoding.UTF8)

    [<Theory>]
    [<MemberData(nameof DataSource.pairwiseFiles, MemberType=typeof<DataSource>)>]
    member _.``5 = mergeError``(bas,ext) =
        let basDefs = getBasDefs bas
        let extDefs = getExtDefs ext

        let errors = DefinitionExtension.mergeError basDefs extDefs
        show errors
        // 不能有错误
        Assert.Empty(errors)

    [<Theory>]
    [<MemberData(nameof DataSource.pairwiseFiles, MemberType=typeof<DataSource>)>]
    member _.``6 = generate extended estree``(bas,ext) =
        let basDefs = getBasDefs bas
        let extDefs = getExtDefs ext

        let newEs = DefinitionExtension.merge basDefs extDefs
        let y =
            newEs
            |> DefinitionExtension.sortDefinitions
            |> List.map Render.renderDefinition
            |> String.concat "\r\n"
        let outputPath = Path.Combine(PathUtils.extendedPath, ext)
        if File.Exists(outputPath) && y = File.ReadAllText(outputPath) then
            output.WriteLine("new same as old!")
        else
            File.WriteAllText(outputPath,y,System.Text.Encoding.UTF8)

