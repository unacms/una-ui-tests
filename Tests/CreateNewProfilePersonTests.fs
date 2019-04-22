
namespace Tests.CreateNewProfilePersonTests

open NUnit.Framework


open VCanopy.GivenWhenThen
open VCanopy.Functions
open VCanopy.NUnit

open Common
open CreateNewProfilePerson
open ProfileToolbar
open PostToFeed

[<Parallelizable(ParallelScope.Children)>]
type ``Create a New Personal Profile`` () =


    let setup credentials= 
        Login.userLogin credentials

    [<TearDown>]    
    member this.TearDown()= 
        Login.userLogout()

    [<UseDriver>]
    [<Test>]
    member this.``Create person profile and delete profile``() =
        setup user_luck
        let maleProfile = {defaultProfile with Gender = "Man"; FullName="Valentin"}
        createPersonProfileWithAccessibilityTesting maleProfile
        deleteProfile()

    [<UseDriver>]
    [<Test>]
    member this.``Create person profile, post to feed, verify mesage, delete profile``() =
        setup user_lily
        let maleProfile = {defaultProfile with Gender = "Man"; FullName="Valentin"}        
        createPersonProfile maleProfile
        postMessageAndVerify "Hello world"
        deleteProfile()

          
