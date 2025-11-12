module App.Navigator

open System
open App.Types

type NavigatorState =
| ShowMenuA
| ShowMenuB
| ShowMenuC
| Terminated

type State = {
    NavigatorState: NavigatorState
}

let initState() =
    {
        NavigatorState = ShowMenuA
    }

let showMenuA state =
    Console.Clear()
    let choice = MenuA.mostrarMenu 20 10
    match choice with 
    | MenuACommand.GoToB -> 
        {state with NavigatorState = ShowMenuB}

    | MenuACommand.Exit ->
        { state with NavigatorState = Terminated}

let showMenuB state =
    Console.Clear()
    let choice = MenuB.mostrarMenu 20 10
    match choice with 
    | MenuBCommand.GoToA -> 
        {state with NavigatorState = ShowMenuA}

    | MenuBCommand.GoToC ->
        {state with NavigatorState = ShowMenuC}

    | MenuBCommand.Exit ->
        { state with NavigatorState = Terminated}

let showMenuC state =
    Console.Clear()
    let choice = MenuC.mostrarMenu 20 10
    match choice with 
    | MenuCCommand.GoToB -> 
        {state with NavigatorState = ShowMenuB}

    | MenuCCommand.Exit ->
        { state with NavigatorState = Terminated}


let updateState state =
    match state.NavigatorState with
    | ShowMenuA -> showMenuA state
    | ShowMenuB -> showMenuB state
    | ShowMenuC -> showMenuC state
    | _ -> state


let rec mainLoop state =
    let newState = state |> updateState
    if newState.NavigatorState <> Terminated then
        mainLoop newState

let mostrarNavegador() =
    initState()
    |> mainLoop
