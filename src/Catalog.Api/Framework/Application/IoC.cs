using Catalog.Application.Services.Catalog;
using Microsoft.Extensions.DependencyInjection;

namespace Catalog.Api.Framework.Application
{
    public static class IoC
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddApplicationServices();
            services.AddAppicationUseCases();
            return services;
        }

        private static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<ICatalogService, CatalogService>();
            return services;
        }

        private static IServiceCollection AddAppicationUseCases(this IServiceCollection services)
        {
            services.AddScoped<ICatalogService, CatalogService>();
            return services;
        }
    }
}