namespace Pages

type Home =
    static member uri = Common.baseuri

type Signup = 
    static member uri = sprintf "%s/page/create-account" Common.baseuri        