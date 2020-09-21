using System;
using System.Threading.Tasks;
using Catalog.Application.Dto.Common;
using LanguageExt;
using static LanguageExt.Prelude;

namespace Catalog.Application.Services.Catalog
{
    public class CatalogService : ICatalogService
    {
        public async Task<Option<ProductDto>> GetProductById(Guid id, int shopId = 1)
        {
            return None;
        }
    }
}