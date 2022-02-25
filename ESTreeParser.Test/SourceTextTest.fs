namespace ESTreeParser

open Xunit
open Xunit.Abstractions

open FSharp.Literals
open FSharp.xUnit

type SourceTextTest(output:ITestOutputHelper) =
    let show res =
        res
        |> Literal.stringify
        |> output.WriteLine
            
    [<Fact>]
    member _.``1 = isDeclar test``() =
        let x = "interface SequenceExpression <: Expression {"
        let y = SourceText.isDeclar x
        show y
        Should.equal y 
        <| true
        
