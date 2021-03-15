namespace Playground

open System.Net
open System.Threading.Tasks

/// To learn more, see: https://docs.microsoft.com/en-us/dotnet/fsharp/tutorials/asynchronous-and-concurrent-programming/async
module Async =

    open System
    open System.IO
    
    let printTotalFileBytes path =
        async {
            let! bytes = File.ReadAllBytesAsync(path) |> Async.AwaitTask
            let fileName = Path.GetFileName(path)
            printfn $"File {fileName} has %d{bytes.Length} bytes"
        }
    
    // let result =
    //     printTotalFileBytes "readme.txt"
    //         |> Async.RunSynchronously
    
    let resultParallel = 
        ["readme.txt";"readme.txt"]
        |> Seq.map printTotalFileBytes
        |> Async.Parallel
        |> Async.Ignore
        
    let resultSequential = 
        ["readme.txt";"readme.txt"]
        |> Seq.map printTotalFileBytes
        |> Async.Sequential
        |> Async.Ignore
        
    // How to work with .NET async and Task<T>
    // Working with .NET async libraries and codebases that use Task<TResult> (that is, async computations that have return values) is straightforward and has built-in support with F#.
    
    // You can use the Async.AwaitTask function to await a .NET asynchronous computation:
    
    let downloadHtmlFromUrlAsync url =
        async { 
            let uri = new System.Uri(url)
            let webClient = new WebClient()
            // C# => var html = await webClient.DownloadStringTaskAsync(uri)
            let! html = webClient.DownloadStringTaskAsync(uri) |> Async.AwaitTask
            return html
            }

    // You can use the Async.StartAsTask function to pass an asynchronous computation to a .NET caller:
    (*
    let computationForCaller param =
        async {
            let! result = getAsyncResult param
            return result
        } |> Async.StartAsTask
    *)

    // How to work with .NET async and Task
    // To work with APIs that use Task (that is, .NET async computations that do not return a value), 
    // you may need to add an additional function that will convert an Async<'T> to a Task:
    
    // Async<unit> -> Task
    let startTaskFromAsyncUnit (comp: Async<unit>) =
        // :> => Operator that converts a type to type that is higher in the hierarchy.
        Async.StartAsTask comp :> Task
    
    // There is already an Async.AwaitTask that accepts a Task as input. 
    // With this and the previously defined startTaskFromAsyncUnit function, 
    // you can start and await Task types from an F# async computation.
