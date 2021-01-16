using System.Threading.Tasks;
using Catalog.Core.Dto.Filter;
using Catalog.Core.Queries.Search;
using LanguageExt;

namespace Catalog.Core.Services.Search
{
    public interface IProductFilter
    {
        Task<PagedProductListDto?> FilterProducts(FilterProductsQuery query);
    }
}