namespace ESTreeParser

open Xunit
open Xunit.Abstractions

open FSharp.Literals
open FSharp.xUnit

type FenceUtilsTest(output:ITestOutputHelper) =
    let show res =
        res
        |> Literal.stringify
        |> output.WriteLine
            
    [<Fact>]
    member _.``1 = tokenize FENCESTART test``() =
        let x = "  ```ts  "
        let y = FenceUtils.tokenize x
        show y
        Should.equal y 
        <| FENCESTART "ts"
        
    [<Fact>]
    member _.``2 = tokenize test``() =
        let x = "  ```  "
        let y = FenceUtils.tokenize x
        show y
        Should.equal y 
        <| FENCE

    [<Fact>]
    member _.``3 = tokenize Line test``() =
        let x = "line"
        let y = FenceUtils.tokenize x
        show y
        Should.equal y 
        <| LINE x
