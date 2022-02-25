module ESTreeParser.FenceUtils
open System.Text.RegularExpressions

let fenceRgx = Regex @"^\s*`{3,}(?<lang>[^`]*)$"

let tokenize (line:string) =
    let mat = fenceRgx.Match(line.TrimEnd())
    if mat.Success then
        let lang = mat.Groups.["lang"].Value
        if lang = "" then
            FENCE
        else
            FENCESTART lang
    else
        LINE line

let getTag (token:FenceToken) =
    match token with
    | FENCESTART _ -> "FENCESTART"
    | FENCE -> "FENCE"
    | LINE _ -> "LINE"

let getDeclar (lexbuf:FenceToken list) =
    let rec butLast acc ls =
        match ls with
        | [] | [_] -> acc
        | h::t -> butLast (h::acc) t

    let lines = 
        butLast [] lexbuf.Tail
        |> List.map(function LINE x -> x | _ -> "") 

    if lines |> List.exists(SourceText.isDeclar) then
        lines 
        |> List.rev 
    else []
