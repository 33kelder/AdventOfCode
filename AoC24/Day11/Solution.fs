module AoC24Day11

open System.Collections.Generic 

type Stone = int64
type Line = Stone Dictionary


let parseInputAsLine (input:string array) = 
    let line : Line = input[0].Split(' ') |> Array.map int64
    line

let (|IsZero|HasEvenDigits|Other|) (stone:Stone) = 
    match stone with
    | _ when stone = 0 -> IsZero
    | _ when stone.ToString().Length % 2 = 0 -> HasEvenDigits
    | _ -> Other

let getNewStones (stone:Stone) : Stone array =
    match stone with
    | IsZero -> [|1|]
    | HasEvenDigits ->
        let stoneString = stone.ToString()
        let leftStoneString = stoneString.Substring (0,stoneString.Length/2)
        let leftStone : Stone = int leftStoneString
        let rightStoneString = stoneString.Substring (stoneString.Length/2,stoneString.Length/2)
        let rightStone : Stone = int rightStoneString
        [| leftStone; rightStone |]
    | Other ->
        [|stone * 2024L|]
    
let getResultPart1 (input:string array) =
    let mutable line = parseInputAsLine input
    for blinking in [1..25] do
        let mutable newLine : Line = Array.empty
        for i in [0..line.Length - 1] do
            let newStones = getNewStones line[i]
            newLine <- Array.concat [|newLine;newStones|]
        line <- newLine
    line.Length

let getResultPart2 (input:string array) =
    let mutable line = parseInputAsLine input
    for blinking in [1..25] do
        let mutable newLine : Line = Array.empty
        for i in [0..line.Length - 1] do
            let newStones = getNewStones line[i]
            newLine <- Array.concat [|newLine;newStones|]
        line <- newLine
    line.Length

let input = System.IO.File.ReadAllLines(System.IO.Path.Combine(__SOURCE_DIRECTORY__, "Input.txt"))
let resultPart1 = 1//getResultPart1 input
let resultPart2 = getResultPart2 input