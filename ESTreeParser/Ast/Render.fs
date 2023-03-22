module ESTreeParser.Ast.Render
open System
open FSharp.Idioms
open FSharp.Idioms.StringOps 
let rec renderAnnotation (annot:Annotation) =
    match annot with
    | StringLiteral x -> JsonString.quote x
    | NamedType x -> x
    | Union ls -> ls |> List.map renderAnnotation |> String.concat "|"
    | Array x -> $"[{renderAnnotation x}]"
    | Object ls -> 
        let ms =
            ls
            |> List.map(fun(n,a)-> $"{n}:{renderAnnotation a};")
            |> String.concat ""
        sprintf "{%s}" ms

let renderEnum (e,n,ms) =
    let e = if e then "extend " else ""
    let ms =
        ms
        |> List.map(JsonString.quote )
        |> String.concat "|"
    let body = sprintf "{\r\n%s%s\r\n}" (space 4) ms
    $"{e}enum {n} {body}"

let renderInterface (e,n,i:string list,ms:list<string*Annotation>) =
    let e = if e then "extend " else ""
    let i = if i.IsEmpty then "" else sprintf "<: %s " (String.concat ", " i)
    let ms =
        ms
        |> List.map(fun(n,a)-> $"{space4 1}{n}:{renderAnnotation a};")
        |> String.concat "\r\n"
    let body = sprintf "{\r\n%s\r\n}" ms
    $"{e}interface {n} {i}{body}"

let renderDefinition definition =
    match definition with
    | Interface (e,n,i,m) -> renderInterface(e,n,i,m)
    | Enum (e,n,m) -> renderEnum (e,n,m)
