extend interface Function {
    async: boolean;
}
interface AwaitExpression <: Expression {
    type: "AwaitExpression";
    argument: Expression;
}