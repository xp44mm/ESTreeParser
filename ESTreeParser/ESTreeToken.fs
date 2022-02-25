namespace ESTreeParser

type ESTreeToken =
    | QUOTED of string
    | ID of string
    | EXTEND
    | INTERFACE
    | ENUM
    | INHERIT
    | COMMA
    | COLON
    | BAR
    | SEMICOLON
    | LBRACK
    | RBRACK
    | LBRACE
    | RBRACE

