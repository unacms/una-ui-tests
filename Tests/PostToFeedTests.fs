module PostToFeedTests

open System
open canopy.runner.classic
open canopy.classic
open CanopyExtensions
open Common
open Header
open PostToFeed
open canopy
open CreateNewProfilePerson
open ProfileToolbar
open Audit

type PostToFeedFrom = 
    | PostToFeedFromAccount
    | PostToFeedFromProfile


let all postToFeedFrom =

    match postToFeedFrom with
    | PostToFeedFromProfile -> context "PostToFeedFromProfileTests"
    | PostToFeedFromAccount -> context "PostToFeedFromAccountTests"

    once (fun _ -> 
        Login.userLogin defaultAdmin

        let mutable reportName = ""

        match postToFeedFrom with
        | PostToFeedFromProfile -> 
            let maleProfile = {defaultProfile with Gender = "Man"; FullName="Vasily"}
            createPersonProfile maleProfile  
            reportName <- "AccessibilityReport-PostToFeedFromProfile"              
        | PostToFeedFromAccount -> 
            reportName <- "AccessibilityReport-PostToFeedFromAccount"

        //Here we run accessibility tests in both(PostToFeedFromAccount,PostToFeedFromProfile) cases
        createAndWriteAccessibilityReport reportName
    )

    lastly ( fun _ -> 
        match postToFeedFrom with
        | PostToFeedFromProfile -> 
            deleteProfile()                
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
        clickEmojiButton ":joy:"
        _visibility << "Public"
        //_publishAt << "2019-02-21 00:00"
        click _postButton
        _postedMessage == "😂"
    )

    tn("Click Add link -> Adds to feed a post message with a link") &&& fun _ ->        
        click _addLinkButton
        _addLinkTextBox << "https://ci.una.io/test/"
        click _addLinkSubmitButton 
        _visibility << "Public"
        //_publishAt << "2019-02-21 00:00"
        clickAndWait _postButton _firstPostedLinkFrame 10

    tn("Click Add link, Do not insert link -> Adds to feed a post message with a link") &&& fun _ ->        
        //clickAndWait _addLinkButton _addLinkSubmitButton 10
        click _addLinkButton
        click _addLinkSubmitButton 
        _addLinkError == "Correct Link is essential. Please, fill in this field."
        click _addLinkCloseButton

    tn("Click Add link, insert 12345 into link field -> Adds to feed a post message with a link") &&& fun _ ->        
        click _addLinkButton
        _addLinkTextBox << "12345"
        click _addLinkSubmitButton 
        _addLinkError == "Correct Link is essential. Please, fill in this field."
        click _addLinkCloseButton



    tn("Click Add and Delete link -> Deletes added link") &&& fun _ ->        
        click _addLinkButton
        _addLinkTextBox << "https://ci.una.io/test/"
        click _addLinkSubmitButton 
        _visibility << "Public"
        
        //click delete anchor in the first section with added link
        let firstAddedLinkSection = addedLinkSection 1
        element firstAddedLinkSection |> elementWithin "a" |> click

        //make sure that element is not displayed
        throwIfElementExists firstAddedLinkSection 5
