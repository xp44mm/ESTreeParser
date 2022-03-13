module ESTreeParser.DefinitionExtension
open ESTreeParser.Ast
open FSharp.Literals
open FSharp.Idioms

let intersect ls1 ls2 =
    Set.intersect (set ls1) (set ls2)

// 合并定义前，需要测试错误
let mergeError (baseAst:Definition list) (newAst:Definition list) =
    baseAst @ newAst
    |> List.groupBy(fun df -> df.name)
    |> List.map snd
    |> List.filter(fun group ->
        let hasError =
            match group with
            | [x] -> x.extend
            | [x;y] ->
                match x,y with
                | Interface(false,_,i1,b1),
                  Interface(true ,_,i2,b2) -> 
                    let inheritErr() = 
                        let ins = intersect i1 i2
                        not ins.IsEmpty
                    let bodyErr() = 
                        let ins = intersect b1 b2
                        not ins.IsEmpty
                    inheritErr() || bodyErr()
                | Enum(false,_,b1),
                  Enum(true ,_,b2) ->
                    let i = intersect b1 b2
                    not i.IsEmpty
                | _ -> true
            | _ -> true
        hasError
    )

// 合并已经排除错误的AST
let merge (baseAst:Definition list) (newAst:Definition list) =
    baseAst @ newAst
    |> List.groupBy(fun df -> df.name)
    |> List.map(fun (nm,group) ->
        match group with
        | [x] -> x
        | [Interface(_,_,i1,b1);
           Interface(_,_,i2,b2)] ->
            let i = i1 @ i2 |> List.distinct
            let b = 
                b1 @ b2
                |> List.groupBy fst
                |> List.map(snd>>List.last)
            Interface(false,nm,i,b)

        | [Enum(_,_,b1);
           Enum(_,_,b2)] -> 
                let b = b1 @ b2 |> List.distinct
                Enum(false,nm,b)
        | _ -> failwith ""
    )

//let key = function
//    | Enum _ -> 0
//    | Interface (e,n,i,m) -> 
//        if i.IsEmpty && m.IsEmpty then 1
//        elif i.IsEmpty then 2
//        elif m.IsEmpty then 3
//        else 4

let sortDefinitions (definitions:Definition list) =
    let enums,interfaces =
        definitions
        |> List.partition(function
        | Enum _ -> true
        | Interface _ -> false)
    //let enums = enums |> List.sort
    enums @ interfaces


// 继承关系
let inherits (definitions:Definition list) =
    definitions
    |> List.collect(function
        | Enum _ -> []
        | Interface (_,nm,bs,m) -> 
            bs 
            |> List.map(fun b -> nm,b)
    )

let primitives = set ["string";"boolean";"null";"number";"RegExp";"bigint";"true";"false"]

//包含依赖关系
let rec getNamedTypes (annot:Annotation) =
    [
        match annot with
        | StringLiteral _ -> ()
        | NamedType x -> 
            if primitives.Contains x then
                ()
            else yield x

        | Union ls -> yield! ls |> List.collect getNamedTypes 
        | Array x -> yield! getNamedTypes x
        | Object ls -> yield! ls |> List.collect (snd>>getNamedTypes)
    ]
