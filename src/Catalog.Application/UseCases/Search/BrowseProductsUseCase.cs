using System.Threading.Tasks;
using Catalog.Application.Dto.Filter;
using Catalog.Application.Queries.Search;
using Catalog.Application.Services.Catalog;
using Catalog.Application.Services.Search;
using LanguageExt;

namespace Catalog.Application.UseCases.Search
{
    public class BrowseProductsUseCase
    {
        private IProductFilter _filter;
        private ICatalogService _catalogServicde;
        public Task<Option<PagedProductListDto>> Execute(FilterProductsQuery query)
        {
            return null;
        }
    }
}