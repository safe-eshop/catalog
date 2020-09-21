namespace Catalog.Domain.Repository

open System.Threading.Tasks
open Catalog.Domain.Model

type ICatalogRepository =
    abstract member GetById: id: ProductId * shopId: ShopId  -> Task<Product option>