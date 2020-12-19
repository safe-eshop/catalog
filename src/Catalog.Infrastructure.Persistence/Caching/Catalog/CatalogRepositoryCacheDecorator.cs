using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Catalog.Infrastructure.Persistence.Mappers;
using Catalog.Infrastructure.Persistence.Model;
using LanguageExt;
using Microsoft.Extensions.Caching.Distributed;
using static LanguageExt.Prelude;

namespace Catalog.Infrastructure.Persistence.Caching.Catalog
{
    public class CatalogRepositoryCacheDecorator : ICatalogRepository
    {
        private readonly IDistributedCache _distributedCache;
        private readonly ICatalogRepository _catalogRepository;

        public CatalogRepositoryCacheDecorator(IDistributedCache distributedCache, ICatalogRepository catalogRepository)
        {
            _distributedCache = distributedCache;
            _catalogRepository = catalogRepository;
        }

        public async Task<Option<Product>> GetById(ProductId id, ShopId shopId)
        {
            var key = $"{nameof(ICatalogRepository)}:{nameof(GetById)}:{id.Value}:{shopId.Value}";
            var result = await _distributedCache.GetStringAsync(key);
            if (string.IsNullOrEmpty(result))
            {
                var dbResult = await _catalogRepository.GetById(id, shopId);
                await dbResult.IfSomeAsync(async res =>
                {
                    await _distributedCache.SetAsync(key,
                        Encoding.UTF8.GetBytes(JsonSerializer.Serialize(res.ToMongoProduct(DateTime.UtcNow))), new DistributedCacheEntryOptions
                            {AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10)});
                });
                return dbResult;
            }

            var obj = JsonSerializer.Deserialize<MongoProduct?>(result);
            return Optional(obj).Map(x => x.ToProduct());
        }

        public IAsyncEnumerable<Product> GetByIds(IEnumerable<ProductId> ids, ShopId shopId)
        {
            return _catalogRepository.GetByIds(ids, shopId);
        }
    }
}