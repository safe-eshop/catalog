using System.Linq;
using Catalog.Core.Dto.Common;
using Catalog.Core.Model;

namespace Catalog.Core.Mappers.Common
{
    public static class ProductMapperExtensions
    {
        public static ProductDetailsDto MapToDto(this ProductDetails details) =>
            new(details.Weight, details.WeightUnits, details.Picture, details.Color);

        public static ProductDescriptionDto MapToDto(this ProductDescription desc) =>
            new(desc.Name, desc.Brand, desc.Description);

        public static PriceDto MapToDto(this Price price)
        {
            return new PriceDto(price.Regular, price.Promotional);
        }

        public static ProductDto MapToDto(this Product product)
        {
            return new ProductDto(product.Id, product.ShopId, product.Slug, product.Description.MapToDto(),
                product.Price.MapToDto(), product.Details.MapToDto(), product.Tags.Value.Select(x => x.Value).ToList());
        }
    }
}