//
//
// Programming in the Large
//
// Modular programming
// Namespace
//
module App.Main

open System
open App.Types
Console.Clear()
Console.CursorVisible <- false
let choice =Menu.mostrarMenu()

Console.Clear()
Console.CursorVisible <- true

match choice with
| OpenFile -> printfn "Elegiste abrir archivo"
| SaveFile -> printfn "Elegiste Guardar"
| Exit -> printfn "Elegiste Salir"








