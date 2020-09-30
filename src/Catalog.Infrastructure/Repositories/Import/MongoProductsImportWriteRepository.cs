using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Catalog.Domain.Helpers;
using Catalog.Domain.Model;
using Catalog.Domain.Repository;
using Microsoft.FSharp.Core;

namespace Catalog.Infrastructure.Repositories.Import
{
    public class MongoProductsImportWriteRepository : IProductsImportWriteRepository
    {
        public async Task<FSharpResult<Unit, Exception>> Store(IAsyncEnumerable<Product> productsSource)
        {
            await foreach (var res in productsSource)
            {
                Console.WriteLine(res);
            }

            return Result.UnitOk<Exception>();
        }
    }
}