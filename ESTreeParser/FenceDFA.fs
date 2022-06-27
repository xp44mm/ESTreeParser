module ESTreeParser.FenceDFA
let nextStates = [|0u,[|"FENCE",1u;"FENCESTART",1u;"LINE",3u|];1u,[|"FENCE",2u;"LINE",1u|]|]
// **fence.fslex**
open FslexFsyacc.Runtime
open ESTreeParser
open ESTreeParser.FenceUtils
type token = FenceToken
let rules:(uint32[]*uint32[]*_)[] = [|
    [|2u|],[||],fun (lexbuf:token list) ->
        getDeclar lexbuf
    [|3u|],[||],fun (lexbuf:token list) ->
        []
|]
let analyzer = Analyzer(nextStates, rules)
let analyze (tokens:seq<_>) = 
    analyzer.analyze(tokens,getTag)