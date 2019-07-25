module MainMenubar

open VCanopy.Functions

open System
open Header
open AccountPopup
open TestHelpers
open TestTypes
open CreateNewProfilePerson







let _personalProfileInfoButton = css "#bx-menu-main-bar .bx-menu-item-persons-profile-info"
let _emailInfo = xpath "//div[contains(concat(' ',@class,' '),' bx-form-row-view-wrapper ') and ./div[contains(concat(' ',@class,' '),' bx-form-row-view-caption ') and text()='Email:']]//div[contains(concat(' ',@class,' '),' bx-form-row-view-value ' )]/a"
let _statusInfo = xpath "//div[contains(concat(' ',@class,' '),' bx-form-row-view-wrapper ') and ./div[contains(concat(' ',@class,' '),' bx-form-row-view-caption ') and text()='Status:']]//div[contains(concat(' ',@class,' '),' bx-form-row-view-value ' )]/a"
let _personalProfileFriendButton = css "#bx-menu-main-bar .bx-menu-item-persons-profile-friends a"
let _friendName = xpath "//div[@id='bx-page-persons-profile-friends'] //a[contains(concat(' ',@class,' '), 'bx-def-unit-info-title ') and text()]"
let _personalProfileRelationshipButton = css "#bx-menu-main-bar .bx-menu-item-persons-profile-relations a"
let _relationshipInfo = xpath "//div[@id='bx-grid-cont-sys_grid_related_me']//td[2][contains(concat(' ',@class,' '),' bx-def-padding-sec-top ') and text()]"
let _personalProfileFollowButton = css ".bx-menu-item-persons-profile-subscriptions a"
let _followersName = xpath "//div[@id='bx-grid-cont-sys_grid_subscribed_me']//a[contains(concat(' ',@class,' '),' bx-def-unit-info-title ') and text()]"
let _followingName = xpath "//div[@id='bx-grid-cont-sys_grid_subscriptions']//a[contains(concat(' ',@class,' '),' bx-def-unit-info-title ') and text()]"