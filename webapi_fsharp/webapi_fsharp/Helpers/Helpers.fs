namespace webapi_fsharp

open System

module Helpers =
    let (|IsNull|HasValue|) (x: _ Nullable) =
        if x.HasValue then HasValue x.Value else IsNull

    let containsInsensitive (text: string) (search: string) = text.Contains(search, StringComparison.InvariantCultureIgnoreCase)