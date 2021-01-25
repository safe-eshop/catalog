using System.Threading;
using System.Threading.Channels;
using Catalog.Core.Model;

namespace Catalog.Core.Services.Import.Abstractions
{
    public interface IProductsProducer
    {
        ChannelReader<Product> ProduceProducts(CancellationToken cancellationToken = default);
    }
}