namespace Tests.ProfileToolbarTests


open NUnit.Framework

open VCanopy.GivenWhenThen
open VCanopy.Functions
open VCanopy.NUnit

open Common
open Header
open CreateNewProfilePerson
open Pages
open ProfileToolbar
open MainMenubar
open PostToFeed
open CanopyExtensions
open AccountPopup

[<Parallelizable(ParallelScope.Children)>]

type ProfileToolbarTests1 () =

    let mutable userCredential = user_luck
    let mutable profileNatalia = defaultProfile
    let mutable profileValentin = defaultProfile

    let fullProfileName name =
        sprintf "%s-%s" userCredential.userName name

    let setup credentials= 
        userCredential <- credentials
        Login.userLogin credentials

        //while(not System.Diagnostics.Debugger.IsAttached) do System.Threading.Thread.Sleep(500);

        deleteAllProfiles credentials
        profileNatalia <- {defaultProfile with FullName = Some (fullProfileName "Natalia")} 
        createPersonProfileWithAccessibilityTesting profileNatalia 
        profileValentin <- {defaultProfile with Gender = Some "Man"; FullName = Some (fullProfileName "Valentin")}
        createPersonProfileWithAccessibilityTesting profileValentin
    
    let switchProfile profile=
        switchProfile profile.FullName.Value

    let selectProfile profile =         
        selectProfile profile.FullName.Value


    [<TearDown>]
    member this.TearDown()=
        switchProfile profileValentin
        selectProfile profileValentin
        deleteProfile()
        switchProfile profileNatalia
        selectProfile profileNatalia
        deleteProfile()
        Login.userLogout()
      

    // THE value must be one always as we just created profile and there should not be any reports
    // THE value must be one always as we just created profile and there should not be any reports

    [<UseDriver>]
    [<Category("Positive")>] 
    [<Test>]    
    member this.SwitchProfileRaiseReport_ProperNumberOfReportRaised()=
        let user = user_luck
        setup user
        switchProfile profileNatalia
        clickMoreButton()
        click _reportButton
        _reportType << "spam"       
        click _postReportButton
        _reportCounter == "1" 
        Login.userLogout()
        Login.userLogin user
        switchProfile profileNatalia
        selectProfile profileValentin
        clickMoreButton()
        click _reportButton
        _reportType << "spam"       
        click _postReportButton
        _reportCounter == "2" 

   
    [<UseDriver>]
    [<Category("Positive")>] 
    [<Test>]    
    member this.SwitchProfileAddFriend_FriendRequestSent()=
        let user = user_lily
        setup user
        switchProfile profileNatalia
        selectProfile profileValentin
        _addFriendProfileButtonInfo == "Add friend"
        click _addFriendProfileButton
        _addFriendRequestedProfileButtonInfo == "Add friend (requested)"
        
        
    [<UseDriver>]
    [<Category("Positive")>] 
    [<Test>]    
    member this.SwitchProfileAddFriendAndCancel_FriendRequestCanceled()=
        let user = user_eva
        setup user
        switchProfile profileNatalia
        selectProfile profileValentin
        _addFriendProfileButtonInfo == "Add friend"
        click _addFriendProfileButton
        _addFriendRequestedProfileButtonInfo == "Add friend (requested)"
        clickMoreButton ()
        click _cancelFriendRequestProfileButton
        _addFriendProfileButtonInfo == "Add friend"

    [<UseDriver>]
    [<Category("Positive")>] 
    [<Test>]    
    member this.SwitchProfileAddFriend_RequestAccepted()=
        let user = user_linda
        setup user
        //we're already in Valentins profile switchProfile profileValentin
        selectProfile profileNatalia
        //_addFriendProfileButtonInfo == "Add friend"
        click _addFriendProfileButton
        _addFriendRequestedProfileButtonInfo == "Add friend (requested)"
        Login.userLogout()
        Login.userLogin user
        switchProfile profileNatalia
        selectProfile profileValentin
        _acceptFriendRequestButtonInfo == "Accept friend request"
        click _acceptFriendRequestButton
        _unfriendButtonInfo == "Unfriend" 
        click _personalProfileFriendButton
        _friendName == profileNatalia.FullName.Value

    [<UseDriver>]
    [<Category("Positive")>] 
    [<Test>]    
    member this.SwitchProfileUnfriend_Unfriended()=
        let user = user_emma
        setup user
        //we're already in Valentins profile switchProfile profileValentin
        selectProfile profileNatalia
        //_addFriendProfileButtonInfo == "Add friend"
        click _addFriendProfileButton
        _addFriendRequestedProfileButtonInfo == "Add friend (requested)"
        Login.userLogout()
        Login.userLogin user
        switchProfile profileNatalia
        selectProfile profileValentin
        _acceptFriendRequestButtonInfo == "Accept friend request"
        click _acceptFriendRequestButton
        _unfriendButtonInfo == "Unfriend"
        click _personalProfileFriendButton
        _friendName == profileNatalia.FullName.Value
        _unfriendButtonInfo == "Unfriend"   
        click _unfriendButton 
        click _personalProfileFriendButton
        _addFriendProfileButtonInfo == "Add friend"


    
    [<UseDriver>]
    [<Test>]    
    member this.SwitchProfileFollow_Followed()=
        let user = user_karen
        setup user
        switchProfile profileValentin
        selectProfile profileNatalia
        _followProfileButtonInfo == "Follow"
        click _followProfileButtonInfo
        _unfollowProfileButtonInfo == "Unfollow" 

    [<UseDriver>]
    [<Test>]    
    member this.SwitchProfileUnfollow_Unfollowed()=
        let user = user_ella
        setup user
        switchProfile profileValentin
        selectProfile profileNatalia
        _followProfileButtonInfo == "Follow"
        click _followProfileButtonInfo
        _unfollowProfileButtonInfo == "Unfollow"  
        click _unfollowProfileButton
        _followProfileButtonInfo == "Follow"   
        

    [<UseDriver>]
    [<Test>]    
    member this.SwitchProfileFollow_GetsFollowers()=
        let user = user_viky
        setup user
        selectProfile profileNatalia
        _followProfileButtonInfo == "Follow"
        click _followProfileButtonInfo
        _unfollowProfileButtonInfo == "Unfollow"  
        Login.userLogout()
        Login.userLogin user
        switchProfile profileNatalia
        selectProfile profileNatalia 
        click _personalProfileFollowButton
        scrollTo _followersName
        _followersName == profileValentin.FullName.Value


    [<UseDriver>]
    [<Test>]    
    member this.SwitchProfileFollow_GetsFollowing()=
        let user = user_mila
        setup user
        selectProfile profileNatalia
        _followProfileButtonInfo == "Follow"
        click _followProfileButtonInfo
        _unfollowProfileButtonInfo == "Unfollow"  
        Login.userLogout()
        Login.userLogin user
        switchProfile profileValentin
        selectProfile profileValentin 
        click _personalProfileFollowButton
        _followingName == profileNatalia.FullName.Value

