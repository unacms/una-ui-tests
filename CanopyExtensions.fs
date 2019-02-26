module CanopyExtensions

open canopy
open canopy.classic
open OpenQA.Selenium

let private _placeholder value = sprintf "[placeholder = '%s']" value
let placeholder value = _placeholder value |> css

let private _name value = sprintf "[name = '%s']" value
let name value = _name value |> css

let private _options value = sprintf "select[name = '%s'] option" value
let options value = _options value |> css

let private _data_qa_name value = sprintf "[data-qa-name = '%s']" value
let data_qa_name value = _data_qa_name value |> css


let goto uri = url uri

let on url =
  let message = sprintf "Validating on page %s" url
  waitFor2 message (fun _ -> currentUrl().Contains(url))

let optionsToInts selector =
  elements selector
  |> List.map(fun element -> element.GetAttribute("value"))
  |> List.filter (fun value -> value <> "")
  |> List.map(fun value -> int value)
  |> List.sort

let firstOption selector = optionsToInts selector |> List.head |> string
let recentOption selector = optionsToInts selector |> List.rev |> List.head |> string

// it might happen you need to scroll to an element as it may ocasionaly fail if element is at the bottom of the screen
let scrollTo selector =
// .getBoundingClientRect()
// window.scrollTo(500, 0);
    canopy.classic.waitForElement selector
    let e = canopy.classic.element selector
    let executor = canopy.classic.browser :?> IJavaScriptExecutor
    executor.ExecuteScript("var posBefore=window.scrollY;arguments[0].scrollIntoView(); if (posBefore == window.scrollY){console.log('alternative way to scroll');window.scrollTo(arguments[0].getBoundingClientRect().top, 0) }", e) |> ignore