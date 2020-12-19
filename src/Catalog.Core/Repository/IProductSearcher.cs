using System.Collections.Generic;
using Catalog.Core.Model;

namespace Catalog.Core.Repository
{
    public interface IProductSearcher
    {
        IAsyncEnumerable<Product> Search(IEnumerable<ProductId> ids, ShopId shopId);
    }
}