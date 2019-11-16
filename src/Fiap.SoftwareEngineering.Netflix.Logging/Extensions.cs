using Microsoft.AspNetCore.Hosting;
using Serilog;

namespace Fiap.SoftwareEngineering.Netflix.Logging
{
    public static class Extensions
    {
        public static IWebHostBuilder UseLog(this IWebHostBuilder hostBuilder) => 
            hostBuilder.UseSerilog();
    }
}
