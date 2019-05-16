module CreateNewProfileOrganization

open VCanopy.Functions
open Header
open System
open CanopyExtensions
open Audit


let _createAnewProfile = css ".bx-menu-account-popup-profile-switcher-link a"
let _organizationButton = css "ul.bx-menu-object-sys_add_profile li:nth-child(2) a"
let _organizationName = css "input[name='org_name']"
let _category = css "select[name='org_cat']"
let _location = css "input[name='location']"
let _visibleTo = css "select[name='allow_view_to']"
let _submitButton = css "button[type='submit']"
let _organizationNameError = css "#bx-form-element-org_name .bx-form-warn"
let _categoryError = css "#bx-form-element-org_cat .bx-form-warn"
let _submitButtonError = css "#bx-form-element-do_submit .bx-form-warn"

type OrganizationProfile = { OrganizationName: string option; Category: string option; Location: string; VisibleTo: string }
let defaultProfileOrganization = {
    OrganizationName = Some "Head dummy organization";
    Category =  Some "Agriculture";
    Location = "Australia";
    VisibleTo = "Public"
}

let createOrganizationProfileEx profile runAccessibilityTests = 
    click _accountButton
    click _createAnewProfile
    if runAccessibilityTests then createAndWriteAccessibilityReport "AccessibilityReport-CreateOrganizationProfile"
    click  _organizationButton
    _organizationName <<< profile.OrganizationName
    _category <<< profile.Category
    _location << profile.Location
    _visibleTo << profile.VisibleTo
    click _submitButton     


let createOrganizationProfile profile = createOrganizationProfileEx profile false
let createOrganizationProfileWithAccessibilityTesting profile = createOrganizationProfileEx profile true   