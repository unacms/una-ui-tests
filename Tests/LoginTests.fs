module LoginTests

open canopy.runner.classic
open canopy.classic
open Common
open Header
open CanopyExtensions
open Audit


let all () =

    context "Login tests"  

    before (fun _ ->         
        goto Pages.Login.uri
    )

    "Log in test" &&& fun _ ->
        createAndWriteAccessibilityReport "HomePage"
        Login._email << defaultAdmin.userName
        Login._password << defaultAdmin.userPassword
        click Login._loginButton
        //verify that we logged as admin
        
        // ToDo replace defaultAdmin with a newly created user in the test so that user has no any profiles and has the name of the actual user
        // _loggedAccountName == "admin"
        Login.userLogout()
