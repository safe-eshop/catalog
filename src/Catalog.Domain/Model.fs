﻿namespace Catalog.Domain.Model

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
    static member Create (regular: decimal) (promotional: Nullable<decimal>) =
        match (regular, promotional) with
        | reg, prom when prom.HasValue && (prom.Value > reg) -> failwith "Regular price is grater than promotional"
        | reg, prom ->
            { Regular = reg
              Promotional = prom }

type Product =
    { Id: ProductId
      ShopId: ShopId
      Slug: string
      Description: ProductDescription
      Price: Price
      Details: ProductDetails
      Tags: string seq }

module Product =
    let generateSlug (pid: ProductId) (sid: ShopId) = sprintf "%d_%d" pid.Value sid.Value
