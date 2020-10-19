using Cocona.Hosting;
using Serilog;
using Serilog.Events;
using Serilog.Exceptions;
using Serilog.Sinks.SystemConsole.Themes;

namespace Catalog.ImportData.Framework.Logging
{
    public class SerilogOptions
    {
        public bool ConsoleEnabled { get; set; } = true;
        public string MinimumLevel { get; set; } = "Information";
        public string Format { get; set; } = "compact";
    }
    
    public static class SerilogExtensions
    {
        public static CoconaAppHostBuilder UseLogger(this CoconaAppHostBuilder hostBuilder, string applicationName = null)
        {
            return hostBuilder.ConfigureLogging(builder =>
            {
                var conf = new LoggerConfiguration()
                    .MinimumLevel.Is(LogEventLevel.Information)
                    .Enrich.FromLogContext()
                    .Enrich.WithProperty("ApplicationName", applicationName)
                    .Enrich.WithEnvironmentUserName()
                    .Enrich.WithProcessId()
                    .Enrich.WithProcessName()
                    .Enrich.WithThreadId()
                    .Enrich.WithExceptionDetails();

                conf.WriteTo.Async((logger) =>
                {

                    logger.Console(theme: AnsiConsoleTheme.Code);
                    logger.Trace();
                });
                builder.AddSerilog(conf.CreateLogger());
            });
        }
    }
}