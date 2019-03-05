module ProfileToolbar

open canopy.classic
open TestTypes
open CanopyExtensions


let _moreButton = css "li.bx-menu-item-more-auto > span.bx-base-general-entity-action a.bx-btn"
let _deleteProfileButton = css "li.bx-menu-item-more-auto .bx-menu-item-delete-persons-profile a.bx-btn"
let _deleteAccountButton = css "li.bx-menu-item-more-auto .bx-menu-item-delete-persons-account a.bx-btn"
let _deleteAccountWithContentButton = css "li.bx-menu-item-more-auto .bx-menu-item-delete-persons-account-content a.bx-btn"
let _checkboxButton = css "input[name='delete_confirm']"
let _deleteProfileSubmitButton = css "button[type='submit']"

let deleteProfile() = 
    click _moreButton
    click _deleteProfileButton
    click _checkboxButton
    click _deleteProfileSubmitButton
