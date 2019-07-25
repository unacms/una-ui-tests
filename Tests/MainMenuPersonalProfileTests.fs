namespace Tests.MainMenuPersonalProfileTests

open NUnit.Framework


open VCanopy.GivenWhenThen
open VCanopy.Functions
open VCanopy.NUnit

open Common
open Header
open ProfileToolbar
open MainMenubar
open CreateNewProfileOrganization
open OrganizationProfileToolbar
open CreateNewProfilePerson
open CreateNewProfileOrganization

[<Parallelizable(ParallelScope.Children)>]
type MainMenuPersonalProfileTests () =

    let setup credentials= 
        Login.userLogin credentials

    [<TearDown>]    
    member this.TearDown()= 
        deleteProfile ()
        Login.userLogout()

    [<UseDriver>]
    [<Test>]
    [<Category("Positive")>]
    member this.PressInfoButton_ValidName() =
        setup user_lily
        createPersonProfileWithAccessibilityTesting defaultProfile
        click _personalProfileInfoButton
        _fullNameInfo == "Sandra"
      

    //[<UseDriver>]
    //[<Test>]
    //[<Category("Positive")>]
    //member this.PressInfoButton_ValidEmail() =
    //    setup user_eva
    //    createPersonProfileWithAccessibilityTesting defaultProfile
    //    click _personalProfileInfoButton
    //    _emailInfo == "eva@example.com"
                  
    //[<UseDriver>]
    //[<Test>]
    //[<Category("Positive")>]
    //member this.PressInfoButton_ValidStatus() =
    //    setup user_linda
    //    createPersonProfileWithAccessibilityTesting defaultProfile
    //  click _personalProfileInfoButton
    //    _statusInfo == "Active"              