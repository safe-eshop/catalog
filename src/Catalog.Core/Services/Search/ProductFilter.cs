using System.Threading.Tasks;
using Catalog.Core.Dto.Filter;
using Catalog.Core.Queries.Search;
using LanguageExt;
using static LanguageExt.Prelude;

namespace Catalog.Core.Services.Search
{
    public class ProductFilter : IProductFilter
    {
        public async Task<Option<PagedProductListDto>> FilterProducts(FilterProductsQuery query)
        {
            return None;
        }
    }
}