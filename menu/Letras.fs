module App.Letras

let letraA =
    [|
        " ████ "
        "█    █ "
        "█    █"
        "██████"
        "█    █"
        "█    █"
    |]


let letraB =
    [|
        "█████ "
        "█    █"
        "█████"
        "█    █"
        "█    █"
        "█████ "
    |]

let letraC =
    [|
        " ████ "
        "█    █"
        "█     "
        "█     "
        "█    █"
        " ████"
    |]

let letraD =
    [|
        "████  "
        "█   █ "
        "█    █"
        "█    █"
        "█   █"
        "████"
    |]

let letraE =
    [|
        "██████"
        "█"
        "█████"
        "█"
        "█"
        "██████"
    |]

let letraF =
    [|
        "██████"
        "█"
        "█████"
        "█"
        "█"
        "█"
    |]

let letraG =
    [|
        " ████ "
        "█    █"
        "█     "
        "█   ██"
        "█    █"
        " ████"
    |]

let letraH =
    [|
        "█    █"
        "█    █"
        "██████"
        "█    █"
        "█    █"
        "█    █"
    |]

let letraI =
    [|
        "  ███ "
        "   █  "
        "   █"
        "   █"
        "   █"
        "  ███"
    |]

let letraJ =
    [|
        "   ███"
        "    █ "
        "    █ "
        "█   █"
        "█   █"
        " ███"
    |]

let letraK =
    [|
        "█   █"
        "█  █"
        "███"
        "█  █"
        "█   █"
        "█    █"  
    |]

let letraL =
    [|
        "█"
        "█"
        "█"
        "█"
        "█"
        "██████"
    |]

let letraM =
    [|
        "█    █"
        "██  ██"
        "█ ██ █"
        "█    █"
        "█    █"
        "█    █"
    |]

let letraN =
    [|
        "█    █"
        "██   █"
        "█ █  █"
        "█  █ █"
        "█   ██"
        "█    █"
    |]

let letraO =
    [|
        " ████ "
        "█    █"
        "█    █"
        "█    █"
        "█    █"
        " ████"
    |]

let letraP =
    [|
        "█████ "
        "█    █"
        "█    █"
        "█████"
        "█"
        "█"
    |]

let letraQ =
    [|
        " ████ "
        "█    █"
        "█    █"
        "█  █ █"
        "█   ██"
        " ████"
    |]

let letraR =
    [|
        "█████ "
        "█    █"
        "█    █"
        "█████"
        "█   █"
        "█    █"
    |]

let letraS =
    [|
        " █████"
        "█"
        " ████"
        "     █"
        "     █"
        "█████"
    |]

let letraT =
    [|
        "█████ "
        "  █"
        "  █"
        "  █"
        "  █"
        "  █"
    |]

let letraU =
    [|
        "█    █"
        "█    █"
        "█    █"
        "█    █"
        "█    █"
        " ████"  
    |]

let letraV =
    [|
        "█    █"
        "█    █"
        "█    █"
        "█    █"
        " █  █"
        "  ██ "  
    |]

let letraW =
    [|
        "█    █"
        "█    █"
        "█    █"
        "█    █"
        "█ ██ █"
        " █  █ "  
    |]

let letraX =
    [|
        "█    █"
        " █  █ "
        "  ██  "
        "  ██  "
        " █  █ "
        "█    █"
    |]

let letraY =
    [|
        "█   █ "
        " █ █  "
        "  █   "
        "  █   "
        "  █   "
        "  █   "
    |]

let letraZ =
    [|
        "██████"
        "    █ "
        "   █"
        "  █"
        " █"
        "██████"
    |]

let letraSpc =
    [|
        "      "
        "      "
        "      "
        "      "
        "      "
        "      "
    |]

let letraBang =
    [|
        "   █ "
        "   █"
        "   █"
        "   █"
        "     "
        "   █"
    |]



let mapaDeletras = 
    [
        'A',letraA
        'B',letraB
        'C',letraC
        'D',letraD
        'E',letraE
        'F',letraF
        'G',letraG
        'H',letraH
        'I',letraI
        'J',letraJ
        'K',letraK
        'L',letraL
        'M',letraM
        'N',letraN
        'O',letraO
        'P',letraP
        'Q',letraQ
        'R',letraR
        'S',letraS
        'T',letraT
        'U',letraU
        'V',letraV
        'W',letraW
        'X',letraX
        'Y',letraY
        'Z',letraZ
        ' ',letraSpc
        '!',letraBang
    ]
    |> Map.ofList


let econtrarLetra letra =
    mapaDeletras
    |> Map.tryFind letra
    |> Option.defaultValue letraSpc

