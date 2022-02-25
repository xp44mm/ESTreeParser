module ESTreeParser.Parser

/// extract all of type definitions from a md file
let extractDefinitions linesMd =
    let lines = 
        linesMd
        |> Array.map FenceUtils.tokenize
    
    FenceDFA.analyze lines
    |> Seq.concat
    |> String.concat "\r\n"

let parse txt =
    txt
    |> ESTreeTokenUtils.tokenize
    |> ESTreeParseTable.parse
