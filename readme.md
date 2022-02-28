# ESTreeParser

This package is A Parser for AST Descriptor Syntax of [estree](https://github.com/estree/estree). It is uploaded to [nuguet](https://www.nuget.org/packages/ESTreeParser/).

## Feature

- Use BNF to describe AST Descriptor Syntax of estree.
  The details see to [yacc file](https://github.com/xp44mm/ESTreeParser/blob/master/ESTreeParser/estree.fsyacc).

- Extract all of type definitions from a md file.
  The usages see to [test file](https://github.com/xp44mm/ESTreeParser/blob/master/ESTreeParser.Test/ExtractDefinitionTest.fs).
  Some exacted definition files in [codes folder](https://github.com/xp44mm/ESTreeParser/blob/master/codes/).
  
- Parse the type definition to AST of AST Descriptor Syntax of estree.
  The AST definitions see to [code file](https://github.com/xp44mm/ESTreeParser/blob/master/ESTreeParser/Ast.fs).
  The usages see to [test file](https://github.com/xp44mm/ESTreeParser/blob/master/ESTreeParser.Test/ParserTest.fs).
  
- Serialize AST structure instance to JSON.
  The usages see to [test file](https://github.com/xp44mm/ESTreeParser/blob/master/ESTreeParser.Test/GenerateJsonFile.fs).
  Some serialized JSON files in [jsons folder](https://github.com/xp44mm/ESTreeParser/blob/master/jsons/).

- Solve the definition after the syntax extension.
  The usages see to [test file](https://github.com/xp44mm/ESTreeParser/blob/master/ESTreeParser.Test/DefinitionFileTest.fs).
  Some extended syntax files in [extended folder](https://github.com/xp44mm/ESTreeParser/blob/master/extended/).
