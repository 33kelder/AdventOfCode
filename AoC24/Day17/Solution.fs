module AoC24Day17

let getResultPart1 (input:string array) =
    input.Length
let getResultPart2 (input:string array) =
    input.Length

let input = System.IO.File.ReadAllLines(System.IO.Path.Combine(__SOURCE_DIRECTORY__, "Input.txt"))
let resultPart1 = getResultPart1 input
let resultPart2 = getResultPart2 input