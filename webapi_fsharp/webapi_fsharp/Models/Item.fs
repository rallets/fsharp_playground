namespace webapi_fsharp.Models

open System

type Tag = { Id: Guid; Name: string }

type IItemBase =
    abstract Id : Guid
    abstract Name : string

type ItemResponse =
    { Id: Guid
      Name: string
      Description: string
      Tags: Tag list }
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
      Tags: Tag list }

    member this.ToHeaderApiModel() =
        { Id = this.Id
          Name = this.Name
          NumTags = this.Tags |> List.length }

    member this.ToApiModel() =
        { Id = this.Id
          Name = this.Name
          Description = this.Description
          Tags = this.Tags }
