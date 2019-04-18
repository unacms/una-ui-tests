
namespace Tests.CreateNewProfilePersonTests

open NUnit.Framework


open VCanopy.GivenWhenThen
open VCanopy.Functions
open VCanopy.NUnit

open Common
open CreateNewProfilePerson
open ProfileToolbar
open PostToFeed

[<Parallelizable(ParallelScope.All)>]
type ``Create a New Personal Profile`` () =


    [<SetUp>]    
    member this.Setup()= 
        Login.userLogin defaultAdmin

    [<TearDown>]    
    member this.TearDown()= 
        Login.userLogout()

    [<UseDriver>]
    [<Test>]
    member this.``Create person profile and delete profile``() =
        let maleProfile = {defaultProfile with Gender = "Man"; FullName="Valentin"}
        createPersonProfileWithAccessibilityTesting maleProfile
        deleteProfile()

    [<UseDriver>]
    [<Test>]
    member this.``Create person profile, post to feed, verify mesage, delete profile``() =
        let maleProfile = {defaultProfile with Gender = "Man"; FullName="Valentin"}        
        createPersonProfile maleProfile
        postMessageAndVerify "Hello world"
        deleteProfile()

          
