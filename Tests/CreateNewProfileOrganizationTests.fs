namespace Tests.CreateNewProfileOrganizationTests

open NUnit.Framework


open VCanopy.GivenWhenThen
open VCanopy.Functions
open VCanopy.NUnit

open Common
open CreateNewProfileOrganization
open OrganizationProfileToolbar


[<Parallelizable(ParallelScope.Children)>]
type CreateNewOrganizationProfile () =


    let setup credentials= 
        Login.userLogin credentials

    [<TearDown>]    
    member this.TearDown()= 
        Login.userLogout()

    [<UseDriver>]
    [<Test>]
    [<Category("Positive")>]
    member this.``Create organization profile and delete organization profile``() =
        setup user_luck
        let orgProfile = {defaultProfileOrganization with OrganizationName = Some "A dummy organization"; Category = Some "Food"}
        createOrganizationProfileWithAccessibilityTesting orgProfile
        deleteOrganizationProfile ()

    [<UseDriver>]
    [<Test>]
    [<Category("Negative")>]
    member this.``Create organization profile and leave Organization Name field empty``() =
        setup user_lily
        let orgProfile = {defaultProfileOrganization with  Category = Some "Food"; OrganizationName = None}
        createOrganizationProfile orgProfile
        _organizationNameError == "This information is essential. Please, fill in this field."
        _submitButtonError == "Incorrect info. Please, check your inputs and try to submit again."

    [<UseDriver>]
    [<Test>]
    [<Category("Negative")>]
    member this.CategoryFieldEmpty_ErrorMessage() =
        setup user_lily
        let orgProfile = {defaultProfileOrganization with  OrganizationName = Some "Second organization"; Category = None}
        createOrganizationProfile orgProfile
        _categoryError == "Category field is mandatory"
        _submitButtonError == "Incorrect info. Please, check your inputs and try to submit again."

    [<UseDriver>]
    [<Test>]
    [<Category("Positive")>]
    member this.OrganizationName50characters_CreatesProfile() =
        setup user_eva
        let orgProfile = {defaultProfileOrganization with OrganizationName = Some "12345678901234567890123456789012345678901234567890"; Category = Some "Food"}
        createOrganizationProfileWithAccessibilityTesting orgProfile
        deleteOrganizationProfile ()        