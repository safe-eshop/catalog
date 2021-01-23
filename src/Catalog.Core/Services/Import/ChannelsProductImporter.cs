using System.Collections.Generic;
using System.Threading;
using System.Threading.Channels;
using Catalog.Core.Model;
using Catalog.Core.Repository;
using LanguageExt;
using System.Linq;
using System.Threading.Tasks;
using Open.ChannelExtensions;

namespace Catalog.Core.Services.Import
{
    public class ChannelsProductWriter : IProductWriter
    {
        private IEnumerable<IProductsImportWriteRepository> _importWriteRepositories;

        public ChannelsProductWriter(IEnumerable<IProductsImportWriteRepository> importWriteRepositories)
        {
            _importWriteRepositories = importWriteRepositories;
        }

        public ChannelReader<Either<ProductImported, ProductImportFailed>> Write(
            ChannelReader<Product> productReader, CancellationToken cancellationToken = default)
        {
            var channel = Channel.CreateUnbounded<Either<ProductImported, ProductImportFailed>>();
            
            var consumers =
                _importWriteRepositories.Select(async repo =>
                    await repo.Store(productReader, channel.Writer, cancellationToken));

            Task.Run(async () =>
            {
                await Task.WhenAll(consumers);
                await channel.CompleteAsync();
            }, cancellationToken);

            return channel;
        }
    }
}