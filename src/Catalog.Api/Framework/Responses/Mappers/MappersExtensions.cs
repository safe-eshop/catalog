using Catalog.Application.Dto.Common;

namespace Catalog.Api.Framework.Responses.Mappers
{
    public static class MappersExtensions
    {
        public static ProductResponse ToResponse(this ProductDto dto)
        {
            return new ProductResponse()
            {
                Id = dto.Id.Value,
                ShopId = dto.ShopId.Value,
                Description = dto.Description,
                Details = dto.Details,
                Price = dto.Price,
                Tags = dto.Tags
            };
        }
    }
}