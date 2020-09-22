namespace Catalog.Domain.Model

open System

type ProductId = Guid

type ShopId = int

type Product = { Id: ProductId; ShopId: ShopId }