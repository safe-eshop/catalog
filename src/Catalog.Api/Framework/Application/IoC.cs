using Catalog.Core.Services.Catalog;
using Catalog.Core.Services.Search;
using Catalog.Core.UseCases.Catalog;
using Catalog.Core.UseCases.Search;
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