module AoC24Day08

type Location = {X:int; Y:int}
type Antenna = {Freq:char; Loc:Location}
type Antinode = {Loc:Location;Antenna1:Antenna;Antenna2:Antenna}
type MapDimension = {TopLeft:Location;BottomRight:Location}

let locationIsOnMap (mapDimension:MapDimension) (location:Location) = 
    not (location.X < mapDimension.TopLeft.X  || 
         location.Y < mapDimension.TopLeft.Y ||
         location.X > mapDimension.BottomRight.X ||
         location.Y > mapDimension.BottomRight.Y )

let mapCharToAntenna (character:char) (row:int) (col:int) = 
    match character with
    | '.' -> None
    | _ -> Some {Antenna.Freq = character;Antenna.Loc = {X = col;Y = row}}

let getAntennas (input:string array) =
    seq { for i in 0..input.Length - 1 do
            for j in 0..input[0].Length - 1 do
                yield (mapCharToAntenna (input[i][j]) i j) }
    |> Seq.choose id
    |> Seq.toArray

let getFrequencies (antennas: Antenna array) = 
    antennas
    |> Array.map (fun antenna -> antenna.Freq)
    |> Array.distinct

let filterByFrequency (antennas:Antenna array) (frequency:char) =
    antennas |> Array.filter (fun antenna -> antenna.Freq = frequency)

let getAntennaCombis (antennas:Antenna array) = 
    let combis = Array.allPairs antennas antennas
    combis |> Array.filter (fun (a1, a2) -> a1 <> a2)

let getAntinodesPerCombiPart1 (antenna1:Antenna,antenna2:Antenna) = 
    [|{X = 2 * antenna1.Loc.X - antenna2.Loc.X; Y = 2 * antenna1.Loc.Y - antenna2.Loc.Y}|]

let getAntinodesPerCombiPart2 (mapDimension:MapDimension) (antenna1:Antenna,antenna2:Antenna) = 
    let mutable insideMap = true;
    let mutable antinodes = [|antenna1.Loc|]
    let mutable currentStep = antenna1.Loc
    let step = {X= antenna2.Loc.X - antenna1.Loc.X;Y= antenna2.Loc.Y - antenna1.Loc.Y}
    while insideMap do
        currentStep <- {X = currentStep.X - step.X;Y = currentStep.Y - step.Y}
        if (locationIsOnMap mapDimension currentStep) then
            antinodes <- Array.append antinodes [|currentStep|]
        else 
            insideMap <- false
    antinodes

let getAntinodesPerFrequency (mapDimension:MapDimension) (antennas:Antenna array) getAntinodesPerCombi (frequency:char)  = 
    let antennasWithSameFrequency = antennas |> Array.filter (fun antenna -> antenna.Freq = frequency)
    let antennaCombis = getAntennaCombis antennasWithSameFrequency
    antennaCombis 
    |> Array.map getAntinodesPerCombi 
    |> Array.concat
    |> Array.filter (locationIsOnMap mapDimension)

let getTotalNrOfAntinodes input mapDimension getAntinodesPerCombiPart  = 
    let antennas = getAntennas input 
    let frequenties = getFrequencies antennas
    frequenties
    |> Array.map (getAntinodesPerFrequency mapDimension antennas getAntinodesPerCombiPart)
    |> Array.concat
    |> Array.distinct
    |> Array.length

let getResultPart1 (input:string array) =
    let mapDimension = {TopLeft = {X = 0; Y = 0}; BottomRight = {X = input.Length - 1; Y = input[0].Length - 1}}
    getTotalNrOfAntinodes input mapDimension getAntinodesPerCombiPart1 

let getResultPart2 (input:string array) =
    let mapDimension = {TopLeft = {X = 0; Y = 0}; BottomRight = {X = input.Length - 1; Y = input[0].Length - 1}}
    let getAntinodesPerCombiPart2' = getAntinodesPerCombiPart2 mapDimension
    getTotalNrOfAntinodes input mapDimension getAntinodesPerCombiPart2' 

let input = System.IO.File.ReadAllLines(System.IO.Path.Combine(__SOURCE_DIRECTORY__, "Input.txt"))
let resultPart1 = getResultPart1 input
let resultPart2 = getResultPart2 input