namespace Catalog.Domain.Model

open System

type ProductId = { Value: int }

type ShopId = int

type Product = { Id: ProductId; ShopId: ShopId }