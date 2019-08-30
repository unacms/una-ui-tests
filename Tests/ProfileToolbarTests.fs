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

type ProfileToolbarTests () =

    let mutable userCredential = user_luck

    let fullProfileName name =
        sprintf "%s-%s" userCredential.userName name

    let toFullProfileName profile=
        match profile.FullName with
        | Some name -> {profile with FullName = Some (fullProfileName name)}
        | None -> profile


    let setup credentials= 
        userCredential <- credentials
        Login.userLogin credentials

        //while(not System.Diagnostics.Debugger.IsAttached) do System.Threading.Thread.Sleep(500);

        deleteAllProfiles credentials
        let femaleProfile = {defaultProfile with FullName = Some "Natalia"} |> toFullProfileName
        //let femaleProfile = {defaultProfile with FullName = Some (fullProfileName "Natalia")} 
        createPersonProfileWithAccessibilityTesting femaleProfile 
        let maleProfile = {defaultProfile with Gender = Some "Man"; FullName = Some "Valentin"} |> toFullProfileName
        createPersonProfileWithAccessibilityTesting maleProfile
    
    let switchProfile profileName=
        let fullProfileName = fullProfileName profileName
        AccountPopup.switchProfile fullProfileName
    

    [<TearDown>]
    member this.TearDown()=
        switchProfile "Valentin"
        selectProfile "Valentin"
        deleteProfile()
        switchProfile "Natalia"
        selectProfile "Natalia"
        deleteProfile()
        Login.userLogout()
      

    // THE value must be one always as we just created profile and there should not be any reports
    // THE value must be one always as we just created profile and there should not be any reports

    [<UseDriver>]
    [<Category("Positive")>] 
    [<Test>]    
    member this.SwithProfileRaisReport_ProperNumberOfReportPaised()=
        let user = user_luck
        setup user
        switchProfile "Natalia"
        clickMoreButton()
        click _reportButton
        _reportType << "spam"       
        click _postReportButton
        _reportCounter == "1" 
        Login.userLogout()
        Login.userLogin user
        switchProfile "Natalia"
        selectProfile "Valentin"
        clickMoreButton()
        click _reportButton
        _reportType << "spam"       
        click _postReportButton
        _reportCounter == "2" 

   
    [<UseDriver>]
    [<Category("Positive")>] 
    [<Test>]    
    member this.SwithProfileAddFriend_FriendRequestSent()=
        let user = user_lily
        setup user
        switchProfile "Natalia"
        selectProfile "Valentin"
        _addFriendProfileButtonInfo == "Add friend"
        click _addFriendProfileButton
        _addFriendRequestedProfileButtonInfo == "Add friend (requested)"
        
        
    [<UseDriver>]
    [<Category("Positive")>] 
    [<Test>]    
    member this.SwithProfileAddFriendAndCancel_FriendRequestCanceled()=
        let user = user_eva
        setup user
        switchProfile "Natalia"
        selectProfile "Valentin"
        _addFriendProfileButtonInfo == "Add friend"
        click _addFriendProfileButton
        _addFriendRequestedProfileButtonInfo == "Add friend (requested)"
        clickMoreButton ()
        click _cancelFriendRequestProfileButton
        _addFriendProfileButtonInfo == "Add friend"

    [<UseDriver>]
    [<Category("Positive")>] 
    [<Test>]    
    member this.SwithProfileAddFriend_RequestAccepted()=
        let user = user_linda
        setup user
        //switchProfile "Valentin"
        selectProfile "Natalia"
        //_addFriendProfileButtonInfo == "Add friend"
        click _addFriendProfileButton
        _addFriendRequestedProfileButtonInfo == "Add friend (requested)"
        Login.userLogout()
        Login.userLogin user
        switchProfile "Natalia"
        selectProfile "Valentin"
        _acceptFriendRequestButtonInfo == "Accept friend request"
        click _acceptFriendRequestButton
        _unfriendButtonInfo == "Unfriend" 
        click _personalProfileFriendButton
        _friendName == "Natalia"

    [<UseDriver>]
    [<Category("Positive")>] 
    [<Test>]    
    member this.SwithProfileUnfriend_Unfriended()=
        let user = user_emma
        setup user
        //switchProfile "Valentin"
        selectProfile "Natalia"
        //_addFriendProfileButtonInfo == "Add friend"
        click _addFriendProfileButton
        _addFriendRequestedProfileButtonInfo == "Add friend (requested)"
        Login.userLogout()
        Login.userLogin user
        switchProfile "Natalia"
        selectProfile "Valentin"
        _acceptFriendRequestButtonInfo == "Accept friend request"
        click _acceptFriendRequestButton
        _unfriendButtonInfo == "Unfriend"
        click _personalProfileFriendButton
        _friendName == "Natalia"
        _unfriendButtonInfo == "Unfriend"   
        click _unfriendButton 
        click _personalProfileFriendButton
        _addFriendProfileButtonInfo == "Add friend"


    
    [<UseDriver>]
    [<Test>]    
    member this.SwithProfileFollow_Followed()=
        let user = user_karen
        setup user
        switchProfile "Valentin"
        selectProfile "Natalia"
        _followProfileButtonInfo == "Follow"
        click _followProfileButtonInfo
        _unfollowProfileButtonInfo == "Unfollow" 

    [<UseDriver>]
    [<Test>]    
    member this.SwithProfileUnfollow_Unfollowed()=
        let user = user_ella
        setup user
        switchProfile "Valentin"
        selectProfile "Natalia"
        _followProfileButtonInfo == "Follow"
        click _followProfileButtonInfo
        _unfollowProfileButtonInfo == "Unfollow"  
        click _unfollowProfileButton
        _followProfileButtonInfo == "Follow"   
        

    [<UseDriver>]
    [<Test>]    
    member this.SwithProfileFollow_GetsFollowers()=
        let user = user_viky
        setup user
        selectProfile "Natalia"
        _followProfileButtonInfo == "Follow"
        click _followProfileButtonInfo
        _unfollowProfileButtonInfo == "Unfollow"  
        Login.userLogout()
        Login.userLogin user
        switchProfile "Natalia"
        selectProfile "Natalia" 
        click _personalProfileFollowButton
        scrollTo _followersName
        _followersName == "Valentin"


    [<UseDriver>]
    [<Test>]    
    member this.SwithProfileFollow_GetsFollowing()=
        let user = user_mila
        setup user
        selectProfile "Natalia"
        _followProfileButtonInfo == "Follow"
        click _followProfileButtonInfo
        _unfollowProfileButtonInfo == "Unfollow"  
        Login.userLogout()
        Login.userLogin user
        switchProfile "Valentin"
        selectProfile "Valentin" 
        click _personalProfileFollowButton
        _followingName == "Natalia"

