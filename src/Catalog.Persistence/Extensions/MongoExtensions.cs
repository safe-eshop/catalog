using Catalog.Persistence.Model;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;

namespace Catalog.Persistence.Extensions
{
    public static class MongoExtensions
    {
        public static void AddCollections(this IMongoDatabase db)
        {
            BsonClassMap.RegisterClassMap<MongoProduct>();
        }
    }
}