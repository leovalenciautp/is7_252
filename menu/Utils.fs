module App.Utils

open System
let dormirUnPoco() =
    System.Threading.Thread.Sleep 40

let displayMessage x y color (mensaje:string) =
    Console.SetCursorPosition(x,y)
    Console.ForegroundColor <- color
    Console.Write mensaje

let printLetra x y color letra =
    letra
    |> Array.iteri ( fun i line -> displayMessage x (y+i) color line )

let displayMessageGigante x y color (mensaje:string) =
    mensaje.ToUpper()
    |> Seq.map Letras.econtrarLetra
    |> Seq.iteri (fun i letra -> printLetra (x+i*7) y color letra )

    
