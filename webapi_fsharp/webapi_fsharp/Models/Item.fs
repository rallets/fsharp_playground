namespace webapi_fsharp.Models

open System
open System.ComponentModel.DataAnnotations
open webapi_fsharp.Helpers

type Tag = { Id: Guid; Name: string }

type IItemBase =
    abstract Id : Guid
    abstract Name : string

type ItemResponse =
    { Id: Guid
      Name: string
      Description: string
      Tags: Tag array }
    interface IItemBase with
        member x.Id = x.Id
        member x.Name = x.Name

type ItemHeaderResponse =
    { Id: Guid
      Name: string
      NumTags: int }
    interface IItemBase with
        member x.Id = x.Id
        member x.Name = x.Name

type Item =
    { Id: Guid
      Name: string
      Description: string
      Tags: Tag array }

    member this.ToHeaderApiModel() =
        { Id = this.Id
          Name = this.Name
          NumTags = this.Tags |> Array.length }

    member this.ToApiModel() =
        { Id = this.Id
          Name = this.Name
          Description = this.Description
          Tags = this.Tags }

[<CLIMutable>]
type ValidTextRequest =
    { [<Required>]
      Text: string }

[<CLIMutable>]
type ItemTagEditRequest =
    { Id: Nullable<Guid>
      [<Required>]
      Name: string }

    member this.ToModel() =
        let result : Tag =
            { Id =
                  match this.Id with
                  | IsNull -> Guid.NewGuid()
                  | HasValue v -> v
              Name = this.Name }

        result

[<CLIMutable>]
type ItemEditRequest =
    { Name: string
      Description: string
      Tags: ItemTagEditRequest array }

    member this.ToModel(existingRecordId: Nullable<Guid>) =
        let id =
            match existingRecordId with
            | IsNull _ -> Guid.NewGuid()
            | HasValue v -> v

        let tags =
            if isNull this.Tags then
                [||]
            else
                this.Tags

        let result : Item =
            { Id = id
              Name = this.Name
              Description = this.Description
              Tags = Array.map (fun (t: ItemTagEditRequest) -> t.ToModel()) tags }

        result

type EItemsSearchType =
    | Metadata = 1
    | Tags = 2

[<CLIMutable>]
type ItemsSearchRequest =
    { [<Required>]
      Text: string
      [<Required>]
      Type: EItemsSearchType }
