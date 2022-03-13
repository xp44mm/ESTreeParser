namespace ESTreeParser
open ESTreeParser.Ast

open Xunit
open Xunit.Abstractions

open System
open System.IO
open System.Threading.Tasks
open System.Reactive.Linq
open FSharp.Control.Tasks.V2

open FSharp.Idioms
open FSharp.Literals

type InterfaceTest(output:ITestOutputHelper) =
    let show res =
        res
        |> Literal.stringify
        |> output.WriteLine


    let getDefinition path file =
        let filePath = Path.Combine(path,file)
        let text =
            try
                File.ReadAllText(filePath)
            with :? FileNotFoundException as e ->
                failwith e.Message
        Parser.parse text
    let source = PathUtils.extendedPath

    let es2022 = "es2022.estree"

    let enums = ["UnaryOperator";"UpdateOperator";"BinaryOperator";"AssignmentOperator";"LogicalOperator"]
    let interfaces = ["Node";"SourceLocation";"Position";"Statement";"Declaration";"Expression";"Pattern";"ModuleDeclaration";"Identifier";"Literal";"RegExpLiteral";"Program";"Function";"ExpressionStatement";"Directive";"BlockStatement";"FunctionBody";"EmptyStatement";"DebuggerStatement";"WithStatement";"ReturnStatement";"LabeledStatement";"BreakStatement";"ContinueStatement";"IfStatement";"SwitchStatement";"SwitchCase";"ThrowStatement";"TryStatement";"CatchClause";"WhileStatement";"DoWhileStatement";"ForStatement";"ForInStatement";"FunctionDeclaration";"VariableDeclaration";"VariableDeclarator";"ThisExpression";"ArrayExpression";"ObjectExpression";"Property";"FunctionExpression";"UnaryExpression";"UpdateExpression";"BinaryExpression";"AssignmentExpression";"LogicalExpression";"MemberExpression";"ConditionalExpression";"CallExpression";"NewExpression";"SequenceExpression";"ForOfStatement";"Super";"SpreadElement";"ArrowFunctionExpression";"YieldExpression";"TemplateLiteral";"TaggedTemplateExpression";"TemplateElement";"AssignmentProperty";"ObjectPattern";"ArrayPattern";"RestElement";"AssignmentPattern";"Class";"ClassBody";"MethodDefinition";"ClassDeclaration";"ClassExpression";"MetaProperty";"ModuleSpecifier";"ImportDeclaration";"ImportSpecifier";"ImportDefaultSpecifier";"ImportNamespaceSpecifier";"ExportNamedDeclaration";"ExportSpecifier";"AnonymousDefaultExportedFunctionDeclaration";"AnonymousDefaultExportedClassDeclaration";"ExportDefaultDeclaration";"ExportAllDeclaration";"AwaitExpression";"BigIntLiteral";"ChainExpression";"ChainElement";"ImportExpression";"PropertyDefinition";"PrivateIdentifier";"StaticBlock"]
    let inherits = [
        "Node","Statement";
        "Statement","Declaration";
        "Node","Expression";
        "Node","Pattern";
        "Node","ModuleDeclaration";
        "Expression","Identifier";
        "Pattern","Identifier";
        "Expression","Literal";
        "Literal","RegExpLiteral";
        "Node","Program";
        "Node","Function";
        "Statement","ExpressionStatement";
        "ExpressionStatement","Directive";
        "Statement","BlockStatement";
        "BlockStatement","FunctionBody";
        "Statement","EmptyStatement";
        "Statement","DebuggerStatement";
        "Statement","WithStatement";
        "Statement","ReturnStatement";
        "Statement","LabeledStatement";
        "Statement","BreakStatement";
        "Statement","ContinueStatement";
        "Statement","IfStatement";
        "Statement","SwitchStatement";
        "Node","SwitchCase";
        "Statement","ThrowStatement";
        "Statement","TryStatement";
        "Node","CatchClause";
        "Statement","WhileStatement";
        "Statement","DoWhileStatement";
        "Statement","ForStatement";
        "Statement","ForInStatement";
        "Function","FunctionDeclaration";
        "Declaration","FunctionDeclaration";
        "Declaration","VariableDeclaration";
        "Node","VariableDeclarator";
        "Expression","ThisExpression";
        "Expression","ArrayExpression";
        "Expression","ObjectExpression";
        "Node","Property";
        "Function","FunctionExpression";
        "Expression","FunctionExpression";
        "Expression","UnaryExpression";
        "Expression","UpdateExpression";
        "Expression","BinaryExpression";
        "Expression","AssignmentExpression";
        "Expression","LogicalExpression";
        "ChainElement","MemberExpression";
        "Expression","MemberExpression";
        "Pattern","MemberExpression";
        "Expression","ConditionalExpression";
        "ChainElement","CallExpression";
        "Expression","CallExpression";
        "Expression","NewExpression";
        "Expression","SequenceExpression";
        "ForInStatement","ForOfStatement";
        "Node","Super";
        "Node","SpreadElement";
        "Expression","ArrowFunctionExpression";
        "Function","ArrowFunctionExpression";
        "Expression","YieldExpression";
        "Expression","TemplateLiteral";
        "Expression","TaggedTemplateExpression";
        "Node","TemplateElement";
        "Property","AssignmentProperty";
        "Pattern","ObjectPattern";
        "Pattern","ArrayPattern";
        "Pattern","RestElement";
        "Pattern","AssignmentPattern";
        "Node","Class";
        "Node","ClassBody";
        "Node","MethodDefinition";
        "Declaration","ClassDeclaration";
        "Class","ClassDeclaration";
        "Expression","ClassExpression";
        "Class","ClassExpression";
        "Expression","MetaProperty";
        "Node","ModuleSpecifier";
        "ModuleDeclaration","ImportDeclaration";
        "ModuleSpecifier","ImportSpecifier";
        "ModuleSpecifier","ImportDefaultSpecifier";
        "ModuleSpecifier","ImportNamespaceSpecifier";
        "ModuleDeclaration","ExportNamedDeclaration";
        "ModuleSpecifier","ExportSpecifier";
        "Function","AnonymousDefaultExportedFunctionDeclaration";
        "Class","AnonymousDefaultExportedClassDeclaration";
        "ModuleDeclaration","ExportDefaultDeclaration";
        "ModuleDeclaration","ExportAllDeclaration";
        "Expression","AwaitExpression";
        "Literal","BigIntLiteral";
        "Expression","ChainExpression";
        "Node","ChainElement";
        "Expression","ImportExpression";
        "Node","PropertyDefinition";
        "Node","PrivateIdentifier";
        "BlockStatement","StaticBlock"]

    let components = ["Node","SourceLocation";"SourceLocation","Position"]




    [<Fact>]
    member this.``names test``() =
        let names =
            getDefinition source "es2022.estree"
            |> List.map(function
                | Enum (_,nm,_)
                | Interface(_,nm,_,_)
                    -> nm)
        show names

    [<Fact>]
    member this.``enum and interface test``() =
        let enums,interfaces =
            getDefinition source "es2022.estree"
            |> List.map(function
                | Enum (_,nm,_) -> 0,nm
                | Interface(_,nm,_,_) ->1,nm)
            |> List.partition(fst>>(=)0)
        let enums = enums |> List.map snd
        let interfaces = interfaces |> List.map snd

        show enums
        show interfaces

    [<Fact>]
    member this.``interface inherit test``() =
        let definitions = getDefinition source "es2022.estree"

        let inherits =
            definitions
            |> List.collect(function
                | Enum (_,nm,_) -> []
                | Interface(_,nm,ps,_) ->
                    ps |> List.map(fun p -> p,nm)
                )
        show inherits

    [<Fact>]
    member this.``sole test``() =
        let x = Graph.getVertices inherits
        let y = set interfaces - x
        show y

    [<Fact>]
    member this.``composition test``() =
        let definitions = getDefinition source es2022
        let components =
            definitions
            |> List.collect(function
                | Enum (_,nm,_) -> []
                | Interface(_,nm,_,ms) ->
                    let annotations =
                        ms
                        |> List.map snd

                    let namedtypes =
                        annotations
                        |> List.collect(fun x -> DefinitionExtension.getNamedTypes x)
                        |> List.distinct
                        |> List.filter(fun s -> (set enums).Contains s |> not)

                    namedtypes
                    |> List.map(fun x -> nm,x)
                )
        show components

    [<Fact>]
    member this.``supplementary components test``() =
        let i = Graph.getVertices inherits
        let c = Graph.getVertices components

        let diff = c - i
        show diff

        let components = 
            components 
            |> List.filter(fun(u,v)->diff.Contains v)
        show components


    [<Fact>]
    member this.``topo test``() =
        let y = 
            inherits @ components
            |> Graph.topologicalSort
            |> List.rev

        show y
    [<Fact>]
    member this.``interface type test``() =
        let definitions = getDefinition source es2022
        let interfaces =
            definitions
            |> List.choose(function
                | Enum _ -> None
                | Interface (_,nm,inhs,ms) ->
                    let tp =
                        ms
                        |> List.tryPick(fun(x,annot)->
                            if x = "type" then
                                match annot with
                                | StringLiteral x -> Some x
                                | _ -> None
                            else None
                        )
                    Some(tp,inhs,nm)
            )
            
        let typeNames =
            interfaces
            |> List.filter(fun(tp,inhs,nm) ->
                tp.IsNone || tp.Value <> nm
            )
            |> List.sort
        show typeNames

