#r "../../packages/FSharp.Data/lib/net40/FSharp.Data.dll"
#r "../../packages/Newtonsoft.Json/lib/net45/Newtonsoft.Json.dll"

open FSharp.Data
open FSharp.Data.HttpRequestHeaders
open System.Text
open System.Net
open Newtonsoft.Json
open System
open System.IO

Environment.CurrentDirectory <- __SOURCE_DIRECTORY__

let uriPart = "issue/JIR-1"
let baseUri = "https://talbottmike.atlassian.net/rest/api/2/"
let credentials = File.ReadAllText("../../PrivateCredentials.txt")

let encodedTicketCredentials (credentials:string) =
  let byteCredentials = UTF8Encoding.UTF8.GetBytes(credentials)
  Convert.ToBase64String(byteCredentials)

let requestHeader = [ "Authorization", "Basic " + encodedTicketCredentials credentials ]
let result = Http.RequestString(baseUri + uriPart, headers = requestHeader)
let jsonFormatted = Newtonsoft.Json.Linq.JValue.Parse(result).ToString(Formatting.Indented)
let fileName = "issue.json"
File.WriteAllText("../../sample-json/" + fileName, jsonFormatted)
