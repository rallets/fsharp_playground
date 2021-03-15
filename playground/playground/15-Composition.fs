namespace Playground

/// Say that you have a function “f” that maps from type “T1” to type “T2”, 
/// and say that you also have a function “g” that maps from type “T2” to type “T3”. 
/// Then you can connect the output of “f” to the input of “g”, creating a new function that maps from type “T1” to type “T3”.
/// To learn more, see: https://fsharpforfunandprofit.com/posts/function-composition/
module FunctionComposition =

    let f (x:int) = float x * 3.0  // f is int->float
    let g (x:float) = x > 4.0      // g is float->bool
    
    // We can create a new function h that takes the output of “f” and uses it as the input for “g”.
    let h1 (x:int) =
        let y = f(x)
        g(y)                   // return output of g
    
    // A much more compact way is this:
    let h2 (x:int) = g ( f(x) ) // h is int->bool
    
    //test
    let result1 = h2 1
    let result2 = h2 2

    // So far, so straightforward. What is interesting is that we can define a new function called “compose” that, 
    // given functions “f” and “g”, combines them in this way without even knowing their signatures.
    let compose f g x = g ( f(x) )

    // As we have seen, the actual definition of compose uses the “>>” symbol.
    let (>>) f g x = g ( f(x) )

    // Given this definition, we can now use composition to build new functions from existing ones.
    let add1 x = x + 1
    let times2 x = x * 2
    let add1Times2 x = (>>) add1 times2 x
    
    //test
    let result3 = add1Times2 3

module FunctionCompositionPractical =
    let add1 x = x + 1
    let times2 x = x * 2

    //The explicit style is quite cluttered. We can do a few things to make it easier to use and understand.
    //First, we can leave off the x parameter so that the composition operator returns a partial application.
    let add1Times2_A = (>>) add1 times2
    
    // And now we have a binary operation, so we can put the operator in the middle.
    let add1Times2_B = add1 >> times2
    
    // And there you have it. Using the composition operator allows code to be cleaner and more straightforward.
    
    //old style
    let add1Times2_C x = times2(add1 x)
    
    //new style
    let add1Times2_D = add1 >> times2

    // Composition can also be done backwards using the “<<” operator, if needed.
    let add n x = x + n
    let times n x = x * n

    let times2Add1 = add 1 << times 2
    let result1 = times2Add1 3

    // Reverse composition is mainly used to make code more English-like. For example, here is a simple example:
    let myList = []
    let result2 = myList |> List.isEmpty |> not    // straight pipeline
    
    let result3 = myList |> (not << List.isEmpty)  // using reverse composition

    // As long as the inputs and outputs match, the functions involved can use any kind of value. 
    // For example, consider the following, which performs a function twice:
    let twice f = f >> f    //signature is ('a -> 'a) -> ('a -> 'a)


// Composition vs. pipeline
// At this point, you might be wondering what the difference is between the composition operator and the pipeline operator, 
// as they can seem quite similar.
// Basically:
// a) when piping, it is the arguments of the functions that are the data, and the functions are called one after another along the pipe. 
// b) When composing on the other hand, it is the functions themselves that are the data, and the functions are glued together to form a composite function, which can then be called.
