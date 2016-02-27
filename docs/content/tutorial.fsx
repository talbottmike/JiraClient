(*** hide ***)
// This block of code is omitted in the generated HTML documentation. Use 
// it to define helpers that you do not want to show in the documentation.
#I "../../bin/JiraClient"

(**
Introducing JiraClient
======================

The following are some basic examples of how to use the library.

*)

#r "JiraClient.dll"
open JiraClient

let baseUri = "https://talbottmike.atlassian.net/rest/api/2/"
let credentials = "adminuser:notmypassword"
let client = Jira(baseUri, credentials)

// Create a new issue
let testIssue = { IssueRequest.Project="EA"; Summary="Test Summary"; Description="Test Desc"; Issuetype=3 }
let myIssue = client.CreateIssue testIssue

// Retrieve an existing issue
client.GetIssue "JIR-1"

// Delete an existing issue
client.DeleteIssue "JIR-1"

// Get issue metadata
client.AvailableFields

// Update an existing issue
client.UpdateIssue "" [("","")]

// Dump a rest response to file
client.Dump "issue/KB-2040"

(**
Some more info
*)
