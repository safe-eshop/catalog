using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Catalog.Core.Dto.Common;
using Catalog.Core.Mappers.Common;
using Catalog.Core.Model;
using Catalog.Core.Repository;
using LanguageExt;

namespace Catalog.Core.Services.Catalog
{
    public class CatalogService : ICatalogService
    {
        private ICatalogRepository _catalogRepository;

        public CatalogService(ICatalogRepository catalogRepository)
        {
            _catalogRepository = catalogRepository;
        }

        public async Task<Option<ProductDto>> GetProductById(ProductId id, ShopId shopId)
        {
            var result = await _catalogRepository.GetById(id, shopId).ConfigureAwait(false);
            return result.Map(prod => prod.MapToDto());
        }

        public IAsyncEnumerable<ProductDto> GetProductByIds(IEnumerable<ProductId> ids, ShopId shopId)
        {
            return _catalogRepository.GetByIds(ids, shopId).Select(x => x.MapToDto());
        }
    }
}