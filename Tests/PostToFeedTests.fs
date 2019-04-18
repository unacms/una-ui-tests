namespace Tests.PostToFeedTests

open System

open NUnit.Framework
open VCanopy.GivenWhenThen
open VCanopy.Functions
open VCanopy.NUnit
open Common
open PostToFeed
open CreateNewProfilePerson
open ProfileToolbar
open Audit

type PostToFeedFrom = 
    | PostToFeedFromAccount of string
    | PostToFeedFromProfile of string


[<Parallelizable(ParallelScope.All)>]
[<TestFixture("PostToFeedFromAccount")>]
[<TestFixture("PostToFeedFromProfile")>]
type PostToFeedTests(postToFeedFrom:string) =
// type PostToFeedTests() =
// //    let postToFeedFrom = "PostToFeedFromAccount"
//     let postToFeedFrom = "PostToFeedFromProfile"


    [<SetUp>]    
    member this.Setup()= 
        Login.userLogin defaultAdmin

        let reportName = 
            match postToFeedFrom with
                | "PostToFeedFromProfile" -> 
                    let maleProfile = {defaultProfile with Gender = "Man"; FullName="Vasily"}
                    createPersonProfile maleProfile  
                    "AccessibilityReport-PostToFeedFromProfile"              
                | _ -> 
                    "AccessibilityReport-PostToFeedFromAccount"

        //Here we run accessibility tests in both(PostToFeedFromAccount,PostToFeedFromProfile) cases
        createAndWriteAccessibilityReport reportName

    [<TearDown>]
    member this.TearDown()=
        match postToFeedFrom with
        | "PostToFeedFromProfile" -> 
            deleteProfile()                
        | _ -> ()    





    [<UseDriver>]
    [<Test>]    
    member this.``Post 'Hello yyyyMMdd-hhmmss' to Feed -> adds 'Hello yyyyMMdd-hhmmss' message to feed`` ()=
        let now = DateTime.Now.ToString("yyyyMMdd-hhmmss")        
        let messageToPost = sprintf "Hello %s" now
        insertPostMessage messageToPost
        _visibility << "Public"
        //_publishAt << "2019-02-21 00:00"
        click _postButton
        _postedMessage == messageToPost
    
    [<UseDriver>]
    [<Test>]    
    member this.``Click Add emoji -> Adds to feed a post message with emoji`` ()=
        scrollTo _postToFeedHeader
        clickEmojiButton ":joy:"
        _visibility << "Public"
        //_publishAt << "2019-02-21 00:00"
        click _postButton
        _postedMessage == "😂"

    [<UseDriver>]
    [<Test>]    
    member this.``Click Add link -> Adds to feed a post message with a link`` ()=
        click _addLinkButton
        _addLinkTextBox << "https://ci.una.io/test/"
        click _addLinkSubmitButton 
        _visibility << "Public"
        //_publishAt << "2019-02-21 00:00"
        click _postButton

    [<UseDriver>]
    [<Test>]    
    member this.``Click Add link, Do not insert link -> Adds to feed a post message with a link`` ()=
        //clickAndWait _addLinkButton _addLinkSubmitButton 10
        click _addLinkButton
        click _addLinkSubmitButton 
        _addLinkError == "Correct Link is essential. Please, fill in this field."
        click _addLinkCloseButton

    [<UseDriver>]
    [<Test>]    
    member this.``Click Add link, insert 12345 into link field -> Adds to feed a post message with a link`` ()=
        click _addLinkButton
        _addLinkTextBox << "12345"
        click _addLinkSubmitButton 
        _addLinkError == "Correct Link is essential. Please, fill in this field."
        click _addLinkCloseButton



    [<UseDriver>]
    [<Test>]    
    member this.``Click Add and Delete link -> Deletes added link`` ()=
        click _addLinkButton
        _addLinkTextBox << "https://ci.una.io/test/"
        click _addLinkSubmitButton 
        _visibility << "Public"
        
        //click delete anchor in the first section with added link
        let firstAddedLinkSection = addedLinkSection 1
        click (addedLinkSectionChild 1 " a")

        //make sure that element is not displayed
        throwIfElementDisplayed firstAddedLinkSection
