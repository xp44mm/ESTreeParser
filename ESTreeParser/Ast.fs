namespace ESTreeParser.Ast

type Annotation =
    | StringLiteral of string
    | NamedType of string
    | Union of Annotation list
    | Array of Annotation
    | Object of list<string*Annotation>

type Definition =
    | Interface of bool*string*list<string>*list<string*Annotation>
    | Enum of bool*string*list<string>

    member this.extend =
        match this with
        | Interface (extend,_,_,_)
        | Enum (extend,_,_)
            -> extend

    member this.name =
        match this with
        | Interface (_,name,_,_)
        | Enum (_,name,_)
            -> name

