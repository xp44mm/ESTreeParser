# ESTreeParser

This package is A Parser for AST Descriptor Syntax of [estree](https://github.com/estree/estree.git). It is uploaded to [nuguet](https://www.nuget.org/packages/ESTreeParser/).

## Feature

- Use BNF to describe AST Descriptor Syntax of estree.
  The details see to [yacc file](https://github.com/xp44mm/ESTreeParser/ESTreeParser/estree.fsyacc).

- Extract all of type definitions from a md file.
  The usages see to [test file](https://github.com/xp44mm/ESTreeParser/ESTreeParser.Test/ExtractDefinitionTest.fs).
  Some exacted definition files in [codes folder](https://github.com/xp44mm/ESTreeParser/codes/).
  
- Parse the type definition to AST of AST Descriptor Syntax of estree.
  The AST definitions see to [code file](https://github.com/xp44mm/ESTreeParser/ESTreeParser.Test/Ast.fs).
  The usages see to [test file](https://github.com/xp44mm/ESTreeParser/ESTreeParser.Test/ParserTest.fs).
  
- Serialize AST structure instance to JSON.
  The usages see to [test file](https://github.com/xp44mm/ESTreeParser/ESTreeParser.Test/GenerateJsonFile.fs).
  Some serialized JSON files in [jsons folder](https://github.com/xp44mm/ESTreeParser/jsons/).
