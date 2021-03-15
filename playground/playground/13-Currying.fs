namespace Playground

/// To learn more, see: https://fsharpforfunandprofit.com/posts/currying/
module Currying = 
    // normal version, implicitly curried
    let printTwoParameters x y = printfn "x=%i y=%i" x y

    // Internally, the compiler rewrites it as something more like:
    // explicitly curried version
    let printTwoParameters2 x  =    // only one parameter!
       let subFunction y =
          printfn "x=%i y=%i" x y  // new function with one param
       subFunction                 // return the subfunction
    
    // eval with one argument get back a function
    let fn = printTwoParameters 1

    // So what you are really doing when you call printTwoParameters with two arguments is:
    // - You call printTwoParameters with the first argument (x)
    // - printTwoParameters returns a new function that has “x” baked into it.
    // - You then call the new function with the second argument (y)
    // Here is an example of the step by step version, and then the normal version again.
    
    // step by step version
    let x = 6
    let y = 99
    let intermediateFn = printTwoParameters x  // return fn with x "baked in"
    let result1  = intermediateFn y
    
    // inline version of above
    let result2  = (printTwoParameters x) y
    
    // normal version
    let result3  = printTwoParameters x y

    // Here is another example:
    
    //normal version
    let addTwoParameters1 x y = x + y
    
    //explicitly curried version
    let addTwoParameters2 x  =      // only one parameter!
       let subFunction y =
          x + y                    // new function with one param
       subFunction                 // return the subfunction
    
    // now use it step by step
    let x4 = 6
    let y4 = 99
    let intermediateFn4 = addTwoParameters1 x4  // return fn with x "baked in"
    let result4  = intermediateFn y4
    
    // normal version
    let result5  = addTwoParameters1 x y
    
    // using plus as a single value function
    let x6 = 6
    let y6 = 99
    let intermediateFn6 = (+) x6     // return add with x baked in
    let result6  = intermediateFn6 y6
    
    // using plus as a function with two parameters
    let result7  = (+) x6 y6
    
    // normal version of plus as infix operator
    let result8  = x6 + y6
