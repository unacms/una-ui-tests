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
let _location = css "input[name='location']"
//let _locationOkButton = css "button.dismissButton"
let _visibleTo = css "select[name='allow_view_to']"
let _whoCanPost = css "select[name='allow_post_to']"
let _submitButton = css "button[type='submit']"
let _fullNameError = css "#bx-form-element-fullname .bx-form-warn"
let _birthdayError = css "#bx-form-element-birthday .bx-form-warn"
let _genderInfo = xpath "//div[contains(concat(' ',@class,' '),' bx-form-row-view-wrapper ') and ./div[contains(concat(' ',@class,' '),' bx-form-row-view-caption ') and text()='Gender:']]/div[contains(concat(' ',@class,' '),' bx-form-row-view-value ')]"


type Profile = {Gender: string option; Birthday: DateTime option; FullName: string option; Location: string option; VisibleTo:string option; WhoCanPost: string option}

let defaultProfile = {
    Gender = None;// "Woman";
    Birthday = None; //Some (DateTime(1990,3, 18));
    FullName = Some "Sandra";
    Location = None;//"Australia";
    VisibleTo = None;//"Public"
    WhoCanPost = None; //"Public"
}

let createPersonProfileEx profile runAccessibilityTests = 
    click _accountButton
    click _createAnewProfile
    if runAccessibilityTests then createAndWriteAccessibilityReport "AccessibilityReport-CreatePersonalProfile"
    click  _personButton
    _gender <<< profile.Gender
    
    let birthday = profile.Birthday |> Option.map (fun dob -> dob.ToString("yyyy-MM-dd"))
//    let birthday = Option.map (fun (dob:DateTime) -> dob.ToString("yyyy-MM-dd")) profile.Birthday
    _birthday <<< birthday
    click _gender
    _fullName <<< profile.FullName
    _location <<< profile.Location
    //click _locationOkButton
    _visibleTo <<< profile.VisibleTo
    _whoCanPost <<< profile.WhoCanPost
    click _submitButton     


let createPersonProfile profile = createPersonProfileEx profile false

let createPersonProfileWithAccessibilityTesting profile = createPersonProfileEx profile true   

   








