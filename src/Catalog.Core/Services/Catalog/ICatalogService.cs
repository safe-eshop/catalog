using System.Collections.Generic;
using System.Threading.Tasks;
using Catalog.Core.Dto.Common;
using Catalog.Core.Model;
using LanguageExt;

namespace Catalog.Core.Services.Catalog
{
    public interface ICatalogService
    {
        Task<Option<ProductDto>> GetProductById(ProductId id, ShopId shopId);
        IAsyncEnumerable<ProductDto> GetProductByIds(IEnumerable<ProductId> id, ShopId shopId);
    }
}