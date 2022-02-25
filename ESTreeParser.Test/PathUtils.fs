module ESTreeParser.PathUtils
open System.IO

let solutionPath = DirectoryInfo(__SOURCE_DIRECTORY__).Parent.FullName
let mainProjPath = Path.Combine(solutionPath,"ESTreeParser")

let rootPath = DirectoryInfo(solutionPath).Parent.FullName

let sourcePath = Path.Combine(solutionPath, @"ESTreeParser")
let estreePath = Path.Combine(rootPath, @"estree")

let tscodesPath = Path.Combine(__SOURCE_DIRECTORY__, "tscodes")
