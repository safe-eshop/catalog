using System.Threading.Tasks;
using Catalog.Application.Dto.Filter;
using Catalog.Application.Queries.Search;
using Catalog.Application.Services.Search;
using LanguageExt;

namespace Catalog.Application.UseCases.Search
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