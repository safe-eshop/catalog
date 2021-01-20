// using System;
// using System.Threading.Tasks;
// using Catalog.Core.Model;
// using Catalog.Infrastructure.Persistence.Extensions;
// using Catalog.Infrastructure.Persistence.Queries;
// using Catalog.Infrastructure.Persistence.Repositories.Catalog;
// using FluentAssertions;
// using MongoDB.Driver;
// using Xunit;
//
// namespace Catalog.Infrastructure.Tests.Repository
// {
//     public class MongoDbFixture : IDisposable
//     {
//         public IMongoClient Client { get; }
//         public IMongoDatabase Database { get; }
//
//         public MongoDbFixture()
//         {
//             Client = new MongoClient(Environment.GetEnvironmentVariable("TST_MONGO_BASKET") ?? "mongodb://root:rootpassword@127.0.0.1:27017");
//             Database = Client.GetDatabase($"{nameof(Catalog)}{nameof(Infrastructure)}{nameof(Tests)}");
//             Database.AddCollections();
//         }
//
//
//         public void Dispose()
//         {
//             Database.DropCollection(Collections.ProductsCollectionName);
//         }
//     }
//     
//     public class CatalogRepositoryTests: IClassFixture<MongoDbFixture>
//     {
//         private MongoDbFixture _mongoDbFixture;
//
//         public CatalogRepositoryTests(MongoDbFixture mongoDbFixture)
//         {
//             _mongoDbFixture = mongoDbFixture;
//         }
//
//         [Fact]
//         public async Task TestWhenProductNotExists()
//         {
//             var id = new ProductId(1);
//             var shopId = new ShopId(1);
//             var repository = new ProductRepository(_mongoDbFixture.Database);
//
//             var productOpt = await repository.GetById(id, shopId);
//
//             productOpt.IsNone.Should().BeTrue();
//         } 
//     }
// }