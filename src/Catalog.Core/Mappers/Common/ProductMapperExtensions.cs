using Catalog.Application.Dto.Common;
using Catalog.Domain.Model;

namespace Catalog.Application.Mappers.Common
{
    public static class ProductMapperExtensions
    {
        public static ProductDetailsDto MapToDto(this ProductDetails details)
        {
            return new ProductDetailsDto(details.Weight, details.WeightUnits, details.Picture, details.Color);
        } 
        
        public static ProductDescriptionDto MapToDto(this ProductDescription desc)
        {
            return new ProductDescriptionDto(desc.Name, desc.Brand, desc.Description);
        } 
        
        public static PriceDto MapToDto(this Price price)
        {
            return new PriceDto(price.Regular, price.Promotional);
        } 
        
        public static ProductDto MapToDto(this Product product)
        {
            return new ProductDto(product.Id, product.ShopId, product.Slug, product.Description.MapToDto(), product.Details.MapToDto(), product.Price.MapToDto(), product.Tags);
        }
    }
}