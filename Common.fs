module Common

open TestTypes
open System



let baseuri = "https://ci.una.io/test"

let defaultAdmin = {
    userEmail = "admin@example.com";

    //to make this work add on local machine into your ~/.bashrc next line
    // export adminexamplepassword=apasswordforadminuser
    // in windows run setx adminexamplepassword apasswordforadminuser
    userPassword = Environment.GetEnvironmentVariable("adminexamplepassword");
    userName = "admin";
    isAdmin = true
}

let createUser userName =
    {defaultAdmin with userEmail = userName + "@example.com"; userName = userName; 
                        // ToDo uncomment later once users will be changed to not admins
                        //isAdmin = false
    }

let user_luck = createUser "luck" 
let user_lily = createUser "lily"
let user_eva = createUser "eva" 
let user_linda = createUser "linda"
let user_emma = createUser "emma"
let user_karen = createUser "karen"
let user_ella = createUser "ella"
let user_viky = createUser "viky"
let user_mila = createUser "mila"
let user_eric = createUser "eric"
let user_jack = createUser "jack"
let user_rob = createUser "rob"
let user_dave = createUser "dave"
let user_tom = createUser "tom"
let user_andy = createUser "andy"
let user_ivan = createUser "ivan"


let generateResultsFullFileName filename = sprintf "./TestResults/%s" filename

