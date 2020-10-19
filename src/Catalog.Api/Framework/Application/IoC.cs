using Catalog.Application.Services.Catalog;
using Catalog.Application.Services.Search;
using Catalog.Application.UseCases.Catalog;
using Catalog.Application.UseCases.Search;
using Microsoft.Extensions.DependencyInjection;

namespace Catalog.Api.Framework.Application
{
    public static class IoC
    {
        public static void AddApplication(this IServiceCollection services)
        {
            services.AddApplicationServices();
            services.AddApplicationUseCases();
        }

        private static void AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<ICatalogService, CatalogService>();
            services.AddScoped<IProductFilter, ProductFilter>();
        }

        private static void AddApplicationUseCases(this IServiceCollection services)
        {
            services.AddScoped<GetCatalogByIdUseCase>();
            services.AddScoped<BrowseProductsUseCase>();
        }
    }
}