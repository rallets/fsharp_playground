namespace Playground

/// To learn more, see: https://fsharpforfunandprofit.com/posts/defining-functions/
module DefiningFunctionsLambda =

    // typical functions using the “let” syntax, below:
    let add1 x y = x + y

    // An anonymous function (or “lambda expression”) is defined using the form:
    // fun parameter1 parameter2 etc -> expression
    let add2 = fun x y -> x + y

    // with separately defined function
    let add3 i = i + 1
    let result3 = [1..10] |> List.map add3
    
    // inlined without separately defined function
    let result4 = [1..10] |> List.map (fun i -> i + 1)

    // Lambdas are also used when you want to make it clear that you are returning a function from another function.
    // original definition
    let adderGenerator1 x = (+) x
    
    // The lambda version is slightly longer, but makes it clear that an intermediate function is being returned.
    let adderGenerator2 x = fun y -> x + y

    // You can nest lambdas as well. Here is yet another definition of adderGenerator, this time using lambdas only.
    let adderGenerator3 = fun x -> (fun y -> x + y)

/// When defining a function, you can pass an explicit parameter, as we have seen, but you can also pattern match directly in the parameter section. 
/// In other words, the parameter section can contain patterns, not just identifiers
module DefiningFunctionsPatternMatching =

    type Name = {first:string; last:string} // define a new type
    let bob = {first="bob"; last="smith"}   // define a value

    // single parameter style
    let f1 name =                       // pass in single parameter
       let {first=f; last=l} = name     // extract in body of function
       printfn "first=%s; last=%s" f l

    // match in the parameter itself
    let f2 {first=f; last=l} =          // direct pattern matching
       printfn "first=%s; last=%s" f l

    // test
    f1 bob
    f2 bob

module DefiningFunctionsTuples = 
    // A function that takes two distinct parameters separated with spaces.
    let addTwoParams x y = x + y

    // A function that takes a single tuple parameter. 
    // It takes a single parameter, and then binds “x” and “y” to the inside of the tuple and does the addition
    let addTuple aTuple =
       let (x,y) = aTuple
       x + y

    // Another function that takes a single tuple parameter but looks like it takes two ints
    // Takes a single parameter just like “addTuple”, but the tricky thing is that the tuple is unpacked and bound as part of the parameter definition using pattern matching.
    // Behind the scenes, it is exactly the same as “addTuple”.
    let addTuple2 (x,y) = x + y

    // Now let’s use them:
    let result1 = addTwoParams 1 2  // uses spaces to separate args
    let result2 = addTuple (1,2)    // pass a single tuple (to make a tuple, use a comma)
    let result3 = addTuple2 (1,2)   // pass a single tuple (to make a tuple, use a comma)

    // When would we want to use tuple parameters instead of individual ones?
    // 1) When the tuples are meaningful in themselves. 
    //      For example, if we are working with three dimensional coordinates, a three-tuple might well be more convenient than three separate dimensions.
    // 2) Tuples are occasionally used to bundle data together in a single structure that should be kept together. 
    //      For example, the TryParse functions in .NET library return the result and a Boolean as a tuple. 
    //      But if you have a lot of data that is kept together as a bundle, then you will probably want to define a record or class type to store it.

    // Here are some general guidelines of how to structure parameters when you are designing your own functions.
    // 1) In general, it is always better to use separate parameters rather than passing them as a single structure such as a tuple or record. 
    // This allows for more flexible behavior such as partial application.
    // 2) But, when a group of parameters must all be set at once, then do use some sort of grouping mechanism.
    // Example:
    
    // Pass in two numbers for addition.
    // The numbers are independent, so use two parameters
    let add x y = x + y
    
    // Pass in two numbers as a geographical co-ordinate.
    // The numbers are dependent, so group them into a tuple or record
    let locateOnMap (xCoord,yCoord) = 
        // do something
        printfn "xCoord=%f; yCoord=%f" xCoord yCoord
    
    // Set first and last name for a customer.
    // The values are dependent, so group them into a record.
    type CustomerName = {First:string; Last:string}
    // good
    let setCustomerName1 aCustomerName = printfn "aCustomerName=%s" aCustomerName
    // not recommended
    let setCustomerName2 first last = printfn "first=%s; last=%s" first last
    
    // Set first and last name and and pass the
    // authorizing credentials as well.
    // The name and credentials are independent, keep them separate
    let setCustomerName3 myCredentials aName =
        printfn "myCredentials=%s; aName=%s" myCredentials aName

/// A special case: tuples and .NET library functions
module DefiningFunctionsTuplesBcl = 

    // The reason is that .NET library functions are not curried and cannot be partially applied. 
    // All the parameters must always be passed in, and using a tuple-like approach is the obvious way to do this.
    // correct
    let result1 = System.String.Compare("a","b")

    // incorrect
    // let result2 = System.String.Compare "a" "b"

    // But do note that although these calls look like tuples, they are actually a special case. 
    // Real tuples cannot be used, so the following code is invalid:

    let tuple = ("a","b")
    // let result3 = System.String.Compare tuple   // error
    // let result4 = System.String.Compare "a","b" // error

    // If you do want to partially apply .NET library functions, it is normally trivial to write wrapper functions for them, as we have seen earlier
    // create a wrapper function
    let strCompare x y = System.String.Compare(x,y)
    
    // partially apply it
    let strCompareWithB = strCompare "B"
    
    // use it with a higher order function
    let result5 = 
        ["A";"B";"C"]
        |> List.map strCompareWithB

/// Sometimes we may want functions that don’t take any parameters at all. 
/// For example, we may want a “hello world” function that we can call repeatedly. 
/// As we saw in a previous section, the naive definition will not work.
module DefiningFunctionsParameterLess = 
    open System

    // not what we want
    let sayHello1 = printfn "Hello World!"     

    // The fix is to add a unit parameter to the function, or use a lambda.
    // good
    let sayHello2() = printfn "Hello World!"
    // good
    let sayHello3 = fun () -> printfn "Hello World!"

    // And then the function must always be called with a unit argument:
    sayHello2()
    sayHello3()

    // This is particularly common with the .NET libraries. 
    // Do remember to call them with the unit parameter!
    // Some examples are:
    let line = Console.ReadLine()
    let args = System.Environment.GetCommandLineArgs()
    let dir = System.IO.Directory.GetCurrentDirectory()

/// We have already seen many examples of leaving off the last parameter of functions to reduce clutter. 
/// This style is referred to as point-free style or tacit programming.
module DefiningFunctionsPointFree = 

    let add1 x y = x + y   // explicit
    let add2 x = (+) x     // point free
    
    let add1Times2_1 x = (x + 1) * 2    // explicit
    let add1Times2_2 = (+) 1 >> (*) 2   // point free
    
    let sum1 list = List.reduce (fun sum e -> sum+e) list // explicit
    let sum2 = List.reduce (+)                            // point free

module DefiningFunctionsRecursive =
    
    // Often, a function will need to refer to itself in its body. The classic example is the Fibonacci function:
    // You have to tell the compiler that this is a recursive function using the rec keyword.
    
    let rec fib i =
       match i with
       | 1 -> 1
       | 2 -> 1
       | n -> fib(n-1) + fib(n-2)