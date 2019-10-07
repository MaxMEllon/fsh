// Learn more about F# at http://fsharp.org

open System

let PrintPrompt = fun () ->
  printf "> "

let rec forever f = 
  f()
  forever f

[<EntryPoint>]
let main =
  forever <| fun () ->
    PrintPrompt()
    let line = Console.In.ReadLine()
    printfn "%s" line