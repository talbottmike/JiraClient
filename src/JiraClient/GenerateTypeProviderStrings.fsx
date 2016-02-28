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

let credentials = File.ReadAllText("../../PrivateCredentials.txt")

let encodedTicketCredentials (credentials:string) =
  let byteCredentials = UTF8Encoding.UTF8.GetBytes(credentials)
  Convert.ToBase64String(byteCredentials)

let requestHeader = [ "Authorization", "Basic " + encodedTicketCredentials credentials ]
let baseUri = "https://talbottmike.atlassian.net/rest/api/2/"
let request uri = Http.RequestString(baseUri + uri, headers = requestHeader)
let formatJson apiResult = Newtonsoft.Json.Linq.JValue.Parse(apiResult).ToString(Formatting.Indented)
let dumpToFile fileName jsonString = File..WriteAllText("../../sample-json/" + fileName, jsonString)
let getToFile uri fileName =
  uri
  |> request
  |> formatJson
  |> dumpToFile fileName

// Get a single issue
getToFile "issue/JIR-1" "issue.json"

// Get a project
getToFile "project/JIR" "project.json"

// Get all projects
getToFile "project" "projects.json"

// Get issue comments
getToFile "issue/JIR-1/comment" "comments.json"

// Get edit issue meta
getToFile "issue/JIR-1/editmeta" "issueeditmeta.json"

// Get issue transitions
getToFile "issue/JIR-1/transitions" "issuetransitions.json"

// Get issue watchers
getToFile "issue/JIR-1/watchers" "issuewatchers.json"

// Get issue worklog
getToFile "issue/JIR-1/worklog" "issueworklog.json"

// Get create issue meta
getToFile "issue/createmeta" "issuecreatemeta.json"

// Get all issue types
getToFile "issuetype" "issuetypes.json"

// Get myself
getToFile "myself" "myself.json"

// Get priorities
getToFile "priority" "priorities.json"

// Get resolutions
getToFile "resolution" "resolutions.json"

// Get statuses
getToFile "status" "statuses.json"

// Get user
getToFile "user?talbottmike" "user.json"

//type xmlProvider = FSharp.Data.XmlProvider<"https://docs.atlassian.com/jira/REST/ondemand/jira-rest-plugin.wadl">
Http.RequestString("https://docs.atlassian.com/jira/REST/ondemand/jira-rest-plugin.wadl")
|> dumpToFile "wadl.xml"
