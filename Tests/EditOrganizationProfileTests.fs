namespace Tests.EditOrganizationProfileTests

open NUnit.Framework


open VCanopy.GivenWhenThen
open VCanopy.Functions
open VCanopy.NUnit

open Common
open Header
open ProfileToolbar
open CreateNewProfileOrganization
open OrganizationProfileToolbar
open CreateNewProfilePerson


[<Parallelizable(ParallelScope.Children)>]
type EditOrganizationProfile () =

 

    let setup credentials= 
        Login.userLogin credentials

    [<TearDown>]    
    member this.TearDown()= 
       deleteOrganizationProfile ()
       Login.userLogout()

    [<UseDriver>]
    [<Test>]
    [<Category("Positive")>]
    member this.ChangingOrganizationName_OrganizationNameGetsUpdated() =
        setup user_luck
        let orgProfile = {defaultProfileOrganization with OrganizationName = Some "A dummy organization name"; Category = Some "Food"}
        createOrganizationProfileWithAccessibilityTesting orgProfile
        startEditOrganizationProfile ()
        _editProfileOrganizationName << "A new org name"
        click _editProfileSubmitButton
        _loggedAccountName == "A new org name"
        

    // Can't make a decision which is better GWT vs plain implementation.
    // From one side GWT gives some more information about the intent
    // From another side it gives some more noise especially if the test is short,
    // it kind'a doesn't give much value as the test is so short and there's no need for GWT.
    [<UseDriver>]
    [<Test>]
    [<Category("Negative")>]
    member this.UserEntersEmptyOrganizationName_ShowsErrorMessage() = 
        // This Given statement kind of incorrect from programming point of view
        // as we don't a user having an organization profile
        // and we might go with Given user, when user enters a valid credentials, clicks submit button,...
        // But that way of writting would be oufull to read from business point of view. It will be to detailed.
        // From business point of view what is currently decribed is easier to read and understand.
        Given "a user modifies organization profile" (fun _ ->
            setup user_lily
            let orgProfile = {defaultProfileOrganization with OrganizationName = Some "A dummy organization"; Category = Some "Food"}
            createOrganizationProfileWithAccessibilityTesting orgProfile
            startEditOrganizationProfile ()
            ) |>
        When "user enters empty organization name and clicks submit button" (fun _ ->        
            _editProfileOrganizationName << ""
            click _editProfileSubmitButton
            ) |>
        Then "the `This information is essential. Please, fill in this field.` message is displayed" (fun _ ->
            _editProfileOrganizationNameErorr == "This information is essential. Please, fill in this field."           
            ) |>
        Run 
  

    
    [<UseDriver>]
    [<Test>]
    [<Category("Positive")>]
    member this.ChangingOrganizationCategory_CategoryGetsUpdated() = 
        setup user_eva
        let orgProfile = {defaultProfileOrganization with OrganizationName = Some "A dummy organization"; Category = Some "Food"}
        createOrganizationProfileWithAccessibilityTesting orgProfile
        startEditOrganizationProfile ()
        _editProfileOrganizationCategory << "Energy"
        click _editProfileSubmitButton
        _changedOrganizationCategory == "Energy"

    

    [<UseDriver>]
    [<Category("Positive")>]
    [<TestCase("Me only", 0)>]
    [<TestCase("Public", 1)>]
    [<TestCase("Friends", 2)>]
    [<TestCase("Closed", 3)>]
    [<TestCase("Secret", 4)>]

    member this.ChangingOrganizationVisibleTo_VisibleToGetsUpdatedtoMeOnly visibleTo index = 
    // [<Test>]
    // member this.ChangingOrganizationVisibleTo_VisibleToGetsUpdatedtoMeOnly() =
    //     let visibleTo="Public"
    //     let index = 0
        let user = [user_linda; user_emma; user_karen; user_ella; user_viky].[index]
        setup user
        let orgProfile = {defaultProfileOrganization with OrganizationName = Some "A dummy organization"; Category = Some "Food"}
        createOrganizationProfileWithAccessibilityTesting orgProfile
        startEditOrganizationProfile ()
        scrollTo _editProfileOrganizationVisibleTo
        _editProfileOrganizationVisibleTo << visibleTo
        click _editProfileSubmitButton
        startEditOrganizationProfile ()
        scrollTo _editProfileOrganizationVisibleTo
        _editProfileOrganizationVisibleTo == visibleTo
    

    [<UseDriver>]
    [<Category("Positive")>]   
    [<TestCase("Me only", 0)>]
    [<TestCase("Public", 1)>]
    [<TestCase("Friends", 2)>]
    [<TestCase("Employees", 3)>]
    [<TestCase("Followers", 4)>]
   
    member this.ChangingOrganizationWhoCanPost_WhoCanPostGetsUpdated whoCanPost index = 
        let user = [user_mila; user_eric; user_jack; user_rob; user_dave].[index]
        setup user
        let orgProfile = {defaultProfileOrganization with OrganizationName = Some "A dummy organization"; Category = Some "Food"}
        createOrganizationProfileWithAccessibilityTesting orgProfile
        startEditOrganizationProfile ()
        scrollTo _editProfileOrganizationWhoCanPost
        _editProfileOrganizationWhoCanPost << whoCanPost
        click _editProfileSubmitButton
        startEditOrganizationProfile ()
        scrollTo _editProfileOrganizationWhoCanPost
        _editProfileOrganizationWhoCanPost == whoCanPost

    [<UseDriver>]
    [<Test>]
    [<Category("Negative")>]
    member this.EmptyOrganizationCategory_ShowsErrorMessage() = 
        setup user_andy
        let orgProfile = {defaultProfileOrganization with OrganizationName = Some "A dummy organization"; Category = Some "Food"}
        createOrganizationProfileWithAccessibilityTesting orgProfile
        startEditOrganizationProfile ()
        _editProfileOrganizationCategory << ""
        click _editProfileSubmitButton
        _editProfileOrganizationCategoryError == "Category field is mandatory"

    