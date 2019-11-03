module ProfileToolbar

open VCanopy.Functions

open System
open Header
open AccountPopup
open TestHelpers
open TestTypes
open CreateNewProfilePerson
open CanopyExtensions



let private _moreButton = css "li.bx-menu-item-more-auto > span.bx-base-general-entity-action a.bx-btn"
let _deleteProfileButton = css ".bx-menu-item-delete-persons-profile a.bx-btn"
let _deleteAccountButton = css ".bx-menu-item-delete-persons-account a.bx-btn"
let _deleteAccountWithContentButton = css ".bx-menu-item-delete-persons-account-content a.bx-btn"
let _checkboxButton = css "input[name='delete_confirm']"
let _deleteProfileSubmitButton = css "button[type='submit']"
let _addFriendProfileButton = css ".bx-menu-item-profile-friend-add a.bx-btn"
let _addFriendProfileButtonInfo = css ".bx-menu-item-profile-friend-add .bx-def-margin-sec-left-auto"
let _addFriendRequestedProfileButton = css ".bx-menu-item-profile-friend-add a.bx-btn"
let _addFriendRequestedProfileButtonInfo = css ".bx-menu-item-profile-friend-add .bx-def-margin-sec-left-auto"
let _cancelFriendRequestProfileButton = css ".bx-menu-item-profile-friend-remove a.bx-btn"
let _acceptFriendRequestButtonInfo = css ".bx-menu-item-profile-friend-add .bx-def-margin-sec-left-auto"
let _acceptFriendRequestButton = css ".bx-menu-item-profile-friend-add .bx-btn"
let _personalProfileFriends = css "#bx-page-persons-profile-friends"
let _unfriendButtonInfo = css ".bx-menu-item-profile-friend-remove .bx-def-margin-sec-left-auto"
let _unfriendButton = css ".bx-menu-item-profile-friend-remove a.bx-btn"
let _addRelationshipButtonInfo = css ".bx-menu-item-profile-relation-add .bx-def-margin-sec-left-auto"
let _addRelationshipButton = css ".bx-menu-item-profile-relation-add a.bx-btn"
//let _relationshipStatusButton = xpath "//ul[contains(concat(' ',@class,' '),' bx-menu-object-sys_add_relation ')]//li[contains(concat(' ',@class,' '),'bx-def-round-corners ')]//a"
let _followProfileButton = css ".bx-menu-item-profile-subscribe-add a.bx-btn"
let _unfollowProfileButton = css ".bx-menu-item-profile-subscribe-remove a.bx-btn"
let _followProfileButtonInfo = css ".bx-menu-item-profile-subscribe-add .bx-def-margin-sec-left-auto"
let _unfollowProfileButtonInfo = css ".bx-menu-item-profile-subscribe-remove .bx-def-margin-sec-left-auto"
let _reportButton = css "a[id^=bx-report-do-link-bx-persons]"
let _reportType = css "#bx-form-element-type select[name='type']"
let _postReportButton = css "#bx-form-element-submit button[type='submit']"
let _removeRelationshipInfo = xpath "//li[contains(concat(' ',@class,' '),' bx-menu-item-profile-relation-remove ')]//span[contains(concat(' ',@class,' '),' bx-def-margin-sec-left-auto ') and text()='Remove Relationship']"
let _removeRelationshipButton = css ".bx-menu-item-profile-relation-remove a.bx-btn"
//let _reportCounter = css ".bx-report-counter-holder a"
let _reportCounter = css ".bx-view-counter"
let _editPersonalProfileButton = css ".bx-menu-item-edit-persons-profile a.bx-btn"
let _editPersonalProfileGender = css "select[name='gender']"
let _editPersonalProfileSubmitButton = css "button[type='submit']"
let _genderInfo = xpath "//div[contains(concat(' ',@class,' '),' bx-form-row-view-wrapper ') and ./div[contains(concat(' ',@class,' '),' bx-form-row-view-caption ') and text()='Gender:']]//div[contains(concat(' ',@class,' '),' bx-form-row-view-value ' )]"
let _editPersonalProfileFullName = css "input[name='fullname']"
let _fullNameInfo = xpath "//div[contains(concat(' ',@class,' '),' bx-form-row-view-wrapper ') and ./div[contains(concat(' ',@class,' '),' bx-form-row-view-caption ') and text()='Full Name:']]//div[contains(concat(' ',@class,' '),' bx-form-row-view-value ' )]"
let _editPersonalProfileFullNameError = css "#bx-form-element-fullname .bx-form-warn"
let _editPersonalProfileSubmitButtonError = css "#bx-form-element-do_submit .bx-form-warn" 
let _editPersonalProfileLocation = css "input[name='location']"
let _editPersonalProfileVisibleTo = css "select[name='allow_view_to']"
let _editPersonalProfileWhoCanPost = css "select[name='allow_post_to']"
let _editPersonalProfileBirthday = css "input[name='birthday']"
let _editPersonalProfileBirthdayError = css "#bx-form-element-birthday .bx-form-warn"

let readReportCounter () =
    let sc = read _reportCounter
    if (sc.Length=0) then
        0
    else Int32.Parse(sc)

let clickMoreButton()=
    scrollTo (css ".bx-base-pofile-cover")
    Threading.Thread.Sleep 3000
    click _moreButton
    
/// Delete current active profile
let deleteProfile() =
    if (not (isDisplayed _deleteProfileButton)) then
        clickMoreButton()
    clickUntilDisplayed _deleteProfileButton _checkboxButton
    //click _checkboxButton
    clickUntilDisplayed _checkboxButton _deleteProfileSubmitButton    
    click _deleteProfileSubmitButton




//Helpers for debugging

///the method fails for some reason as it can't press any(_currentUserProfileButton,_profileButton) of the profile buttons.
let rec deleteAllProfiles credentials =
    let mutable currentProfileName = Header.currentProfileName()
    if (not (currentProfileName.StartsWith(credentials.userName+"-"))) then
        openAccountMenu()
        let profileNameSelector = xpath <| sprintf "(//a[contains(concat(' ',@class,' '),' bx-def-unit-info-title ') and contains(text(),'%s-')])[1]" credentials.userName
        let profileName = readUnstable profileNameSelector
        match profileName with
        | Some pn -> switchProfile pn
        | None -> ()
            

    //example if currentProfileName = "ella-Valentin" and credentials.userName="ella", then we need to delete "ella-Valentin" profile
    //minus added below to avoid deleting itself    
    currentProfileName <- Header.currentProfileName()
    if (currentProfileName.StartsWith(credentials.userName+"-")) then                    
        retry 3 (fun _ ->
            openAccountMenu()
            hover _profileButton
            click _profileButton
            )

        deleteProfile()
        deleteAllProfiles credentials

let startEditPersonalProfile() = 
    if (not (isDisplayed _editPersonalProfileButton)) then
        clickMoreButton()
    click _editPersonalProfileButton

let editPersonalProfileBirthday = Option.map (fun (dob:DateTime) -> dob.ToString("yyyy-MM-dd")) 

let setLocation locationPrefix locationSuffix =
    _editPersonalProfileLocation << locationPrefix
    click (xpath (sprintf "//div[contains(concat(' ',@class,' '),' pac-container ')]/div[.//span[text()='%s']]" locationSuffix))


let clickSelectRelationshipStatus relationshipStatus = 
   let selector = sprintf "//ul[contains(concat(' ',@class,' '),' bx-menu-object-sys_add_relation ')]//span[text()='%s']" relationshipStatus  
   click (xpath selector)
