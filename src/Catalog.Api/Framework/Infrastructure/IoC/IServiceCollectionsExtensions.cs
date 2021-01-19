using System.Text.Json.Serialization;
using Catalog.Core.Repository;
using Catalog.Infrastructure.Persistence.Caching.Catalog;
using Catalog.Infrastructure.Persistence.Extensions;
using Catalog.Infrastructure.Persistence.Framework.IoC;
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
            services.AddHealthChecks().AddCatalogInfrastructureHealthChecks(configuration);
            return services;
        }
        
    }
}