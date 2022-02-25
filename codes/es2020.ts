extend interface Literal <: Expression {
    type: "Literal";
    value: string | boolean | null | number | RegExp | bigint;
}
interface BigIntLiteral <: Literal {
  bigint: string;
}
interface ChainExpression <: Expression {
  type: "ChainExpression";
  expression: ChainElement;
}

interface ChainElement <: Node {
  optional: boolean;
}

extend interface CallExpression <: ChainElement {}

extend interface MemberExpression <: ChainElement {}
interface ImportExpression <: Expression {
  type: "ImportExpression";
  source: Expression;
}
extend enum LogicalOperator {
    "||" | "&&" | "??"
}
extend interface ExportAllDeclaration {
  exported: Identifier | null;
}