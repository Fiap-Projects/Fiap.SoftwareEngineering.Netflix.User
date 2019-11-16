using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Fiap.SoftwareEngineering.Netflix.Configuration
{
    public static class Extensions
    {
        public static IServiceCollection AddConfiguration<TSettings>(this IServiceCollection services,
            IConfiguration configuration) where TSettings : class
        {
            AppSettingsManager<TSettings>.Load(configuration);
            services.Configure<TSettings>(configuration.Bind);

            return services;
        } 
    }
}
