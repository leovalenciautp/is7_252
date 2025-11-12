module App.PauseMenu
open App.Types
let mostrarMenu x y =
    [
        PauseCommand.ContinueGame, "Continue"
        PauseCommand.SaveGame, "Save Game"
        PauseCommand.Exit, "Exit"
    ]
    |> Menu.mostrarMenu x y