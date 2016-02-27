namespace System
open System.Reflection

[<assembly: AssemblyTitleAttribute("JiraClient")>]
[<assembly: AssemblyProductAttribute("JiraClient")>]
[<assembly: AssemblyDescriptionAttribute("Library for interacting with the Atlassian Jira REST API")>]
[<assembly: AssemblyVersionAttribute("0.0.1")>]
[<assembly: AssemblyFileVersionAttribute("0.0.1")>]
do ()

module internal AssemblyVersionInformation =
    let [<Literal>] Version = "0.0.1"
