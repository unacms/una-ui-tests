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


[<Parallelizable(ParallelScope.Children)>]
[<TestFixture("PostToFeedFromAccount")>]
[<TestFixture("PostToFeedFromProfile")>]
type PostToFeedTests(postToFeedFrom:string) =
// // type PostToFeedTests() =
// //     let postToFeedFrom = "PostToFeedFromAccount"
// // //    let postToFeedFrom = "PostToFeedFromProfile"


    
    let setup credentials= 
        Login.userLogin credentials

        let reportName = 
            match postToFeedFrom with
                | "PostToFeedFromProfile" -> 
                    let maleProfile = {defaultProfile with Gender = "Man"; FullName = Some "Vasily"}
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
    [<Category("Positive")>]
    member this.``Post 'Hello yyyyMMdd-hhmmss' to Feed -> adds 'Hello yyyyMMdd-hhmmss' message to feed`` ()=
        setup user_luck
        let now = DateTime.Now.ToString("yyyyMMdd-hhmmss")        
        let messageToPost = sprintf "Hello %s" now
        insertPostMessage messageToPost
        _visibility << "Public"
        click _postButton
        _postedMessage == messageToPost
    
    [<UseDriver>]
    [<Test>]
    [<Category("Positive")>]
    member this.``Click Add emoji -> Adds to feed a post message with emoji`` ()=
        setup user_lily
        scrollTo _postToFeedHeader
        clickEmojiButton ":joy:"
        _visibility << "Public"
        click _postButton
        _postedMessage == "😂"
        

    [<UseDriver>]
    [<Test>]
    [<Category("Positive")>] 
    member this.``Click Add link -> Adds to feed a post message with a link`` ()=
        setup user_eva
        click _addLinkButton
        _addLinkTextBox << "https://ci.una.io/test/"
        click _addLinkSubmitButton 
        _visibility << "Public"
        //_publishAt << "2019-02-21 00:00"
        click _postButton
        //Just check conteiner in place, requiered visiual testing
        waitForDisplayed _postedLinkFrame

    [<UseDriver>]
    [<Test>]    
    [<Category("Negative")>]
    member this.``Click Add link, Do not insert link -> Adds to feed a post message with a link`` ()=
        setup user_linda
        click _addLinkButton
        click _addLinkSubmitButton 
        _addLinkError == "Correct Link is essential. Please, fill in this field."
        click _addLinkCloseButton

    [<UseDriver>]
    [<Test>]    
    [<Category("Negative")>]
    member this.``Click Add link, insert 12345 into link field -> Adds to feed a post message with a link`` ()=
        setup user_emma
        click _addLinkButton
        _addLinkTextBox << "12345"
        click _addLinkSubmitButton 
        _addLinkError == "Correct Link is essential. Please, fill in this field."
        click _addLinkCloseButton



    [<UseDriver>]
    [<Test>]
    [<Category("Positive")>] 
    member this.``Click Add and Delete link -> Deletes added link`` ()=
        setup user_karen
        click _addLinkButton
        _addLinkTextBox << "https://ci.una.io/test/"
        click _addLinkSubmitButton 
        _visibility << "Public"
        
        //click delete anchor in the first section with added link
        let firstAddedLinkSection = addedLinkSection 1
        scrollTo firstAddedLinkSection
        let sc = (addedLinkSectionChild 1 " a")
        click sc //(addedLinkSectionChild 1 " a")

        //make sure that element is not displayed
        throwIfElementDisplayed firstAddedLinkSection


    [<UseDriver>]
    [<Test>] 
    [<Category("Negative")>]  
    member this.``Leave empty Post to Feed field and post it`` ()=
        setup user_ella
        _visibility << "Public"
        click _postButton
        _postToFeedError == "The post is empty."


    [<UseDriver>]
    [<Test>]
    [<Category("Positive")>]
    member this.``Adds to feed a post message with emoji and a link`` ()=
        setup user_viky
        scrollTo _postToFeedHeader
        clickEmojiButton ":joy:"
        click _addLinkButton
        _addLinkTextBox << "https://ci.una.io/test/"
        click _addLinkSubmitButton 
        _visibility << "Public"
        click _postButton
        _postedMessage == "😂"
        waitForDisplayed _postedLinkFrame



    [<UseDriver>]
    [<Test>]
    [<Category("Positive")>]
    member this.``Adds to feed a post message emoji and 'Hello yyyyMMdd-hhmmss' `` ()=
        setup user_mila
        scrollTo _postToFeedHeader
        let now = DateTime.Now.ToString("yyyyMMdd-hhmmss")        
        let messageToPost = sprintf "Hello %s" now
        insertPostMessage messageToPost
        clickEmojiButton ":joy:"
        _visibility << "Public"
        click _postButton
        // the emoji appears in front just because the cursor was at the beginning

        _postedMessage == "😂" + messageToPost
              

    
