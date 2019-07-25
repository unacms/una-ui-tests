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

type PostToFeedTests() =
    let postToFeedFrom = "PostToFeedFromProfile"


    
    let setup credentials= 
        Login.userLogin credentials

        let reportName = 
            match postToFeedFrom with
                | "PostToFeedFromProfile" -> 
                    let maleProfile = {defaultProfile with Gender = Some "Man"; FullName = Some "Vasily"}
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
    member this.PostHelloDateOnMoment_PostsMessageWithNowDate ()=
        setup user_luck
        scrollTo _postToFeedHeader
        let now = DateTime.Now.ToString("yyyyMMdd-hhmmss")        
        let messageToPost = sprintf "Hello %s" now
        insertPostMessage messageToPost
        click _postButton
        _postedMessage == messageToPost
    
    [<UseDriver>]
    [<Test>]
    [<Category("Positive")>]
    member this.ClickAddEmoji_PostsMessageWithEmoji()=
        setup user_lily
        scrollTo _postToFeedHeader
        clickEmojiButton ":joy:"
        click _addEmojiButton
        click _postButton
        _postedMessage == "😂"
        

    [<UseDriver>]
    [<Test>]
    [<Category("Positive")>] 
    member this.ClickAddLink_PostsMessageWithLink()=
        setup user_eva
        click _addLinkButton
        _addLinkTextBox << "https://ci.una.io/test/"
        click _addLinkSubmitButton 
        //_publishAt << "2019-02-21 00:00"
        click _postButton
        //Just check conteiner in place, requiered visiual testing
        waitForDisplayed _postedLinkFrame

    [<UseDriver>]
    [<Test>]    
    [<Category("Negative")>]
    member this.ClickAddLinkWithEmtyField_ShowsErrorMessage ()=
        setup user_linda
        click _addLinkButton
        click _addLinkSubmitButton 
        _addLinkError == "Correct Link is essential. Please, fill in this field."
        click _addLinkCloseButton

    [<UseDriver>]
    [<Test>]    
    [<Category("Negative")>]
    member this.ClickAddLinkWith12345_ShowsErrorMessage ()=
        setup user_emma
        click _addLinkButton
        _addLinkTextBox << "12345"
        click _addLinkSubmitButton 
        _addLinkError == "Correct Link is essential. Please, fill in this field."
        click _addLinkCloseButton



    [<UseDriver>]
    [<Test>]
    [<Category("Positive")>] 
    member this.ClickAddAndDeleteLink_DeletesMessageWithLink ()=
        setup user_karen
        click _addLinkButton
        _addLinkTextBox << "https://ci.una.io/test/"
        click _addLinkSubmitButton 
        
        //click delete anchor in the first section with added link
        let firstAddedLinkSection = addedLinkSection 1
        scrollTo firstAddedLinkSection
        click (addedLinkSectionChild 1 " a")

        //make sure that element is not displayed
        throwIfElementDisplayed firstAddedLinkSection


    [<UseDriver>]
    [<Test>] 
    [<Category("Negative")>]  
    member this.EmptyPostToFeedField_ShowsErrorMessage ()=
        setup user_ella
        //not entering a message
        click _postButton
        _postToFeedError == "The post is empty."


    [<UseDriver>]
    [<Test>]
    [<Category("Positive")>]
    member this.PostMessageWithEmojiAndLink_PostsMessageWithEmojiAndLink()=
        setup user_viky
        scrollTo _postToFeedHeader
        clickEmojiButton ":joy:"
        click _addLinkButton
        _addLinkTextBox << "https://ci.una.io/test/"
        click _addLinkSubmitButton 
        click _postButton
        _postedMessage == "😂"
        waitForDisplayed _postedLinkFrame



    [<UseDriver>]
    [<Test>]
    [<Category("Positive")>]
    member this.AddsEmojiAndDateOnMoment_PostsMessageWithEmojiAndDate()=
        setup user_mila
        scrollTo _postToFeedHeader
        let now = DateTime.Now.ToString("yyyyMMdd-hhmmss")        
        let messageToPost = sprintf "Hello %s" now
        insertPostMessage messageToPost
        clickEmojiButton ":joy:"
        click _addEmojiButton
        click _postButton
        // the emoji appears in front just because the cursor was at the beginning

        _postedMessage == "😂" + messageToPost
              

    
