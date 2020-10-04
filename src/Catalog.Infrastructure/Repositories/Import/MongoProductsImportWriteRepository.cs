using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Catalog.Domain.Helpers;
using Catalog.Domain.Model;
using Catalog.Domain.Repository;
using Catalog.Infrastructure.Mappers;
using Catalog.Persistence.Model;
using Catalog.Persistence.Queries;
using LanguageExt;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using Open.ChannelExtensions;
using static LanguageExt.Prelude;

namespace Catalog.Infrastructure.Repositories.Import
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
                .Pipe(x => x.ToMongoProduct(DateTime.UtcNow.AddDays(1)))
                .PipeAsync(Environment.ProcessorCount,
                    async p => await products.ReplaceOneAsync(x => x.ProductId == p.ProductId && x.ShopId == p.ShopId,
                        p, new ReplaceOptions() { IsUpsert = true }))
                .AsAsyncEnumerable();

            await foreach (var res in result)
            {
                _logger.LogInformation("Inserted {Result}", res);
            }

            return Right<Exception, Unit>(Unit.Default);
        }
    }
}