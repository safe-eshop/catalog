using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Catalog.Application.Dto.Common;
using Catalog.Domain.Model;
using LanguageExt;

namespace Catalog.Application.Services.Catalog
{
    public interface ICatalogService
    {
        Task<Option<ProductDto>> GetProductById(ProductId id, ShopId shopId);
        IAsyncEnumerable<ProductDto> GetProductByIds(IEnumerable<ProductId> id, ShopId shopId);
    }
}