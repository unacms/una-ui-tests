namespace Tests.ProfileToolbarTests


open NUnit.Framework

open VCanopy.GivenWhenThen
open VCanopy.Functions
open VCanopy.NUnit

open Common
open Header
open CreateNewProfilePerson
open Pages
open ProfileToolbar
open PostToFeed
open CanopyExtensions
open AccountPopup

[<Parallelizable(ParallelScope.Children)>]

type ProfileToolbarTests () =
     

    let setup credentials= 
        Login.userLogin credentials

        //while(not System.Diagnostics.Debugger.IsAttached) do System.Threading.Thread.Sleep(500);

        deleteAllProfiles()
        let femaleProfile = {defaultProfile with FullName="Natalia"}
        createPersonProfileWithAccessibilityTesting femaleProfile 
        let maleProfile = {defaultProfile with Gender = "Man"; FullName="Valentin"}
        createPersonProfileWithAccessibilityTesting maleProfile
   

    [<TearDown>]
    member this.TearDown()=
        switchProfile "Valentin"
        selectProfile "Valentin"
        deleteProfile()
        switchProfile "Natalia"
        selectProfile "Natalia"
        deleteProfile()
        Login.userLogout()
      

    // THE value must be one always as we just created profile and there should not be any reports
    // THE value must be one always as we just created profile and there should not be any reports

    [<UseDriver>]
    [<Test>]    
    member this. ``Switch profile, raise a report and check that a proper number of report(s) is raised``()=
        
        setup user_luck
        switchProfile "Natalia"
        clickMoreButton()
        click _reportButton
        _reportType << "spam"       
        click _postReportButton
        _reportCounter == "1" 
        Login.userLogout()
        Login.userLogin defaultAdmin
        switchProfile "Natalia"
        selectProfile "Valentin"
        clickMoreButton()
        click _reportButton
        _reportType << "spam"       
        click _postReportButton
        _reportCounter == "2" 
             



