using System;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Catalog.Persistence.Model
{
    public class MongoProduct
    {
        [BsonId] 
        [BsonRepresentation(BsonType.String)]
        public string Id { get; set; }
        
        public int ProductId { get; set; }
        public int ShopId { get; set; }
        public DateTime EffectiveDate { get; set; }
        public MongoProductDescription Description { get; set; }
        public MongoProductDetails Details { get; set; }
        public MongoPrice Price { get; set; }
        public IEnumerable<string> Tags { get; set; }
    }

    public class MongoProductDetails
    {
        public double Weight { get; set; }
        public string WeightUnits { get; set; }
        public string Picture { get; set; }
        public string Color { get; set; }
    }

    public class MongoProductDescription
    {
        public string Name { get; set; }
        public string Brand { get; set; }
        public string Description { get; set; }
    }

    public class MongoPrice
    {
        public double Regular { get; set; }
        public double? Promotional { get; set; }
    }
}