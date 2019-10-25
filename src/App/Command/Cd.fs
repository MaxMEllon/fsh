module Fsh.Command.Cd

open System
open System.IO

let Move (current: string, args: string) =
  let pathList = args.Split("/")
  let mutable copiedCurrent = current
  for path in pathList do
    match path.Trim() with
      | ".." ->
        let splited = copiedCurrent.Split("/")
        let len = splited.Length
        copiedCurrent <- String.Join ("/", splited.[0..(len-2)])
      | _ -> 
        let joinedPath = current + "/" + path
        if Directory.Exists joinedPath then
          copiedCurrent <- joinedPath
  copiedCurrent