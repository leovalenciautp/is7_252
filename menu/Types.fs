module App.Types

type ProgramStatus =
| Running
| Terminated

type MenuCommand =
| OpenFile
| SaveFile
| Exit

type State = {
    ProgramStatus: ProgramStatus
}

let initState() = 
    {
        ProgramStatus = Running
    }
