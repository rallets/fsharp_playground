namespace Playground

open System

/// This construct allows the compiler, which can understand the "shape" of data types,
/// to force you to account for all possible cases when using a data type through what is known as Exhaustive Pattern Matching. 
/// This is incredibly powerful for correctness, and can be cleverly used to "lift" what would normally be a runtime concern into compile-time.
module PatternMatching =

    /// A record for a person's first and last name
    type Person = {
        First : string
        Last  : string
    }

    /// A Discriminated Union of 3 different kinds of employees
    type Employee =
        | Engineer of engineer: Person
        | Manager of manager: Person * reports: List<Employee>
        | Executive of executive: Person * reports: List<Employee> * assistant: Employee

    /// Count everyone underneath the employee in the management hierarchy, including the employee. 
    /// The matches bind names to the properties
    /// of the cases so that those names can be used inside the match branches.
    /// Note that the names used for binding do not need to be the same as the
    /// names given in the DU definition above.
    let rec countReports(emp: Employee) =
        1 + match emp with
            | Engineer(_person) ->
                0
            | Manager(_person, reports) ->
                reports |> List.sumBy countReports
            | Executive(_person, reports, assistant) ->
                (reports |> List.sumBy countReports) + countReports assistant

    /// Find all managers/executives named "Dave" who do not have any reports.
    /// This uses the 'function' shorthand to as a lambda expression.
    let findName6WithOpenPosition(emps: List<Employee>) =
        emps |>
        List.filter(function
                       | Manager({First = "Name6"}, []) -> true // [] matches an empty list.
                       | Executive({First = "Name6"}, [], _assistant) -> true
                       | _ -> false) // '_' is a wildcard pattern that matches anything.
                                     // This handles the "or else" case.
    
    // TODO: how can we write a generic function findPersonWithOpenPosition "Name1" emps ?

    let pers1 =  { First = "Name1"; Last = "Surname1" }
    let pers2 =  { First = "Name2"; Last = "Surname2" }
    let pers3 =  { First = "Name3"; Last = "Surname3" }
    let pers4 =  { First = "Name4"; Last = "Surname4" }
    let pers5 =  { First = "Name5"; Last = "Surname5" }
    let pers6 =  { First = "Name6"; Last = "Surname6" }

    let engineer1 =  Engineer (pers1)
    let engineer2 = Engineer (pers2)
    let manager1 = Manager (pers3, [engineer1 ; engineer2] )
    let assistant1 = Engineer (pers4)
    let executive1 = Executive (pers5, [engineer1 ; engineer2 ; manager1], assistant1)
    let withOpenPosition = Manager (pers6, [] )

    let company = [
        engineer1
        engineer2
        manager1
        executive1
        withOpenPosition
    ]

    let allReports = countReports executive1

    let ex1() = printfn $"Executive1 has {allReports} reports"

    let openPosition = findName6WithOpenPosition company

    let ex2() = printfn $"Company has {openPosition|> List.length} employee with open positions"

    /// You can also use the shorthand function construct for pattern matching, 
    /// which is useful when you're writing functions which make use of Partial Application.
    let private parseHelper (f: string -> bool * 'T) = f >> function
        | (true, item) -> Some item
        | (false, _) -> None
    
    let parseDateTimeOffset = parseHelper DateTimeOffset.TryParse
    
    let ex3() = 
        let result = parseDateTimeOffset "1970-01-01"
        match result with
        | Some dto -> printfn $"It parsed to {dto}!"
        | None -> printfn "It didn't parse!"
    
    // Define some more functions which parse with the helper function.
    let parseInt = parseHelper Int32.TryParse
    let parseDouble = parseHelper Double.TryParse
    let parseTimeSpan = parseHelper TimeSpan.TryParse

    // Active Patterns are another powerful construct to use with pattern matching.
    // They allow you to partition input data into custom forms, decomposing them at the pattern match call site. 
    //
    // To learn more, see: https://docs.microsoft.com/dotnet/fsharp/language-reference/active-patterns
    let (|Int|_|) = parseInt
    let (|Double|_|) = parseDouble
    let (|Date|_|) = parseDateTimeOffset
    let (|TimeSpan|_|) = parseTimeSpan
    
    /// Pattern Matching via 'function' keyword and Active Patterns often looks like this.
    let printParseResult = function
        | Int x -> printfn $"%d{x}"
        | Double x -> printfn $"%f{x}"
        | Date d -> printfn $"%O{d}"
        | TimeSpan t -> printfn $"%O{t}"
        | _ -> printfn "Nothing was parse-able!"
    
    // Call the printer with some different values to parse.
    let ex4() = 
        printParseResult "12"
        printParseResult "12,045"
        printParseResult "12/12/2016"
        printParseResult "9:01PM"
        printParseResult "banana!"