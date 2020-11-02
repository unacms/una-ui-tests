namespace Tests.LoginTests2


open NUnit.Framework
open VCanopy.GivenWhenThen
open VCanopy.Functions
open VCanopy.NUnit
open Login
open Common




[<Parallelizable(ParallelScope.Children)>]
type LoginTests () =

    [<UseDriver>]
    [<Test>]  
    [<Category("Positive")>]  
    member this.LoginNewUser_CreatesNewUser()=
        let user = user_luck
        //while(not System.Diagnostics.Debugger.IsAttached) do System.Threading.Thread.Sleep(500);
        goto Pages.Login.uri
        click Header._loginButton
        _email << user.userEmail
        _password << user.userPassword
        click _loginButton
        userLogout()

    [<UseDriver>]
    [<Test>]
    [<Category("Positive")>]
    member this.SmokeLoginTestGWT ()=
        Given "a not logged in user on home page"       (fun _ ->
            goto Pages.Login.uri
                                                        ) |>
        When "user clicks header login button and enters a correct login details"      (fun _ ->
            let user = user_lily
            click Header._loginButton
            _email << user.userEmail
            _password << user.userPassword
                                                        ) |>
        AndWhen "user clicks submit login button "      (fun _ ->
            click _loginButton
            
                                                        ) |>
        Then "user logs in "                            (fun _ ->
            //todo add a check that we're in a portal
            ()
                                                        ) |>
        AndThen "can logout by pressing `account` button & `sign out` button "    (fun _ ->
            userLogout() 

                                                        ) |>
        Run                                                                


    [<UseDriver>]
    [<Test>] 
    [<Category("Negative")>]

    member this.EmptyEmailField_ShowsErrorMessage()=
        
        //while(not System.Diagnostics.Debugger.IsAttached) do System.Threading.Thread.Sleep(500);
        goto Pages.Login.uri
        _password << "unaUna123"
        click _loginButton

        Login._loginEmailError == "Error Occurred"

    [<UseDriver>]
    [<TestCase("a.com")>] 
    [<TestCase("acom")>] 
    [<TestCase("acom#com")>] 
    [<TestCase("acom@")>] 
    [<TestCase("a..b@com")>] 
    [<TestCase("рпрвыф@com")>] 
    [<TestCase(".A@com")>]
    [<TestCase("a@-com")>] 
    [<TestCase("a@12345.121")>]  
    [<TestCase("@a.com")>] 
    [<TestCase("ca#p@com")>] 
    [<TestCase("ca$p@com")>] 
    [<TestCase("ca%p@com")>] 
    [<TestCase("ca^p@com")>] 
    [<TestCase("ca&p@com")>] 
    [<TestCase("ca*p@com")>] 
    [<TestCase("a.@com")>] 
    [<Category("Negative")>]

    member this.IncorrectEmailAddress_ShowsErrorMessage email =
        goto Pages.Login.uri
        _email << email
        _password << "unaUna123"
        click _loginButton

        Login._loginEmailError == "Entered email or password is incorrect. Please try again."   


    [<UseDriver>]
    [<TestCase(null,0)>] 
    [<TestCase("",1)>] 
    [<TestCase("1234",1)>] 
    [<TestCase("qwerty",2)>] 
    [<Category("Negative")>]
    member this.IncorrectPasswordField_ShowsErrorMessage password userId=
        //we can't use same email more than 2 times as it would lock the account
        let user = if (userId = 1) then user_eva else user_linda
        goto Pages.Login.uri
        _email << user.userEmail
        if not (isNull password) then
            _password << password
        click _loginButton

        Login._loginEmailError == "Entered email or password is incorrect. Please try again."
