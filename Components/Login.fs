module Login

open VCanopy.Functions
open TestTypes
open CanopyExtensions
open Header


let _email = css "input[name='ID']"
let _password = css "input[name='Password']"
let _loginButton = css "button[type='submit']"
let _nextButton = xpath "//footer//ul//a[text()='Next' and contains(concat(' ',@class,' '),' shepherd-button-primary ')]"
let _doneButton = xpath "//footer//ul//a[text()='Done' and contains(concat(' ',@class,' '),' shepherd-button-primary ')]"
let _loginEmailError = css "#bx-form-element-ID .bx-form-warn"



let userLogin userCredentials = 
    printfn "about to login using %A" userCredentials
    goto Pages.Login.uri
    _email << userCredentials.userEmail
    _password << userCredentials.userPassword
    click _loginButton
    waitForElement Header._loggedAccountName // this is just to make sure we're on the profile page & we have logged

    // ToDo find out the conditions when those 2 buttons appear and call click only if they appear
    clickUnstable _nextButton
    clickUnstable _doneButton
    printfn "logged in using %A" userCredentials

let userLogout() = 
    click _accountButton
    click _accountLogout    
