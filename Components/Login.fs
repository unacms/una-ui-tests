module Login

open VCanopy.Functions
open TestTypes
open CanopyExtensions
open Header
open VCanopy.NUnit
open AccountPopup


let _email = css "input[name='ID']"
let _password = css "input[name='Password']"
let _loginButton = css "button[type='submit']"
let _nextButton = xpath "//footer//ul//a[text()='Next' and contains(concat(' ',@class,' '),' shepherd-button-primary ')]"
let _doneButton = xpath "//footer//ul//a[text()='Done' and contains(concat(' ',@class,' '),' shepherd-button-primary ')]"
let _loginEmailError = css "#bx-form-element-ID .bx-form-warn"

let homePageTourHidden()=
    let driver = getCurrentDriver()
    let cookie = driver.Manage().Cookies.GetCookieNamed("homepage-tour-hidden")
    if not (isNull cookie) then cookie.Value else null

let userLogin userCredentials = 
    printfn "about to login using %A" userCredentials
    goto Pages.Login.uri
    _email << userCredentials.userEmail
    _password << userCredentials.userPassword
    click _loginButton
    waitForElement Header._loggedAccountName // this is just to make sure we're on the profile page & we have logged

    // if (userCredentials.isAdmin && homePageTourHidden()<>"1") then
    //     click _nextButton
    //     click _doneButton
    printfn "logged in using %A" userCredentials

let userLogout() = 
    openAccountMenu()
    click _accountLogout    
