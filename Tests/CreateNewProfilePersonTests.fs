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

    let setup credentials= 
        Login.userLogin credentials

    [<TearDown>]    
    member this.TearDown()= 
        Login.userLogout()

    [<UseDriver>]
    [<Test>]
    [<Category("Positive")>]
    member this.ValidInputs_CreatesPersonalProfile() =
        setup user_luck
        createPersonProfileWithAccessibilityTesting defaultProfile
        deleteProfile()

    [<UseDriver>]
    [<Test>]
    [<Category("Negative")>]
    member this.EmptyFullName_ShowsRequiredFieldError() =
        setup user_eva
        createPersonProfile {defaultProfile with FullName = None}
        _fullNameError == "This information is essential. Please, fill in this field."
                  
    [<UseDriver>]
    [<Test>]
    [<Category("Negative")>]
    member this.YangerThan18YO_ShowsAgeError() =
        setup user_eva 
        createPersonProfile {defaultProfile with Birthday = Some (DateTime.Now.AddYears(-18).AddDays(1.0))}
        _birthdayError == "Your age should be in the range of 18 to 99 years"
    
    [<UseDriver>]
    [<Test>]
    [<Category("Negative")>]
    member this.Turn99YearsOldToday_ShowsAgeError() =
        setup user_eva         
        createPersonProfile {defaultProfile with Birthday = Some (DateTime.Now.AddYears(-99))} 
        _birthdayError == "Your age should be in the range of 18 to 99 years"  

    [<UseDriver>]
    [<Test>]
    [<Category("Positive")>]
    member this.Turn18Today_CreatesProfile() =
        setup user_eva
        createPersonProfile {defaultProfile with Birthday = Some (DateTime.Now.AddYears(-18))}
        deleteProfile()

    [<UseDriver>]
    [<Test>]
    [<Category("Positive")>]
    member this.Turn99Tomorrow_CreatesProfile() =
        setup user_linda 
        createPersonProfile {defaultProfile with Birthday = Some (DateTime.Now.AddYears(-99).AddDays(1.))}
        deleteProfile()

    [<UseDriver>]
    [<Test>]
    [<Category("Positive")>]
    member this.EmptyBirthday_CreatesProfile() =
        setup user_emma
        createPersonProfile {defaultProfile with Birthday = None}
        deleteProfile()        

    [<UseDriver>]
    [<Test>]
    [<Category("Positive")>]
    member this.FullName50characters_CreatesProfile() =
        setup user_karen
        createPersonProfile  {defaultProfile with FullName = Some "12345678901234567890123456789012345678901234567890"}
        deleteProfile()  

     
    [<UseDriver>]
    [<Test>]
    [<Category("Positive")>]
    member this.EmptyGenderField_CreatesPersonalProfile() =
        setup user_ella    
        createPersonProfile {defaultProfile with Gender = None} 
        deleteProfile()

