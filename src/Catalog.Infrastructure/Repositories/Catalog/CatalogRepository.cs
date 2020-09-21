using System;
using System.Threading.Tasks;
using Catalog.Domain.Model;
using Catalog.Domain.Repository;
using Microsoft.FSharp.Core;

namespace Catalog.Infrastructure.Repositories.Catalog
{
    public class CatalogRepository : ICatalogRepository
    {
        public async Task<FSharpOption<Product>> GetById(Guid id, int shopId)
        {
            return FSharpOption<Product>.None;
        }
    }
}