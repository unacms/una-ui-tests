module Common

open TestTypes
open System



let baseuri = "https://ci.una.io/test"

let defaultAdmin = {
    userEmail = "admin@example.com";

    //to make this work add on local machine into your ~/.bashrc next line
    // export adminexamplepassword=apasswordforadminuser
    userPassword = Environment.GetEnvironmentVariable("adminexamplepassword");
    userName = "admin"
}

let createUser userName =
    {defaultAdmin with userEmail = userName + "@example.com"; userName = userName}

let user_luck = createUser "luck" 
let user_lily = createUser "lily"
let user_eva = createUser "eva" 
let user_linda = createUser "linda"
let user_emma = createUser "emma"
let user_karen = createUser "karen"
let user_ella = createUser "ella"
let user_viky = createUser "viky"

let generateResultsFullFileName filename = sprintf "./TestResults/%s" filename

