//
// let (rec)
// if..then..else
// match...with
// fun
// |> (syntactic sugar)
// (,,)
// type -> Discriminated Unions, records
// ::  CONS @ concat

//
// Estructuras de Datos
// Tuplas
// Records
// Listas
// Sequences (que son lazy, y se llaman iterables)

//
// Arrays versus List
//
// 

open System
let lista = [1;2;3]
let leoArray = [|1;2;3|]

leoArray
|> Array.iter Console.WriteLine

//
// Performance -> Rendimiento
//
// Memoria, velocidad
//
// Operaciones de lista, consumen mucho menos memoria que arrays
// pero son muy lentas.

let bigList = [1..2_000_000_000]
let ultimo = bigList |> List.last

let nuevaLista = 10 :: bigList
let bigArray = bigList |> List.toArray
let ultimoArray = bigArray |> Array.last


// Nuance

//
// Crear una lista es bastante rapido
// insertar un elemento nuevo en una lista, es una operación
// Increiblmente rápida, que no depende del tamaño de la lista
//
// Array, acceder a un elemento del array, es increiblemente rapido
// y no depende del tamaño del array
//

let e = bigArray[1_000_000_000] // Constant time
let e2 = bigList[1_000_000_000] // O(n)

//
//Array, guarda los elementos un solo bloque de memoria
//
// ______________________
// +        1           +
// ----------------------
// ______________________
// +         2          +
// ----------------------
// ______________________
// +          3         +
// ----------------------
// ______________________
// +         4          +
// ----------------------

//
// Lista guarda la informacion en bloque no contiguos
//

//
// [1] -> [2] -> [3] ... [2_0000_000_000]
//
//
// [10] -> [1]

type Pinta =
| Corazones
| Diamantes
| Picas
| Treboles

type Carta =
| As of Pinta
| Q of Pinta
| K of Pinta
| J of Pinta
| Numero of int*Pinta

let carta = Numero (8,Corazones)

let baraja = [|
    //
    // List comprenhensions
    //
    for pinta in [Corazones;Diamantes;Picas;Treboles] do
        for valor in 2..10 do Numero(valor,pinta)
        As pinta
        Q pinta
        K pinta
        J pinta //Syntactic sugar
|]

let generador = new Random()

let nuevaCarta = baraja[generador.Next(0,52)]
