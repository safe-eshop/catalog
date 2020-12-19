using System;
using System.Collections.Generic;
using Catalog.Core.Model;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Catalog.Infrastructure.Persistence.Model
{
    public class MongoProduct
    {
        [BsonId] 
        [BsonRepresentation(BsonType.String)]
        public string MongoId { get; set; }
        public int Id { get; set; }
        public int ShopId { get; set; }
        [BsonDateTimeOptions(Kind = DateTimeKind.Utc, DateOnly = true)]
        public DateTime EffectiveDate { get; set; }
        public MongoProductDescription? Description { get; set; }
        public MongoProductDetails? Details { get; set; }
        public MongoPrice? Price { get; set; }
        public IEnumerable<string>? Tags { get; set; }

        public string Slug => $"{Id}__{ShopId}";

        public static MongoProduct Create(ProductId id, ShopId shopId, DateTime effectiveDate, MongoProductDescription description, MongoProductDetails details, MongoPrice price, IEnumerable<string> tags)
        {
            return new MongoProduct()
            {
                MongoId = GenerateMongoId(id, shopId, effectiveDate),
                Id = id.Value,
                ShopId = shopId.Value,
                Description = description,
                Details = details,
                Price = price,
                Tags = tags,
                EffectiveDate = effectiveDate
            };
        }

        public static string GenerateMongoId(ProductId id, ShopId shopId, DateTime effectiveDate)
        {
            return $"{id.Value}_{shopId.Value}_{effectiveDate:ddMMyyyy}";
        }
    }

    public class MongoProductDetails
    {
        public double Weight { get; set; }
        public string? WeightUnits { get; set; }
        public string? Picture { get; set; }
        public string? Color { get; set; }
    }

    public class MongoProductDescription
    {
        public string? Name { get; set; }
        public string? Brand { get; set; }
        public string? Description { get; set; }
    }

    public class MongoPrice
    {
        public double Regular { get; set; }
        public double? Promotional { get; set; }
    }
}