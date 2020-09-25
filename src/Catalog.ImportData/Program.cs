using System;
using System.Threading.Tasks;
using Catalog.ImportData.Framework.Logging;
using Cocona;
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
                    services.AddTransient<MyService>();
                })
                .ConfigureAppConfiguration(builder =>
                {
                    builder
                        .AddJsonFile("appsettings.json", true, true)
                        .AddEnvironmentVariables();
                })
                .RunAsync<Program>(args);
        }

        public void Hello([FromService] MyService myService, [FromService] IConfiguration configuration)
        {
            myService.Hello($"Hello {configuration["Test"]}");
        }
    }

    class MyService
    {
        private readonly ILogger _logger;

        public MyService(ILogger<MyService> logger)
        {
            _logger = logger;
        }

        public void Hello(string message)
        {
            _logger.LogInformation(message);
        }
    }
}