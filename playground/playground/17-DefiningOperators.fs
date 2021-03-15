namespace Playground

/// You can define functions named using one or more of the operator symbols
/// See the F# documentation for the exact list of symbols that you can use: 
/// https://docs.microsoft.com/en-us/dotnet/fsharp/language-reference/operator-overloading
///
/// To learn more, see: https://fsharpforfunandprofit.com/posts/defining-functions/
module DefiningOperators =

    // You must use parentheses around the symbols when defining them.
    let (.*%) x y = x + y + 1
    
    // Note that for custom operators that begin with *, a space is required; otherwise the (* is interpreted as the start of a comment:
    let ( *+* ) x y = x + y + 1
    
    // Once defined, the new function can be used in the normal way, again with parentheses around the symbols:
    let result' = (.*%) 2 3
    
    // If the function has exactly two parameters, you can use it as an infix operator without parentheses.
    let result'' = 2 .*% 3

    // You can also define prefix operators that start with ! or ~ (with some restrictions – see the F# documentation on operator overloading)
    let (~%%) (s:string) = s.ToCharArray()
    
    // use
    let result''' = %% "hello"
    
    // In F# it is quite common to create your own operators, 
    // and many libraries will export operators with names such as >=> and <*>.
