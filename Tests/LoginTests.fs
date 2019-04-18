namespace Tests.LoginTests2


open NUnit.Framework
open VCanopy.GivenWhenThen
open VCanopy.Functions
open VCanopy.NUnit
open Login
open Common






[<Parallelizable(ParallelScope.All)>]
type LoginTests () =




    [<UseDriver>]
    [<Test>]    
    member this.``Smoke Login test and check name with spaces`` ()=
        //while(not System.Diagnostics.Debugger.IsAttached) do System.Threading.Thread.Sleep(500);
        goto Pages.Login.uri
        click Header._loginButton
        _email << defaultAdmin.userName
        _password << defaultAdmin.userPassword
        click _loginButton
        click Header._accountButton
        click Header._accountLogout 
        ()

    [<UseDriver>]
    [<Test>]
    member this.SmokeLoginTestGWT ()=
        Given "a not logged in user on home page"       (fun _ ->
            goto Pages.Login.uri
                                                        ) |>
        When "user clicks header login button and enters a correct login details"      (fun _ ->
            click Header._loginButton
            _email << defaultAdmin.userName
            _password << defaultAdmin.userPassword
                                                        ) |>
        AndWhen "user clicks submit login button "      (fun _ ->
            click _loginButton
            
                                                        ) |>
        Then "user logs in "                            (fun _ ->
            //todo add a check that we're in a portal
            ()
                                                        ) |>
        AndThen "can logout by pressing `account` button & `sign out` button "    (fun _ ->
            click Header._accountButton
            click Header._accountLogout 

                                                        ) |>
        Run                                                                

