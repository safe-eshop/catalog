using Catalog.Core.Repository;
using Catalog.Infrastructure.Persistence.Caching.Catalog;
using Catalog.Infrastructure.Persistence.Extensions;
using Catalog.Infrastructure.Persistence.Repositories.Catalog;
using Catalog.Infrastructure.Persistence.Repositories.Import;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;

namespace Catalog.Infrastructure.Persistence.Framework.IoC
{
    public static class IocExtensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddRepositories();
            services.AddDatabase(configuration);
            services.AddCache(configuration);
            return services;
        }

        public static IServiceCollection AddImportInfrastructure(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddImportRepositories();
            services.AddDatabase(configuration);
            services.AddCache(configuration);
            return services;

        }

        public static IHealthChecksBuilder AddCatalogInfrastructureHealthChecks(this IHealthChecksBuilder builder, IConfiguration configuration)
        {
            return builder.AddMongoDb(configuration.GetConnectionString("Products"));
        }
        
        private static void AddRepositories(this IServiceCollection services)
        {
            services.AddTransient<IProductRepository, ProductRepository>();
            services.Decorate<IProductRepository, ProductRepositoryCacheDecorator>();
        }
        
        private static void AddImportRepositories(this IServiceCollection services)
        {
            services.AddTransient<IProductsProvider, FakeProductsProvider>();
            services.AddTransient<IProductsImportWriteRepository, MongoProductsImportWriteRepository>();
        }
        
        private static void AddDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IMongoClient>(_ =>
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