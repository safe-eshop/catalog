using Catalog.Domain.Model;
using Catalog.Persistence.Model;

namespace Catalog.Infrastructure.Mappers
{
    public static class ProductMapperExtensions
    {
        public static ProductDetails ToProductDetails(this MongoProductDetails details)
        {
            return new ProductDetails(det);
        } 
        public static Product ToProduct(this MongoProduct product)
        {
            return new Product(new ProductId(product.Id), new ShopId(product.ShopId));
        } 
        public static Product ToProduct(this MongoProduct product)
        {
            return new Product(new ProductId(product.Id), new ShopId(product.ShopId));
        } 
    }
}