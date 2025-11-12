module App.Types

type ProgramStatus =
| Running
| Terminated

type MenuCommand =
| OpenFile
| SaveFile
| Exit

type GameCommand =
| NewGame
| ContinueGame
| Exit

type PauseCommand =
| ContinueGame
| SaveGame
| Exit

type MenuACommand =
| GoToB
| Exit

type MenuBCommand =
| GoToA
| GoToC
| Exit

type MenuCCommand =
| GoToB
| Exit


type State = {
    ProgramStatus: ProgramStatus
}

let initState() = 
    {
        ProgramStatus = Running
    }
