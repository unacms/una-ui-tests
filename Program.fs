open canopy
open canopy.classic
open canopy.runner.classic

[<EntryPoint>]
let main _ =
  configuration.wipSleep <- 0.2
  configuration.compareTimeout <- 10.0
  configuration.elementTimeout <- 10.0
  configuration.pageTimeout <- 10.0
  configuration.autoPinBrowserRightOnLaunch <- false
  //configuration.failFast := true


  configuration.chromeDir <- "/usr/bin"
//   configuration.firefoxDriverDir <- "c:\work"
//   configuration.firefoxDir <- @"c:\Users\alex\AppData\Local\Mozilla Firefox\firefox.exe"
//   configuration.showInfoDiv <- false

  
  try

    start chrome
  
    SignupTests.all()

    run()

  finally
    quit()

  canopy.runner.classic.failedCount