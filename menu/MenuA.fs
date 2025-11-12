module App.MenuA

open App.Types

let mostrarMenu x y =
    [
        MenuACommand.GoToB,"Ir al menu B"
        MenuACommand.Exit, "Salir"        
    ]
    |> Menu.mostrarMenu x y