using System.Collections.Generic;
using System.Threading.Tasks;
using Catalog.Core.Model;
using LanguageExt;

namespace Catalog.Core.Repository
{
    public interface IProductRepository
    {
        Task<Option<Product>> GetById(ProductId id, ShopId shopId);
        IAsyncEnumerable<Product> GetByIds(IEnumerable<ProductId> ids, ShopId shopId);
    }
}