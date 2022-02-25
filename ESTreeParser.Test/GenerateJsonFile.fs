namespace ESTreeParser

open Xunit
open Xunit.Abstractions

open System
open System.IO
open System.Threading.Tasks
open System.Reactive.Linq
open FSharp.Control.Tasks.V2

type GenerateJsonFile(output:ITestOutputHelper) =
    let source = PathUtils.codesPath
    let target = PathUtils.jsonsPath

    [<Fact>]
    member this.``path test``() =
        output.WriteLine(nameof PathUtils.codesPath)
        output.WriteLine(PathUtils.codesPath)

        output.WriteLine(nameof PathUtils.jsonsPath)
        output.WriteLine(PathUtils.jsonsPath)

    [<Fact>]
    member this.``GetFiles test``() =
        for file in Directory.GetFiles(source) do
            output.WriteLine(file)

    [<Fact>]//(Skip="once")
    member _.``1 = write tscodes Async``() =

        //删除目标目录下所有文件
        Directory.GetFiles(target)
        |> Array.iter(File.Delete)

        let tcs = TaskCompletionSource<string>()
        let observable =
            Directory.GetFiles(source)
                .ToObservable()
                .Select(fun sfile ->
                    task {
                        let! text = File.ReadAllTextAsync(sfile)
                        let json =
                            text
                            |> Parser.parse
                            |> JSON.read
                            |> UnquotedJson.JSON.stringifyNormalJson

                        let tfile =                                 
                            Path.GetFileNameWithoutExtension(sfile)
                            |> fun name -> Path.Combine(target,$"{name}.json")
                        do! File.WriteAllTextAsync(tfile,json)
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
                
