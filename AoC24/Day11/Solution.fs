module AoC24Day11 

type Stone = int
type Line = Stone array

let (|IsZero|HasEvenDigits|Other|) (stone:Stone) = 
    match stone with
    | _ when stone = 0 -> IsZero
    | _ when stone.ToString().Length % 2 = 0 -> HasEvenDigits
    | _ -> Other

let parseInputAsLine (input:string array) = 
    let line : Line = input[0].Split(' ') |> Array.map int
    line

let getResultPart1 (input:string array) =
    input.Length
let getResultPart2 (input:string array) =
    input.Length

let input = System.IO.File.ReadAllLines(System.IO.Path.Combine(__SOURCE_DIRECTORY__, "Input.txt"))
let resultPart1 = getResultPart1 input
let resultPart2 = getResultPart2 input