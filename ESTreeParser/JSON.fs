module ESTreeParser.JSON
open ESTreeParser.Ast

open System
open UnquotedJson
open UnquotedJson.Converters.FSharpConverter

open FSharp.Idioms

let tryReadAnnotation(ty:Type) (value:obj) = 
    if ty = typeof<Annotation> then
        fun (loopRead:Type->obj->_) -> 
            match value :?> Annotation with
            | StringLiteral x -> 
                Quotation.quote x |> JsonValue.String
            | NamedType x -> 
                x |> JsonValue.String
            | Union x -> 
                let jv = loopRead (x.GetType()) x
                JsonValue.Object["union",jv]
            | Array x ->
                let jv = loopRead (x.GetType()) x
                JsonValue.Object["array",jv]
            | Object x ->
                let jv = loopRead (x.GetType()) x
                JsonValue.Object["object",jv]
        |> Some
    else None

let tryReadDefinition(ty:Type) (value:obj) = 
    if ty = typeof<Definition> then
        fun (loopRead:Type->obj->_) -> 
            match value :?> Definition with
            | Interface (e,n,i,m) ->
                JsonValue.Object [
                    "name", JsonValue.String n
                    "extend", if e then JsonValue.True else JsonValue.False
                    "interface",JsonValue.Object[
                        "inherit", loopRead (i.GetType()) i
                        "body",loopRead (m.GetType()) m
                    ]
                ]
            | Enum (e,n,m) ->
                JsonValue.Object [
                    "name", JsonValue.String n
                    "extend", if e then JsonValue.True else JsonValue.False
                    "enum", loopRead (m.GetType()) m
                ]
        |> Some
    else None

let tryReaders = 
    tryReadAnnotation :: 
    tryReadDefinition :: 
    tryReaders

let readDynamic (ty:Type) (value:obj) = readObj tryReaders ty value

/// convert from value to json
let read<'t> (value:'t) = readDynamic typeof<'t> value
