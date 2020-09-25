using System.Threading.Tasks;
using Catalog.Application.Dto.Filter;
using Catalog.Application.Queries.Search;
using Catalog.Application.Services.Catalog;
using Catalog.Application.Services.Search;
using Catalog.Domain.Model;
using LanguageExt;
using static LanguageExt.Prelude;

namespace Catalog.Application.UseCases.Search
{
    public class BrowseProductsUseCase
    {
        private readonly IProductFilter _filter;
        private readonly ICatalogService _catalogServicde;

        public BrowseProductsUseCase(IProductFilter filter, ICatalogService catalogServicde)
        {
            _filter = filter;
            _catalogServicde = catalogServicde;
        }

        public async Task<Option<PagedProductListDto>> Execute(FilterProductsQuery query)
        {
            var productIdsOpt = await _filter.FilterProducts(query);
            ;

            return await productIdsOpt
                .BindAsync(async list =>
                {
                    var result =
                        await _catalogServicde.GetProductByIds(list.ProductsIds, ShopId.Create(query.ShopNumber));
                    return result.Map(ids => new PagedProductListDto(ids, list.TotalItems, list.TotalPages)).ToAsync();
                }).ToOption();
        }
    }
}