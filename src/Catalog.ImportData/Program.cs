using System.Threading.Tasks;
using Catalog.Core.Repository;
using Catalog.Core.UseCases.Import;
using Catalog.Domain.Repository;
using Catalog.ImportData.Framework.Logging;
using Catalog.Infrastructure.Persistence.Extensions;
using Catalog.Infrastructure.Persistence.Repositories.Import;
using Cocona;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;

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

        public static async Task Main(string[] args)
        {
            await CoconaApp.Create()
                .UseLogger("Catalog.ImportData")
                .ConfigureServices((ctx, services) =>
                {
                    services.AddTransient<IProductsImportSource, FakeProductsImportSource>();
                    services.AddTransient<IProductsImportWriteRepository, MongoProductsImportWriteRepository>();
                    services.AddSingleton<IMongoClient>(sp =>
                    {
                        var connectionString = ctx.Configuration.GetConnectionString("Products");
                        return new MongoClient(new MongoUrl(connectionString));
                    });

                    services.AddSingleton<IMongoDatabase>(sp =>
                    {
                        var client = sp.GetService<IMongoClient>();
                        var db = client.GetDatabase("Catalog");
                        db.AddCollections();
                        return db;
                    });
                    services.AddScoped<FullImportProductsTodayUseCase>();
                })
                .ConfigureAppConfiguration(builder =>
                {
                    builder
                        .AddJsonFile("appsettings.json", true, true)
                        .AddEnvironmentVariables();
                })
                .RunAsync<Program>(args);
        }

        public async Task<int> Import([FromService] FullImportProductsTodayUseCase fullImportProductsTodayUse)
        {
            await fullImportProductsTodayUse.Execute();
            return 0;
        }
    }
}