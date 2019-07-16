namespace Tests.CreateNewProfileOrganizationTests

open NUnit.Framework


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
    member this.OrganizationName45characters_CreatesProfile() =
        setup user_eva
        createOrganizationProfile {defaultProfileOrganization with OrganizationName = Some "123456789012345678901234567890123456789012345"}
        deleteOrganizationProfile () 

    [<UseDriver>]
    [<Test>]
    [<Category("Positive")>]
    member this.EmptyLocationField_CreatesProfile() =
        setup user_linda
        createOrganizationProfile {defaultProfileOrganization with Location = None}
        deleteOrganizationProfile ()           

    [<UseDriver>] 
    [<Test>]
    [<Category("Positive")>]
    member this.ValidLocationField_CreatesProfile() =
        setup user_emma
        createOrganizationProfile {defaultProfileOrganization with Location = Some "Australia"}
        deleteOrganizationProfile () 

    [<UseDriver>]
    [<Test>]
    [<Category("Positive")>]
    [<TestCase("Me only",0)>]
    [<TestCase("Public",1)>]
    [<TestCase("Friends",2)>]
    [<TestCase("Closed",3)>]
    [<TestCase("Secret",4)>]
    member this.ValidVisibleToField_CreatesProfile visibleTo index =
        let user = [user_karen; user_ella; user_viky; user_mila; user_eric].[index]
        setup user
        createOrganizationProfile {defaultProfileOrganization with VisibleTo = Some visibleTo}
        deleteOrganizationProfile ()   

    [<UseDriver>]
    [<Test>]
    [<Category("Positive")>]
    [<TestCase("Me only",0)>]
    [<TestCase("Public",1)>]
    [<TestCase("Friends",2)>]
    [<TestCase("Employees",3)>]
    [<TestCase("Followers",4)>]
    member this.ValidWhoCanPostField_CreatesProfile whoCanPost index =
        let user = [user_jack; user_rob; user_dave; user_tom; user_andy].[index]
        setup user
        createOrganizationProfile {defaultProfileOrganization with WhoCanPost = Some whoCanPost}
        deleteOrganizationProfile ()        
