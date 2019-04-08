module Login

open VCanopy.Functions
open TestTypes
open CanopyExtensions
open Header


let _email = css "input[name='ID']"
let _password = css "input[name='Password']"
let _loginButton = css "button[type='submit']"

let userLogin userCredentials = 
    printfn "about to login using %A" userCredentials
    goto Pages.Login.uri
    _email << userCredentials.userName
    _password << userCredentials.userPassword
    click _loginButton
    waitForElement Header._loggedAccountName // this is just to make sure we're on the profile page & we have logged

    // It might fail here on those 2 clicks as it may not show these messages for the user after user is no longer a new starter of this app
    // click "footer ul li a.shepherd-button-primary"
    // click "footer ul li a.shepherd-button-primary"


    // ToDo rewrite using XPATH
    // elementsWithText "footer ul li a.shepherd-button-primary" "NEXT" |> List.iter (fun element -> 
    //     printfn "clicked....."
    //     click element
    //     )
    // elementsWithText "footer ul li a.shepherd-button-primary" "DONE" |> List.iter (fun element -> 
    //     printfn "clicked....."
    //     click element
    //     )
    printfn "logged in using %A" userCredentials

let userLogout() = 
    click _accountButton
    click _accountLogout    
