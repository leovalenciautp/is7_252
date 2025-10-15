open System
open System.IO
open System.Text.Json

let rec obtenerEntero (mensaje:string) =
    printf $"{mensaje}"
    let r = Console.ReadLine()
    match Int32.TryParse r with
    | true, x -> x
    | false, _ ->
        printfn "Entrada Invalida"
        obtenerEntero mensaje

let rec obtenerDecimal (mensaje:string) =
    printf $"{mensaje}"
    let r = Console.ReadLine()
    match Decimal.TryParse r with
    | true, x -> x
    | false, _ ->
        printfn "Entrada Invalida"
        obtenerDecimal mensaje


let tablaMultiplicar factor x =
    printfn $"{factor}x{x}={factor*x}"

// let factor = obtenerEntero "Cual tabla quieres?: "

// [1..10]
// |> Seq.iter (tablaMultiplicar factor)




//
// Hint,
// List.takeWhile()
//

let generador = new Random()

let adivinar ganador (i:int) =
    let guess = obtenerEntero "Entra un numero: "
    guess = ganador,i

// [1..3]
// |>Seq.map (adivinar <| generador.Next(1,11))
// |>Seq.takeWhile (fun e ->
//     match e with 
//     | true,_  ->   
//         printfn "Felicitaciones Ganaste!"
//         false
//     | _,i ->
//         if i = 3 then 
//             printfn "GAME OVER"
//         else
//             printfn "Incorrecto, intenta de nuevo"
//         true
// )
// |> Seq.toList
// |> ignore

let obtenerDolarDeInternet() =
    if generador.NextDouble() <= 0.8 then
        Some 4_000m
    else
        None
    
let convertirAPesos x =
    obtenerDolarDeInternet()
    |> Option.map (fun dolar -> dolar*x)


// [1..20]
// |>List.map (fun _ -> obtenerDolarDeInternet())
// |>List.iter (fun o ->
//     match o with
//     | Some x -> printfn $"Dollar a {x}"
//     | None -> printfn "ERROR DE INTERNET!"
// )

//
// Crear un programa, que usa una base de datos de amigos
// 
// Nombre, email, GamerTag
//
let amigos = [
    "Vitamina","vit@gmail.com",None
    "Antonio","ant@gmail.com", Some "AsmonGold"
    "Julia", "julia@gmail.com",Some "VenusFighter"
    "Eli", "eli@gmail.com", None
] 

//
// Funcion, para obtener una lista de amigos sin GamerTag
//

let encontrarAmigosSinGamerTag lista =
    lista
    |> Seq.filter (fun (_,_,gamerTag) -> gamerTag |> Option.isNone)

// amigos
// |> encontrarAmigosSinGamerTag
// |> Seq.iter (fun (nombre,email,_) -> printfn $"{nombre} {email}" )

//
// Buscar a los amigos que si tiene gamertag
// Order alfabeticamente por nombre de amigo
// Imprimir en pantall solo el email
//
// ()
// 10X Programmer

// amigos
// |> Seq.filter (fun (_,_,g) -> g |> Option.isSome)
// |> Seq.sortBy (fun (nombre,_,_) -> nombre)
// |> Seq.iter (fun (_,email,_)-> printfn $"{email}")


type Idiomas =
| Español
| English
| Francois
| Dutch

//
// Records
//
type Amigos = { // Curly brace
    Nombre: string // This is a "field"
    Email: string
    GamerTag: string option
    Telefono: decimal
}

type State = {
    BaseDeDatos: Amigos list
}

//
// UX
// CLI Command Language Interface
//

// Refactoring
let obtenerDatosDeAmigo() =
    printf "Entra el nombre: "
    let nombre = Console.ReadLine()
    printf "Entra el emai: "
    let email = Console.ReadLine()
    printf "Gamertag (presione Enter si no tiene): "
    let r = Console.ReadLine()
    let gamerTag =
        if r = "" then 
            None
        else
            Some r
    let telefono = obtenerDecimal "Entra el telefono: "

    {
        Nombre = nombre
        Email = email
        GamerTag = gamerTag
        Telefono = telefono
    }
let rec crearBaseDeDatosDeAmigos lista =
    
    let amigo = obtenerDatosDeAmigo()
    let nuevaLista = amigo :: lista

    printf "Quieres agregar otro amigo (s/n): "
    let c = Console.ReadLine()
    if c = "n" then
        nuevaLista
    else
        crearBaseDeDatosDeAmigos nuevaLista


