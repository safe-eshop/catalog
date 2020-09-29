using System;
using Catalog.Domain.Model;
using Catalog.Persistence.Model;

namespace Catalog.Infrastructure.Mappers
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
            return new Product(new ProductId(product.Id), new ShopId(product.ShopId), product.Slug,
                ToProductDescription(product.Description), ToProductPrice(product.Price),
                ToProductDetails(product.Details), product.Tags);
        }
    }
}