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
        
        public MongoProduct()
        {
            
        }

        public MongoProduct(Product product)
        {
            var effectiveDate = DateTime.UtcNow;
            Id = product.Id.Value;
            MongoId = GenerateMongoId(product.Id, product.ShopId, effectiveDate);
            ShopId = product.ShopId.Value;
            EffectiveDate = effectiveDate.Date;
            Description = new MongoProductDescription(product.Description);
            Details = new MongoProductDetails(product.Details);
            Price = new MongoPrice(product.Price);
            Tags = product.Tags.GetTags();
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

        public MongoProductDetails()
        {
            
        }

        public MongoProductDetails(ProductDetails details)
        {
            Weight = details.Weight;
            WeightUnits = details.WeightUnits;
            Picture = details.Picture;
            Color = details.Color;
        }
    }

    public class MongoProductDescription
    {
        public string? Name { get; set; }
        public string? Brand { get; set; }
        public string? Description { get; set; }

        public MongoProductDescription()
        {
            
        }

        public MongoProductDescription(ProductDescription description)
        {
            Name = description.Name;
            Brand = description.Brand;
            Description = description.Description;
        }
    }

    public class MongoPrice
    {
        public double Regular { get; set; }
        public double? Promotional { get; set; }

        public MongoPrice(Price price)
        {
            Regular = (double)price.Regular;
            Promotional = (double?) price.Promotional;
        }
    }
}