namespace Catalog.Domain.Repository

open System
open System.Collections.Generic
open System.Threading.Tasks
open Catalog.Domain.Model

type ICatalogRepository =
    abstract member GetById: id: ProductId * shopId: ShopId  -> Task<Product option>
    abstract member GetByIds: ids: ProductId seq * shopId: ShopId  -> Task<Product option>
    
    
type IProductsImportSource =
    abstract member GetProductsToImport: unit  -> IAsyncEnumerable<Product>
    
type IProductsImportWriteRepository =
    abstract member Store: Product seq  -> Task<Result<unit, Exception>>