module JiraClient.Tests

open FSharp.Data
open JiraClient
open NUnit.Framework

[<Test>]
let ``fieldJson succeeds`` () =
  let expected = """{"summary": "new summary"}"""
  let actual = UpdateField.Summary "new summary" |> (fun x -> x.ToUpdateParameter) |> JsonHelpers.fieldJson
  Assert.AreEqual(expected,actual)

[<Test>]
let ``updateJson succeeds`` () =
  let expected = """{ "fields": {"summary": "new summary"} }"""
  let actual = JsonHelpers.updateJson [UpdateField.Summary "new summary"]
  Assert.AreEqual(expected,actual)

[<Test>]
let ``updateJson with multiple fields succeeds`` () =
  let expected = """{ "fields": {"summary": "new summary"},{"description": "new description"} }"""
  let actual = JsonHelpers.updateJson [UpdateField.Summary "new summary"; UpdateField.Description "new description"]
  Assert.AreEqual(expected,actual)
