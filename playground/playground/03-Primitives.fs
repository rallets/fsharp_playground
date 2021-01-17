namespace Playground.Primitives

module IntegersAndNumbers = 

    /// This is a sample integer.
    let sampleInteger = 176

    /// This is a sample floating point number.
    let sampleDouble = 4.1

    /// This computed a new number by some arithmetic.  Numeric types are converted using
    /// functions 'int', 'double' and so on.
    let sampleInteger2 = (sampleInteger/4 + 5 - 7) * 4 + int sampleDouble

    /// This is a list of the numbers from 0 to 99.
    let sampleNumbers = [ 0 .. 299 ]

    /// This is a list of all tuples containing all the numbers from 0 to 99 and their squares.
    let sampleTableOfSquares = [ for i in sampleNumbers -> (i, i*i) ]

    // The next line prints a list that includes tuples, using an interpolated string.
    let ex1() = 
        // dumps only the first 5 elements
        printfn $"The table of squares from 0 to 99 is:\n{sampleTableOfSquares}"
        // dumps only the first 100 elements
        printfn $"The table of squares from 0 to 99 is:\n%A{sampleTableOfSquares}"
        // dumps all the elements
        sampleTableOfSquares |> List.map (sprintf "%A") |> String.concat ", " |> printfn "[%s]"

module Booleans =

    /// Booleans values are 'true' and 'false'.
    let boolean1 = true
    let boolean2 = false

    /// Operators on booleans are 'not', '&&' and '||'.
    let boolean3 = not boolean1 && (boolean2 || false)

    // This line uses '%b' to print a boolean value.  This is type-safe.
    let ex1() = printfn $"The expression 'not boolean1 && (boolean2 || false)' is %b{boolean3}"

module StringManipulation = 

    /// Strings use double quotes.
    let string1 = "Hello"
    let string2  = "world"

    /// Strings can also use @ to create a verbatim string literal.
    /// This will ignore escape characters such as '\', '\n', '\t', etc.
    let string3 = @"C:\Program Files\"

    /// String literals can also use triple-quotes.
    let string4 = """The computer said "hello world" when I told it to!"""

    /// String concatenation is normally done with the '+' operator.
    let helloWorld = string1 + " " + string2 

    // This line uses '%s' to print a string value. This is type-safe.
    let ex1() = printfn "%s" helloWorld

    /// Substrings use the indexer notation.  This line extracts the first 7 characters as a substring.
    /// Note that like many languages, Strings are zero-indexed in F#.
    let substring = helloWorld.[0..6]
    let ex2() = printfn $"{substring}"

    let ex3() = 
        printf $"Outputs a formatted string, no newline %A{1}"
        printfn $"Outputs a formatted string, adds a newline %A{1}"
        sprintf $"Returns a formatted string, no newline %A{1}" |> ignore
        printf "Done"
