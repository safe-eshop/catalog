using System;
using System.Threading.Tasks;
using Catalog.Application.UseCases.Import;
using Catalog.Domain.Repository;
using Catalog.ImportData.Framework.Logging;
using Catalog.Infrastructure.Repositories.Import;
using Cocona;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Catalog.ImportData
{
    class Program
    {
        private readonly IConfiguration _configuration;

        public Program(ILogger<Program> logger, IConfiguration configuration)
        {
            _configuration = configuration;
            logger.LogInformation("Create Instance");
        }

        static async Task Main(string[] args)
        {
            await CoconaApp.Create()
                .UseLogger("Catalog.ImportData")
                .ConfigureServices((ctx, services) =>
                {
                    services.AddTransient<IProductsImportSource, FakeProductsImportSource>();
                    services.AddTransient<IProductsImportWriteRepository, MongoProductsImportWriteRepository>();
                    services.AddScoped<FullImportProductsTomorrowUseCase>();
                })
                .ConfigureAppConfiguration(builder =>
                {
                    builder
                        .AddJsonFile("appsettings.json", true, true)
                        .AddEnvironmentVariables();
                })
                .RunAsync<Program>(args);
        }

        public async Task<int> Import([FromService] FullImportProductsTomorrowUseCase fullImportProductsTomorrowUse)
        {
            await fullImportProductsTomorrowUse.Execute();
            return 0;
        }
    }
}