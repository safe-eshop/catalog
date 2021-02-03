using System;
using System.Linq;
using Catalog.Core.Model;
using Catalog.Infrastructure.Persistence.Model;

namespace Catalog.Infrastructure.Persistence.Mappers
{
    public static class ProductMapperExtensions
    {
        internal static ProductDetails ToProductDetails(this MongoProductDetails details)
        {
            return new ProductDetails(details.Weight, details.WeightUnits, details.Picture, details.Color);
        }

        internal static ProductDescription ToProductDescription(this MongoProductDescription description)
        {
            return new ProductDescription(description.Name, description.Brand, description.Description);
        }

        internal static Price ToProductPrice(this MongoPrice price)
        {
            return Price.Create((decimal) price.Regular, (decimal?) price.Promotional);
        }

        internal static Product ToProduct(this MongoProduct product)
        {
            return new Product(new ProductId(product.Id), new ShopId(product.ShopId), product.Slug,
                ToProductDescription(product.Description), ToProductPrice(product.Price),
                ToProductDetails(product.Details),
                new Tags(product.Tags.Select(x => new Tag(x)).ToList()));
        }
    }
}