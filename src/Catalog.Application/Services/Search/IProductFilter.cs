using System.Collections.Generic;
using System.Threading.Tasks;
using Catalog.Application.Queries.Search;
using Catalog.Domain.Model;
using LanguageExt;

namespace Catalog.Application.Services.Search
{
    public interface IProductFilter
    {
        Task<Option<IList<ProductId>>> FilterProducts(FilterProductsQuery query);
    }
}