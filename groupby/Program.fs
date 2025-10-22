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


//
// Performance -> Rendimiento
//
// Memoria, velocidad
//
// Operaciones de lista, consumen mucho menos memoria que arrays
// pero son muy lentas.


// Nuance

//
// Crear una lista es bastante rapido
// insertar un elemento nuevo en una lista, es una operación
// Increiblmente rápida, que no depende del tamaño de la lista
//
// Array, acceder a un elemento del array, es increiblemente rapido
// y no depende del tamaño del array
//

// let e = bigArray[1_000_000_000] // Constant time
// let e2 = bigList[1_000_000_000] // O(n)

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

let rec generateNewRandoNumber usedNumberSet =
    let number = generador.Next(0,52)
    if usedNumberSet |> Set.contains number then
        generateNewRandoNumber usedNumberSet
    else
        number

let imprimirCarta (carta:Carta) =

    match carta with
    | K color -> $"K de {color}"
    | Q palo -> $"Q de {palo}"
    | J x -> $"J de {x}"
    | As palo -> $"As de {palo}"
    | Numero (x,palo) -> $"El {x} de {palo}"
    |> Console.WriteLine

let getRandomDeck() =
    [1..52]
    |> List.fold ( fun acc _ -> acc |> Set.add (generateNewRandoNumber acc) ) ([] |> set)

getRandomDeck()
|> Seq.map (fun i -> baraja[i])
|> Seq.iter imprimirCarta

//
// Cualquier funcion de busqueda de listas, o arrays
// es secuencial -> O(n)
//

//
// 1,2,3,4,5,6,7,8,9,10,11 -> binary search

//
// Hay que ordernar la lista primero
// List.sort o List.sortBy -> QuickSort iventando por Tonny Hoare
// [10,1,8,7,5.3]
//


//
// Google job interview.
//
// Busqueda en constant time.
// Hash map
//
// Hash function
//
let miHash (x:string) =
    //
    // hace magia con el string
    // y retorna un int
    //
    x.GetHashCode()


//
// Tarea para el miercoles. Como usar hash functions para
// crear busquedas en tiempo constante.
//
// Bitcoin -> proof of work
//
// 1111111111111110b


//
// Nuevas estructuras de Datos
//
// Set -> Conjunto, es una colleccion de elementos UNICOS
// Map (Mapa)


//
// En F# podemos usar dos tipos de Set
// - Un tipo funcional inmutable llamado Set, esta implementado como un binary tree.
// - busqueda es O(log(n))
//
// Dotnet tiene un Set
//  Esta implementado con hash functions.
//  Es un tipo de datos mutable.
//  La busqueda es O(1)

//
// Map es una estructura de datos fundamental, en muchos lenguajes se conoce
// como Dictionary.
// Es un tipos de datos asociativo, entre un key y un valor.
// Siempre es un par Key,Value pair
//

let a = [] |> set

let b = a |> Set.contains 20

let c = a |> Set.add 10


let baseDeDatos = [
    "leov@mail,com","Leonardo Valencia"
    "sebastian@utp","Sebastian Cardenas"
    "camila@utp","Camila Franco"
]

let m = baseDeDatos |> Map.ofList

let nuevaBases = baseDeDatos |> Map.add ("mariana@utp","Mariana Arenas")

type Amigo = {
    Nombre: string
    Apodo: string Option
}

let amigos = [
    123,
    {
        Nombre ="Leonardo"
        Apodo = None
    }
] 

let index = amigos |> Map.ofList

let nuevaBase = index |> Map.add (123,{Nombre="Santiago";Apodo=Some "WoL"})

type Moneda =
| Euros
| Dolares
| Pesos

type Cuenta = {
    Banco: string
    Moneda: Moneda
    Saldo: decimal
}

let bancos = [
    {
        Banco = "Bancolombia"
        Moneda= Pesos
        Saldo = 1000m 
    }
    {
        Banco = "Nequi"
        Moneda = Pesos
        Saldo = 5000m
    }
    {
        Banco = "Citi"
        Moneda = Dolares
        Saldo = 1000m
    }
    {
        Banco = "UBS"
        Moneda = Euros
        Saldo = 1000m
    }

]

let result = bancos |> List.groupBy ( fun e -> e.Moneda) |> Map.ofList

let pesos = result[Pesos] |> List.sumBy (fun e -> e.Saldo)
let dolares = result[Dolares] |> List.sumBy (fun e -> e.Saldo)
let euros = result[Euros] |> List.sumBy _.Saldo