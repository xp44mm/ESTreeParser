module ESTreeParser.Program
open ESTreeParser.Ast

let e = [None,[],"Node";
None,[],"Position";
None,[],"SourceLocation";
None,["BlockStatement"],"FunctionBody";
None,["ExpressionStatement"],"Directive";
None,["Literal"],"BigIntLiteral";
None,["Literal"],"RegExpLiteral";
None,["Node"],"ChainElement";
None,["Node"],"Class";
None,["Node"],"Expression";
None,["Node"],"Function";
None,["Node"],"ModuleDeclaration";
None,["Node"],"ModuleSpecifier";
None,["Node"],"Pattern";
None,["Node"],"Statement";
None,["Statement"],"Declaration";
Some "ClassDeclaration",["Class"],"AnonymousDefaultExportedClassDeclaration";
Some "FunctionDeclaration",["Function"],"AnonymousDefaultExportedFunctionDeclaration";
Some "Property",["Property"],"AssignmentProperty"]



let [<EntryPoint>] main _ = 
    0
