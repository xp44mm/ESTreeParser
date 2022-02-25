extend interface ForOfStatement {
  await: boolean;
}
extend interface ObjectExpression {
    properties: [ Property | SpreadElement ];
}
extend interface TemplateElement {
    value: {
        cooked: string | null;
        raw: string;
    };
}
extend interface ObjectPattern {
    properties: [ AssignmentProperty | RestElement ];
}