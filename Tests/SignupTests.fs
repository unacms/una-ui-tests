module SignupTests


open canopy.runner.classic
open canopy.classic
open CanopyExtensions
open Audit

let all () =

    context "Signup tests"  

    before (fun _ -> 
        goto Pages.Signup.uri
    )

    "Account Name not entered shows error required field" &&& fun _ ->  
        createAndWriteAccessibilityReport "AccessibilityReport-SignupPage"      
        click Signup._submitButton
        Signup._accountNameError == "This is a required field."
