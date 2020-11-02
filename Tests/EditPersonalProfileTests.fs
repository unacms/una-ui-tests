namespace Tests.EditPersonalProfileTests

open NUnit.Framework


open VCanopy.GivenWhenThen
open VCanopy.Functions
open VCanopy.NUnit

open Common
open Header
open ProfileToolbar
open CreateNewProfileOrganization
open OrganizationProfileToolbar
open CreateNewProfilePerson
open CreateNewProfileOrganization
open System


[<Parallelizable(ParallelScope.Children)>]
type EditPersonalProfileTests () =

    let setup credentials= 
        Login.userLogin credentials

    [<TearDown>]    
    member this.TearDown()= 
        deleteProfile ()
        Login.userLogout()

    [<UseDriver>]
    [<Test>]
    [<Category("Positive")>]
    [<TestCase("Man",0)>]
    [<TestCase("Woman",1)>]
    member this.ChangingPersonalGenderField_GenderFieldGetsUpdated gender index =
        let user = [user_luck; user_lily].[index]
        setup user
        createPersonProfile defaultProfile
        startEditPersonalProfile ()
        _editPersonalProfileGender << gender
        click _editPersonalProfileSubmitButton
        _genderInfo == gender
        
    [<UseDriver>]
    [<Test>]
    [<Category("Positive")>]
    member this.EmtyPersonalGenderField_GenderFieldGetsUpdated() = 
        setup user_eva
        createPersonProfile defaultProfile
        startEditPersonalProfile ()
        _editPersonalProfileGender << "Not Specified"
        click _editPersonalProfileSubmitButton
        startEditPersonalProfile ()
        _editPersonalProfileGender == "Not Specified"
        

    [<UseDriver>]
    [<Test>]
    [<Category("Negative")>]
    member this.EmtyPersonalFullNameField_ShowsErrorMessage() = 
        setup user_linda
        createPersonProfile defaultProfile 
        startEditPersonalProfile ()
        _editPersonalProfileFullName << ""
        click _editPersonalProfileSubmitButton
        _editPersonalProfileFullNameError == "This information is essential. Please, fill in this field."
        _editPersonalProfileSubmitButtonError == "Incorrect info. Please, check your inputs and try to submit again."
         

    [<UseDriver>]
    [<Test>]
    [<Category("Positive")>]
    member this.ChangingPersonalFullNameField_FullNameGetsUpdate() = 
        setup user_emma
        createPersonProfile defaultProfile 
        startEditPersonalProfile ()
        _editPersonalProfileFullName << "Alex"
        click _editPersonalProfileSubmitButton
        _fullNameInfo == "Alex"
        

    [<UseDriver>]
    [<Test>]
    [<Category("Positive")>]
    member this.ChangingPersonalLocationField_LocationFieldGetsUpdate() = 
        setup user_karen
        createPersonProfile defaultProfile 
        startEditPersonalProfile ()
        setLocation "Austr" "alia"
        click _editPersonalProfileSubmitButton
        startEditPersonalProfile ()
        _editPersonalProfileLocation == "Australia"
       
    [<UseDriver>]
    [<Category("Positive")>]   
    [<TestCase("Me only", 0)>]
    [<TestCase("Public", 1)>]
    [<TestCase("Friends", 2)>]
    //[<TestCase("Selected Friends...", 3)>] to do...
    [<TestCase("Relationships",3)>]
    //[<TestCase("Selected Relationships...", 5)>] to do...
    
   
    member this.ChangingPersonalVisibleTo_VisibleToGetsUpdated visibleTo index = 
        let user = [user_ella; user_viky; user_mila; user_eric].[index]
        setup user
        createPersonProfile defaultProfile
        startEditPersonalProfile ()
        scrollTo _editPersonalProfileVisibleTo
        _editPersonalProfileVisibleTo << visibleTo
        click _editPersonalProfileSubmitButton
        startEditPersonalProfile ()
        scrollTo _editPersonalProfileVisibleTo
        _editPersonalProfileVisibleTo == visibleTo  

    [<UseDriver>]
    [<Category("Positive")>]   
    [<TestCase("Me only", 0)>]
    [<TestCase("Public", 1)>]
    [<TestCase("Friends", 2)>]
    //[<TestCase("Selected Friends...", 3)>] to do...
    [<TestCase("Relationships", 3)>]
    //[<TestCase("Selected Relationships...", 5)>] to do...
   
   
    member this.ChangingPersonalWhoCanPost_WhoCanPostGetsUpdated whoCanPost index = 
        let user = [user_tom; user_andy; user_ivan; user_luck].[index]
        setup user
        createPersonProfile defaultProfile
        startEditPersonalProfile ()
        scrollTo _editPersonalProfileWhoCanPost
        _editPersonalProfileWhoCanPost << whoCanPost
        click _editPersonalProfileSubmitButton
        startEditPersonalProfile ()
        scrollTo _editPersonalProfileWhoCanPost
        _editPersonalProfileWhoCanPost == whoCanPost 

    [<UseDriver>]
    [<Test>]
    [<Category("Positive")>]
       member this.ChangingPersonalBirthdayField_BirtdayFieldGetsUpdate() = 
        setup user_emma
        createPersonProfile defaultProfile
        startEditPersonalProfile ()
        _editPersonalProfileBirthday << "2000-03-12"
        click _editPersonalProfileSubmitButton 
        startEditPersonalProfile ()
        _editPersonalProfileBirthday == "2000-03-12"

       

    [<UseDriver>]
    [<Test>]
    [<Category("Negative")>]
       member this.YoungerThan18PersonalBirthdayField_ShowsErrorMessage() = 
        setup user_karen
        createPersonProfile defaultProfile
        startEditPersonalProfile ()
        let birthday = DateTime.Now.AddYears(-18).AddDays(1.0).ToString("yyyy-MM-dd")
        _editPersonalProfileBirthday << birthday
        click _editPersonalProfileSubmitButton 
        _editPersonalProfileBirthdayError == "Your age should be in the range of 18 to 99 years"     
        _editPersonalProfileSubmitButtonError == "Incorrect info. Please, check your inputs and try to submit again." 

    [<UseDriver>]
    [<Test>]
    [<Category("Negative")>]
       member this.OlderThan99PersonalBirthdayField_ShowsErrorMessage() = 
        setup user_linda
        createPersonProfile defaultProfile
        startEditPersonalProfile ()
        let birthday = DateTime.Now.AddYears(-99).AddDays(-1.0).ToString("yyyy-MM-dd")
        _editPersonalProfileBirthday << birthday
        click _editPersonalProfileSubmitButton 
        _editPersonalProfileBirthdayError == "Your age should be in the range of 18 to 99 years"     
        _editPersonalProfileSubmitButtonError == "Incorrect info. Please, check your inputs and try to submit again."                     