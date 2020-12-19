using Catalog.Core.Repository;
using Catalog.Infrastructure.Persistence.Caching.Catalog;
using Catalog.Infrastructure.Persistence.Extensions;
using Catalog.Infrastructure.Persistence.Repositories.Catalog;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;

namespace Catalog.Api.Framework.Infrastructure.IoC
{
    public static class IServiceCollectionsExtensions
    {
        
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddRepositories();
            services.AddDatabase(configuration);
            services.AddCache(configuration);
        }
        private static void AddRepositories(this IServiceCollection services)
        {
            services.AddTransient<ICatalogRepository, CatalogRepository>();
            services.Decorate<ICatalogRepository, CatalogRepositoryCacheDecorator>();
        }
        
        private static void AddDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IMongoClient>(ctx =>
            {
                var connectionString = configuration.GetConnectionString("Products");
                return new MongoClient(new MongoUrl(connectionString));
            });

            services.AddSingleton<IMongoDatabase>(ctx =>
            {
                var client = ctx.GetService<IMongoClient>();
                var db = client.GetDatabase("Catalog");
                db.AddCollections();
                return db;
            });
        }
        
        private static void AddCache(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = configuration.GetConnectionString("ProductsCache");
                options.InstanceName = nameof(Catalog);
            });
        }
    }
}