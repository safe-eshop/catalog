using System.Threading.Tasks;
using Catalog.Application.Dto.Filter;
using Catalog.Application.Queries.Search;
using Catalog.Application.Services.Catalog;
using Catalog.Application.Services.Search;
using Catalog.Domain.Model;
using LanguageExt;

namespace Catalog.Application.UseCases.Search
{
    public class BrowseProductsUseCase
    {
        private IProductFilter _filter;
        private ICatalogService _catalogServicde;
        public async Task<Option<PagedProductListDto>> Execute(FilterProductsQuery query)
        {
            var productIdsOpt = await _filter.FilterProducts(query); ;
            
            return productIdsOpt
                .BindAsync(ids => _catalogServicde.GetProductByIds(ids, ShopId.Create(query.ShopNumber)).ToAsync())
                .Map(products =>
                {
                    returm 
                })
            
        }
    }
}