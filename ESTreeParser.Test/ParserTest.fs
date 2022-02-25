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

    let ess = [|
        "es5.md"
        "es2015.md"
        "es2016.md"
        "es2017.md"
        "es2018.md"
        "es2019.md"
        "es2020.md"
        "es2021.md"
        "es2022.md"
    |]

    let source = PathUtils.estreePath

    //[<Fact(Skip="once")>]
    member _.``1 = write tscodes Async``() =
        let target = Path.Combine(__SOURCE_DIRECTORY__, "tscodes")

        //删除目标目录下所有文件
        Directory.GetFiles(target)
        |> Array.iter(File.Delete)

        let tcs = TaskCompletionSource<string>();
        let observable =
            ess.ToObservable()
                .Select(fun md ->
                    task {
                        let filePath = Path.Combine(source,md)
                        if File.Exists filePath then
                            let! lines = File.ReadAllLinesAsync(filePath)
                            let ts = Parser.extractDefinitions lines

                            let targetFileName = 
                                Path.GetFileNameWithoutExtension(md) + ".ts"
                                |> fun file -> Path.Combine(target,file)

                            do! File.WriteAllTextAsync(targetFileName,ts)
                    }
                )
                .Merge()

        observable.Subscribe({
            new IObserver<unit> with
                member this.OnNext _ = ()
                member this.OnError _ = ()
                member this.OnCompleted() = 
                    output.WriteLine("done!")
                    tcs.SetResult("done!")

            })
        |> ignore
        tcs.Task

        
    [<Fact>]
    member _.``2 = parser``() =
        let source = PathUtils.tscodesPath
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
