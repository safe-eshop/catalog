using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Channels;
using System.Threading.Tasks;
using Catalog.Core.Model;
using Catalog.Core.Repository;
using Catalog.Core.Services.Import;
using Catalog.Infrastructure.Persistence.Mappers;
using Catalog.Infrastructure.Persistence.Model;
using Catalog.Infrastructure.Persistence.Queries;
using LanguageExt;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using Open.ChannelExtensions;
using static LanguageExt.Prelude;

namespace Catalog.Infrastructure.Persistence.Repositories.Import
{
    internal class MongoProductsImportWriteRepository : IProductsImportWriteRepository
    {
        private IMongoDatabase _database;
        private readonly ILogger<MongoProductsImportWriteRepository> _logger;

        public MongoProductsImportWriteRepository(IMongoDatabase database,
            ILogger<MongoProductsImportWriteRepository> logger)
        {
            _database = database;
            _logger = logger;
        }

        public async Task<Either<Exception, Unit>> Store(ChannelReader<Product> productsSource,
            CancellationToken cancellationToken = default)
        {
            var products = _database.ProductsCollection();
            var result = productsSource
                .Pipe(x => new MongoProduct(x), cancellationToken: cancellationToken)
                .PipeAsync(Environment.ProcessorCount, async p => await products.ReplaceOneAsync(
                        x => p.MongoId == x.MongoId,
                        p, new ReplaceOptions {IsUpsert = true}, cancellationToken),
                    cancellationToken: cancellationToken);

            await foreach (var res in result.ReadAllAsync(cancellationToken))
            {
                _logger.LogDebug("Inserted {@Result}", res);
            }

            return Right<Exception, Unit>(Unit.Default);
        }

        public async Task Store(ChannelReader<Product> products, ChannelWriter<Either<ProductImported, ProductImportFailed>> writer, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}