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

type Misil = {
    X: int
    Y: int
}

type State = {
    Width: int
    Height: int
    AlienX: int
    AlienY: int
    Counter: int
    Tick: int
    ProgramState: ProgramState
    Misiles: Misil list
    EnemyX: int
    EnemyY: int
    EnemySpeed: float
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
        Misiles = [] 
        EnemyX = Console.BufferWidth-10
        EnemyY = 0
        EnemySpeed = Math.PI/50.0
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
    state.Misiles
    |> List.iter ( fun m -> displayMessage m.X m.Y ConsoleColor.Cyan "=>")
    state

let displayEnemy state =
    displayMessage state.EnemyX state.EnemyY ConsoleColor.Yellow "👾"
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
        let nuevoMisil = {X=state.AlienX+2;Y=state.AlienY}
        {state with Misiles = nuevoMisil :: state.Misiles}
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

let clearEnemy state =
    displayMessage state.EnemyX state.EnemyY ConsoleColor.Yellow "  "
    state

let clearMisil state =
    state.Misiles
    |> List.iter (fun m -> 
        displayMessage m.X m.Y ConsoleColor.Cyan "  "
    )
    state

let clearOldObjects state =
    state
    |> clearAlien
    |> clearMisil
    |> clearEnemy
    |> ignore

let updateTick state =
    {state with Tick = state.Tick+1}

let updateCounter state =
    if state.Tick % 25 = 0 then
        {state with Counter=state.Counter+1}
    else
        state

let updateMisil state =
    let nuevosMisiles =
        state.Misiles
        |> Seq.map ( fun m -> {m with X = m.X+1})
        |> Seq.filter ( fun m -> m.X < state.Width-2)
        |> Seq.toList
    { state with Misiles = nuevosMisiles}

let updateEnemy state =
    let nuevoY = - float state.Height/2.0*(Math.Cos (float state.Tick*state.EnemySpeed) - 1.0)
    {state with EnemyY = min (state.Height-1) (int nuevoY)}
let updateState state =
    state
    |> updateTick
    |> updateCounter
    |> updateMisil
    |> updateEnemy
    |> updateKeyboard

let updateScreen state =
    state
    |> displayCounter 
    |> displayAlien
    |> displayMisil
    |> displayEnemy
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


