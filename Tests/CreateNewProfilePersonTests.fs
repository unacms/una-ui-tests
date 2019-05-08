
namespace Tests.CreateNewProfilePersonTests

open NUnit.Framework


open VCanopy.GivenWhenThen
open VCanopy.Functions
open VCanopy.NUnit

open Common
open CreateNewProfilePerson
open ProfileToolbar
open PostToFeed
open System

[<Parallelizable(ParallelScope.Children)>]
type ``Create a New Personal Profile`` () =


    let setup credentials= 
        Login.userLogin credentials

    [<TearDown>]    
    member this.TearDown()= 
        Login.userLogout()

    [<UseDriver>]
    [<Test>]
    [<Category("Positive")>]
    member this.``Create person profile and delete profile``() =
        setup user_luck
        let maleProfile = {defaultProfile with Gender = "Man"; FullName="Valentin"}
        createPersonProfileWithAccessibilityTesting maleProfile
        deleteProfile()

    [<UseDriver>]
    [<Test>]
    [<Category("Positive")>]
    member this.``Create person profile, post to feed, verify mesage, delete profile``() =
        setup user_lily
        let maleProfile = {defaultProfile with Gender = "Man"; FullName="Valentin"}        
        createPersonProfile maleProfile
        postMessageAndVerify "Hello world"
        deleteProfile()

    [<UseDriver>]
    [<Test>]
    [<Category("Negative")>]
    member this.``Create person profile and leave FullName field empty``() =
        setup user_eva
        let maleProfile = {defaultProfile with Gender = "Man"}
        _fullNameError == "This information is essential. Please, fill in this field."
                  
    [<UseDriver>]
    [<Test>]
    [<Category("Negative")>]
    member this.``Create person profile and put the birthday date less then 18 yo``() =
        setup user_eva
        let maleProfile = {defaultProfile with Gender = "Man"; Birthday = DateTime.Now.AddYears(-18).AddDays(1.0)}
        _birthdayError == "Your age should be in the range of 18 to 99 years"
    
    [<UseDriver>]
    [<Test>]
    [<Category("Negative")>]
    member this.``Create person profile and put the birthday date more than 99``() =
        setup user_eva
        let maleProfile = {defaultProfile with Gender = "Man"; Birthday = DateTime.Now.AddYears(-99).AddDays(1.0)}
        _birthdayError == "Your age should be in the range of 18 to 99 years"  

    [<UseDriver>]
    [<Test>]
    [<Category("Positive")>]
    member this.``Create person profile and put the birthday date 18 yo``() =
        setup user_eva
        let maleProfile = {defaultProfile with Gender = "Man"; Birthday = DateTime.Now.AddYears(-18); FullName = "Nick"}
        createPersonProfile maleProfile
        deleteProfile()

    [<UseDriver>]
    [<Test>]
    [<Category("Positive")>]
    member this.``Create person profile and put the birthday date 99 yo``() =
        setup user_linda
        let maleProfile = {defaultProfile with Gender = "Man"; Birthday = DateTime.Now.AddYears(-99).AddDays(1.); FullName = "Nathan"}
        createPersonProfile maleProfile
        deleteProfile()