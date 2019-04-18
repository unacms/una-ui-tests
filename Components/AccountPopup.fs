module AccountPopup

open VCanopy.Functions
open Header
open System
open CanopyExtensions
open Audit



let _dashButton = css ".bx-menu-item-dashboard a"
let _profileButton = css ".bx-menu-object-sys_account_notifications .bx-menu-item-profile a"
let _accountSettingsButton = css ".bx-menu-item-account-settings a"
let _addContentButton = css ".bx-menu-floating-blocks-wide .bx-menu-item-add-content a"
let _studioButton = css ".bx-menu-item-studio a"
let _shoppingCartButton = css ".bx-menu-item-cart a"
let _conversationButton = css ".bx-menu-item-notifications-convos a"
let _notificationButton = css ".bx-menu-item-notifications-notifications a"
let _signOutButton = css ".bx-menu-item-logout a"
let _closeMenuButton = css "#bx-sliding-menu-account a.bx-sliding-menu-close a"
//let _currentUserProfileButton = css ".bx-menu-account-popup-profile .bx-base-pofile-unit a"
let _createAnewProfile = css ".bx-menu-account-popup-profile-switcher-link"




//open profile for viewing. It is not switching
let clickSelectProfile profileName = 
      let selector = sprintf "//a[contains(concat(' ',@class,' '), 'bx-def-unit-info-title ') and text()='%s']" profileName
      click (xpath selector) 

let switchAccountButton profileName =
      let selector = sprintf "//div[contains(concat(' ',@class,' '),' sys-profile-switch-row ') and .//a[contains(concat(' ',@class,' '),' bx-def-unit-info-title ') and text()='%s'] ]//div[contains(concat(' ',@class,' '),' sys-profile-switch-row-control ')]/a" profileName
      xpath selector


//clicks 'switch' button for switching to a profile. admin user may switch to a user profile
let clickSwitchAccountButton profileName =
      click (switchAccountButton profileName) 
      

//switch to a profile. admin user may switch to a user profile
let switchProfile profileName =
      let currentProfileName = Header.currentProfileName()
      if (profileName <> currentProfileName) then
            let switchBtn = switchAccountButton profileName
            hover _accountButton
            click _accountButton
            clickSwitchAccountButton profileName

let selectProfile profileName = 
      click _accountButton
      clickSelectProfile profileName
