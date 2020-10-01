using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Catalog.Domain.Helpers;
using Catalog.Domain.Model;
using Catalog.Domain.Repository;
using Catalog.Infrastructure.Mappers;
using Catalog.Persistence.Model;
using Catalog.Persistence.Queries;
using Microsoft.Extensions.Logging;
using Microsoft.FSharp.Core;
using MongoDB.Driver;
using Open.ChannelExtensions;

namespace Catalog.Infrastructure.Repositories.Import
{
    public class MongoProductsImportWriteRepository : IProductsImportWriteRepository
    {
        private IMongoDatabase _database;
        private readonly ILogger<MongoProductsImportWriteRepository> _logger;

        public MongoProductsImportWriteRepository(IMongoDatabase database, ILogger<MongoProductsImportWriteRepository> logger)
        {
            _database = database;
            _logger = logger;
        }

        public async Task<FSharpResult<Unit, Exception>> Store(IAsyncEnumerable<Product> productsSource)
        {
            var products = _database.ProductsCollection();
            var result = productsSource.ToChannel()
                .Pipe(x => x.ToMongoProduct(DateTime.UtcNow.AddDays(1)))
                .Pipe(x => new InsertOneModel<MongoProduct>(x))
                .Batch(1000)
                .PipeAsync(Environment.ProcessorCount, async p => await products.BulkWriteAsync(p))
                .AsAsyncEnumerable();

            await foreach (var res in result)
            {
                _logger.LogDebug("Inserted {Count}", res.InsertedCount);
            }

            return Result.UnitOk<Exception>();
        }
    }
}