
//
// Map/Reduce (massive parellelism)
//
// Google inventa el concepto y lo regala al mundo.
// 

open System

[1..1000]
|> Seq.map ( fun e -> 2*e)
|> Seq.sum // Reduce
|> Console.WriteLine

let rec factorial x acumulador =
    if x > 10 then
        acumulador
    else
        factorial (x+1) (x*acumulador)

let x = factorial 1 1

//
// List.fold -> toma un lambda y el valor initial del acumulador
// el lambda toma dos parametros  fun acc e
// acc es el acumulador actual, y e es el elemento de la lista
// debe retornar un nuevo valor para el acumulador
//
let factorialLista x =
    [1..x]
    |> List.fold (fun acc e -> e*acc ) 1 // Reduce se llama fold en F#

let sumaLista x =
    [1..x]
    |> List.fold (fun acc e -> e+acc) 0

//
// 0,1,1,2,3,5,8...
let fibonnacci x = 
    [1..x]
    |> List.fold (fun (a,b) _ -> b,a+b ) (0,1)

let _,n = fibonnacci 5
//
// Tarea modificar la funcion para retornar una lista de la sequencia
// de Fibonnaci