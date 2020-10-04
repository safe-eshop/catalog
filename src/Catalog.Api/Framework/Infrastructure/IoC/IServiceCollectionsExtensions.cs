using Catalog.Domain.Repository;
using Catalog.Infrastructure.Caching.Catalog;
using Catalog.Infrastructure.Repositories.Catalog;
using Catalog.Persistence.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Catalog.Api.Framework.Infrastructure.IoC
{
    public static class IServiceCollectionsExtensions
    {
        
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddRepositories();
            services.AddDatabase(configuration);
            services.AddCache(configuration);
            return services;
        }
        private static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddTransient<ICatalogRepository, CatalogRepository>();
            services.Decorate<ICatalogRepository, CatalogRepositoryCacheDecorator>();
            return services;
        }
        
        private static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
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
            return services;
        }
        
        private static IServiceCollection AddCache(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = configuration.GetConnectionString("ProductsCache");
                options.InstanceName = nameof(Catalog);
            });
            return services;
        }
    }
}