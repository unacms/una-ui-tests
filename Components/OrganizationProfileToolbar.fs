module OrganizationProfileToolbar

open VCanopy.Functions

open System
open Header
open AccountPopup
open TestHelpers
open TestTypes


let _moreButton = css "li.bx-menu-item-more-auto > span.bx-base-general-entity-action a.bx-btn"
let _deleteOrganizationProfileButton = css ".bx-menu-item-delete-organization-profile a.bx-btn"
let _checkboxButton = css "input[name='delete_confirm']"
let _deleteOrganizationProfileSubmitButton = css "button[type='submit']"

let clickMoreButton()=
    scrollTo (css ".bx-base-pofile-cover")
    click _moreButton


/// Delete current active profile
let deleteOrganizationProfile() = 
    clickMoreButton()
    click _deleteOrganizationProfileButton
    click _checkboxButton
    click _deleteOrganizationProfileSubmitButton