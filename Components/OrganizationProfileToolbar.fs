module OrganizationProfileToolbar

open VCanopy.Functions

open System
open Header
open AccountPopup
open TestHelpers
open TestTypes
open ProfileToolbar

let _deleteOrganizationProfileButton = css ".bx-menu-item-delete-organization-profile a.bx-btn"
let _checkboxButton = css "input[name='delete_confirm']"
let _deleteOrganizationProfileSubmitButton = css "button[type='submit']"
let _editCoverButton = css ".bx-menu-item-edit-organization-cover"
let _editCoverSubmitButton = css "button[type='submit']"
let _editProfileButton = css ".bx-menu-item-edit-organization-profile a.bx-btn"
let _editProfileOrganizationName = css "input[name='org_name']"
let _editProfileSubmitButton = css "button[type='submit']"
let _editProfileSubmitButtonError = css "#bx-form-element-do_submit .bx-form-warn"
let _editProfileOrganizationNameErorr = css "#bx-form-element-org_name .bx-form-warn"
let _editProfileOrganizationCategory = css "select[name='org_cat']"
let _changedOrganizationCategory = css "a.bx-category-link"
let _editProfileOrganizationCategoryError = css "#bx-form-element-org_cat .bx-form-warn"
let _editProfileOrganizationVisibleTo = css "select[name='allow_view_to']"
let _editProfileOrganizationVisibleToError = css "#bx-form-element-allow_view_to .bx-form-warn"
let _editProfileOrganizationWhoCanPost = css "select[name='allow_post_to']"
let _editProfileOrganizationWhoCanPostError = css "#bx-form-element-allow_post_to .bx-form-warn"


/// Delete current active profile
let deleteOrganizationProfile() = 
    clickMoreButton()
    click _deleteOrganizationProfileButton
    click _checkboxButton
    click _deleteOrganizationProfileSubmitButton

let startEditOrganizationProfile () = 
    clickMoreButton()
    click _editProfileButton    
