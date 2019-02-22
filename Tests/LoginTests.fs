module LoginTests

open CanopyExtensions
open canopy.runner.classic
open canopy.classic
open Common
open Header


let all () =

    context "Login tests"  

    before (fun _ -> 
        goto Pages.Login.uri
    )

    "Log in test" &&& fun _ ->
        Login._email << defaultAdmin.userName
        Login._password << defaultAdmin.userPassword
        click Login._loginButton
        //verify that we logged as admin
        _loggedAccountName == "admin"
        click _accountButton
        click _accountLogout