[<Parallelizable(ParallelScope.Children)>]
type ProfileToolbarTests2 () = 

    let mutable userCredential = user_luck
    let mutable profileNatalia = defaultProfile
    let mutable profileValentin = defaultProfile

    let fullProfileName name =
        sprintf "%s-%s" userCredential.userName name

    let setup credentials= 
        userCredential <- credentials
        Login.userLogin credentials

        //while(not System.Diagnostics.Debugger.IsAttached) do System.Threading.Thread.Sleep(500);

        deleteAllProfiles credentials
        profileNatalia <- {defaultProfile with FullName = Some (fullProfileName "Natalia")} 
        createPersonProfileWithAccessibilityTesting profileNatalia 
        profileValentin <- {defaultProfile with Gender = Some "Man"; FullName = Some (fullProfileName "Valentin")}
        createPersonProfileWithAccessibilityTesting profileValentin
    
    let switchProfile profile=
        switchProfile profile.FullName.Value

    let selectProfile profile =         
        selectProfile profile.FullName.Value

    
    [<TearDown>]
    member this.TearDown()=
        switchProfile profileValentin
        selectProfile profileValentin
        deleteProfile()
        switchProfile profileNatalia
        selectProfile profileNatalia
        deleteProfile()
        Login.userLogout()

    [<UseDriver>]
    [<Category("Positive")>] 
    [<TestCase("husband",0)>]
    [<TestCase("wife",1)>]
    [<TestCase("father",2)>]
    [<TestCase("mother",3)>]
    [<TestCase("son",4)>]
    [<TestCase("daughter",5)>]
    [<TestCase("brother",6)>]
    [<TestCase("sister",7)>]
    member this.SwitchProfileAddRelationship_AddedRelation relation index=
        let user = [user_karen; user_ella; user_viky; user_mila; user_eric; user_jack; user_rob; user_dave].[index]
        setup user
        switchProfile profileValentin
        selectProfile profileNatalia
        _addFriendProfileButtonInfo == "Add friend"
        click _addFriendProfileButton
        _addFriendRequestedProfileButtonInfo == "Add friend (requested)"
        Login.userLogout()
        Login.userLogin user
        switchProfile profileNatalia
        selectProfile profileValentin
        _acceptFriendRequestButtonInfo == "Accept friend request"
        click _acceptFriendRequestButton
        _unfriendButtonInfo == "Unfriend"
        click _personalProfileFriendButton
        _friendName == profileNatalia.FullName.Value
        _addRelationshipButtonInfo == "Add Relationship"
        click _addRelationshipButton
        clickSelectRelationshipStatus relation
        click _personalProfileRelationshipButton
        scrollTo _relationshipInfo
        _relationshipInfo == relation


    [<UseDriver>]
    [<Category("Positive")>] 
    [<TestCase("husband",0)>]
    [<TestCase("wife",1)>]
    [<TestCase("father",2)>]
    [<TestCase("mother",3)>]
    [<TestCase("son",4)>]
    [<TestCase("daughter",5)>]
    [<TestCase("brother",6)>]
    [<TestCase("sister",7)>]
    member this.SwitchProfileRemoveRelationship_RemoveRelation relation index=
        let user = [user_tom; user_andy; user_ivan; user_luck; user_lily; user_eva; user_linda; user_emma].[index]
        setup user
        switchProfile profileValentin
        selectProfile profileNatalia
        _addFriendProfileButtonInfo == "Add friend"
        click _addFriendProfileButton
        _addFriendRequestedProfileButtonInfo == "Add friend (requested)"
        Login.userLogout()
        Login.userLogin user
        switchProfile profileNatalia
        selectProfile profileValentin
        _acceptFriendRequestButtonInfo == "Accept friend request"
        click _acceptFriendRequestButton
        _unfriendButtonInfo == "Unfriend"
        click _personalProfileFriendButton
        let friendName = read _friendName
        printfn "before check: friendName=%s   profileNatalia.FullName.Value=%s" friendName profileNatalia.FullName.Value
        _friendName == profileNatalia.FullName.Value
        _addRelationshipButtonInfo == "Add Relationship"
        click _addRelationshipButton
        clickSelectRelationshipStatus relation
        click _personalProfileRelationshipButton
        scrollTo _relationshipInfo
        _relationshipInfo == relation
        click _removeRelationshipButton
        _addRelationshipButtonInfo == "Add Relationship"
