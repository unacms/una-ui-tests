module PostToFeedTests


open CanopyExtensions
open canopy.runner.classic
open canopy.classic

let all () =

    context "Signup tests"  

    before (fun _ -> 
        goto Pages.Login.uri
        // elnter login & password from object Common.defaultAdmin
        //click login button
        //click other buttons if needed

    )

    "Account Name not entered shows error required field" &&& fun _ ->        
        click Signup._submitButton
        Signup._accountNameError == "This is a required field."
    
