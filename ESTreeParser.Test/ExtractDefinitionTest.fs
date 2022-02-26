namespace ESTreeParser

open Xunit
open Xunit.Abstractions

open System
open System.IO
open System.Threading.Tasks
open System.Reactive.Linq
open FSharp.Control.Tasks.V2

type ExtractDefinitionTest(output:ITestOutputHelper) =
    [<Fact>]
    member this.``path test``() =
        output.WriteLine(nameof PathUtils.estreePath)
        output.WriteLine(PathUtils.estreePath)

        output.WriteLine(nameof PathUtils.codesPath)
        output.WriteLine(PathUtils.codesPath)

    [<Fact(Skip="once")>]//
    member _.``1 = write tscodes Async``() =
        let source = PathUtils.estreePath
        let target = PathUtils.codesPath

        //删除目标目录下所有文件
        Directory.GetFiles(target)
        |> Array.iter(File.Delete)

        let tcs = TaskCompletionSource<string>()
        let observable =
            DataSource.files
                .ToObservable()
                .Select(fun md ->
                    task {
                        let filePath = Path.Combine(source,$"es{md}.md")
                        if File.Exists filePath then
                            let! lines = File.ReadAllLinesAsync(filePath)
                            let code = Parser.extractDefinitions lines
                            let targetFileName =                                 
                                Path.Combine(target,$"es{md}.estree")
                            do! File.WriteAllTextAsync(targetFileName,code)
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
                
