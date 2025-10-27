
open System


let width = Console.BufferWidth // Ancho del buffer de la consola
let height = Console.BufferHeight // Alto del buffer de la consola


let oldForeGround = Console.ForegroundColor
let oldBackground = Console.BackgroundColor
Console.ForegroundColor <- ConsoleColor.Yellow
Console.BackgroundColor <- ConsoleColor.DarkBlue
Console.Clear() // Limpia la consola

Console.SetCursorPosition(width-10,height/2)

Console.WriteLine "Hola Mundo"
Console.ReadLine() |> ignore
Console.ForegroundColor <- oldForeGround
Console.BackgroundColor <- oldBackground
Console.Clear()

let mostrarMensaje x y (mensaje:string)=
    Console.SetCursorPosition(x,y)
    Console.WriteLine mensaje

let borrarMensaje x y (mensaje:string)=
    Console.SetCursorPosition(x,y)
    [1..mensaje.Length]
    |> Seq.iter (fun _ -> Console.Write " ")



