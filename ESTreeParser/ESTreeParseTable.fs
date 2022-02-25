module ESTreeParser.ESTreeParseTable
let rules = [|["file";"definitions"],"List.rev s0";["definitions";"definition"],"[s0]";["definitions";"definitions";"definition"],"s1::s0";["definition";"interfaceDefinition"],"s0";["definition";"enumDefinition"],"s0";["interfaceDefinition";"interfaceHead";"interfaceBody"],"let e,n,i = s0\r\nInterface(e,n,i,s1)";["interfaceHead";"INTERFACE";"ID"],"false,s1,[]";["interfaceHead";"EXTEND";"INTERFACE";"ID"],"true ,s2,[]";["interfaceHead";"INTERFACE";"ID";"<:";"ids"],"false,s1,s3";["interfaceHead";"EXTEND";"INTERFACE";"ID";"<:";"ids"],"true ,s2,s4";["ids";"ID"],"[s0]";["ids";"ids";",";"ID"],"s2::s0";["interfaceBody";"{";"}"],"[]";["interfaceBody";"{";"fields";"}"],"List.rev s1";["fields";"field"],"[s0]";["fields";"fields";"field"],"s1::s0";["field";"ID";":";"annotation";";"],"s0,s2";["annotation";"QUOTED"],"StringLiteral s0";["annotation";"ID"],"NamedType s0";["annotation";"annotation";"|";"annotation"],"match s0 with\r\n| Union ls -> Union(ls @ [s2])\r\n| _ -> Union [s0;s2]";["annotation";"[";"annotation";"]"],"Array s1";["annotation";"interfaceBody"],"Object s0";["enumDefinition";"enumHead";"enumBody"],"let e,n = s0\r\nEnum(e,n,s1)";["enumHead";"ENUM";"ID"],"false,s1";["enumHead";"EXTEND";"ENUM";"ID"],"true ,s2";["enumBody";"{";"}"],"[]";["enumBody";"{";"cases";"}"],"List.rev s1";["cases";"QUOTED"],"[s0]";["cases";"cases";"|";"QUOTED"],"s2::s0"|]
let actions = [|[|"ENUM",25;"EXTEND",27;"INTERFACE",49;"definition",17;"definitions",18;"enumDefinition",15;"enumHead",23;"file",1;"interfaceDefinition",16;"interfaceHead",44|];[|"",0|];[|";",-1;"]",-1;"|",-1|];[|";",-2;"]",-2;"|",-2|];[|"ID",2;"QUOTED",3;"[",4;"annotation",5;"interfaceBody",10;"{",41|];[|"]",6;"|",9|];[|";",-3;"]",-3;"|",-3|];[|";",-4;"]",-4;"|",-4|];[|";",32;"|",9|];[|"ID",2;"QUOTED",3;"[",4;"annotation",7;"interfaceBody",10;"{",41|];[|";",-5;"]",-5;"|",-5|];[|"|",-6;"}",-6|];[|"|",13;"}",21|];[|"QUOTED",14|];[|"|",-7;"}",-7|];[|"",-8;"ENUM",-8;"EXTEND",-8;"INTERFACE",-8|];[|"",-9;"ENUM",-9;"EXTEND",-9;"INTERFACE",-9|];[|"",-10;"ENUM",-10;"EXTEND",-10;"INTERFACE",-10|];[|"",-20;"ENUM",25;"EXTEND",27;"INTERFACE",49;"definition",19;"enumDefinition",15;"enumHead",23;"interfaceDefinition",16;"interfaceHead",44|];[|"",-11;"ENUM",-11;"EXTEND",-11;"INTERFACE",-11|];[|"QUOTED",11;"cases",12;"}",22|];[|"",-12;"ENUM",-12;"EXTEND",-12;"INTERFACE",-12|];[|"",-13;"ENUM",-13;"EXTEND",-13;"INTERFACE",-13|];[|"enumBody",24;"{",20|];[|"",-14;"ENUM",-14;"EXTEND",-14;"INTERFACE",-14|];[|"ID",26|];[|"{",-15|];[|"ENUM",28;"INTERFACE",46|];[|"ID",29|];[|"{",-16|];[|":",31|];[|"ID",2;"QUOTED",3;"[",4;"annotation",8;"interfaceBody",10;"{",41|];[|"ID",-17;"}",-17|];[|"ID",-18;"}",-18|];[|"ID",30;"field",35;"}",42|];[|"ID",-19;"}",-19|];[|",",-21;"{",-21|];[|",",39;"{",-27|];[|",",39;"{",-29|];[|"ID",40|];[|",",-22;"{",-22|];[|"ID",30;"field",33;"fields",34;"}",43|];[|"",-23;";",-23;"ENUM",-23;"EXTEND",-23;"INTERFACE",-23;"]",-23;"|",-23|];[|"",-24;";",-24;"ENUM",-24;"EXTEND",-24;"INTERFACE",-24;"]",-24;"|",-24|];[|"interfaceBody",45;"{",41|];[|"",-25;"ENUM",-25;"EXTEND",-25;"INTERFACE",-25|];[|"ID",47|];[|"<:",48;"{",-26|];[|"ID",36;"ids",37|];[|"ID",50|];[|"<:",51;"{",-28|];[|"ID",36;"ids",38|]|]
let closures = [|[|0,0,[||];-8,0,[||];-9,0,[||];-10,0,[||];-11,0,[||];-14,0,[||];-15,0,[||];-16,0,[||];-20,0,[||];-25,0,[||];-26,0,[||];-27,0,[||];-28,0,[||];-29,0,[||]|];[|0,1,[|""|]|];[|-1,1,[|";";"]";"|"|]|];[|-2,1,[|";";"]";"|"|]|];[|-1,0,[||];-2,0,[||];-3,0,[||];-3,1,[||];-4,0,[||];-5,0,[||];-23,0,[||];-24,0,[||]|];[|-3,2,[||];-4,1,[||]|];[|-3,3,[|";";"]";"|"|]|];[|-4,3,[|";";"]";"|"|]|];[|-4,1,[||];-17,3,[||]|];[|-1,0,[||];-2,0,[||];-3,0,[||];-4,0,[||];-4,2,[||];-5,0,[||];-23,0,[||];-24,0,[||]|];[|-5,1,[|";";"]";"|"|]|];[|-6,1,[|"|";"}"|]|];[|-7,1,[||];-12,2,[||]|];[|-7,2,[||]|];[|-7,3,[|"|";"}"|]|];[|-8,1,[|"";"ENUM";"EXTEND";"INTERFACE"|]|];[|-9,1,[|"";"ENUM";"EXTEND";"INTERFACE"|]|];[|-10,1,[|"";"ENUM";"EXTEND";"INTERFACE"|]|];[|-8,0,[||];-9,0,[||];-11,1,[||];-14,0,[||];-15,0,[||];-16,0,[||];-20,1,[|""|];-25,0,[||];-26,0,[||];-27,0,[||];-28,0,[||];-29,0,[||]|];[|-11,2,[|"";"ENUM";"EXTEND";"INTERFACE"|]|];[|-6,0,[||];-7,0,[||];-12,1,[||];-13,1,[||]|];[|-12,3,[|"";"ENUM";"EXTEND";"INTERFACE"|]|];[|-13,2,[|"";"ENUM";"EXTEND";"INTERFACE"|]|];[|-12,0,[||];-13,0,[||];-14,1,[||]|];[|-14,2,[|"";"ENUM";"EXTEND";"INTERFACE"|]|];[|-15,1,[||]|];[|-15,2,[|"{"|]|];[|-16,1,[||];-26,1,[||];-27,1,[||]|];[|-16,2,[||]|];[|-16,3,[|"{"|]|];[|-17,1,[||]|];[|-1,0,[||];-2,0,[||];-3,0,[||];-4,0,[||];-5,0,[||];-17,2,[||];-23,0,[||];-24,0,[||]|];[|-17,4,[|"ID";"}"|]|];[|-18,1,[|"ID";"}"|]|];[|-17,0,[||];-19,1,[||];-23,2,[||]|];[|-19,2,[|"ID";"}"|]|];[|-21,1,[|",";"{"|]|];[|-22,1,[||];-27,5,[|"{"|]|];[|-22,1,[||];-29,4,[|"{"|]|];[|-22,2,[||]|];[|-22,3,[|",";"{"|]|];[|-17,0,[||];-18,0,[||];-19,0,[||];-23,1,[||];-24,1,[||]|];[|-23,3,[|"";";";"ENUM";"EXTEND";"INTERFACE";"]";"|"|]|];[|-24,2,[|"";";";"ENUM";"EXTEND";"INTERFACE";"]";"|"|]|];[|-23,0,[||];-24,0,[||];-25,1,[||]|];[|-25,2,[|"";"ENUM";"EXTEND";"INTERFACE"|]|];[|-26,2,[||];-27,2,[||]|];[|-26,3,[|"{"|];-27,3,[||]|];[|-21,0,[||];-22,0,[||];-27,4,[||]|];[|-28,1,[||];-29,1,[||]|];[|-28,2,[|"{"|];-29,2,[||]|];[|-21,0,[||];-22,0,[||];-29,3,[||]|]|]
let header = "// **estree.fsyacc**\r\nopen ESTreeParser\r\nopen ESTreeParser.ESTreeTokenUtils\r\nopen ESTreeParser.Ast"
let declarations = [|"QUOTED","string";"ID","string";"file","Definition list";"definitions","Definition list";"definition","Definition";"interfaceDefinition","Definition";"enumDefinition","Definition";"interfaceHead","bool*string*list<string>";"ids","string list";"interfaceBody","list<string*Annotation>";"fields","list<string*Annotation>";"field","string*Annotation";"annotation","Annotation";"enumHead","bool*string";"enumBody","string list";"cases","string list"|]
// **estree.fsyacc**
open ESTreeParser
open ESTreeParser.ESTreeTokenUtils
open ESTreeParser.Ast
let fxRules:(string list*(obj[]->obj))[] = [|
    ["file";"definitions"],fun (ss:obj[]) ->
            let s0 = unbox<Definition list> ss.[0]
            let result:Definition list =
                List.rev s0
            box result
    ["definitions";"definition"],fun (ss:obj[]) ->
            let s0 = unbox<Definition> ss.[0]
            let result:Definition list =
                [s0]
            box result
    ["definitions";"definitions";"definition"],fun (ss:obj[]) ->
            let s0 = unbox<Definition list> ss.[0]
            let s1 = unbox<Definition> ss.[1]
            let result:Definition list =
                s1::s0
            box result
    ["definition";"interfaceDefinition"],fun (ss:obj[]) ->
            let s0 = unbox<Definition> ss.[0]
            let result:Definition =
                s0
            box result
    ["definition";"enumDefinition"],fun (ss:obj[]) ->
            let s0 = unbox<Definition> ss.[0]
            let result:Definition =
                s0
            box result
    ["interfaceDefinition";"interfaceHead";"interfaceBody"],fun (ss:obj[]) ->
            let s0 = unbox<bool*string*list<string>> ss.[0]
            let s1 = unbox<list<string*Annotation>> ss.[1]
            let result:Definition =
                let e,n,i = s0
                Interface(e,n,i,s1)
            box result
    ["interfaceHead";"INTERFACE";"ID"],fun (ss:obj[]) ->
            let s1 = unbox<string> ss.[1]
            let result:bool*string*list<string> =
                false,s1,[]
            box result
    ["interfaceHead";"EXTEND";"INTERFACE";"ID"],fun (ss:obj[]) ->
            let s2 = unbox<string> ss.[2]
            let result:bool*string*list<string> =
                true ,s2,[]
            box result
    ["interfaceHead";"INTERFACE";"ID";"<:";"ids"],fun (ss:obj[]) ->
            let s1 = unbox<string> ss.[1]
            let s3 = unbox<string list> ss.[3]
            let result:bool*string*list<string> =
                false,s1,s3
            box result
    ["interfaceHead";"EXTEND";"INTERFACE";"ID";"<:";"ids"],fun (ss:obj[]) ->
            let s2 = unbox<string> ss.[2]
            let s4 = unbox<string list> ss.[4]
            let result:bool*string*list<string> =
                true ,s2,s4
            box result
    ["ids";"ID"],fun (ss:obj[]) ->
            let s0 = unbox<string> ss.[0]
            let result:string list =
                [s0]
            box result
    ["ids";"ids";",";"ID"],fun (ss:obj[]) ->
            let s0 = unbox<string list> ss.[0]
            let s2 = unbox<string> ss.[2]
            let result:string list =
                s2::s0
            box result
    ["interfaceBody";"{";"}"],fun (ss:obj[]) ->
            let result:list<string*Annotation> =
                []
            box result
    ["interfaceBody";"{";"fields";"}"],fun (ss:obj[]) ->
            let s1 = unbox<list<string*Annotation>> ss.[1]
            let result:list<string*Annotation> =
                List.rev s1
            box result
    ["fields";"field"],fun (ss:obj[]) ->
            let s0 = unbox<string*Annotation> ss.[0]
            let result:list<string*Annotation> =
                [s0]
            box result
    ["fields";"fields";"field"],fun (ss:obj[]) ->
            let s0 = unbox<list<string*Annotation>> ss.[0]
            let s1 = unbox<string*Annotation> ss.[1]
            let result:list<string*Annotation> =
                s1::s0
            box result
    ["field";"ID";":";"annotation";";"],fun (ss:obj[]) ->
            let s0 = unbox<string> ss.[0]
            let s2 = unbox<Annotation> ss.[2]
            let result:string*Annotation =
                s0,s2
            box result
    ["annotation";"QUOTED"],fun (ss:obj[]) ->
            let s0 = unbox<string> ss.[0]
            let result:Annotation =
                StringLiteral s0
            box result
    ["annotation";"ID"],fun (ss:obj[]) ->
            let s0 = unbox<string> ss.[0]
            let result:Annotation =
                NamedType s0
            box result
    ["annotation";"annotation";"|";"annotation"],fun (ss:obj[]) ->
            let s0 = unbox<Annotation> ss.[0]
            let s2 = unbox<Annotation> ss.[2]
            let result:Annotation =
                match s0 with
                | Union ls -> Union(ls @ [s2])
                | _ -> Union [s0;s2]
            box result
    ["annotation";"[";"annotation";"]"],fun (ss:obj[]) ->
            let s1 = unbox<Annotation> ss.[1]
            let result:Annotation =
                Array s1
            box result
    ["annotation";"interfaceBody"],fun (ss:obj[]) ->
            let s0 = unbox<list<string*Annotation>> ss.[0]
            let result:Annotation =
                Object s0
            box result
    ["enumDefinition";"enumHead";"enumBody"],fun (ss:obj[]) ->
            let s0 = unbox<bool*string> ss.[0]
            let s1 = unbox<string list> ss.[1]
            let result:Definition =
                let e,n = s0
                Enum(e,n,s1)
            box result
    ["enumHead";"ENUM";"ID"],fun (ss:obj[]) ->
            let s1 = unbox<string> ss.[1]
            let result:bool*string =
                false,s1
            box result
    ["enumHead";"EXTEND";"ENUM";"ID"],fun (ss:obj[]) ->
            let s2 = unbox<string> ss.[2]
            let result:bool*string =
                true ,s2
            box result
    ["enumBody";"{";"}"],fun (ss:obj[]) ->
            let result:string list =
                []
            box result
    ["enumBody";"{";"cases";"}"],fun (ss:obj[]) ->
            let s1 = unbox<string list> ss.[1]
            let result:string list =
                List.rev s1
            box result
    ["cases";"QUOTED"],fun (ss:obj[]) ->
            let s0 = unbox<string> ss.[0]
            let result:string list =
                [s0]
            box result
    ["cases";"cases";"|";"QUOTED"],fun (ss:obj[]) ->
            let s0 = unbox<string list> ss.[0]
            let s2 = unbox<string> ss.[2]
            let result:string list =
                s2::s0
            box result
|]
open FslexFsyacc.Runtime
let parser = Parser(fxRules, actions, closures)
let parse (tokens:seq<_>) =
    parser.parse(tokens, getTag, getLexeme)
    |> unbox<Definition list>