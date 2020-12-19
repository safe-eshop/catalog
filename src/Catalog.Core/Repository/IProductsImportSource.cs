using System.Collections.Generic;
using Catalog.Core.Model;

namespace Catalog.Core.Repository
{
    public interface IProductsImportSource
    {
        IAsyncEnumerable<Product> GetProductsToImport();
    }
}