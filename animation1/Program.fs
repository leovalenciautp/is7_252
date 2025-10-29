open System
open System.Threading

type State = {
    ForegroundColor: ConsoleColor
    BackgroundColor: ConsoleColor
    Width: int
    Height: int
}

let initSate() =
    {
        ForegroundColor = Console.ForegroundColor
        BackgroundColor = Console.BackgroundColor
        Width = Console.BufferWidth
        Height = Console.BufferHeight
    }

let restoreState state =
    Console.ForegroundColor <- state.ForegroundColor
    Console.BackgroundColor <- state.BackgroundColor


let displayMessage x y color (mensaje:string) =
    Console.ForegroundColor <- color
    Console.SetCursorPosition(x,y)
    Console.WriteLine mensaje



let dormirUnMomento() =
    Thread.Sleep 40

let waitForEnter() =
    Console.ReadLine() |> ignore



let state = initSate()

Console.BackgroundColor <- ConsoleColor.Blue
Console.Clear()

let centerX = state.Width/2-5
let centerY = state.Height/2-1

let animarMarciano() =
    [centerX .. -1 .. 1]
    |> Seq.iter ( fun x ->
        displayMessage x centerY ConsoleColor.Yellow "  "
        displayMessage (x-1) centerY ConsoleColor.Yellow "👽"
        dormirUnMomento()
    )


displayMessage centerX centerY ConsoleColor.Yellow "👽"

let rec esperarEscape() =
    let tecla = Console.ReadKey(true)
    match tecla.Key with
    | ConsoleKey.Escape ->
        printfn "El usuario presiono Escape!"
    | _ -> esperarEscape()

esperarEscape()

restoreState state
Console.Clear()


