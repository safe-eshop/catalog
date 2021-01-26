﻿using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Channels;
using System.Threading.Tasks;
using Bogus;
using Catalog.Core.Model;
using Catalog.Core.Repository;
using Open.ChannelExtensions;

namespace Catalog.Infrastructure.Persistence.Repositories.Import
{
    internal class FakeProductsProvider : IProductsProvider
    {

        private static IEnumerable<Product> Generate(ProductId productId, IEnumerable<ShopId> shopIds)
        {
            var faker = new Faker("en");
            var details = new ProductDetails(faker.Commerce.Random.Double(), "kg", faker.Image.PicsumUrl(),
                faker.Commerce.Color());
            var desc = new ProductDescription(faker.Commerce.ProductName(), faker.Company.CompanyName(),
                faker.Commerce.ProductDescription());
            var tags = faker.Commerce.Categories(5);
            return shopIds.Select(shopId =>
            {
                var slug = Product.GenerateSlug(productId, shopId);
                return new Product(productId, shopId, slug, desc, new Price(decimal.Parse(faker.Commerce.Price(100)),
                    faker.Random.Bool() ? (decimal?) null : decimal.Parse(faker.Commerce.Price(1, 99))), details, Tags.From(tags));
            });
        }

        public IAsyncEnumerable<Product> ProduceProducts(CancellationToken cancellationToken = default)
        {
            return Enumerable.Range(0, 100)
                .Select(id => (id, Enumerable.Range(0, 200)))
                .Select(x =>
                {
                    var (id, shopNums) = x;
                    return Generate(new ProductId(id), shopNums.Select(shopId => new ShopId(shopId)).ToList());
                }).SelectMany(x => x)
                .ToAsyncEnumerable();
        }
    }
}