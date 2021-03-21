namespace webapi_fsharp

module DataContext =
    open System
    open webapi_fsharp.Models

    type ItemsRepository() =

        let mutable items : Item array = [||]

        member this.getAll() = items

        member this.get(id: Guid) : Option<Item> =
            Array.tryFind (fun i -> i.Id = id) items

        member this.add(item: Item) = items <- Array.append items [| item |]

        member this.add(request: ItemEditRequest) =
            let item = request.ToModel(Nullable<Guid>())
            this.add item
            let result = item.ToApiModel()
            result

        member this.addRange(range: Item array) = items <- Array.append items range

        member this.edit (item: Item) (request: ItemEditRequest) =
            let idx =
                Array.findIndex (fun (i: Item) -> i.Id = item.Id) items

            let updated = request.ToModel item.Id
            items.[idx] <- updated
            let result = updated.ToApiModel()
            result

        member this.remove(id: Guid) =
            try
                items <- Array.filter (fun (i: Item) -> i.Id <> id) items
                true
            with _ -> false

        member this.searchInMetadata(text: string) =
            let result =
                Array.filter
                    (fun (i: Item) ->
                        Helpers.containsInsensitive i.Name text
                        || Helpers.containsInsensitive i.Description text)
                    items

            result

        member this.searchInTags(text: string) =
            let result =
                Array.filter
                    (fun (i: Item) -> Array.exists (fun (t: Tag) -> Helpers.containsInsensitive t.Name text) i.Tags)
                    items

            result

    let Initialize (context: ItemsRepository) =
        // context.Database.EnsureDeleted() |> ignore //Deletes the database
        // context.Database.EnsureCreated() |> ignore //check if the database is created, if not then creates it

        // default Items for testing
        let items : Item array =
            [| { Id = Guid.NewGuid()
                 Name = "Name 01"
                 Description = "Desc 01"
                 Tags = [| { Id = Guid.NewGuid(); Name = "Tag 01" } |] }
               { Id = Guid.NewGuid()
                 Name = "Name 02"
                 Description = "Desc 02"
                 Tags = [||] }
               { Id = Guid.NewGuid()
                 Name = "Name 03"
                 Description = "Desc 03"
                 Tags = [||] } |]

        if Array.isEmpty (context.getAll ()) then
            context.addRange items
