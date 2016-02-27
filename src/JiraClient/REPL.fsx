// Script used for REPL testing/development
#r "../../packages/FSharp.Data/lib/net40/FSharp.Data.dll"
#r "../../packages/Newtonsoft.Json/lib/net45/Newtonsoft.Json.dll"

open FSharp.Data
open FSharp.Data.HttpRequestHeaders
open System.Text
open System.Net
open Newtonsoft.Json
open System
open System.IO

#load "Jira.fs"

open JiraClient

type UpdateField =
  Summary of string
  | Description of string

let toUpdateParameter update =
    match update with
    | Summary s -> ("summary", s)
    | Description s -> ("description", s)

let updateJson (field, value) =
  sprintf "{ \"fields\": {\"" + field + "\": \"" + value + "\"} }"

UpdateField.Description "Foo"
|> toUpdateParameter
|> updateJson
|> printfn "%s"
