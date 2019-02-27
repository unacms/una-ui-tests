module PostToFeedTests

open System
open CanopyExtensions
open canopy.runner.classic
open canopy.classic
open Common
open Header
open PostToFeed
open canopy

let all () =

    context "PostToFeedtests"  

    once (fun _ -> 
        Login.userLogin defaultAdmin
    )

    lastly( fun _ -> 
        Login.userLogout()
    )

    "Post 'Hello yyyyMMdd-hhmmss' to Feed -> adds 'Hello yyyyMMdd-hhmmss' message to feed" &&& fun _ ->        
        let now = DateTime.Now.ToString("yyyyMMdd-hhmmss")        
        let messageToPost = sprintf "Hello %s" now
        insertPostMessage messageToPost
        _visibility << "Public"
        //_publishAt << "2019-02-21 00:00"
        click _postButton
        _postedMessage == messageToPost
    
    "Add emoji into Post to Feed" &&& (fun _ ->        
        scrollTo _postToFeedHeader
        sleep 3 //it might happen that it needs some time to scroll and may fail 1 in 5 attempts
        clickEmojiButton ":joy:"
        _visibility << "Public"
        //_publishAt << "2019-02-21 00:00"
        click _postButton
        _postedMessage == "😂"
    )

    "Add link into Post to Feed" &&& fun _ ->        
        click _addLinkButton
        _addLink << "https://ci.una.io/test/"
        click _addLinkSubmitButton 
        _visibility << "Public"
        //_publishAt << "2019-02-21 00:00"
        sleep 1 // that delay required as in CI/CD it may fail without it
        click _postButton
        waitForElement _firstPostedLinkFrame
