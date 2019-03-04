module CreateNewProfilePersonTests



open canopy.runner.classic
open canopy.classic
open Common
open Header
open CreateNewProfilePerson
open Pages
open ProfileToolbar
open PostToFeed
open CanopyExtensions

let all () =

    context "Create a New Personal Profile" 

    before (fun _ -> 
        Login.userLogin defaultAdmin
    )

    after (fun _ -> 
        Login.userLogout()
    )    

    "Create person profile and delete profile" &&& fun _ ->
        let maleProfile = {defaultProfile with Gender = "Man"; FullName="Valentin"}
        createPersonProfile maleProfile
        deleteProfile()

    "Create person profile, post to feed, verify mesage, delete profile" &&& fun _ ->
        let maleProfile = {defaultProfile with Gender = "Man"; FullName="Valentin"}
        createPersonProfile maleProfile
        postMessageAndVerify "Hello world"
        deleteProfile()

          
