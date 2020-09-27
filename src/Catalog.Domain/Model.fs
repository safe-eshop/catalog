namespace Catalog.Domain.Model

open System

[<Struct>]
type ProductId = { Value: int }

[<Struct>]
type ShopId =
    { Value: int }
    static member Create(value: Nullable<int>) =
        if value.HasValue then { Value = value.Value } else { Value = 1 }

type ProductDescription =
    { Name: string
      Brand: string
      Description: string }

type ProductDetails =
    { Weight: double
      WeightUnits: string
      Picture: string
      Color: string }

type Price =
    { Regular: decimal
      Promotional: Nullable<decimal> }

type Product =
    { Id: ProductId
      ShopId: ShopId
      Slug: string
      Description: ProductDescription
      Price: Price
      Details: ProductDetails
      Tags: string seq }
    
module Product =
    let generateSlug(pid: ProductId)(sid: ShopId) =
        sprintf "%d_%d" pid.Value sid.Value
