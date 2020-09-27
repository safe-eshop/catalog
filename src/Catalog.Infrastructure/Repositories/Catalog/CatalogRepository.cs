using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Catalog.Domain.Model;
using Catalog.Domain.Repository;
using Catalog.Infrastructure.Mappers;
using Catalog.Persistence.Queries;
using LanguageExt;
using Microsoft.FSharp.Core;
using MongoDB.Driver;

namespace Catalog.Infrastructure.Repositories.Catalog
{
    public class CatalogRepository : ICatalogRepository
    {
        private readonly IMongoDatabase _database;

        public CatalogRepository(IMongoDatabase database)
        {
            _database = database;
        }

        public async Task<FSharpOption<Product>> GetById(ProductId id, ShopId shopId)
        {
            var result = await _database.ProductsCollection().GetProductById(id, shopId).ConfigureAwait(false);
            return result.Map(mongo => mongo.ToProduct()).ToFSharp();
        }

        public IAsyncEnumerable<Product> GetByIds(IEnumerable<ProductId> ids, ShopId shopId)
        {
            
        }
    }
}