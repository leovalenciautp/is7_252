module App.Utils

open System
let dormirUnPoco() =
    System.Threading.Thread.Sleep 40

let displayMessage x y color (mensaje:string) =
    Console.SetCursorPosition(x,y)
    Console.ForegroundColor <- color
    Console.Write mensaje