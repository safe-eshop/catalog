using System.Text.Json.Serialization;
using Catalog.Core.Repository;
using Catalog.Infrastructure.Persistence.Caching.Catalog;
using Catalog.Infrastructure.Persistence.Extensions;
using Catalog.Infrastructure.Persistence.Repositories.Catalog;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using MongoDB.Driver;

namespace Catalog.Api.Framework.Infrastructure.IoC
{
    public static class IServiceCollectionsExtensions
    {
        public static IServiceCollection AddSAspNetCore(this IServiceCollection services)
        {
            services.AddControllers().AddJsonOptions(x =>
            {
                x.JsonSerializerOptions.IgnoreNullValues = true;
                x.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            });
            return services;
        }
        public static IServiceCollection AddSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Catalog.Api", Version = "v1" });
            });
            return services;
        }
        
        public static IServiceCollection AddHealth(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHealthChecks().AddMongoDb(configuration.GetConnectionString("Products"));
            return services;
        }
        
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddRepositories();
            services.AddDatabase(configuration);
            services.AddCache(configuration);
            return services;
        }
        private static void AddRepositories(this IServiceCollection services)
        {
            services.AddTransient<IProductRepository, ProductRepository>();
            services.Decorate<IProductRepository, ProductRepositoryCacheDecorator>();
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