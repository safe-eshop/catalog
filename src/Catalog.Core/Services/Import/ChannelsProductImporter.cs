using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Channels;
using Catalog.Core.Model;
using Catalog.Core.Repository;
using LanguageExt;
using System.Linq;
using System.Threading.Tasks;
using Catalog.Core.Services.Import.Abstractions;
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

        public ChannelReader<Either<ProductImportFailed, ProductImported>> Write(
            ChannelReader<Product> productReader, CancellationToken cancellationToken = default)
        {
            var channel = Channel.CreateUnbounded<Either<ProductImportFailed, ProductImported>>();

            var consumers = Enumerable.Range(0, Environment.ProcessorCount)
                .Select(_ => Consumer(productReader, channel.Writer));

            Task.Run(async () =>
            {
                await Task.WhenAll(consumers);
                await channel.CompleteAsync();
            }, cancellationToken);

            return channel;
        }

        private async Task Consumer(ChannelReader<Product> productReader, ChannelWriter<Either<ProductImportFailed, ProductImported>> writer)
        {
            await foreach (var msg in productReader.ReadAllAsync())
            {
                var result = await Consume(msg);
                await writer.WriteAsync(result);
            }
        }
        
        private async Task<Either<ProductImportFailed, ProductImported>> Consume(Product product)
        {
            var consumers =
                _importWriteRepositories.Select(async repo =>
                    await repo.Store(product));

            var results = await Task.WhenAll(consumers);

            return results.All(x => x.IsLeft) ? results.First() : results.First(x => x.IsRight);
        }
    }
}