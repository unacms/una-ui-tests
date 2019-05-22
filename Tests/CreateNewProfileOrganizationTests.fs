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
    member this.ValidInputs_CreatesOrganizationProfile() =
        setup user_luck
        createOrganizationProfileWithAccessibilityTesting defaultProfileOrganization
        deleteOrganizationProfile ()

    [<UseDriver>]
    [<Test>]
    [<Category("Negative")>]
    member this.EmptyOrganizationNameField_ShowsErrorMessage() =
        setup user_lily
        createOrganizationProfile {defaultProfileOrganization with OrganizationName = None}
        _organizationNameError == "This information is essential. Please, fill in this field."
        _submitButtonError == "Incorrect info. Please, check your inputs and try to submit again."

    [<UseDriver>]
    [<Test>]
    [<Category("Negative")>]
    member this.CategoryFieldEmpty_ShowsErrorMessage() =
        setup user_lily
        createOrganizationProfile {defaultProfileOrganization with Category = None}
        _categoryError == "Category field is mandatory"
        _submitButtonError == "Incorrect info. Please, check your inputs and try to submit again."

    [<UseDriver>]
    [<Test>]
    [<Category("Positive")>]
    member this.OrganizationName50characters_CreatesProfile() =
        setup user_eva
        createOrganizationProfile {defaultProfileOrganization with OrganizationName = Some "12345678901234567890123456789012345678901234567890"}
        deleteOrganizationProfile ()        