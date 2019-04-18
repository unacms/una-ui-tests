module TestHelpers

open System.Threading

let rec retry times fn = 
    if times > 1 then
        try
            fn()
        with 
        | _ -> 
            Thread.Sleep 1000
            retry (times - 1) fn
    else
        fn()