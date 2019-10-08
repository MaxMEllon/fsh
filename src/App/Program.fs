// Learn more about F# at http://fsharp.org

open System

let PrintPrompt = fun (currentPath: string) ->
  printf "%s > " currentPath

let rec forever f = 
  f()
  forever f

let rec append a b =
    match a, b with
    | [], ys -> ys
    | x::xs, ys -> x::append xs ys

type Env() =
  let mutable currentPath = System.Environment.GetEnvironmentVariable("HOME").Split("/") |> Array.toList
  member x.GetHome () =
    System.Environment.GetEnvironmentVariable("HOME")
  member x.GetCurrentPath () =
    String.Join ("/", currentPath |> List.toArray)
  member x.SetCurrentPath (path: string) =
    let modifyPath (p: string) = (append (currentPath) [p]) 

    for p in path.Split("/") do
      currentPath <- modifyPath p

let (|Prefix|_|) (p:string) (s:string) =
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
      | Prefix "cd " args -> env.SetCurrentPath args
      | _ -> printfn "%s" line