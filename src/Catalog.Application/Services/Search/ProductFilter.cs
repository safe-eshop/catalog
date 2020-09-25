using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Catalog.Application.Dto.Filter;
using Catalog.Application.Queries.Search;
using Catalog.Domain.Model;
using LanguageExt;
using Open.ChannelExtensions;
using static LanguageExt.Prelude;

namespace Catalog.Application.Services.Search
{
    public class ProductFilter : IProductFilter
    {
        public async Task<Option<FilteredProductList>> FilterProducts(FilterProductsQuery query)
        {
            
            return None;
        }
    }
}