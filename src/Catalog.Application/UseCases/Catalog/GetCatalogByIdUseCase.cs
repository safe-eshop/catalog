using System;
using System.Threading.Tasks;
using Catalog.Application.Dto.Common;
using Catalog.Application.Services.Catalog;
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

        public async Task<Option<ProductDto>> Execute(Guid id, int shopId) => await _catalogService.GetProductById(id, shopId);
    }
}