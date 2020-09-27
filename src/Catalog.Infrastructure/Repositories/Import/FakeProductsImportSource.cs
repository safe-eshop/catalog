using System;
using System.Collections.Generic;
using System.Linq;
using Bogus;
using Catalog.Domain.Model;
using Catalog.Domain.Repository;

namespace Catalog.Infrastructure.Repositories.Import
{
    public class FakeProductsImportSource : IProductsImportSource
    {
        public IAsyncEnumerable<Product> GetProductsToImport()
        {
            return Enumerable.Range(0, 10).Select(id => (id, Enumerable.Range(0, 10)))
                .Select(x =>
                {
                    var (id, shopNums) = x;
                    return shopNums.Select(shopId => new Product(new ProductId(id), new ShopId(shopId)));
                }).SelectMany(x => x).ToAsyncEnumerable();
        }
    }
}