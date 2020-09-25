using System;
using System.Threading.Tasks;
using Cocona;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Catalog.ImportData
{
    class Program
    {
        public Program(ILogger<Program> logger)
        {
            logger.LogInformation("Create Instance");
        }

        static void Main(string[] args)
        {
            CoconaApp.Create()
                .ConfigureServices(services => { services.AddTransient<MyService>(); })
                .ConfigureAppConfiguration(builder =>
                {
                    builder
                        .AddJsonFile("appsettings.json", true, true)
                        .AddEnvironmentVariables();
                })
                .Run<Program>(args);
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