let nombreArchivo = "amigos.txt"
let obtenerBaseDeDatosDeAmigos() =
    // Chequear que el archivo exista
    if File.Exists nombreArchivo then
        let text = File.ReadAllText nombreArchivo
        JsonSerializer.Deserialize<Amigos list> text
    else
        let baseDeDatos=crearBaseDeDatosDeAmigos []
        let json = JsonSerializer.Serialize baseDeDatos
        File.WriteAllText(nombreArchivo,json)
        baseDeDatos
        
let imprimirAmigo amigo =
    let gamerTag =
            match amigo.GamerTag with
            | Some x -> x
            | None -> "No Tiene (loser)"

    printfn $"%-10s{amigo.Nombre} %-20s{amigo.Email} %-20s{gamerTag} %-15.0f{amigo.Telefono}"

let imprimirHeader()=
    let nombre = "Nombre"
    let email = "Email"
    let gamertag = "Gamertag"
    let telefono = "Teléfono"
    printfn $"%-10s{nombre} %-20s{email} %-20s{gamertag} %-15s{telefono}"
let listarAmigos lista =
    imprimirHeader()
    lista
    |> Seq.sortBy (fun r -> r.Nombre)
    |> Seq.iter imprimirAmigo

let encontrarUnAmigo lista =
    printf "Nombre de amigo: "
    let nombre = Console.ReadLine()
    lista |> List.tryFind (fun r -> r.Nombre = nombre)

let mostrarAmigos lista =
    match encontrarUnAmigo lista with 
    | Some amigo -> 
        imprimirHeader()
        imprimirAmigo amigo
    | None -> printfn "No se encontró el amigo"

let borrarBaseDeDatos() =
    File.Delete nombreArchivo

let guardarBaseDeDatos (baseDeDatos:Amigos list) =
    File.WriteAllText(nombreArchivo, baseDeDatos |> JsonSerializer.Serialize)

let borrarUnAmigo amigo lista =
    lista
    |> List.filter (fun r -> r.Nombre <> amigo.Nombre)

let borrarAmigo lista =
    match encontrarUnAmigo lista with
    | None -> 
        printfn "Ese amigo no existe"
        lista
    | Some amigo -> 
        let nuevaLista = lista |> borrarUnAmigo amigo
        guardarBaseDeDatos nuevaLista
        nuevaLista 



let añadirAmigo lista =
    let amigo = obtenerDatosDeAmigo()
    let nuevaLista = amigo :: lista
    guardarBaseDeDatos nuevaLista
    nuevaLista


