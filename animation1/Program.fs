open System
open System.Threading

type State = {
    ForegroundColor: ConsoleColor
    BackgroundColor: ConsoleColor
    Width: int
    Height: int
    AlienX: int
    AlienY: int
}

let initSate() =
    {
        ForegroundColor = Console.ForegroundColor
        BackgroundColor = Console.BackgroundColor
        Width = Console.BufferWidth
        Height = Console.BufferHeight
        AlienY = Console.BufferHeight/2
        AlienX = Console.BufferWidth/2 
    }

let restoreState state =
    Console.ForegroundColor <- state.ForegroundColor
    Console.BackgroundColor <- state.BackgroundColor


let displayMessage x y color (mensaje:string) =
    Console.ForegroundColor <- color
    Console.SetCursorPosition(x,y)
    Console.WriteLine mensaje



let dormirUnMomento() =
    Thread.Sleep 40




let state = initSate()

let centerX = state.Width/2-5
let centerY = state.Height/2-1

let animarMarciano() =
    [centerX .. -1 .. 1]
    |> Seq.iter ( fun x ->
        displayMessage x centerY ConsoleColor.Yellow "  "
        displayMessage (x-1) centerY ConsoleColor.Yellow "👽"
        dormirUnMomento()
    )

let displayAlien state =
    displayMessage state.AlienX state.AlienY ConsoleColor.Yellow "👽"
    state

let updateAlienKeyboard state key =
    match key with
    | ConsoleKey.LeftArrow ->
        {state with AlienX= state.AlienX-1}
    | ConsoleKey.RightArrow ->
        { state with AlienX = state.AlienX+1}
    | ConsoleKey.UpArrow ->
        { state with AlienY = state.AlienY-1}
    | ConsoleKey.DownArrow ->
        {state with AlienY = state.AlienY+1 }
    | _ -> state

let updateKeyboard state =
    if Console.KeyAvailable then
        let key = Console.ReadKey(true)
        updateAlienKeyboard state key.Key
    else
        state


let clearAlien state =
    displayMessage state.AlienX state.AlienY ConsoleColor.Yellow "  "

let clearOldObjects state =
    state
    |> clearAlien
    |> ignore

let updateState state =
    state
    |> updateKeyboard

let updateScreen state =
    state 
    |> displayAlien
    |> ignore

Console.BackgroundColor <- ConsoleColor.Blue
Console.Clear()
Console.CursorVisible <- false


let rec mainLoop state =
    let newState = updateState state
    clearOldObjects state
    updateScreen newState
    dormirUnMomento()
    mainLoop newState


mainLoop state

restoreState state
Console.Clear()
Console.CursorVisible <- true


