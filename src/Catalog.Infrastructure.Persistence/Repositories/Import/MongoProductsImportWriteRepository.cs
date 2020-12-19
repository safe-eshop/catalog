using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Catalog.Core.Model;
using Catalog.Core.Repository;
using Catalog.Infrastructure.Persistence.Mappers;
using Catalog.Infrastructure.Persistence.Queries;
using LanguageExt;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using Open.ChannelExtensions;
using static LanguageExt.Prelude;

namespace Catalog.Infrastructure.Persistence.Repositories.Import
{
    public class MongoProductsImportWriteRepository : IProductsImportWriteRepository
    {
        private IMongoDatabase _database;
        private readonly ILogger<MongoProductsImportWriteRepository> _logger;

        public MongoProductsImportWriteRepository(IMongoDatabase database,
            ILogger<MongoProductsImportWriteRepository> logger)
        {
            _database = database;
            _logger = logger;
        }

        public async Task<Either<Exception, Unit>> Store(IAsyncEnumerable<Product> productsSource)
        {
            var products = _database.ProductsCollection();
            var result = productsSource.ToChannel()
                .Pipe(x => x.ToMongoProduct(DateTime.UtcNow))
                .PipeAsync(Environment.ProcessorCount,
                    async p => await products.ReplaceOneAsync(x => p.MongoId == x.MongoId,
                        p, new ReplaceOptions() { IsUpsert = true }))
                .AsAsyncEnumerable();

            await foreach (var res in result)
            {
                _logger.LogDebug("Inserted {Result}", res);
            }

            return Right<Exception, Unit>(Unit.Default);
        }
    }
}