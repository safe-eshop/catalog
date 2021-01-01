using Catalog.Core.Model;

namespace Catalog.Core.Queries.Search
{
    public record FilterProductsQuery(ShopId ShopNumber, int PageSize, int Page, int? CategoryId, double? MaxPrice,
        double? MinPrice, double? MinRating, string SortOrder);
}