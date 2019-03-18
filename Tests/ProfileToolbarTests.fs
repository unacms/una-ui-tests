module ProfileToolbarTests



open canopy.runner.classic
open canopy.classic
open Common
open Header
open CreateNewProfilePerson
open Pages
open ProfileToolbar
open PostToFeed
open CanopyExtensions
open AccountPopup

let all () =

    context "Create a New Personal Profile" 

    before (fun _ -> 
        Login.userLogin defaultAdmin


        //deleteAllProfiles()

        let femaleProfile = {defaultProfile with FullName="Natalia"}
        createPersonProfileWithAccessibilityTesting femaleProfile 
        let maleProfile = {defaultProfile with Gender = "Man"; FullName="Valentin"}
        createPersonProfileWithAccessibilityTesting maleProfile
    )

    after (fun _ -> 
        switchProfile "Valentin"
        deleteProfile()
        switchProfile "Natalia"
        deleteProfile()
        Login.userLogout()
    )    

    // THE value must be one always as we just created profile and there should not be any reports
    // THE value must be one always as we just created profile and there should not be any reports

    "Switch profile, raise a report and define that number of report is raised" &&& fun _ ->
        switchProfile "Natalia"
        click _moreButton
        click _reportButton
        _reportType << "spam"       
        click _postReportButton
        _reportCounter == "1" 
        Login.userLogout()
        Login.userLogin defaultAdmin
        switchProfile "Natalia"
        click _moreButton
        click _reportButton
        _reportType << "spam"       
        click _postReportButton
        _reportCounter == "2" 
             



