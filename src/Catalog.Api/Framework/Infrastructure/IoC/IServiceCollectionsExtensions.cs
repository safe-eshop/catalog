using Catalog.Domain.Repository;
using Catalog.Infrastructure.Repositories.Catalog;
using Microsoft.Extensions.DependencyInjection;

namespace Catalog.Api.Framework.Infrastructure.IoC
{
    public static class IServiceCollectionsExtensions
    {
        
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddRepositories();
            return services;
        }
        private static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddTransient<ICatalogRepository, CatalogRepository>();
            return services;
        }
    }
}