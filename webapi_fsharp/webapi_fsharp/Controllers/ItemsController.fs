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
            List.map (fun (x: Item) -> x.ToHeaderApiModel()) items

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
