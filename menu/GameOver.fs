module App.GameOver

open App.Types

let mostrarMenu x y =
    [
        GameOverCommand.NewGame, "New Game"
        GameOverCommand.Exit, "Exit"
    ]
    |> Menu.mostrarMenu x y 