
namespace Tests.CreateNewProfilePersonTests

open NUnit.Framework


open VCanopy.GivenWhenThen
open VCanopy.Functions
open VCanopy.NUnit

open Common
open CreateNewProfilePerson
open ProfileToolbar
open System
open System

[<Parallelizable(ParallelScope.Children)>]
type CreateNewPersonalProfileTests () =

    let convertToInt strInt=
        Option.map (fun si -> System.Int32.Parse(si)) strInt

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
        let maleProfile = {defaultProfile with Gender = "Man"; FullName = Some "Valentin"}
        createPersonProfileWithAccessibilityTesting maleProfile
        deleteProfile()

    [<UseDriver>]
    [<Test>]
    [<Category("Negative")>]
    member this.EmptyFullName_ShowsRequiredFieldError() =
        setup user_eva
        let maleProfile = {defaultProfile with Gender = "Man"; FullName = None}
        createPersonProfile maleProfile
        _fullNameError == "This information is essential. Please, fill in this field."
                  
    [<UseDriver>]
    [<Test>]
    [<Category("Negative")>]
    member this.YangerThan18YO_ShowsAgeError() =
        setup user_eva
        let maleProfile = {defaultProfile with Gender = "Man"; Birthday = Some (DateTime.Now.AddYears(-18).AddDays(1.0))}
        createPersonProfile maleProfile
        _birthdayError == "Your age should be in the range of 18 to 99 years"
    
    [<UseDriver>]
    [<Test>]
    [<Category("Negative")>]
    member this.Turn99YearsOldToday_ShowsAgeError() =
        setup user_eva
        let maleProfile = {defaultProfile with Gender = "Man"; Birthday = Some (DateTime.Now.AddYears(-99))}        
        createPersonProfile maleProfile
        _birthdayError == "Your age should be in the range of 18 to 99 years"  

    [<UseDriver>]
    [<Test>]
    [<Category("Positive")>]
    member this.Turn18Today_CreatesProfile() =
        setup user_eva
        let maleProfile = {defaultProfile with Gender = "Man"; Birthday = Some (DateTime.Now.AddYears(-18)); FullName = Some "Nick"}
        createPersonProfile maleProfile
        deleteProfile()

    [<UseDriver>]
    [<Test>]
    [<Category("Positive")>]
    member this.Turn99Tomorrow_CreatesProfile() =
        setup user_linda
        let maleProfile = {defaultProfile with Gender = "Man"; Birthday = Some (DateTime.Now.AddYears(-99).AddDays(1.)); FullName = Some "Nathan"}
        createPersonProfile maleProfile
        deleteProfile()

    [<UseDriver>]
    [<Test>]
    [<Category("Positive")>]
    member this.EmptyBirthday_CreatesProfile() =
        setup user_emma
        let maleProfile = {defaultProfile with Gender = "Man"; Birthday = None; FullName = Some "Oscar"}
        createPersonProfile maleProfile
        deleteProfile()        

    [<UseDriver>]
    [<Test>]
    [<Category("Positive")>]
    member this.FullName50characters_CreatesProfile() =
        setup user_karen
        let maleProfile = {defaultProfile with Gender = "Man"; FullName = Some "12345678901234567890123456789012345678901234567890"}
        createPersonProfile maleProfile
        deleteProfile()     