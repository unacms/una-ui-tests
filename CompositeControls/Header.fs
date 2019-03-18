module Header

open canopy.classic

let _loginButton = css "li.bx-menu-item-login a"
let _loggedAccountName = css "#bx-menu-toolbar-item-account span.bx-menu-toolbar-item-title"
let _accountButton = css "#bx-menu-toolbar-item-account > a"
let _accountLogout = css "li.bx-menu-item-logout > a"


let currentProfileName() =
    waitForElement _loggedAccountName
    read _loggedAccountName


