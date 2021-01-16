using Catalog.Core.Model;
using Catalog.Core.Queries.Search;

namespace Catalog.Api.Framework.Requests
{
    public class FilterProductsRequest
    {
        public int ShopNumber { get; set; }
        public int PageSize { get; set;}
        public int Page { get; set;}
        public int? CategoryId { get; set; }
        public double? MaxPrice { get; set; }
        public double? MinPrice { get; set; }
        public double? MinRating { get; set; }
        public string? SortOrder { get; set; }

        public FilterProductsQuery MapToFilterProductsQuery() =>
            new(new ShopId(ShopNumber), PageSize,
                Page,
                CategoryId, MaxPrice, MinPrice, MinRating, SortOrder);
    }
}