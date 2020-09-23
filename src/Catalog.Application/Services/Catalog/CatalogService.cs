using System;
using System.Threading.Tasks;
using Catalog.Application.Dto.Common;
using Catalog.Application.Mappers.Common;
using Catalog.Domain.Repository;
using FSharpx;
using LanguageExt;
using static LanguageExt.Prelude;
using static LanguageExt.FSharp;

namespace Catalog.Application.Services.Catalog
{
    public class CatalogService : ICatalogService
    {
        private ICatalogRepository _catalogRepository;

        public CatalogService(ICatalogRepository catalogRepository)
        {
            _catalogRepository = catalogRepository;
        }

        public async Task<Option<ProductDto>> GetProductById(int id, int shopId = 1)
        {
            var result = await _catalogRepository.GetById(id, shopId).ConfigureAwait(false);
            return fs(result).Map(prod => prod.MapToDto());
        }
    }
}