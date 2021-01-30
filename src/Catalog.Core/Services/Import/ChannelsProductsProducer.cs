using System.Threading;
using System.Threading.Channels;
using Catalog.Core.Model;
using Catalog.Core.Repository;
using Catalog.Core.Services.Import.Abstractions;
using Open.ChannelExtensions;

namespace Catalog.Core.Services.Import
{
    public class ChannelsProductsProducer : IProductsProducer
    {
        private readonly IProductsProvider _productsProvider;

        public ChannelsProductsProducer(IProductsProvider productsProvider)
        {
            _productsProvider = productsProvider;
        }

        public ChannelReader<Product> ProduceProducts(CancellationToken cancellationToken = default)
        {
            return _productsProvider.ProduceProducts(cancellationToken).ToChannel(cancellationToken: cancellationToken);
        }
    }
}