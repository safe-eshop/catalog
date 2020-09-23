using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Catalog.Persistence.Model
{
    public class MongoProduct
    {
        [BsonId] 
        [BsonRepresentation(BsonType.String)]
        public Guid Id { get; set; }
        public int ShopId { get; set; }
        public DateTime EffectiveDate { get; set; }
    }
}