//
// actualizarDato toma los siguientes parametros
//  nombreCampo -> String a mostrar en pantalla
//  valorAnterior -> valor usado por defecto si no se cambia
//  presentador -> funcion que toma el dato nativo y retorna un string para presentar en pantalla
//  conversor -> funcion que toma un string y retorna dato nativo
//
let rec actualizarDato nombreCampo (valorAnterior:'a) (presentador:'a->string) (conversor:string->Result<'a,string>) =
    let x = presentador valorAnterior
    printf $"{nombreCampo}: {x} -> "
    let valorNuevo = Console.ReadLine()
    if valorNuevo = "" then
        valorAnterior
    else
        match conversor valorNuevo with
        | Ok x -> x
        | Error m ->
            printfn $"{m}"
            actualizarDato nombreCampo valorAnterior presentador conversor

let actualizarDatoString nombreCampo (valorAnterior:string) =
    actualizarDato nombreCampo valorAnterior (fun x -> x) ( fun x -> Ok x)

let presentarOpcion (o:'a option) =
    match o with 
    | Some x -> $"{x}"
    | None -> "No Tiene"

let convertirGamertag cadena =
    if cadena = "-" then
        Ok None
    else
        Ok (Some cadena)

let rec convertirADecimal (mensaje:string) =
    match Decimal.TryParse mensaje with
    | true, x -> Ok x
    | false, _ -> Error "No es un número válido"
let modificarUnAmigo amigo =
    let nuevoNombre = actualizarDatoString "Nombre" amigo.Nombre
    let nuevoEmail = actualizarDatoString "Email" amigo.Email
    let nuevoGamerTag = actualizarDato "Gamertag (- para borrar)" amigo.GamerTag presentarOpcion convertirGamertag
    let nuevoTelefono = actualizarDato "Telefono" amigo.Telefono (fun x -> $"{x}") convertirADecimal
    {
        Nombre = nuevoNombre
        Email = nuevoEmail
        GamerTag = nuevoGamerTag
        Telefono = nuevoTelefono
    }


let modificarAmigo lista =
    let o = encontrarUnAmigo lista
    match o with
    | None ->
        printfn "No existe ese amigo"
        lista
    | Some amigo ->
        let nuevoAmigo = modificarUnAmigo amigo
        if nuevoAmigo <> amigo then 
            let nuevaLista = borrarUnAmigo amigo lista
            let listaFinal = nuevoAmigo :: nuevaLista
            guardarBaseDeDatos listaFinal
            listaFinal
        else
            lista

type ModificarStatus =
| Guardar
| Cancelar

let rec modificarAmigoMenu amigo =
    let gamertag = amigo.GamerTag |> Option.defaultValue "No tiene"
    printfn $"1. Nombre: {amigo.Nombre}"
    printfn $"2. Email: {amigo.Email}"
    printfn $"3. Gamertag: {gamertag}"
    printfn $"4. Teléfono: {amigo.Telefono}"
    printfn "5. Guardar y salir"
    printfn "6. Cancelar y salir"
    printfn ""
    printf "Elije el campo a modificar: "
    let o = Console.ReadLine()
    let nuevoAmigo =
        match o with 
        | "1" ->
            printf "Entra un nuevo nombre: "
            let nombre = Console.ReadLine()
            if nombre = "" then 
                amigo
            else
                {amigo with Nombre = nombre}
        | "2" ->
            printf "Entra un nuevo email: "
            let email = Console.ReadLine()
            if email = "" then 
                amigo
            else
                {amigo with Email = email}
        | "3" ->
            printf "Entra un nuevo GamerTag: "
            let gamertag = Console.ReadLine()
            match gamertag with
            | "" -> amigo
            | "-" -> {amigo with GamerTag=None}
            | x -> {amigo with GamerTag= Some x}

        | "4" ->
            let telefono=  obtenerDecimal "Entra nuevo teléfono: "
            {amigo with Telefono=telefono}
        | _ ->
            amigo
    if o <> "5" && o <> "6" then
        modificarAmigoMenu nuevoAmigo
    else
        if o = "5" then 
            Guardar, nuevoAmigo
        else
            Cancelar, amigo
        

let modificarAmigoAlternativo lista =
    let o = encontrarUnAmigo lista
    match o with
    | None ->
        printfn "No existe ese amigo"
        lista
    | Some amigo ->
        let r = modificarAmigoMenu amigo
        match r with 
        | Guardar, nuevoAmigo ->
            let nuevaLista = borrarUnAmigo amigo lista
            let listaFinal = nuevoAmigo :: nuevaLista
            guardarBaseDeDatos listaFinal
            listaFinal
        | Cancelar,_ ->
            lista

let rec mostrarMenu estado =
    printfn ""
    printfn "Bienvenido a la Base de Datos De Amigos"
    printfn "1. Listar amigos"
    printfn "2. Mostrar un solo amigo"
    printfn "3. Modificar amigo"
    printfn "4. Añadir amigo"
    printfn "5. Borrar amigo"
    printfn "X. Salir"
    printfn ""
    printfn ""
    printf "Entra tu opcion: "
    let o = Console.ReadLine()
    printfn ""
    let nuevoEstado =
        match o with
        | "1" -> 
            listarAmigos estado.BaseDeDatos
            estado
        | "2" -> 
            mostrarAmigos estado.BaseDeDatos
            estado
        | "3" -> 
            {estado with BaseDeDatos = modificarAmigoAlternativo estado.BaseDeDatos}
        | "4" -> 
            let nuevaBaseDeDatos= añadirAmigo estado.BaseDeDatos
            {estado with BaseDeDatos = nuevaBaseDeDatos} // Copy and update
        | "5" ->
            {estado with BaseDeDatos = borrarAmigo estado.BaseDeDatos}
        | _  -> 
            if o <> "X" then
                Console.WriteLine "\a" 
                Console.WriteLine "Opción inválida"
            estado

    if o <> "X" then 
        mostrarMenu nuevoEstado

//
// Este es un ejemplo de CRUD
//
obtenerBaseDeDatosDeAmigos()
|> fun baseDeDatos -> {BaseDeDatos = baseDeDatos}
|> mostrarMenu

//
// FLOW -> Presentar un Menu de los fields a cambiar
// debe tener Salir y guardar
// y tambien Salir sin guardar

//
// Github -> empresa de Microsoft
//

// Comentario innecesario para el program






