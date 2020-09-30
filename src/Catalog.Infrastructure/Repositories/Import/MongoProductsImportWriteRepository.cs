using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Catalog.Domain.Helpers;
using Catalog.Domain.Model;
using Catalog.Domain.Repository;
using Catalog.Infrastructure.Mappers;
using Catalog.Persistence.Model;
using Catalog.Persistence.Queries;
using Microsoft.FSharp.Core;
using MongoDB.Driver;
using Open.ChannelExtensions;

namespace Catalog.Infrastructure.Repositories.Import
{
    public class MongoProductsImportWriteRepository : IProductsImportWriteRepository
    {
        private IMongoDatabase _database;

        public MongoProductsImportWriteRepository(IMongoDatabase database)
        {
            _database = database;
        }

        public async Task<FSharpResult<Unit, Exception>> Store(IAsyncEnumerable<Product> productsSource)
        {

            var products = _database.ProductsCollection();
            productsSource.ToChannel()
                .Pipe(x => x.ToMongoProduct(DateTime.UtcNow.AddDays(1)))
                .Batch(1000)
                .Pipe(x => Bu)
                .PipeAsync(50, async p => await products.BulkWriteAsync());

            return Result.UnitOk<Exception>();
        }
    }
}