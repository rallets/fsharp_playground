namespace Playground

module RecursiveFunctions = 
    
    /// This example shows a recursive function that computes the factorial of an integer. 
    // It uses 'let rec' to define a recursive function.
    let rec factorial n = 
        if n = 0 then 
            1 
        else 
            n * factorial (n-1)

    let ex1() = printfn $"Factorial of 6 is: %d{factorial 6}"

    /// Computes the greatest common factor of two integers.
    ///
    /// Since all of the recursive calls are tail calls,
    /// the compiler will turn the function into a loop,
    /// which improves performance and reduces memory consumption.
    let rec greatestCommonFactor a b =
        if a = 0 then 
            b
        elif a < b then 
            greatestCommonFactor a (b - a)
        else 
            greatestCommonFactor (a - b) b

    let ex2() = printfn $"The Greatest Common Factor of 300 and 620 is %d{greatestCommonFactor 300 620}"

    /// This example computes the sum of a list of integers using recursion.
    ///
    /// nb. y::ys means "take the first element and put it in y, the remaining in ys"
    let rec sumList xs =
        match xs with
        | []    -> 0
        | y::ys -> y + sumList ys

    /// This makes 'sumList' tail recursive, using a helper function with a result accumulator.
    let rec private sumListTailRecHelper accumulator xs =
        match xs with
        | []    -> accumulator
        | y::ys -> sumListTailRecHelper (accumulator+y) ys
    
    /// This invokes the tail recursive helper function, providing '0' as a seed accumulator.
    /// An approach like this is common in F#.
    let sumListTailRecursive xs = sumListTailRecHelper 0 xs

    let oneThroughTen = [10; 2; 3; 4; 5; 6; 7; 8; 9; 1] |> List.sort

    let ex3() = printfn $"The sum %d{oneThroughTen.Head}-%d{List.last oneThroughTen} is %d{sumListTailRecursive oneThroughTen}"
