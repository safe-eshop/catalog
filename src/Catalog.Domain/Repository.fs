namespace Catalog.Domain.Repository

open System.Collections.Generic
open System.Threading.Tasks
open Catalog.Domain.Model

type ICatalogRepository =
    abstract member GetById: id: ProductId * shopId: ShopId  -> Task<Product option>
    
    
type IProductsSource =
    abstract member GetProductsToImport: unit  -> IAsyncEnumerable<Product>