using System;
using System.Threading.Tasks;
using Catalog.Infrastructure.Repositories.Catalog;
using Catalog.Persistence.Extensions;
using Catalog.Persistence.Queries;
using FluentAssertions;
using MongoDB.Driver;
using Xunit;
using static LanguageExt.FSharp;

namespace Catalog.Infrastructure.Tests.Repository
{
    public class MongoDbFixture : IDisposable
    {
        public IMongoClient Client { get; }
        public IMongoDatabase Database { get; }

        public MongoDbFixture()
        {
            Client = new MongoClient(Environment.GetEnvironmentVariable("TST_MONGO_BASKET") ?? "mongodb://root:rootpassword@127.0.0.1:27017");
            Database = Client.GetDatabase($"{nameof(Catalog)}{nameof(Infrastructure)}{nameof(Tests)}");
            Database.AddCollections();
        }


        public void Dispose()
        {
            Database.DropCollection(Collections.ProductsCollectionName);
        }
    }
    
    public class CatalogRepositoryTests: IClassFixture<MongoDbFixture>
    {
        private MongoDbFixture _mongoDbFixture;

        public CatalogRepositoryTests(MongoDbFixture mongoDbFixture)
        {
            _mongoDbFixture = mongoDbFixture;
        }

        [Fact]
        public async Task TestWhenProductNotExists()
        {
            var id = Guid.NewGuid();
            const int shopId = 1;
            var repository = new CatalogRepository(_mongoDbFixture.Database);

            var productOpt = await repository.GetById(id, shopId);

            fs(productOpt).IsNone.Should().BeTrue();
        } 
    }
}