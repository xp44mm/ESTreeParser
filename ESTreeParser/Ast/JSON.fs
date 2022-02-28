module ESTreeParser.Ast.JSON

open System
open FSharp.Idioms
open UnquotedJson

let tryReadAnnotation(ty:Type) (value:obj) =
    if ty = typeof<Annotation> then
        fun (read:Type->obj->_) ->
            match value :?> Annotation with
            | StringLiteral x ->
                Quotation.quote x |> JsonValue.String
            | NamedType x ->
                x |> JsonValue.String
            | Union x ->
                let jv = read (x.GetType()) x
                JsonValue.Object["union",jv]
            | Array x ->
                let jv = read (x.GetType()) x
                JsonValue.Object["array",jv]
            | Object x ->
                let jv = read (x.GetType()) x
                JsonValue.Object["object",jv]
        |> Some
    else None

let tryReadDefinition(ty:Type) (value:obj) =
    if ty = typeof<Definition> then
        fun (read:Type->obj->_) ->
            match value :?> Definition with
            | Interface (e,n,i,m) ->
                JsonValue.Object [
                    "name", JsonValue.String n
                    "extend", if e then JsonValue.True else JsonValue.False
                    "interface",JsonValue.Object[
                        "inherit", read (i.GetType()) i
                        "body",read (m.GetType()) m
                    ]
                ]
            | Enum (e,n,m) ->
                JsonValue.Object [
                    "name", JsonValue.String n
                    "extend", if e then JsonValue.True else JsonValue.False
                    "enum", read (m.GetType()) m
                ]
        |> Some
    else None

open UnquotedJson.Converters.FSharpConverter

let tryReaders =
    tryReadAnnotation ::
    tryReadDefinition ::
    tryReaders

let readDynamic (ty:Type) (value:obj) = readObj tryReaders ty value

/// convert from value to json
let read<'t> (value:'t) = readDynamic typeof<'t> value
