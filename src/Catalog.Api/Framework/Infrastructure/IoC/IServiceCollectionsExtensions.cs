using Catalog.Domain.Repository;
using Catalog.Infrastructure.Repositories.Catalog;
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
            return services;
        }
        private static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddTransient<ICatalogRepository, CatalogRepository>();
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
                return client.GetDatabase("Catalog");
            });
            return services;
        }
    }
}