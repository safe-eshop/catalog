using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Catalog.Domain.Model;
using Catalog.Persistence.Model;
using LanguageExt;
using MongoDB.Driver;
using SolrNet.Utils;

namespace Catalog.Persistence.Queries
{
    public static class Collections
    {
        public const string ProductsCollectionName = "products";

        public static IMongoCollection<MongoProduct> ProductsCollection(this IMongoDatabase db) =>
            db.GetCollection<MongoProduct>(ProductsCollectionName);

        public static async Task<Option<MongoProduct>> GetProductById(this IMongoCollection<MongoProduct> products,
            ProductId id,
            ShopId shopId)
        {
            var find = products
                .Find(x => x.MongoId == MongoProduct.GenerateMongoId(id, shopId, DateTime.UtcNow.Date));

            return await find.FirstOrDefaultAsync();
        }

        public static async IAsyncEnumerable<MongoProduct> GetProductByIds(this IMongoCollection<MongoProduct> products,
            IEnumerable<ProductId> ids,
            ShopId shopId)
        {
            var productsId = ids.Select(x => x.Value).ToList();
            var result = await products
                .FindSync(x => productsId.Contains(x.Id) && x.ShopId == shopId.Value)
                .ToListAsync();

            foreach (var product in result)
            {
                yield return product;
            }
        }
    }
}