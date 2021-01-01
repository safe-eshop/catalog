using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Catalog.Core.Dto.Filter;
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

        public async Task<Option<PagedProductListDto>> FilterProducts(FilterProductsQuery query)
        {
            var productsIds = await _productSearcher.Filter(query);
            var result = productsIds.Case switch
            {
                PagedProductList productList => Some(1),
                _ => None
            };
        }
    }
}