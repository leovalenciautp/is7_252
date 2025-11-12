module App.MenuC

open App.Types

let mostrarMenu x y =
    [
        MenuCCommand.GoToB,"Regresar a Menu B"
        MenuCCommand.Exit,"salir"
    ]
    |>Menu.mostrarMenu x y