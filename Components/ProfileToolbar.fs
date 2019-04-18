module ProfileToolbar

open VCanopy.Functions

open System
open Header
open AccountPopup
open TestHelpers



let private _moreButton = css "li.bx-menu-item-more-auto > span.bx-base-general-entity-action a.bx-btn"
let _deleteProfileButton = css ".bx-menu-item-delete-persons-profile a.bx-btn"
let _deleteAccountButton = css ".bx-menu-item-delete-persons-account a.bx-btn"
let _deleteAccountWithContentButton = css ".bx-menu-item-delete-persons-account-content a.bx-btn"
let _checkboxButton = css "input[name='delete_confirm']"
let _deleteProfileSubmitButton = css "button[type='submit']"
let _addFriendProfileButton = css ".bx-menu-item-profile-friend-add a.bx-btn"
//let _addFriendRequestedProfileButton = css ".bx-menu-item-profile-friend-add a.bx-btn"
let _cancelFriendRequestProfileButton = css ".bx-menu-item-profile-friend-remove a.bx-btn"
let _followProfileButton = css ".bx-menu-item-profile-subscribe-add a.bx-btn"
let _unfollowProfileButton = css ".bx-menu-item-profile-subscribe-remove a.bx-btn"
let _reportButton = css "a[id^=bx-report-do-link-bx-persons]"
let _reportType = css "#bx-form-element-type select[name='type']"
let _postReportButton = css "#bx-form-element-submit button[type='submit']"
let _reportCounter = css ".bx-report-counter-holder a"

let readReportCounter () =
    let sc = read _reportCounter
    if (sc.Length=0) then
        0
    else Int32.Parse(sc)

let clickMoreButton()=
    scrollTo (css ".bx-base-pofile-cover")
    click _moreButton
    
/// Delete current active profile
let deleteProfile() = 
    clickMoreButton()
    click _deleteProfileButton
    click _checkboxButton
    click _deleteProfileSubmitButton


//Helpers for debugging

///the method fails for some reason as it can't press any(_currentUserProfileButton,_profileButton) of the profile buttons.
let rec deleteAllProfiles() =
    let currentProfileName = Header.currentProfileName()
    if ("admin" <> currentProfileName) then                    
        retry 3 (fun _ ->
            hover _accountButton
            click _accountButton
            hover _profileButton
            click _profileButton
            )

        clickMoreButton()
        click _deleteProfileButton
        click _checkboxButton
        click _deleteProfileSubmitButton
        deleteAllProfiles()