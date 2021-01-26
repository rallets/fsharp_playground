// Learn more about F# at http://fsharp.org

open System
open Playground

// Define a new function to print a name.
// It is defined above the main function.
let printGreeting name = printfn "Hello %s from F#!" name

[<EntryPoint>]
let main argv =
    printfn "Hello world from F#!"
    printGreeting "User1"

    Playground.BasicFunctions.ex1()
    Playground.BasicFunctions.ex2()
    Playground.BasicFunctions.ex3()

    Playground.Immutability.ex1()
    Playground.Immutability.ex2()

    Playground.Primitives.Booleans.ex1()
    Playground.Primitives.IntegersAndNumbers.ex1()
    Playground.Primitives.StringManipulation.ex1()
    Playground.Primitives.StringManipulation.ex2()
    Playground.Primitives.StringManipulation.ex3()

    Playground.Tuples.ex1()
    Playground.Tuples.ex2()
    Playground.Tuples.ex3()

    Playground.PipelinesAndComposition.ex1()
    Playground.PipelinesAndComposition.ex2()
    Playground.PipelinesAndComposition.ex3()
    Playground.PipelinesAndComposition.ex4()

    Playground.Lists.Lists.ex1()
    Playground.Lists.Lists.ex2()
    Playground.Lists.Lists.ex3()
    Playground.Lists.Lists.chessboard()

    Playground.Lists.Arrays.ex1()

    Playground.Lists.Sequences.ex1()
    Playground.Lists.Sequences.ex2()

    Playground.RecursiveFunctions.ex1()
    Playground.RecursiveFunctions.ex2()
    Playground.RecursiveFunctions.ex3()

    Playground.RecordTypes.ex1()
    Playground.RecordTypes.ex2()

    Playground.DiscriminatedUnions.printAllCards()
    Playground.DiscriminatedUnions.ex2()
    Playground.DiscriminatedUnions.ex3()

    Playground.PatternMatching.ex1()
    Playground.PatternMatching.ex2()
    Playground.PatternMatching.ex21()
    Playground.PatternMatching.ex3()
    Playground.PatternMatching.ex4()

    Playground.OptionValues.ex1()

    Playground.UnitsOfMeasure.ex1()

    Playground.DefiningClasses.ex1()
    Playground.DefiningGenericClasses.ex1()
    Playground.ImplementingInterfaces.ex1()
    Playground.ImplementingInterfaces.ex2()

    0 // return an integer exit code
