using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Catalog.Core.Model;
using Catalog.Core.Repository;
using Catalog.Infrastructure.Persistence.Mappers;
using Catalog.Infrastructure.Persistence.Model;
using LanguageExt;
using Microsoft.Extensions.Caching.Distributed;
using static LanguageExt.Prelude;

namespace Catalog.Infrastructure.Persistence.Caching.Catalog
{
    internal class ProductRepositoryCacheDecorator : IProductRepository
    {
        private readonly IDistributedCache _distributedCache;
        private readonly IProductRepository _productRepository;

        public ProductRepositoryCacheDecorator(IDistributedCache distributedCache, IProductRepository productRepository)
        {
            _distributedCache = distributedCache;
            _productRepository = productRepository;
        }

        public async Task<Option<Product>> GetById(ProductId id, ShopId shopId)
        {
            var key = $"{nameof(IProductRepository)}:{nameof(GetById)}:{id.Value}:{shopId.Value}";
            var result = await _distributedCache.GetStringAsync(key);
            if (string.IsNullOrEmpty(result))
            {
                var dbResult = await _productRepository.GetById(id, shopId);
                await dbResult.IfSomeAsync(async res =>
                {
                    await _distributedCache.SetAsync(key,
                        Encoding.UTF8.GetBytes(JsonSerializer.Serialize(new MongoProduct(res))), new DistributedCacheEntryOptions
                            {AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10)});
                });
                return dbResult;
            }

            var obj = JsonSerializer.Deserialize<MongoProduct?>(result);
            return Optional(obj).Map(x => x.ToProduct());
        }

        public IAsyncEnumerable<Product> GetByIds(IEnumerable<ProductId> ids, ShopId shopId)
        {
            return _productRepository.GetByIds(ids, shopId);
        }
    }
}