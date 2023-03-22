module ESTreeParser.ESTreeTokenUtils
open FSharp.Idioms
open System.Text.RegularExpressions
open SourceText
open FSharp.Idioms.ActivePatterns
let tokenize inp =
    let rec loop (inp:string) =
        seq {
            match inp with
            | "" -> ()
            | On tryWhiteSpace m ->
                let rest = inp.[m.Length..]
                yield! loop rest

            | On trySingleLineComment  m ->
                let rest = inp.[m.Length..]
                yield! loop rest

            | On tryMultiLineComment  m ->
                let rest = inp.[m.Length..]
                yield! loop rest

            | On tryIdentifier  m ->
                let rest = inp.[m.Length..]
                let x = m.Value
                yield match x with
                        | "extend" -> EXTEND
                        | "interface" -> INTERFACE
                        | "enum" -> ENUM
                        | _ -> ID x

                yield! loop rest

            | On tryDoubleStringLiteral m ->
                let rest = inp.[m.Length..]
                let x = m.Value
                yield QUOTED(JsonString.unquote x)
                yield! loop rest

            | _ when inp.[0] = ',' ->
                yield COMMA
                yield! loop inp.[1..]

            | _ when inp.[0] = ':' ->
                yield COLON
                yield! loop inp.[1..]


            | _ when inp.[0] =  '|' ->
                yield BAR
                yield! loop inp.[1..]

            | _ when inp.[0] = ';' ->
                yield SEMICOLON
                yield! loop inp.[1..]

            | _ when inp.[0] =  '[' ->
                yield LBRACK
                yield! loop inp.[1..]

            | _ when inp.[0] =  ']' ->
                yield RBRACK
                yield! loop inp.[1..]

            | _ when inp.[0] = '{' ->
                yield LBRACE
                yield! loop inp.[1..]

            | _ when inp.[0] = '}' ->
                yield RBRACE
                yield! loop inp.[1..]

            | _ when inp.StartsWith("<:") ->
                yield INHERIT
                yield! loop inp.[2..]

            | never -> failwith never
        }
    
    loop inp

let getTag = function
    | QUOTED _  -> "QUOTED"
    | ID _      -> "ID"
    | EXTEND    -> "EXTEND"
    | INTERFACE -> "INTERFACE"
    | ENUM      -> "ENUM"
    | INHERIT   -> "<:"
    | COMMA     -> ","
    | COLON     -> ":"
    | BAR       -> "|"
    | SEMICOLON -> ";"
    | LBRACK    -> "["
    | RBRACK    -> "]"
    | LBRACE    -> "{"
    | RBRACE    -> "}"

let getLexeme = function
    | QUOTED x -> box x
    | ID     x -> box x
    | _        -> null
