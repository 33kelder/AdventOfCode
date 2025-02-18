open System
open System.IO

open AoC24Library
open AoC24Day10
let day = 11
let test = true

let input = getInput day test

let resultPart1 = getResultPart1 input
Console.WriteLine($"Result part 1: {resultPart1.ToString()}")
let resultPart2 = getResultPart2 input
Console.WriteLine($"Result part 2: {resultPart2.ToString()}")


