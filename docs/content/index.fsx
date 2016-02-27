(*** hide ***)
// This block of code is omitted in the generated HTML documentation. Use 
// it to define helpers that you do not want to show in the documentation.
#I "../../bin/JiraClient"

(**
JiraClient
======================

Documentation

<div class="row">
  <div class="span1"></div>
  <div class="span6">
    <div class="well well-small" id="nuget">
      The JiraClient library can be <a href="https://nuget.org/packages/JiraClient">installed from NuGet</a>:
      <pre>PM> Install-Package JiraClient</pre>
    </div>
  </div>
  <div class="span1"></div>
</div>

Example
-------

This example demonstrates using a GetIssue to retrieve a Jira issue.

*)
#r "JiraClient.dll"
open JiraClient

let baseUri = "https://talbottmike.atlassian.net/rest/api/2/"
let credentials = "adminuser:notmypassword"
let client = Jira(baseUri, credentials)
client.GetIssue "JIR-1"

(**
Some more info

Samples & documentation
-----------------------

The library comes with comprehensible documentation. 

 * [Tutorial](tutorial.html) contains a further explanation of this sample library.

 * [API Reference](reference/index.html) contains automatically generated documentation for all types, modules
   and functions in the library.
 
Contributing and copyright
--------------------------

The project is hosted on [GitHub][gh] where you can [report issues][issues], fork 
the project and submit pull requests. If you're adding a new public API, please also 
consider adding [samples][content] that can be turned into a documentation. You might
also want to read the [library design notes][readme] to understand how it works.

The library is available under Public Domain license, which allows modification and 
redistribution for both commercial and non-commercial purposes. For more information see the 
[License file][license] in the GitHub repository. 

  [content]: https://github.com/fsprojects/JiraClient/tree/master/docs/content
  [gh]: https://github.com/fsprojects/JiraClient
  [issues]: https://github.com/fsprojects/JiraClient/issues
  [readme]: https://github.com/fsprojects/JiraClient/blob/master/README.md
  [license]: https://github.com/fsprojects/JiraClient/blob/master/LICENSE.txt
*)
