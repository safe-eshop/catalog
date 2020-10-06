using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Catalog.Domain.Model;
using Catalog.Domain.Repository;
using Catalog.Persistence.Mappers;
using Catalog.Persistence.Queries;
using LanguageExt;
using MongoDB.Driver;

namespace Catalog.Persistence.Repositories.Catalog
{
    public class CatalogRepository : ICatalogRepository
    {
        private readonly IMongoDatabase _database;

        public CatalogRepository(IMongoDatabase database)
        {
            _database = database;
        }

        public async Task<Option<Product>> GetById(ProductId id, ShopId shopId)
        {
            var result = await _database.ProductsCollection().GetProductById(id, shopId).ConfigureAwait(false);
            return result.Map(mongo => mongo.ToProduct());
        }

        public IAsyncEnumerable<Product> GetByIds(IEnumerable<ProductId> ids, ShopId shopId)
        {
            return _database.ProductsCollection().GetProductByIds(ids, shopId).Select(x => x.ToProduct());
        }
    }
}