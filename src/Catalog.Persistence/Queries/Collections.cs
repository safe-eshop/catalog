using System;
using System.Threading.Tasks;
using Catalog.Persistence.Model;
using LanguageExt;
using MongoDB.Driver;

namespace Catalog.Persistence.Queries
{
    public static class Collections
    {
        public const string ProductsCollectionName = "products";

        public static IMongoCollection<MongoProduct> ProductsCollection(this IMongoDatabase db) =>
            db.GetCollection<MongoProduct>(ProductsCollectionName);

        public static async Task<Option<MongoProduct>> GetProductById(this IMongoCollection<MongoProduct> products, Guid id,
            int shopId)
        {
            var find = products
                .Find(x => x.Id == id);

            return await find.FirstOrDefaultAsync();
        }
    }
}