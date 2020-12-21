using System.Collections.Generic;
using System.Threading;
using System.Threading.Channels;
using Catalog.Core.Model;

namespace Catalog.Core.Repository
{
    public interface IProductsImportSource
    {
        ChannelReader<Product> ProduceProductsToImport(CancellationToken cancellationToken = default);
    }
}