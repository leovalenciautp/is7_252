module App.Navigator

open System
open App.Types

type NavigatorState =
| ShowMainMenu
| ShowGame
| ShowPause
| ShowGameOver
| Terminated

type State = {
    NavigatorState: NavigatorState
}

let initState() =
    {
        NavigatorState = ShowMainMenu
    }


let showMainMenu state =
    Console.Clear()
    Utils.displayMessageGigante 0 0 ConsoleColor.Green "Alien"
    Utils.displayMessageGigante 0 7 ConsoleColor.Red "Attack!"
    match MainMenu.mostrarMenu 20 15 with
    | GameCommand.NewGame ->
        {state with NavigatorState=ShowGame}
    | GameCommand.LoadGame ->
        //
        // La magia ocurre aqui para cargar
        // un juego grabado en disco
        //
        {state with NavigatorState=ShowGame}
    | GameCommand.Exit ->
        {state with NavigatorState=Terminated}

let showGame state =
    Console.Clear()
    Game.mostrarJuego()
    {state with NavigatorState=ShowPause}

let showGameOver state =
    Console.Clear()
    Utils.displayMessageGigante 0 5 ConsoleColor.Red "Game Over"
    match GameOver.mostrarMenu 10 15 with
    | GameOverCommand.NewGame ->
        {state with NavigatorState=ShowGame}
    | GameOverCommand.Exit ->
        { state with NavigatorState=Terminated}


let showPause state =
    Console.Clear()
    Utils.displayMessageGigante 0 5 ConsoleColor.Magenta "Paused"
    match PauseMenu.mostrarMenu 10 15 with
    | PauseCommand.ContinueGame ->
        //
        // Hay una magia aqui
        //
        {state with NavigatorState=ShowGame}

    | PauseCommand.SaveGame ->
        //
        // La magia ocurre aqui tambien
        //
        {state with NavigatorState=Terminated}
    | PauseCommand.Exit ->
        {state with NavigatorState=Terminated}



let updateState state =
    match state.NavigatorState with
    | ShowMainMenu -> showMainMenu state
    | ShowGame -> showGame state
    | ShowPause -> showPause state
    | ShowGameOver -> showGameOver state
    | _ -> state


let rec mainLoop state =
    let newState = state |> updateState
    if newState.NavigatorState <> Terminated then
        mainLoop newState

let mostrarNavegador() =
    initState()
    |> mainLoop
