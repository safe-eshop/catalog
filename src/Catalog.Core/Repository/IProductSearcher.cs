using System.Collections.Generic;
using System.Threading.Tasks;
using Catalog.Core.Model;
using Catalog.Core.Queries.Search;
using LanguageExt;

namespace Catalog.Core.Repository
{
    public record QueryParameters(string Query);

    public record SearchProductsQuery(ShopId ShopNumber, int PageSize, int Page, int? CategoryId, double? MaxPrice,
        double? MinPrice, double? MinRating, string? SortOrder)
    {
        public SearchProductsQuery(FilterProductsQuery query) : this(query.ShopNumber, query.PageSize, query.Page,
            query.CategoryId, query.MaxPrice, query.MinPrice, query.MinRating, query.SortOrder)
        {
        }
    }

    public record SearchAndFilterParameters(string Query, int ShopNumber, int PageSize, int Page, int? CategoryId,
        double? MaxPrice,
        double? MinPrice, double? MinRating, string SortOrder);

    public record PagedProductList(IList<ProductId> ProductIds, int TotalItems, int TotalPages);

    public interface IProductSearcher
    {
        Task<Option<PagedProductList>> Query(QueryParameters parameters);
        Task<PagedProductList?> Search(SearchProductsQuery filterProductsQuery);
    }
}