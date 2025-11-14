module App.Types


type GameCommand =
| NewGame
| LoadGame
| Exit

type PauseCommand =
| ContinueGame
| SaveGame
| Exit

type GameOverCommand=
| NewGame
| Exit


