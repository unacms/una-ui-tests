namespace Tests


open NUnit.Framework
open VCanopy.NUnit
open System
open OpenQA.Selenium.Chrome
open OpenQA.Selenium
open OpenQA.Selenium.Remote
open System

[<SetUpFixture>]
type TestsSetup () =

    static let setDriverProperties (driver:IWebDriver)=
        //driver. to do set time out
        driver.Manage().Window.Size <- System.Drawing.Size(1050,1000)        
        printfn "BrowserSize %A" (driver.Manage().Window.Size)
        driver.Manage().Timeouts().AsynchronousJavaScript <- TimeSpan.FromSeconds(54.)

        //Be carefull with ImplicitWait as if you specify value more then ChromeDriver CommandTimeout which was passed to the ChromeDriver constructor
        //Then tests will fail badly when no element is found. It will look similar like ChromeDriver hangs.
        driver.Manage().Timeouts().ImplicitWait <- TimeSpan.FromSeconds(11.)
        driver.Manage().Timeouts().PageLoad <- TimeSpan.FromSeconds(56.)


    static let createChromeDriver(testName:string):IWebDriver = 
        let co = new ChromeOptions()
        co.AddArgument("no-sandbox")
        let a = ChromeDriverService.CreateDefaultService()

        let driver = new ChromeDriver(a, co, TimeSpan.FromSeconds(59.))
        setDriverProperties driver
        upcast driver

    static let createRemoteDriver (testName:string):IWebDriver =
        let browserUrl = "http://192.168.56.1:4444/wd/hub/" 
        let browserName = "chrome"

        let capability = OpenQA.Selenium.Remote.DesiredCapabilities()
        capability.SetCapability("browserName", browserName) 
        capability.SetCapability("name",  testName);
        let driver = new RemoteWebDriver(Uri(browserUrl), capability, TimeSpan.FromSeconds(59.0))
        setDriverProperties driver
        upcast driver

    [<OneTimeSetUp>]    
    member this.GlobalSetup () = 
        setDriverFactory createChromeDriver
        setConfig {WebDriverInstanceCount = 4; CompleteDriverRelease = true}
        VCanopy.Configuration.configuration1 <- {VCanopy.Configuration.configuration1 with ClickDelayMs=500; SaveScreenshotOnFailure = true
            

            //; ScreenshotPath=Some "."
            }
        //VCanopy.Configuration.configuration1.ScreenshotPath

    [<OneTimeTearDown>]   
    member this.GlobalTeardown () = ()