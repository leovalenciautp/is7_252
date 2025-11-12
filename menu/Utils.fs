module App.Utils

open System
let dormirUnPoco() =
    System.Threading.Thread.Sleep 40

let displayMessage x y color (mensaje:string) =
    Console.SetCursorPosition(x,y)
    Console.ForegroundColor <- color
    Console.Write mensaje

type Letra = {
    Lineas: string array
}

//  ** 
// *  *
//*    *
//******
//*    *
//*    *

let letraA =
    {
        Lineas = [|
            " ████ "
            "█    █ "
            "█    █"
            "██████"
            "█    █"
            "█    █"
        |]
    }

let letraB =
    {
        Lineas = [|
            "█████ "
            "█    █"
            "█████"
            "█    █"
            "█    █"
            "█████ "
        |]
    }


let letras = [
    'A',letraA
    'B',letraB
]

let mapaDeLetras = letras |> Map.ofList


let printLetra letra =
    letra.Lineas 
    |> Array.iteri ( fun i line -> displayMessage 10 (10+i) ConsoleColor.Red line )

let displayMessageGigante x y color (mensaje:string) =
    mensaje
    |> Seq.iter (fun c -> ())
    
