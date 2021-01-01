using System.Collections.Generic;
using System.Threading.Tasks;
using Catalog.Core.Model;
using Catalog.Core.Queries.Search;
using LanguageExt;

namespace Catalog.Core.Repository
{
    public record SearchParameters(string Query);

    public record SearchAndFilterParameters(string Query, int ShopNumber, int PageSize, int Page, int? CategoryId,
        double? MaxPrice,
        double? MinPrice, double? MinRating, string SortOrder);

    public record PagedProductList(IList<ProductId> ProductIds, int TotalItems, int TotalPages);

    public interface IProductSearcher
    {
        Task<Option<PagedProductList>> Search(SearchParameters parameters);
        Task<Option<PagedProductList>> Filter(FilterProductsQuery filterProductsQuery);
    }
}