[<Parallelizable(ParallelScope.Children)>]

type ProfileToolbarTests2 () = 

    let setup credentials= 
        Login.userLogin credentials

        //while(not System.Diagnostics.Debugger.IsAttached) do System.Threading.Thread.Sleep(500);

        deleteAllProfiles credentials
        let femaleProfile = {defaultProfile with FullName = Some "Natalia"}
        createPersonProfileWithAccessibilityTesting femaleProfile 
        let maleProfile = {defaultProfile with Gender = Some "Man"; FullName = Some "Valentin"}
        createPersonProfileWithAccessibilityTesting maleProfile
   

    [<TearDown>]
    member this.TearDown()=
        switchProfile "Valentin"
        selectProfile "Valentin"
        deleteProfile()
        switchProfile "Natalia"
        selectProfile "Natalia"
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
    member this.SwithProfileAddRelationship_AddedRelation relation index=
        let user = [user_karen; user_ella; user_viky; user_mila; user_eric; user_jack; user_rob; user_dave].[index]
        setup user
        //switchProfile "Valentin"
        selectProfile "Natalia"
        _addFriendProfileButtonInfo == "Add friend"
        click _addFriendProfileButton
        _addFriendRequestedProfileButtonInfo == "Add friend (requested)"
        Login.userLogout()
        Login.userLogin user
        switchProfile "Natalia"
        selectProfile "Valentin"
        _acceptFriendRequestButtonInfo == "Accept friend request"
        click _acceptFriendRequestButton
        _unfriendButtonInfo == "Unfriend"
        click _personalProfileFriendButton
        _friendName == "Natalia"
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
    member this.SwithProfileRemoveRelationship_RemoveRelation relation index=
        let user = [user_tom; user_andy; user_ivan; user_luck; user_lily; user_eva; user_linda; user_emma].[index]
        setup user
        switchProfile "Valentin"
        selectProfile "Natalia"
        _addFriendProfileButtonInfo == "Add friend"
        click _addFriendProfileButton
        _addFriendRequestedProfileButtonInfo == "Add friend (requested)"
        Login.userLogout()
        Login.userLogin user
        switchProfile "Natalia"
        selectProfile "Valentin"
        _acceptFriendRequestButtonInfo == "Accept friend request"
        click _acceptFriendRequestButton
        _unfriendButtonInfo == "Unfriend"
        click _personalProfileFriendButton
        _friendName == "Natalia"
        _addRelationshipButtonInfo == "Add Relationship"
        click _addRelationshipButton
        clickSelectRelationshipStatus relation
        click _personalProfileRelationshipButton
        scrollTo _relationshipInfo
        _relationshipInfo == relation
        click _removeRelationshipButton
        _addRelationshipButtonInfo == "Add Relationship"
