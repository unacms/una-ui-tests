module SignupTests


open CanopyExtensions
open canopy.runner.classic
open canopy.classic

let all () =

    context "Signup tests"  

    before (fun _ -> 
        goto Pages.Signup.uri
    )

    "Account Name not entered shows error required field" &&& fun _ ->        
        click Signup._submitButton
        Signup._accountNameError == "This is a required field."
