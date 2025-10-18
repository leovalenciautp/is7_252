//
//
// Segundo Parcial Programación I
//
//

//
// 1.a
//
//  Crear una función que tome un solo parámetro n (de tipo int)
//  y retorne una lista de n números impares, empezando en uno. (Obligatorio usar List)
//
open System
let generarNumerosImpares n =
    [1..n]
    |> List.map ( fun e -> 2*e-1)

//
// 1.b
// 
// Data la siguiente base de datos
//
let atlas = [
    "Colombia","Bogota",100
    "Francia","Paris", 2000
    "USA","Washington",10_000
    "Japon","Tokyo",4_000
]

//
// Crear un programa, que imprima el nombre de los paises ordenados por PIB (ultimo valor)
// de mayor a menor
//

atlas
|> Seq.sortBy (fun (_,_,pib) -> pib)
|> Seq.rev
|> Seq.iter (fun (pais,_,_) -> printfn $"{pais}")

//
// 2.a
//  Definir una base de datos bancaria, que contenga: Nombre del Banco, Moneda y Saldo,
//  La moneda SOLO puede tomar los valores de: Euros, Pesos, Dolares (USANDO RECORDS)
// 
//  Instanciar la base de datos, con al menos 3 elementos, que usen todas las monedas.
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
        Banco = "Bancolombia"
        Moneda = Pesos
        Saldo = 4_000_000_000m
    }
    {
        Banco = "Citi"
        Moneda = Dolares
        Saldo = 1_200_000m
    }
    {
        Banco = "UBS"
        Moneda = Euros
        Saldo = 1_000_000m
    }
]

// 2.b
//  Usando la base de datos anterior y la siguient tabla de conversion:
//
//  1 Dolar = 4000 Pesos
//  1 Euro = 1.18 Dolares
//
// Crear un programa, que liste la base de datos, ordenada por saldo en dolares, 
// de mayor a menor. Ese listado en pantalla debe estar en formato "pretty" (columnas)
// 
// En la ultima linea debe imprimir "Saldo Total en Dolares: " e imprimer el equivalente 
// total en dolares.

let convertirADolares moneda valor =
    match moneda with 
    | Dolares -> valor
    | Pesos -> valor/4_000m
    | Euros -> valor*1.18m

bancos
|> Seq.sortBy (fun r -> convertirADolares r.Moneda r.Saldo)
|> Seq.rev
|> Seq.iter (fun r -> printfn $"%-15s{r.Banco} %-15s{r.Moneda.ToString()} %15.0f{r.Saldo}")

bancos
|> Seq.map (fun r -> convertirADolares r.Moneda r.Saldo)
|> Seq.sum
|> fun total -> printfn $"Saldo Total en Dolares: {total}"

//
// 2.c
//
// Crear un programa, que maneje una base de datos de amigos. 
// Debe contenter al menos: Nombre, Apellido y Apodo. (USANDO RECORDS)
//
// No todos los amigos tiene apodo.
//
// El programa debe permitir ingresar los datos de un amigo, y agregarlos a la 
// base de datos (que arranca vacia). Preguntar si quiere agregar otro amigo
// y si no dease agregar amigos, debe listar la base de datos, por orden de Apellido
// y formatearla "pretty" (en columnas). Si un amigo no tiene apodo, debe salir "No Tiene"
//

type Amigo = {
    Nombre: string
    Apellido: string
    Apodo: string option
}

let obtenerDatosDeAmigo() =
    printf "Entra el nombre: "
    let nombre = Console.ReadLine()
    printf "Entra el apellido: "
    let apellido = Console.ReadLine()
    printf "Entra apodo (Enter si no tiene): "
    let apodo =
        match Console.ReadLine() with 
        | "" -> None
        | x -> Some x
    {
        Nombre = nombre
        Apellido = apellido
        Apodo = apodo
    }

let rec crearBaseDedatos lista =
    let amigo = obtenerDatosDeAmigo()
    let nuevaLista = amigo :: lista
    printf "Deseas agregar otro amigo(s/n)?: "
    match Console.ReadLine() with 
    | "n" -> nuevaLista
    | _ -> crearBaseDedatos nuevaLista

crearBaseDedatos []
|> Seq.sortBy (fun r -> r.Apellido)
|> Seq.iter (fun r ->
    let apodo = r.Apodo |> Option.defaultValue "No tiene"
    printfn $"%-15s{r.Nombre} %-15s{r.Apellido} %-15s{apodo}"
)