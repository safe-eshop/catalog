using System;
using System.Threading.Tasks;
using Catalog.Application.Dto.Common;
using LanguageExt;

namespace Catalog.Application.Services.Catalog
{
    public interface ICatalogService
    {
        Task<Option<ProductDto>> GetProductById(int id, int shopId = 1);
    }
}