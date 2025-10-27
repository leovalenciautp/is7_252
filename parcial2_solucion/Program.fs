open System
let atlas = [
    "Colombia","Bogota",100
    "USA","Whashington DC",10_000
    "Francia","Paris",4_000
    "Japon","Tokyo",8_000
]

//
// Pregunta 1.a
//
let ordenarBaseDeDatos lista =
    lista
    |> Seq.sortBy ( fun (_,_,pib) -> pib)
    |> Seq.iter (fun (nombre,_,_) -> printfn $"{nombre}")

atlas
|> ordenarBaseDeDatos

//
// Pregunta 1.b
//
type Amigo = {
    Nombre: string
    Apodo: string option
}

let obtenerDatosDeamigo() =
    printf "Entra el nombre: "
    let nombre = Console.ReadLine()
    printf "Entra el apodo: "
    let apodo =
        match Console.ReadLine() with
        | "" -> None
        | x -> Some x
    {
        Nombre = nombre
        Apodo = apodo
    }

let adicionarAmigo amigo lista =
   amigo :: lista

//
// Punto 2
//
type Moneda =
| Euros
| Dolares
| Pesos


type Cuentas = {
    Banco: string
    Moneda: Moneda
    Saldo: decimal
}

let bancos = [
    {
        Banco = "Banco A"
        Moneda = Pesos
        Saldo = 1_000_000m
    }
    {
        Banco = "Banco B"
        Moneda = Dolares
        Saldo = 1_000m
    }
    {
        Banco = "Banco C"
        Moneda = Euros
        Saldo = 1_000m
    }
]

let convertirADolares moneda valor =
    match moneda with 
    | Dolares -> valor
    | Pesos -> valor/4_000m
    | Euros -> 1.18m*valor

bancos
|> Seq.sortBy (fun r -> convertirADolares r.Moneda r.Saldo)
|> Seq.rev
|> Seq.iter (fun r -> 
    let moneda = 
        match r.Moneda with
        | Pesos -> "COP$"
        | Euros -> "€"
        | Dolares -> "$"
    printfn $"{r.Banco} {moneda}{r.Saldo}"
)

bancos
|> Seq.map ( fun r -> convertirADolares r.Moneda r.Saldo)
|> Seq.sum
|> Console.WriteLine