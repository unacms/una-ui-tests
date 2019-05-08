module CreateNewProfilePerson

open VCanopy.Functions
open Header
open System
open CanopyExtensions
open Audit

let _createAnewProfile = css ".bx-menu-account-popup-profile-switcher-link a"
let _personButton = css "ul.bx-menu-object-sys_add_profile li:nth-child(1) a"
let _gender = css "select[name='gender']"
let _birthday = css "input[name='birthday']"
let _fullName = css "input[name='fullname']"
let _location = css "input[id='bx_person_input_d5189de027922f81005951e6efe0efd5_location']"
let _locationOkButton = css "button.dismissButton"
let _visibleTo = css "select[name='allow_view_to']"
let _submitButton = css "button[type='submit']"
let _fullNameError = css "#bx-form-element-fullname .bx-form-warn"
let _birthdayError = css "#bx-form-element-birthday .bx-form-warn"

type Profile = {Gender: string; Birthday: DateTime; FullName: string; Location: string; VisibleTo:string }

let defaultProfile = {
    Gender = "Woman";
    Birthday = DateTime(1990,3, 18);
    FullName = "Alexandra";
    Location = "Australia";
    VisibleTo = "Public"
}

let createPersonProfileEx profile runAccessibilityTests = 
    click _accountButton
    click _createAnewProfile
    if runAccessibilityTests then createAndWriteAccessibilityReport "AccessibilityReport-CreatePersonalProfile"
    click  _personButton
    _gender << profile.Gender
    _birthday << profile.Birthday.ToString("yyyy-MM-dd")
    click _gender
    _fullName << profile.FullName
    _location << profile.Location
    click _locationOkButton
    _visibleTo << profile.VisibleTo
    click _submitButton     


let createPersonProfile profile = createPersonProfileEx profile false

let createPersonProfileWithAccessibilityTesting profile = createPersonProfileEx profile true   

   








