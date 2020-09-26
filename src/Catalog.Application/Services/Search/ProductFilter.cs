using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Catalog.Application.Dto.Filter;
using Catalog.Application.Queries.Search;
using Catalog.Domain.Model;
using Catalog.Domain.Repository;
using LanguageExt;
using Open.ChannelExtensions;
using static LanguageExt.Prelude;

namespace Catalog.Application.Services.Search
{
    public class ProductFilter : IProductFilter
    {
        private ICatalogRepository _catalogRepository;
        private IPro

        public ProductFilter(ICatalogRepository catalogRepository)
        {
            _catalogRepository = catalogRepository;
        }

        public async Task<Option<PagedProductListDto>> FilterProducts(FilterProductsQuery query)
        {
            return None;
        }
    }
}