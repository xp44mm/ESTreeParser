module ESTreeParser.FenceDFA
let header = "// **fence.fslex**\r\nopen ESTreeParser\r\nopen ESTreeParser.FenceUtils\r\ntype token = FenceToken"
let nextStates = [|0u,[|"FENCE",1u;"FENCESTART",1u;"LINE",3u|];1u,[|"FENCE",2u;"LINE",1u|]|]
let rules:(uint32[]*uint32[]*string)[] = [|[|2u|],[||],"getDeclar lexbuf";[|3u|],[||],"[]"|]
// **fence.fslex**
open ESTreeParser
open ESTreeParser.FenceUtils
type token = FenceToken
let fxRules:(uint32[]*uint32[]*_)[] = [|
    [|2u|],[||],fun (lexbuf:token list) ->
        getDeclar lexbuf
    [|3u|],[||],fun (lexbuf:token list) ->
        []
|]
open FslexFsyacc.Runtime
let analyzer = Analyzer(nextStates, fxRules)
let analyze (tokens:seq<_>) = 
    analyzer.analyze(tokens,getTag)