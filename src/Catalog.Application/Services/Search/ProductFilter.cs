using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Catalog.Application.Dto.Common;
using Catalog.Application.Dto.Filter;
using Catalog.Application.Queries.Search;
using Catalog.Domain.Model;
using LanguageExt;
using static LanguageExt.Prelude;

namespace Catalog.Application.Services.Search
{
    public class ProductFilter : IProductFilter
    {
        public async Task<Option<IList<ProductId>>> FilterProducts(FilterProductsQuery query)
        {
            return None;
        }
    }
}