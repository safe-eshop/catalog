using Catalog.Domain.Model;
using Catalog.Persistence.Model;

namespace Catalog.Infrastructure.Mappers
{
    public static class ProductMapperExtensions
    {
        public static Product ToProduct(this MongoProduct product)
        {
            return new Product(product.Id);
        } 
    }
}