namespace Playground

open System

/// Option values are any kind of value tagged with either 'Some' or 'None'.
/// They are used extensively in F# code to represent the cases where many other
/// languages would use null references.
///
/// To learn more, see: https://docs.microsoft.com/dotnet/fsharp/language-reference/options
module OptionValues = 

    /// First, define a zip code defined via Single-case Discriminated Union.
    type ZipCode = ZipCode of string

    /// Next, define a type where the ZipCode is optional.
    type Customer = { ZipCode: ZipCode option }

    /// Next, define an interface type that represents an object to compute the shipping zone for the customer's zip code, 
    /// given implementations for the 'getState' and 'getShippingZone' abstract methods.
    type IShippingCalculator =
        abstract GetState : ZipCode -> string option
        abstract GetShippingZone : string -> int

    type ShippingCalculator() =
       interface IShippingCalculator with
          member this.GetState(code) = Some($"A state {code}")
          member this.GetShippingZone(code) = code.Length

    /// Next, calculate a shipping zone for a customer using a calculator instance.
    /// This uses combinators in the Option module to allow a functional pipeline for
    /// transforming data with Optionals.
    let CustomerShippingZone (calculator: IShippingCalculator, customer: Customer) =
        customer.ZipCode 
        |> Option.bind calculator.GetState          // The Option.bind function executes a function on the value, if there is a value. 
                                                    // The function must take exactly one argument, and its parameter type must be the option type. 
                                                    // The return value of the function is another option type.
        
        |> Option.map calculator.GetShippingZone    // Option.map apply a function to the option value

    let ex1() = 
        let v = CustomerShippingZone(ShippingCalculator(), { ZipCode = Some(ZipCode "aaaa") })
        printfn $"The shipping zone is {v.Value}"
