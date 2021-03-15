namespace Playground

/// The idea of partial application is that if you fix the first N parameters of the function, 
/// you get a function of the remaining parameters.
/// To learn more, see: https://fsharpforfunandprofit.com/posts/partial-application/
module PartialApplication = 
    // create an "adder" by partial application of add
    let add42 = (+) 42    // partial application
    let result1 = add42 1
    let result2 = add42 3
    
    // create a new list by applying the add42 function to each element
    let result3 = [1;2;3] |> List.map add42
    
    // create a "tester" by partial application of "less than"
    let twoIsLessThan = (<) 2   // partial application
    let result4 = twoIsLessThan 1
    let result5 = twoIsLessThan 3
    
    // filter each element with the twoIsLessThan function
    let result6 = [1;2;3] |> List.filter twoIsLessThan
    
    // create a "printer" by partial application of printfn
    let printer = printfn "printing param=%i"
    
    // loop over each element and call the printer function
    [1;2;3] |> List.iter printer

    // create an adder that supports a pluggable logging function
    let adderWithPluggableLogger logger x y =
        logger "x" x
        logger "y" y
        let result = x + y
        logger "x+y"  result
        result
    
    // create a logging function that writes to the console
    let consoleLogger argName argValue = 
        printfn "%s=%A" argName argValue
    
    //create an adder with the console logger partially applied
    let addWithConsoleLogger = adderWithPluggableLogger consoleLogger
    let result7 = addWithConsoleLogger 1 2
    let result8 = addWithConsoleLogger 42 99
    
    // create a logging function that uses red text
    let redLogger argName argValue =
        let message = sprintf "%s=%A" argName argValue
        System.Console.ForegroundColor <- System.ConsoleColor.Red
        System.Console.WriteLine("{0}",message)
        System.Console.ResetColor()
    
    //create an adder with the popup logger partially applied
    let addWithRedLogger = adderWithPluggableLogger redLogger
    let result9 = addWithRedLogger 1 2
    let result10 = addWithRedLogger 42 99

/// Wrapping BCL functions for partial application
module PartialApplicationBCL =
    
    // create wrappers for .NET string functions
    let replace oldStr newStr (s:string) = s.Replace(oldValue=oldStr, newValue=newStr)
    
    let startsWith (lookFor:string) (s:string) = s.StartsWith(lookFor)

    // Once the string becomes the last parameter, we can then use them with pipes in the expected way:
    let result1 =
        "hello"
        |> replace "h" "j"
        |> startsWith "j"
    
    let result2 = 
        ["the"; "quick"; "brown"; "fox"] 
        |> List.filter (startsWith "f")
    
    // or with function composition:
    let compositeOp = replace "h" "j" >> startsWith "j"
    let result3 = compositeOp "hello"

/// Understanding the "pipe" function
module PartialApplicationPipe =

    // The pipe function is defined as:
    let (|>) x f = f x

    let doSomething1 x y z = x+y+z
    let result1 = doSomething1 1 2 3 // all parameters after function

    let doSomething2 x y  =
      let intermediateFn z = x+y+z
      intermediateFn        // return intermediateFn
    
    let doSomethingPartial = doSomething2 1 2
    let result2 = doSomethingPartial 3     // only one parameter after function now
    let result3 = 3 |> doSomethingPartial  // same as above - last parameter piped in

    // The pipe operator is extremely common in F#, and used all the time to preserve a natural flow. 
    // Here are some more usages:
    let result4 = "12" |> int               // parses string "12" to an int
    let result5 = 1 |> (+) 2 |> (*) 3       // chain of arithmetic

/// The reverse "pipe" function
module PartialApplicationReversePipe =

    // The "reverse pipe" function is defined as:
    let (<|) f x = f x

    // It seems that this function doesn’t really do anything different from normal, so why does it exist?
    // The reason is that, when used in the infix style as a binary operator, 
    // it reduces the need for parentheses and can make the code cleaner.

    // printf "%i" 1+2          // error
    printf "%i" (1+2)        // using parens
    printf "%i" <| 1+2       // using reverse pipe

    // You can also use piping in both directions at once to get a pseudo infix notation.
    let add x y = x + y
    // let result1 = (1+2) add (3+4)          // error
    let result2 = 1+2 |> add <| 3+4        // pseudo infix
