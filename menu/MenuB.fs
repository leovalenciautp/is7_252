module App.MenuB

open App.Types

let mostrarMenu x y =
    [
        MenuBCommand.GoToA,"Regresar a Menu A"
        MenuBCommand.GoToC, "Ir a Menu C"
        MenuBCommand.Exit, "Salir"
    ]
    |> Menu.mostrarMenu x y