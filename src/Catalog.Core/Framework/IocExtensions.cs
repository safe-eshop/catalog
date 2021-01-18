using Catalog.Core.Services.Catalog;
using Catalog.Core.Services.Search;
using Catalog.Core.UseCases.Catalog;
using Catalog.Core.UseCases.Search;
using Microsoft.Extensions.DependencyInjection;

namespace Catalog.Core.Framework
{
    public static class IocExtensions
    {
        public static IServiceCollection AddCore(this IServiceCollection services)
        {
            services.AddScoped<ICatalogService, CatalogService>();
            services.AddScoped<IProductFilter, ProductFilter>();
            services.AddScoped<GetCatalogByIdUseCase>();
            services.AddScoped<BrowseProductsUseCase>();
            return services;
        }
    }
}