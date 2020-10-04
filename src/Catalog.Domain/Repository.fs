namespace Catalog.Domain.Repository

open System
open System.Collections.Generic
open System.Threading.Tasks
open Catalog.Domain.Model
open LanguageExt

type ICatalogRepository =
    abstract member GetById: id: ProductId * shopId: ShopId  -> Task<LanguageExt.Option<Product>>
    abstract member GetByIds: ids: ProductId seq * shopId: ShopId  -> IAsyncEnumerable<Product>
    
type IProductSearcher =
    abstract member Search: ids: ProductId seq * shopId: ShopId  -> IAsyncEnumerable<Product>


type IProductsImportSource =
    abstract member GetProductsToImport: unit  -> IAsyncEnumerable<Product>
    
type IProductsImportWriteRepository =
    abstract member Store: productsSource:IAsyncEnumerable<Product>  -> Task<LanguageExt.Either<Exception, LanguageExt.Unit>>