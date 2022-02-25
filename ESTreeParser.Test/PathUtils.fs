module ESTreeParser.PathUtils
open System.IO

let accountPath = DirectoryInfo(__SOURCE_DIRECTORY__).Parent.Parent.FullName
let estreePath = Path.Combine(accountPath, @"estree")

let solutionPath = DirectoryInfo(__SOURCE_DIRECTORY__).Parent.FullName
let estreeParserPath = Path.Combine(solutionPath,"ESTreeParser")
let codesPath = Path.Combine(solutionPath, "codes")
let jsonsPath = Path.Combine(solutionPath, "jsons")

let files = [|
    "5"   
    "2015"
    "2016"
    "2017"
    "2018"
    "2019"
    "2020"
    "2021"
    "2022"
    |]

