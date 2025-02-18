module AoC24Library

open System.IO

let getInput (day:int) (test:bool) = 
    let numberString = day.ToString("D2")
    let dayPart = $"Day{numberString}"
    let fileNamePart = 
        if test then "InputTest.txt"
        else "Input.txt"
    let path = Path.Combine(__SOURCE_DIRECTORY__, dayPart, fileNamePart)
    File.ReadAllLines(path)