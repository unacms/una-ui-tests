module Common

open TestTypes
open System



let baseuri = "https://ci.una.io/test"

let defaultAdmin = {
    userName = "admin@example.com";

    //to make this work add on local machine into your ~/.bashrc next line
    // export adminexamplepassword=apasswordforadminuser
    userPassword = Environment.GetEnvironmentVariable("adminexamplepassword")
}

let generateResultsFullFileName filename = sprintf "./TestResults/%s" filename

