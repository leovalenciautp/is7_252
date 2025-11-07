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

type SpriteState =
| Vivo
| Colisionado
| Dormido

type Misil = {
    X: int
    Y: int
}

type State = {
    Width: int
    Height: int
    AlienX: int
    AlienY: int
    AlienState: SpriteState
    AlienColisionTime: int
    AlienLives: int
    Counter: int
    Tick: int
    ProgramState: ProgramState
    Misiles: Misil list
    EnemyX: int
    EnemyY: int
    EnemySpeed: float
    EnemyState: SpriteState
    EnemyMisiles: Misil list
    LastMisileTime: int
    ColisionTime: int
    EnemyT0: int
    Puntaje: int
}

let initSate() =
    {
        Width = Console.BufferWidth
        Height = Console.BufferHeight
        AlienY = Console.BufferHeight/2
        AlienX = Console.BufferWidth/2
        AlienState = Vivo
        AlienColisionTime = 0
        AlienLives = 3
        ProgramState = Running
        Counter = 0
        Tick =0
        Misiles = [] 
        EnemyX = Console.BufferWidth-10
        EnemyY = 0
        EnemySpeed = Math.PI/50.0
        EnemyState = Vivo
        EnemyMisiles = []
        ColisionTime = 0
        EnemyT0 = 0
        Puntaje = 0
        LastMisileTime = 0
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
    match state.AlienState with
    | Vivo -> displayMessage state.AlienX state.AlienY ConsoleColor.Yellow "👽"
    | Colisionado -> displayMessage state.AlienX state.AlienY ConsoleColor.Yellow "⚰️"
    | _ -> ()
    state

let displayCounter state =
    displayJustified (state.Width-1) 0 ConsoleColor.Yellow $"{state.Counter}"
    state

let displayMisil state =
    state.Misiles
    |> List.iter ( fun m -> displayMessage m.X m.Y ConsoleColor.Cyan "=>")
    state

let displayEnemyMisil state =
    state.EnemyMisiles
    |> List.iter ( fun m -> displayMessage m.X m.Y ConsoleColor.Yellow "<=")
    state

let displayEnemy state =
    match state.EnemyState with  
    | Vivo -> displayMessage state.EnemyX state.EnemyY ConsoleColor.Yellow "👾"
    | Colisionado -> displayMessage state.EnemyX state.EnemyY ConsoleColor.Yellow "💥" 
    | _ ->()
    state

let displayScore state =
    displayMessage 0 0 ConsoleColor.Yellow $"Score: {state.Puntaje}"
    state

let displayLives state =
    displayMessage (state.Width/2-10) 0 ConsoleColor.Yellow $"Lives: {state.AlienLives}"
let updateAlienKeyboard key state =
    if state.AlienState = Vivo then
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
    else
        state

let updateScape key state =
    match key with 
    | ConsoleKey.Escape ->
        { state with ProgramState = Terminated}
    | _ -> state

let udpateMisilKeyboard key state =
    if state.AlienState = Vivo then
        match key with
        | ConsoleKey.Spacebar ->
            let nuevoMisil = {X=state.AlienX+2;Y=state.AlienY}
            {state with Misiles = nuevoMisil :: state.Misiles}
        | _ -> state
    else
        state

let dispararMisilEnemigo state =
    if state.Tick - state.LastMisileTime >= 6 then 
        let nuevoMisil = {X=state.EnemyX-2;Y=state.EnemyY}
        {state with EnemyMisiles = nuevoMisil :: state.EnemyMisiles; LastMisileTime = state.Tick}
    else
        state

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
    match state.AlienState with 
    | Vivo | Colisionado -> displayMessage state.AlienX state.AlienY ConsoleColor.Yellow "  "
    | _ -> ()
    state

let clearEnemy state =
    match state.EnemyState with 
    | Vivo | Colisionado -> displayMessage state.EnemyX state.EnemyY ConsoleColor.Yellow "  "
    | _ -> ()
    state

let clearMisil state =
    state.Misiles
    |> List.iter (fun m -> 
        displayMessage m.X m.Y ConsoleColor.Cyan "  "
    )
    state

let clearEnemyMisil state =
    state.EnemyMisiles
    |> List.iter (fun m -> 
        displayMessage m.X m.Y ConsoleColor.Cyan "  "
    )
    state

let clearOldObjects state =
    state
    |> clearAlien
    |> clearMisil
    |> clearEnemyMisil
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

let updateEnemyMisil state =
    let nuevosMisiles =
        state.EnemyMisiles
        |> Seq.map ( fun m -> {m with X = m.X-1})
        |> Seq.filter ( fun m -> m.X >= 0)
        |> Seq.toList
    { state with EnemyMisiles = nuevosMisiles}

let updateEnemy state =
    if state.EnemyState = Vivo then
        let nuevoY = - float state.Height/2.0*(Math.Cos (float (state.Tick - state.EnemyT0)*state.EnemySpeed) - 1.0)
        {state with EnemyY = min (state.Height-1) (int nuevoY)}
        |> dispararMisilEnemigo
    else
        state

let updateColision state =
    match state.EnemyState with 
    | Vivo ->
        state.Misiles
        |> List.filter (fun m -> not (m.X+1=state.EnemyX && m.Y=state.EnemyY))
        |> fun nuevaLista ->
            if nuevaLista.Length <> state.Misiles.Length then
                {state with 
                    Misiles=nuevaLista
                    EnemyState=Colisionado
                    ColisionTime = state.Tick
                    Puntaje = state.Puntaje+1
                }
            else
                state
    | Colisionado ->
        if state.Tick - state.ColisionTime >= 25 then 
            {state with EnemyState=Dormido}
        else
            state // la magia ocurre aqui
    | Dormido ->
        if state.Tick-state.ColisionTime >= 100 then
            {state with EnemyState = Vivo; EnemyT0 = state.Tick}
        else
            state

let updateAlienColision state =
    match state.AlienState with 
    | Vivo ->
        state.EnemyMisiles
        |> List.filter (fun m -> not (m.X=state.AlienX && m.Y=state.AlienY))
        |> fun nuevaLista ->
            if nuevaLista.Length <> state.EnemyMisiles.Length then
                {state with 
                    EnemyMisiles=nuevaLista
                    AlienState=Colisionado
                    AlienColisionTime = state.Tick
                    AlienLives = state.AlienLives-1
                }
            else
                state
    | Colisionado ->
        if state.Tick - state.AlienColisionTime >= 25 then 
            {state with AlienState=Dormido}
        else
            state // la magia ocurre aqui
    | Dormido ->
        if state.Tick-state.AlienColisionTime >= 100 then
            {state with AlienState = Vivo;AlienX = state.Width/2; AlienY = state.Height/2}
        else
            state

let updateVidas state =
    if state.AlienLives <= 0 then
        {state with ProgramState = Terminated}
    else
        state
let updateState state =
    state
    |> updateTick
    |> updateCounter
    |> updateMisil
    |> updateEnemyMisil
    |> updateEnemy
    |> updateColision
    |> updateAlienColision
    |> updateVidas
    |> updateKeyboard

let updateScreen state =
    state
    |> displayCounter 
    |> displayAlien
    |> displayMisil
    |> displayEnemyMisil
    |> displayEnemy
    |> displayScore
    |> displayLives
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


