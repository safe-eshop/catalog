﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Catalog.Core.Model;
using Catalog.Core.Repository;
using Catalog.Infrastructure.Persistence.Mappers;
using Catalog.Infrastructure.Persistence.Queries;
using LanguageExt;
using MongoDB.Driver;

namespace Catalog.Infrastructure.Persistence.Repositories.Catalog
{
    internal class ProductRepository : IProductRepository
    {
        private readonly IMongoDatabase _database;

        public ProductRepository(IMongoDatabase database)
        {
            _database = database;
        }

        public async Task<Option<Product>> GetById(ProductId id, ShopId shopId)
        {
            var result = await _database.ProductsCollection().GetProductById(id, shopId).ConfigureAwait(false);
            return result.Map(mongo => mongo.ToProduct());
        }

        public IAsyncEnumerable<Product> GetByIds(IEnumerable<ProductId> ids, ShopId shopId)
        {
            return _database.ProductsCollection().GetProductByIds(ids, shopId).Select(x => x.ToProduct());
        }
    }
}