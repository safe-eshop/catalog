using Catalog.Application.Dto.Common;
using Catalog.Domain.Model;

namespace Catalog.Application.Mappers.Common
{
    public static class ProductMapperExtensions
    {
        public static ProductDto MapToDto(this Product product)
        {
            return new ProductDto(product.Id);
        }
    }
}