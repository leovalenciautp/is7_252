//
// Siempre deben especificar el nombre del module
// y del namespace donde estan trabajando
//
module App.Menu
open System
open App.Types

type MenuState =
| Active
| Terminated

type State = {
    MenuState: MenuState
    MenuX: int
    MenuY: int
    Commands: (MenuCommand*string) list
    CursorX: int
    CursorIndex: int   
}

let initState() =
    {
        MenuState = Active
        MenuX = 10
        MenuY = 10
        Commands = [
            OpenFile,"Abrir Archivo"
            SaveFile,"Guardar Archivo"
            Exit,"Salir"
        ]
        CursorX = 8
        CursorIndex = 0
    }


let displayMenu state =
    state.Commands
    |> List.iteri (fun i (_,c) -> 
        Utils.displayMessage state.MenuX (state.MenuY+i) ConsoleColor.Yellow c
    )

let displayCursor oldState newState =
    Utils.displayMessage oldState.CursorX (oldState.MenuY+oldState.CursorIndex) ConsoleColor.Yellow "  "
    Utils.displayMessage newState.CursorX (newState.MenuY+newState.CursorIndex) ConsoleColor.Yellow "☠️"

let cursorKeyboard key state =
    match key with 
    | ConsoleKey.UpArrow ->
        {state with CursorIndex = max 0 (state.CursorIndex-1)}
    | ConsoleKey.DownArrow ->
        {state with CursorIndex = min (state.Commands.Length-1) (state.CursorIndex+1)}
    | ConsoleKey.Enter ->
        {state with MenuState = Terminated}
    | _ -> state

let updateKeyBoard state =
    if Console.KeyAvailable then
        let tecla = Console.ReadKey true
        cursorKeyboard tecla.Key state
    else
        state
let updateState state =
    updateKeyBoard state

let updateScreen oldState newState =
    displayMenu newState
    displayCursor oldState newState

let rec mainLoop oldState =
    let newState= updateState oldState
    updateScreen oldState newState
    if newState.MenuState = Active then
        Utils.dormirUnPoco()
        mainLoop newState
    else
        newState

let mostrarMenu() =
    initState()
    |> mainLoop
    |> fun state ->
        let choice = state.CursorIndex
        fst state.Commands[choice]


