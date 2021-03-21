namespace webapi_fsharp.Controllers

open System
open System.Collections.Generic
open System.Linq
open System.Threading.Tasks
open Microsoft.AspNetCore.Mvc
open Microsoft.Extensions.Logging
open webapi_fsharp.Models
open webapi_fsharp.DataContext

[<ApiController>]
[<Route("api/[controller]")>]
type ItemsController(logger: ILogger<ItemsController>, context: ItemsRepository) =
    inherit ControllerBase()

    [<Route("")>]
    [<HttpGet>]
    member __.Get() =
        let items = context.getAll ()

        let result =
            Array.map (fun (x: Item) -> x.ToHeaderApiModel()) items

        result

    [<Route("{id}")>]
    [<HttpGet>]
    member __.Get(id: Guid) =
        let result = context.get (id)

        let response =
            match result with
            | Some item -> base.Ok(item.ToApiModel()) :> IActionResult
            | None -> base.NotFound() :> IActionResult

        response

    [<Route("{id}")>]
    [<HttpPut>]
    member __.EditItem(id: Guid, [<FromBody>] request: ItemEditRequest) =
        let item = context.get (id)

        let response =
            match item with
            | None -> base.NotFound() :> IActionResult
            | Some item ->
                let result = context.edit item request
                base.Ok(result.ToApiModel()) :> IActionResult

        response

    [<HttpPost>]
    member __.CreateItem([<FromBody>] request: ItemEditRequest) =
        let result = context.add request
        let response = result.ToApiModel()
        response

    [<Route("{id}")>]
    [<HttpDelete>]
    member __.DeleteItem(id: Guid) =
        let item = context.get (id)

        let response =
            match item with
            | None -> base.NotFound() :> IActionResult
            | Some item ->
                let result = context.remove id
                base.Ok(result) :> IActionResult

        response

    /// <summary>
    /// Just a dummy validate endpoint to implement an async form validator
    /// </summary>
    [<Route("valid-text")>]
    [<HttpPost>]
    member __.GetIsValidText([<FromBody>] request: ValidTextRequest) =
        // < & > are invalid char, as they can be used to build an HTML tag
        let containInvalidChars =
            request.Text.IndexOfAny([| '<'; '>' |]) <> -1

        not containInvalidChars

    [<Route("search")>]
    [<HttpPost>]
    member __.Search([<FromBody>] request: ItemsSearchRequest) =
        let result =
            match request.Type with
            | EItemsSearchType.Metadata -> context.searchInMetadata (request.Text)
            | EItemsSearchType.Tags -> context.searchInTags (request.Text)
            | _ -> [||]

        let response =
            Array.map (fun (x: Item) -> x.ToHeaderApiModel()) result

        response
