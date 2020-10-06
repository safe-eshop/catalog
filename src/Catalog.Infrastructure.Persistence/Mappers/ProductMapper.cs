using System;
using Catalog.Domain.Model;
using Catalog.Persistence.Model;

namespace Catalog.Persistence.Mappers
{
    public static class ProductMapperExtensions
    {
        public static ProductDetails ToProductDetails(this MongoProductDetails details)
        {
            return new ProductDetails(details.Weight, details.WeightUnits, details.Picture, details.Color);
        }

        public static ProductDescription ToProductDescription(this MongoProductDescription description)
        {
            return new ProductDescription(description.Name, description.Brand, description.Description);
        }

        public static Price ToProductPrice(this MongoPrice price)
        {
            return Price.Create((decimal) price.Regular, (decimal?) price.Promotional);
        }

        public static Product ToProduct(this MongoProduct product)
        {
            return new Product(new ProductId(product.ProductId), new ShopId(product.ShopId), product.Id,
                ToProductDescription(product.Description), ToProductPrice(product.Price),
                ToProductDetails(product.Details), product.Tags);
        }
        
        public static MongoProduct ToMongoProduct(this Product product, DateTime effectiveDate)
        {
            return new MongoProduct()
            {
                Id = product.Slug,
                ProductId = product.Id.Value,
                Description = new MongoProductDescription()
                {
                    Brand = product.Description.Brand,
                    Description = product.Description.Description,
                    Name = product.Description.Name
                },
                Details = new MongoProductDetails()
                {
                    Color = product.Details.Color,
                    Picture = product.Details.Picture,
                    Weight = product.Details.Weight,
                    WeightUnits = product.Details.WeightUnits
                },
                Price = new MongoPrice()
                {
                    Promotional = (double?) product.Price.Promotional,
                    Regular = (double) product.Price.Regular
                },
                Tags = product.Tags,
                EffectiveDate = effectiveDate,
                ShopId = product.ShopId.Value
            };
        }
    }
}