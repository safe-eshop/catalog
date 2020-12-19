using System.Threading.Tasks;
using Catalog.Application.Dto.Filter;
using Catalog.Application.Queries.Search;
using LanguageExt;
using static LanguageExt.Prelude;

namespace Catalog.Application.Services.Search
{
    public class ProductFilter : IProductFilter
    {
        public async Task<Option<PagedProductListDto>> FilterProducts(FilterProductsQuery query)
        {
            return None;
        }
    }
}