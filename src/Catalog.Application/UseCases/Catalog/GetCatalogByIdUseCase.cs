using System;
using System.Threading.Tasks;
using Catalog.Application.Dto.Common;
using Catalog.Application.Services.Catalog;
using Catalog.Domain.Model;
using LanguageExt;

namespace Catalog.Application.UseCases.Catalog
{
    public sealed class GetCatalogByIdUseCase
    {
        private ICatalogService _catalogService;

        public GetCatalogByIdUseCase(ICatalogService catalogService)
        {
            _catalogService = catalogService;
        }

        public async Task<Option<ProductDto>> Execute(ProductId id, ShopId shopId) => await _catalogService.GetProductById(id, shopId);
    }
}