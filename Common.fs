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

let user_luck = {defaultAdmin with userName = "luck@example.com"}
let user_lily = {defaultAdmin with userName = "lily@example.com"}
let user_eva = {defaultAdmin with userName = "eva@example.com"}
let user_linda = {defaultAdmin with userName = "linda@example.com"}
let user_emma = {defaultAdmin with userName = "emma@example.com"}
let user_karen = {defaultAdmin with userName = "karen@example.com"}
let user_ella = {defaultAdmin with userName = "ella@example.com"}
let user_viky = {defaultAdmin with userName = "viky@example.com"}

let generateResultsFullFileName filename = sprintf "./TestResults/%s" filename

