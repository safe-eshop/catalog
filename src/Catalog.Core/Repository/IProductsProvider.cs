using System.Collections.Generic;
using System.Threading;
using Catalog.Core.Model;

namespace Catalog.Core.Repository
{
    public interface IProductsProvider
    {
        IAsyncEnumerable<Product> ProduceProducts(CancellationToken cancellationToken = default);
    }
}