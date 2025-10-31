open System
open System.Threading

//
// Este es un ejemplo de un pattern llamado MVU
// Model View Update
//
// Viene de un lenguaje que se llama Elm
//

type ProgramState =
| Running
| Terminated

type State = {
    Width: int
    Height: int
    AlienX: int
    AlienY: int
    Counter: int
    Tick: int
    ProgramState: ProgramState
    MisilX: int
    MisilY: int
    MisilOn: bool
}

let initSate() =
    {
        Width = Console.BufferWidth
        Height = Console.BufferHeight
        AlienY = Console.BufferHeight/2
        AlienX = Console.BufferWidth/2
        ProgramState = Running
        Counter = 0
        Tick =0
        MisilX = 0
        MisilY =0
        MisilOn = false 
    }



let displayMessage x y color (mensaje:string) =
    Console.ForegroundColor <- color
    Console.SetCursorPosition(x,y)
    Console.Write mensaje


let displayJustified x y color (mensaje:string) =
    let nuevaX = x - (mensaje.Length-1)
    displayMessage nuevaX y color mensaje



let dormirUnMomento() =
    Thread.Sleep 40



let displayAlien state =
    displayMessage state.AlienX state.AlienY ConsoleColor.Yellow "👽"
    state

let displayCounter state =
    displayJustified (state.Width-1) 0 ConsoleColor.Yellow $"{state.Counter}"
    state

let displayMisil state =
    if state.MisilOn then
        displayMessage state.MisilX state.MisilY ConsoleColor.Cyan "=>"
        state
    else
        state

let updateAlienKeyboard key state =
    match key with
    | ConsoleKey.LeftArrow ->
        {state with AlienX= max 0 (state.AlienX-1)} // Guards
    | ConsoleKey.RightArrow ->
        { state with AlienX = min (state.Width-2) (state.AlienX+1)}
    | ConsoleKey.UpArrow ->
        { state with AlienY = max 0 (state.AlienY-1)}
    | ConsoleKey.DownArrow ->
        {state with AlienY = min (state.Height-1) (state.AlienY+1) }
    | _ -> state

let updateScape key state =
    match key with 
    | ConsoleKey.Escape ->
        { state with ProgramState = Terminated}
    | _ -> state

let udpateMisilKeyboard key state =
    match key with
    | ConsoleKey.Spacebar ->
        if not state.MisilOn then
            {state with MisilOn=true;MisilX=state.AlienX+2;MisilY=state.AlienY}
        else
            state
    | _ -> state

let updateKeyboard state =
    if Console.KeyAvailable then
        let key = Console.ReadKey(true)
        state
        |> updateAlienKeyboard key.Key
        |> updateScape key.Key
        |> udpateMisilKeyboard key.Key
    else
        state


let clearAlien state =
    displayMessage state.AlienX state.AlienY ConsoleColor.Yellow "  "
    state

let clearMisil state =
    if state.MisilOn then
        displayMessage state.MisilX state.MisilY ConsoleColor.Cyan "  "
    state

let clearOldObjects state =
    state
    |> clearAlien
    |> clearMisil
    |> ignore

let updateTick state =
    {state with Tick = state.Tick+1}

let updateCounter state =
    if state.Tick % 25 = 0 then
        {state with Counter=state.Counter+1}
    else
        state

let updateMisil state =
    if state.MisilOn then
        let nuevoX = state.MisilX+1
        if nuevoX >= state.Width-2 then
            {state with MisilOn=false}
        else
            {state with MisilX = nuevoX}
    else
        state
let updateState state =
    state
    |> updateTick
    |> updateCounter
    |> updateMisil
    |> updateKeyboard

let updateScreen state =
    state
    |> displayCounter 
    |> displayAlien
    |> displayMisil
    |> ignore

let rec mainLoop state =
    let newState = updateState state
    clearOldObjects state
    updateScreen newState
    dormirUnMomento()
    if newState.ProgramState = Running then
        mainLoop newState


let oldForeground = Console.ForegroundColor
let oldBackground = Console.BackgroundColor
Console.BackgroundColor <- ConsoleColor.Blue
Console.Clear()
Console.CursorVisible <- false


initSate()
|> mainLoop


Console.ForegroundColor <- oldForeground
Console.BackgroundColor <- oldBackground
Console.Clear()
Console.CursorVisible <- true


