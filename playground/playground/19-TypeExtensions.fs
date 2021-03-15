namespace Playground

/// Any F# type, not just classes, can have functions attached to them. 
/// In F#, this is done using a feature called “type extensions”. 
///
/// To learn more, see: https://fsharpforfunandprofit.com/posts/type-extensions/
module TypeExtensions =

    // These examples demonstrate what are called “intrinsic extensions”. 
    // They are compiled into the type itself and are always available whenever the type is used. 
    // They also show up when you use reflection.
    // With intrinsic extensions, it is even possible to have a type definition that divided across several files, 
    // as long as all the components use the same namespace and are all compiled into the same assembly. 
    // Just as with partial classes in C#, this can be useful to separate generated code from authored code.

    // NB.  member => method | A function defined with "member" is a "method"
    // If you are coming from an object-oriented background, you might be tempted to use methods everywhere, because that is what you are familiar with. 
    // But be aware that there some major downsides to using methods as well:
    // 1) Methods don’t play well with type inference
    // 2) Methods don’t play well with higher order functions

    (* So, a plea for those of you new to functionally programming. 
       Don’t use methods at all if you can, especially when you are learning. 
       They are a crutch that will stop you getting the full benefit from functional programming. *)

    module Person =
    
        type T = {First:string; Last:string} with
            // member defined with type declaration
            member this.FullName =
                this.First + " " + this.Last

            // static constructor
            static member Create first last =
                {First=first; Last=last}
    
        // constructor, standalone function
        let create first last =
            {First=first; Last=last}
    
        // another member added later
        type T with
            member this.SortableName =
                this.Last + ", " + this.First

    // test
    let person1 = Person.create "John" "Doe"
    let person2 = Person.T.Create "John" "Doe"
    let fullname = person1.FullName
    let sortableName = person1.SortableName

    // Another alternative is that you can add an extra member from a completely different module. 
    // These are called “optional extensions”. 
    // They are not compiled into the type itself, and require some other module to be in scope for them to work (this behavior is just like C# extension methods).
    // Then, in a different module
    module PersonExtensions =
    
        type Person.T with
            member this.UppercaseName =
                this.FullName.ToUpper()

    // So now let’s test this extension:
    module Test =
        open Person
        open PersonExtensions

        // test
        let person = Person.create "John" "Doe"
        let fullname = person.FullName
        let sortableName = person.SortableName
        let uppercaseName = person.UppercaseName

    module BclExtensions =
        // note: Int32 not int
        type System.Int32 with
            member this.IsEven = this % 2 = 0

        // You can make the member functions static
        type System.Int32 with
            static member IsOdd x = x % 2 = 1

        //test
        let i = 20
        if i.IsEven then printfn "'%i' is even" i

    /// Attaching existing functions
    /// A very common pattern is to attach pre-existing standalone functions to a type.
    module Person2 =
        // type with no members initially
        type T = {First:string; Last:string}
    
        // constructor
        let create first last =
            {First=first; Last=last}
    
        // standalone function
        let fullName {First=first; Last=last} =
            first + " " + last
    
        // attach preexisting function as a member
        type T with
            member this.FullName = fullName this

        // standalone function with multiple parameters, the this parameter is first.
        let hasSameFirstAndLastName (person:T) otherFirst otherLast =
            person.First = otherFirst && person.Last = otherLast

        // attach preexisting function as a member
        type T with
            member this.HasSameFirstAndLastName = hasSameFirstAndLastName this
    
    module Person2Test =
        // test
        let person = Person2.create "John" "Doe"
        let fullname = Person2.fullName person  // functional style
        let fullname2 = person.FullName        // OO style

        let result1 = Person2.hasSameFirstAndLastName person "bob" "smith" // functional style
        let result2 = person.HasSameFirstAndLastName "bob" "smith" // OO style

        // One example of this in the F# libraries is the function that calculates a list’s length. 
        // It is available as a standalone function in the List module, but also as a method on a list instance.
        let list = [1..10]
        
        // functional style
        let len1 = List.length list
        
        // OO style
        let len2 = list.Length

module TupleFormMethods =
    
    type Product = {SKU:string; Price: float} with
    
            // curried style
            member this.CurriedTotal qty discount =
                (this.Price * float qty) - discount
    
            // tuple style
            member this.TupleTotal(qty,discount) =
                (this.Price * float qty) - discount

            // For tuple-style methods, you can specify an optional parameter by prefixing the parameter name with a question mark.
            // optional discount
            member this.TupleTotal2(qty,?discount) =
                let extPrice = this.Price * float qty
                match discount with
                | None -> extPrice                      // If the parameter is not set, it comes through as None
                | Some discount -> extPrice - discount  // If the parameter is set, it comes through as Some value

            // F# does support method overloading, but only for methods (that is functions attached to types)
            // and of these, only those using tuple-style parameter passing.
            // no discount
            member this.TupleTotal3(qty) =
                printfn "using non-discount method"
                this.Price * float qty

            // with discount
            member this.TupleTotal3(qty, discount) =
                printfn "using discount method"
                (this.Price * float qty) - discount

    module CurriedExample =
        let product = {SKU="ABC"; Price=2.0}
        let total1 = product.CurriedTotal 10 1.0
        let total2 = product.TupleTotal(10,1.0)

        let totalFor10 = product.CurriedTotal 10
        let discounts = [1.0..5.0]
        let totalForDifferentDiscounts
            = discounts |> List.map totalFor10

    module TupleStyle =
        let product = {SKU="ABC"; Price=2.0}
        let total3 = product.TupleTotal(qty=10,discount=1.0)
        let total4 = product.TupleTotal(discount=1.0, qty=10)

        // discount not specified
        let total21 = product.TupleTotal2(10)
        // discount specified
        let total22 = product.TupleTotal2(10,1.0)

        // discount not specified
        let total31 = product.TupleTotal3(10)
        // discount specified
        let total32 = product.TupleTotal3(10,1.0)

module MethodsLimitations =
    open TypeExtensions.Person2
      
    // 1) Methods don’t play well with type inference
    
    // using standalone function
    let printFullName person =
        printfn "Name is %s" (fullName person)
    
    // type inference worked:
    //    val printFullName : Person.T -> unit

    // using method with "dotting into"
    // let printFullName2 person =
    //    printfn "Name is %s" (person.FullName)
    // Error: Lookup on object of indeterminate type based on information prior to this program point. A type annotation may be needed prior to this program point to constrain the type of the object. This may allow the lookup to be resolved.
    
    // 2) Methods don’t play well with higher order functions
    
    // For example, let’s say that, given a list of people, we want to get all their full names.
    // With a standalone function, this is trivial:
    let list1 = [
        create "Andy" "Anderson";
        create "John" "Johnson";
        create "Jack" "Jackson"]
    
    // get all the full names at once
    let result1 = list1 |> List.map fullName

    // With object methods, we have to create special lambdas everywhere:

    let list2 = [
        create "Andy" "Anderson";
        create "John" "Johnson";
        create "Jack" "Jackson"]
    
    //get all the full names at once
    let result2 = list2 |> List.map (fun p -> p.FullName)