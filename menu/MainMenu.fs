module App.MainMenu

open App.Types

let mostrarMenu x y =
    [
        GameCommand.NewGame,"New Game"
        GameCommand.ContinueGame,"Continue"
        GameCommand.Exit,"Exit"
    ]
    |> Menu.mostrarMenu x y