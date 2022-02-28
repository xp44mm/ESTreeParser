module ESTreeParser.Program
open ESTreeParser.Ast

let e1 = [
    [Interface(false,"Literal",["Expression"],["type",StringLiteral "Literal";"value",Union [NamedType "string";NamedType "boolean";NamedType "null";NamedType "number";NamedType "RegExp"]]);
     Interface(true,"Literal",["Expression"],["type",StringLiteral "Literal";"value",Union [NamedType "string";NamedType "boolean";NamedType "null";NamedType "number";NamedType "RegExp";NamedType "bigint"]])];[
    Enum(false,"LogicalOperator",["||";"&&"]);
    Enum(true,"LogicalOperator",["||";"&&";"??"])]]

let e2 = [
    [Interface(false,"BinaryExpression",["Expression"],["type",StringLiteral "BinaryExpression";"operator",NamedType "BinaryOperator";"left",NamedType "Expression";"right",NamedType "Expression"]);
    Interface(true,"BinaryExpression",["Expression"],["left",Union [NamedType "Expression";NamedType "PrivateIdentifier"]])
    ];[
    Interface(false,"ImportSpecifier",["ModuleSpecifier"],["type",StringLiteral "ImportSpecifier";"imported",NamedType "Identifier"]);
    Interface(true,"ImportSpecifier",["ModuleSpecifier"],["imported",Union [NamedType "Identifier";NamedType "Literal"]])
    ];[
    Interface(false,"ExportSpecifier",["ModuleSpecifier"],["type",StringLiteral "ExportSpecifier";"exported",NamedType "Identifier"]);
    Interface(true,"ExportSpecifier",["ModuleSpecifier"],["local",Union [NamedType "Identifier";NamedType "Literal"];"exported",Union [NamedType "Identifier";NamedType "Literal"]])]]

let [<EntryPoint>] main _ = 
    0
