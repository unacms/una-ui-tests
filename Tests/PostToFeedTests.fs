module PostToFeedTests

open System
open CanopyExtensions
open canopy.runner.classic
open canopy.classic
open Common
open Header
open PostToFeed

let all () =

    context "PostToFeedtests"  

    once (fun _ -> 
        goto Pages.Login.uri
        Login._email << defaultAdmin.userName
        Login._password << defaultAdmin.userPassword
        click Login._loginButton
        waitForElement _loggedAccountName // this is just to make sure we're on the profile page & we have logged

        // It might fail here on those 2 clicks as it may not show these messages for the user after user is no longer a new starter of this app
        click _hintButton
        click _hintButton
        // elementsWithText "footer ul li a.shepherd-button-primary" "NEXT" |> List.iter (fun element -> 
        //     printfn "clicked....."
        //     click element
        //     )
        // elementsWithText "footer ul li a.shepherd-button-primary" "DONE" |> List.iter (fun element -> 
        //     printfn "clicked....."
        //     click element
        //     )
    )

    "Post 'Hello yyyyMMdd-hhmmss' to Feed -> adds 'Hello yyyyMMdd-hhmmss' message to feed" &&& fun _ ->        
        let now = DateTime.Now.ToString("yyyyMMdd-hhmmss")        
        let messageToPost = sprintf "Hello %s" now
        insertPostMessage messageToPost
        _visibility << "Public"
        //_publishAt << "2019-02-21 00:00"
        click _postButton
        _postedMessage == messageToPost
    
    lastly( fun _ -> 
        click _accountButton
        click _accountLogout
    )

