namespace JiraClient

open FSharp.Data
open FSharp.Data.HttpRequestHeaders
open System.Text
open System.Net
open Newtonsoft.Json
open System
open System.IO

/// FSharp Json Type Provider for parsing the issue response from the Jira REST API
type IssueProvider = FSharp.Data.JsonProvider<"../../sample-json/issue.json", EmbeddedResource="JiraClient, issue.json">

//let issue = IssueProvider.GetSample()

/// Used to provide required parameters for creating a new issue.
type IssueRequest = {Project:string; Summary:string; Description:string; Issuetype:int}

// Union type to represent the updatable fields on an issue
type UpdateField =
  Summary of string
  | Description of string with
    member x.ToUpdateParameter =
      match x with
      | Summary s -> ("summary", s)
      | Description s -> ("description", s)

/// Used to map public types to a serializable format that will produce the expected Json for the REST API
module internal SerializableType =

  type SProject = {Key:string}
  type SIssuetype = {Id:int}
  type IssueFields = {Project:SProject;Summary:string;Description:string;Issuetype:SIssuetype}
  type IssueRequest = {Fields:IssueFields;}

[<AutoOpen>]
module JsonHelpers =
  let fieldJson (field, value) =
    sprintf "{\"" + field + "\": \"" + value + "\"}"

  let updateJson (updates: UpdateField seq) =
    let fieldsJson =
      updates
      |> Seq.map (fun x -> x.ToUpdateParameter)
      |> Seq.map fieldJson

    sprintf "{ \"fields\": " + String.Join(",", fieldsJson) + " }"

/// Creates a Jira Client for the provided URI and Credentials
type Jira (baseUri, credentials:string) =

  let encodedTicketCredentials =
    let byteCredentials = UTF8Encoding.UTF8.GetBytes(credentials)
    Convert.ToBase64String(byteCredentials)

  let requestHeader = [ "Authorization", "Basic " + encodedTicketCredentials ]
  let postHeader = requestHeader |> List.append [ContentType HttpContentTypes.Json]

  let transformRequest (request:IssueRequest) = 
    let project = { SerializableType.SProject.Key = request.Project; }
    let issueType = { SerializableType.SIssuetype.Id = request.Issuetype; }

    { SerializableType.IssueRequest.Fields=
      { Project= project
        Summary = request.Summary;
        Description = request.Description;
        Issuetype = issueType
      }
    }
  
  /// Gets an issue for the provided key
  member __.GetIssue key =
    let i = Http.RequestString(baseUri + "issue/"+ key , headers = requestHeader)
    IssueProvider.Parse(i)

  /// Creates an issue for the provided request
  member __.CreateIssue (request:IssueRequest) =
    let formattedRequest = transformRequest request
    let json = JsonConvert.SerializeObject(formattedRequest, JsonSerializerSettings(ContractResolver = Serialization.CamelCasePropertyNamesContractResolver()))
    Http.Request(baseUri + "issue/",headers = postHeader,body = TextRequest json )

  /// Deletes an issue for the provided key
  member __.DeleteIssue key =
    Http.Request(baseUri + "issue/" + key,headers = postHeader,customizeHttpRequest = fun req -> req.Method <- "DELETE"; req )

  /// Returns available field metadata for issues
  member __.AvailableFields =
    Http.RequestString(baseUri + "issue/createmeta", headers = requestHeader)

  /// Updates an issue with the provided fields and values
  member __.UpdateIssueField key update =
    let json = updateJson [update]
    Http.Request(baseUri + "issue/" + key, headers = postHeader,body = TextRequest json,customizeHttpRequest = fun req -> req.Method <- "PUT"; req )

  /// Updates an issue with the provided fields and values
  member __.UpdateIssueFields (key, updates) = // TODO finish this function
    let json = updateJson updates
    Http.Request(baseUri + "issue/" + key, headers = postHeader,body = TextRequest json,customizeHttpRequest = fun req -> req.Method <- "PUT"; req )

  // Custom request
  member __.CustomRequest uriPart =
    Http.RequestString(baseUri + uriPart, headers = requestHeader)