open System
open Fsh.Command

let PrintPrompt = fun (currentPath: string) ->
  printf "%s > " currentPath

let rec forever f = 
  f()
  forever f

type Env() =
  let mutable currentPath =
    System.Environment.GetEnvironmentVariable("HOME")

  member x.GetHome () =
    System.Environment.GetEnvironmentVariable("HOME")

  member x.GetCurrentPath () =
    currentPath

  member x.SetCurrentPath (path: string) =
    currentPath <- path

let (|Prefix|_|) (p: string) (s: string) =
    if s.StartsWith(p) then
        Some(s.Substring(p.Length))
    else
        None

[<EntryPoint>]
let main =
  let env = Env()
  forever <| fun () ->
    env.GetCurrentPath() |> PrintPrompt
    let line = Console.In.ReadLine()
    match line with
      | Prefix "cd " args -> (env.GetCurrentPath(), args) |> Cd.Move |> env.SetCurrentPath
      | Prefix "pwd " _ ->  env.GetCurrentPath() |> Pwd.ShowCurrentPath
      | _ -> printfn "%s" line