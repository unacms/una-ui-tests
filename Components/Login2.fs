module Login2

open VCanopy.Functions
open System

let _headerLoginBtn = css "#bx-menu-toolbar-item-login"
let _email = css "input[name='ID']"
let _password = css "input[name='Password']"
let _loginButton = css "button[type='submit']"
let _accountButton = css "#bx-menu-toolbar-item-account > a"
let _accountLogout = css "li.bx-menu-item-logout > a"
type Credentials = {userName: string; userPassword: string}
let defaultAdmin = {
    userName = "admin@example.com";

    //to make this work add on local machine into your ~/.bashrc next line
    // export adminexamplepassword=apasswordforadminuser
    userPassword = Environment.GetEnvironmentVariable("adminexamplepassword")
}