module ESTreeParser.FenceDFA
let nextStates = [0u,["FENCE",1u;"FENCESTART",1u;"LINE",3u];1u,["FENCE",2u;"LINE",1u]]
// **fence.fslex**
open FslexFsyacc.Runtime
open ESTreeParser
open ESTreeParser.FenceUtils
type token = FenceToken
let rules:list<uint32 list*uint32 list*(list<token>->_)> = [
    [2u],[],fun (lexbuf:list<_>) ->
        getDeclar lexbuf
    [3u],[],fun (lexbuf:list<_>) ->
        []
]
let analyzer = Analyzer<_,_>(nextStates, rules)