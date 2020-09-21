using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Catalog.Api.Framework.Application;
using Catalog.Api.Framework.Infrastructure.IoC;
using Catalog.Api.Framework.Infrastructure.Logging;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Serilog;

namespace Catalog.Api
{
    public class Startup
    {
        public const string PathBaseEnviromentVariable = "PATH_BASE";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        
        public string PathBase => Configuration[PathBaseEnviromentVariable] ?? string.Empty;


        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers().AddJsonOptions(x =>
            {
                x.JsonSerializerOptions.IgnoreNullValues = true;
                x.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            });
            
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo {Title = "Notification.Center", Version = "v1"});
            });
            services.AddApplication();
            services.AddInfrastructure();
            services.AddHealthChecks().AddMongoDb(Configuration.GetConnectionString("Products"));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (!string.IsNullOrEmpty(PathBase))
            {
                Log.Logger.Information($"Set BasePath {PathBase}");
                app.UsePathBase(PathBase);
            }
            
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            
            app.UseSerilogRequestLogging(opts =>
            {
                opts.EnrichDiagnosticContext = RequestLogging.EnrichFromRequest;
                opts.GetLevel = RequestLogging.ExcludeHealthChecks; // Use the custom level
            });
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint(
                    $"{(!string.IsNullOrEmpty(PathBase) ? PathBase : string.Empty)}/swagger/v1/swagger.json",
                    "");
                c.RoutePrefix = "swagger";
            });

            app.UseRouting();
            app.UseCors(builder =>
            {
                builder.AllowAnyHeader();
                builder.AllowAnyMethod();
                builder.AllowAnyOrigin();
            });
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHealthChecks("/health", new HealthCheckOptions()
                {
                    Predicate = _ => true,
                    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
                });

                endpoints.MapHealthChecks("/ping", new HealthCheckOptions()
                {
                    Predicate = r => r.Name.Contains("self"),
                    ResponseWriter = PongWriteResponse,
                });
            });
        }

        private static Task PongWriteResponse(HttpContext httpContext,
            HealthReport result)
        {
            httpContext.Response.ContentType = "application/json";
            return httpContext.Response.WriteAsync("pong");
        }
    }
}
