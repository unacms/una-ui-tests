module PostToFeedTests

open System
open CanopyExtensions
open canopy.runner.classic
open canopy.classic
open Common
open Header
open PostToFeed
open canopy
open CreateNewProfilePerson
open ProfileToolbar

type PostToFeedFrom = 
    | PostToFeedFromAccount
    | PostToFeedFromProfile


let all postToFeedFrom =

    match postToFeedFrom with
    | PostToFeedFromProfile -> context "PostToFeedFromProfileTests"
    | PostToFeedFromAccount -> context "PostToFeedFromAccountTests"

    once (fun _ -> 
        Login.userLogin defaultAdmin

        match postToFeedFrom with
        | PostToFeedFromProfile -> 
            let maleProfile = {defaultProfile with Gender = "Man"; FullName="Vasily"}
            createPersonProfile maleProfile                
        | PostToFeedFromAccount -> ()

    )

    lastly ( fun _ -> 
        match postToFeedFrom with
        | PostToFeedFromProfile -> 
            deleteProfile                
        | PostToFeedFromAccount -> ()    

        Login.userLogout()
    )

    let tn testName =
        match postToFeedFrom with
        | PostToFeedFromAccount -> "PostToFeedFromAccount. " + testName
        | PostToFeedFromProfile -> "PostToFeedFromProfile. " + testName 



    tn("Post 'Hello yyyyMMdd-hhmmss' to Feed -> adds 'Hello yyyyMMdd-hhmmss' message to feed") &&& fun _ ->        
        let now = DateTime.Now.ToString("yyyyMMdd-hhmmss")        
        let messageToPost = sprintf "Hello %s" now
        insertPostMessage messageToPost
        _visibility << "Public"
        //_publishAt << "2019-02-21 00:00"
        click _postButton
        _postedMessage == messageToPost
    
    tn("Click Add emoji -> Adds to feed a post message with emoji") &&& (fun _ ->        
        scrollTo _postToFeedHeader
        sleep 3 //it might happen that it needs some time to scroll and may fail 1 in 5 attempts
        clickEmojiButton ":joy:"
        _visibility << "Public"
        //_publishAt << "2019-02-21 00:00"
        click _postButton
        _postedMessage == "😂"
    )

    tn("Click Add link -> Adds to feed a post message with a link") &&& fun _ ->        
        click _addLinkButton
        _addLink << "https://ci.una.io/test/"
        click _addLinkSubmitButton 
        _visibility << "Public"
        //_publishAt << "2019-02-21 00:00"
        sleep 1 // that delay required as in CI/CD it may fail without it
        click _postButton
        waitForElement _firstPostedLinkFrame

    tn("Click Add and Delete link -> Deletes added link") &&& fun _ ->        
        click _addLinkButton
        _addLink << "https://ci.una.io/test/"
        click _addLinkSubmitButton 
        _visibility << "Public"
        
        //click delete anchor in the first section with added link
        let firstAddedLinkSection = addedLinkSection 1
        element firstAddedLinkSection |> elementWithin "a" |> click

        //make sure that element is not displayed
        throwIfElementExists firstAddedLinkSection 5
