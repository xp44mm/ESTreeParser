module ESTreeParser.Parser
open FslexFsyacc.Runtime
open ESTreeParser
open ESTreeParser.ESTreeTokenUtils
open ESTreeParser.Ast

let analyze (tokens:seq<_>) = 
    FenceDFA.analyzer.analyze(tokens, FenceUtils.getTag)

/// extract all of type definitions from a md file
let extractDefinitions linesMd =
    let lines = 
        linesMd
        |> Array.map FenceUtils.tokenize
    
    analyze lines
    |> Seq.concat
    |> String.concat "\r\n"

let parser = 
    Parser<_>(
        ESTreeParseTable.rules,
        ESTreeParseTable.actions,
        ESTreeParseTable.closures,

        ESTreeTokenUtils.getTag,
        ESTreeTokenUtils.getLexeme)

let parse txt =
    txt
    |> ESTreeTokenUtils.tokenize
    |> parser.parse
    |> ESTreeParseTable.unboxRoot
