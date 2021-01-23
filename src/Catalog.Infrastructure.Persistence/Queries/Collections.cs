﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Catalog.Core.Model;
using Catalog.Infrastructure.Persistence.Model;
using LanguageExt;
using MongoDB.Driver;

namespace Catalog.Infrastructure.Persistence.Queries
{
    public static class Collections
    {
        public const string ProductsCollectionName = "products";

        internal static IMongoCollection<MongoProduct> ProductsCollection(this IMongoDatabase db) =>
            db.GetCollection<MongoProduct>(ProductsCollectionName);

        internal static async Task<Option<MongoProduct>> GetProductById(this IMongoCollection<MongoProduct> products,
            ProductId id,
            ShopId shopId)
        {
            var find = products
                .Find(x => x.MongoId == MongoProduct.GenerateMongoId(id, shopId, DateTime.UtcNow.Date));

            return await find.FirstOrDefaultAsync();
        }

        internal static async IAsyncEnumerable<MongoProduct> GetProductByIds(this IMongoCollection<MongoProduct> products,
            IEnumerable<ProductId> ids,
            ShopId shopId)
        {
            var productsId = ids.Select(x => MongoProduct.GenerateMongoId(x, shopId, DateTime.UtcNow.Date)).ToList();
            var result = await products
                .FindSync(x => productsId.Contains(x.MongoId))
                .ToListAsync();

            foreach (var product in result)
            {
                yield return product;
            }
        }
    }
}