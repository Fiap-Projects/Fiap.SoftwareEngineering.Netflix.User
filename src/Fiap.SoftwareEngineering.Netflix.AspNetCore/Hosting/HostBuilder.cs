using Fiap.SoftwareEngineering.Netflix.AspNetCore.Abstractions.Hosting;
using Fiap.SoftwareEngineering.Netflix.Logging;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using System;
using System.Threading.Tasks;

namespace Fiap.SoftwareEngineering.Netflix.AspNetCore.Hosting
{
    public class HostBuilder : IHostBuilder
    {
        public async Task<int> CreateWebHostBuilder<TStartup>(string[] args = default, string title = default)
            where TStartup : class
        {
            var logger = Logging.LoggerConfiguration.CreateLogger(EnvironmentVariable.AspnetcoreEnvironment);
            logger.Information("Starting host...");

            try
            {
                var consoleTitle = title;
                if (string.IsNullOrEmpty(consoleTitle))
                    consoleTitle = typeof(TStartup).Assembly.GetName().Name;

                Console.Title = consoleTitle;
                var hotBuilder = WebHost.CreateDefaultBuilder(args)
                    .ConfigureLogging(builder => builder.ClearProviders())
                    .UseStartup<TStartup>().UseLog();
                
                await hotBuilder.Build().RunAsync();
                return 0;
            }
            catch (Exception ex)
            {
                logger.Fatal(ex, "Web terminated unexpectedly.");
                return 1;
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        public static async Task<int> BuildWebHost<TStartup>(string[] args = default, string title = default)
            where TStartup : class => await new HostBuilder().CreateWebHostBuilder<TStartup>(args, title);
    }
}
