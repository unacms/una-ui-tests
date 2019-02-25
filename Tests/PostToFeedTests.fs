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
        Login.userLogin defaultAdmin
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
        Login.userLogout()
    )

