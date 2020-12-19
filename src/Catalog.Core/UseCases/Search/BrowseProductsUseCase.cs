using System.Threading.Tasks;
using Catalog.Core.Dto.Filter;
using Catalog.Core.Queries.Search;
using Catalog.Core.Services.Search;
using LanguageExt;

namespace Catalog.Core.UseCases.Search
{
    public class BrowseProductsUseCase
    {
        private readonly IProductFilter _filter;

        public BrowseProductsUseCase(IProductFilter filter)
        {
            _filter = filter;
        }

        public async Task<Option<PagedProductListDto>> Execute(FilterProductsQuery query)
        {
            return await _filter.FilterProducts(query);
        }
    }
}