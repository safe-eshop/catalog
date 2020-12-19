using System.Collections.Generic;
using Catalog.Core.Model;

namespace Catalog.Core.Dto.Common
{
    public record ProductDto(ProductId Id, ShopId ShopId, string Slug, ProductDescriptionDto Description, PriceDto Price,
        ProductDetailsDto Details, IEnumerable<string> Tags);

    public record ProductDetailsDto(double Weight, string WeightUnits, string Picture, string Color);

    public record ProductDescriptionDto(string Name, string Brand, string Description);

    public record PriceDto(decimal Regular, decimal? Promotional);
}