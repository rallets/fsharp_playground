namespace webapi_fsharp

module DataContext =
    open System
    open webapi_fsharp.Models

    type ItemsRepository() =

        let mutable items : Item list = []

        member this.getAll() = items

        member this.get(id: Guid) = List.tryFind (fun i -> i.Id = id) items

        member this.add(item: Item) = items <- item :: items

        member this.addRange(range: Item list) = items <- List.append items range

    let Initialize (context: ItemsRepository) =
        // context.Database.EnsureDeleted() |> ignore //Deletes the database
        // context.Database.EnsureCreated() |> ignore //check if the database is created, if not then creates it

        // default Items for testing
        let items : Item list =
            [ { Id = Guid.NewGuid()
                Name = "Name 01"
                Description = "Desc 01"
                Tags = [ { Id = Guid.NewGuid(); Name = "Tag 01" } ] }
              { Id = Guid.NewGuid()
                Name = "Name 02"
                Description = "Desc 02"
                Tags = [] }
              { Id = Guid.NewGuid()
                Name = "Name 03"
                Description = "Desc 03"
                Tags = [] } ]

        if List.isEmpty (context.getAll ()) then
            context.addRange items
