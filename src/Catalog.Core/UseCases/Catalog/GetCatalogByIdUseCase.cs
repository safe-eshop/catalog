using System.Collections.Generic;
using System.Threading.Tasks;
using Catalog.Core.Dto.Common;
using Catalog.Core.Model;
using Catalog.Core.Services.Catalog;
using LanguageExt;

namespace Catalog.Core.UseCases.Catalog
{
    public sealed class GetCatalogByIdUseCase
    {
        private ICatalogService _catalogService;

        public GetCatalogByIdUseCase(ICatalogService catalogService)
        {
            _catalogService = catalogService;
        }

        public async Task<Option<ProductDto>> Execute(ProductId id, ShopId shopId) => await _catalogService.GetProductById(id, shopId);

        public IAsyncEnumerable<ProductDto> Execute(IEnumerable<ProductId> ids, ShopId shopId) =>
            _catalogService.GetProductByIds(ids, shopId);
    }
}