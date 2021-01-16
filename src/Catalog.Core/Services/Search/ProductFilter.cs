using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Catalog.Core.Dto.Filter;
using Catalog.Core.Mappers.Common;
using Catalog.Core.Model;
using Catalog.Core.Queries.Search;
using Catalog.Core.Repository;
using LanguageExt;
using static LanguageExt.Prelude;

namespace Catalog.Core.Services.Search
{
    public class ProductFilter : IProductFilter
    {
        private IProductSearcher _productSearcher;
        private IProductRepository _repository;

        public ProductFilter(IProductSearcher productSearcher, IProductRepository repository)
        {
            _productSearcher = productSearcher;
            _repository = repository;
        }

        public async Task<PagedProductListDto?> FilterProducts(FilterProductsQuery query)
        {
            var pagedProductList = await _productSearcher.Search(new SearchProductsQuery(query));

            if (pagedProductList is null)
            {
                return null;
            }

            var productsIds = pagedProductList.ProductIds;
            
            var result = await _repository.GetByIds(productsIds, query.ShopNumber)
                .OrderBy(product => productsIds.IndexOf(product.Id))
                .Select(product => product.MapToDto())
                .ToListAsync();

            return new PagedProductListDto(result, pagedProductList.TotalItems, pagedProductList.TotalPages);
        }
    }
}