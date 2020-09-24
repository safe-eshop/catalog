namespace Catalog.Domain.Model

open System

[<Struct>]
type ProductId = { Value: int }

[<Struct>]
type ShopId =
    { Value: int }
    static member Create(value: Nullable<int>) =
        if value.HasValue then { Value = value.Value } else { Value = 1 }

type Product = { Id: ProductId; ShopId: ShopId }
