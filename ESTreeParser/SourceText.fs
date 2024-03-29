﻿module ESTreeParser.SourceText

open FSharp.Idioms
open System.Text.RegularExpressions
open FSharp.Idioms.RegularExpressions

let tryLineTerminator =
    Regex @"^(\r?\n|\r)"
    |> trySearch

let tryWhiteSpace =
    Regex @"^\s+"
    |> trySearch

let trySingleLineComment =
    Regex @"^//.*"
    |> trySearch

let tryMultiLineComment =
    Regex @"^/\*[\s\S]*?\*/"
    |> trySearch

//An identifier must start with $, _, or any character in the Unicode categories
//“Uppercase letter (Lu)”, “Lowercase letter (Ll)”, “Titlecase letter (Lt)”, “Modifier letter (Lm)”, “Other letter (Lo)”, or
//“Letter number (Nl)”.

//The rest of the string can contain the same characters, plus any
//U+200C zero width non-joiner characters,
//U+200D zero width joiner characters, and characters in the Unicode categories
//“Non-spacing mark (Mn)”, “Spacing combining mark (Mc)”, “Decimal digit number (Nd)”, or “Connector punctuation (Pc)”.

let tryIdentifier =
    Regex @"^[$_\p{L}\p{Nl}][$_\p{L}\p{Mn}\p{Mc}\p{Nl}\p{Nd}\p{Pc}\u200C\u200D]*"
    |> trySearch

let tryOptionalChainingPunctuator =
    Regex @"^\?\.(?!\d)"
    |> trySearch

let tryDivPunctuator = Regex @"^/=?" |> trySearch

//let tryRightBracePunctuator = (|First|_|) '}'

let illegalNumberSep (input: string) = Regex.IsMatch(input, "(^_|_$|\D_|_\D)")

let tryBinaryIntegerLiteral =
    Regex @"^0[bB][01_]+n?\b"
    |> trySearch

let tryOctalIntegerLiteral =
    Regex @"^0[oO][0-7_]+n?\b"
    |> trySearch

let tryHexIntegerLiteral =
    Regex @"^0[xX][0-9a-fA-F_]+n?\b"
    |> trySearch

let tryDecimalIntegerLiteral =
    Regex @"^\d[\d_]*n?\b"
    |> trySearch

let tryDecimalLiteral =
    Regex @"^(?!_)([\d_]*\.[\d_]+|[\d_]+\.[\d_]*)([eE][-+]?[\d_]+)?"
    |> trySearch

let trySingleStringLiteral =
    Regex @"^'(\\\\|\\'|[^'])*'"
    |> trySearch

let tryDoubleStringLiteral =
    Regex """^"(\\\\|\\"|[^"])*(")"""
    |> trySearch

//这是简易模板，要求内部的注释，字符串字面量等不能再包含`反引号。
let tryTemplate =
    Regex @"^`(\\\\|\\`|[^`])*`"
    |> trySearch

let tryRegularExpressionLiteral =
    Regex @"^/(\\\\|\\/|[^/])+/[gimsuy]*"
    |> trySearch

let isDeclar (line:string) =
    let re = Regex @"^\s*(extend\s+)?(interface|enum)\b"
    re.IsMatch(line)
