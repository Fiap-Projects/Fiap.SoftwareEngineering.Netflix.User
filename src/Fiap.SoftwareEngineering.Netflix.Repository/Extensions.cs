using Fiap.SoftwareEngineering.Netflix.Repository.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace Fiap.SoftwareEngineering.Netflix.Repository
{
    public static class Extensions
    {
        public static IServiceCollection AddRepository(this IServiceCollection services)
        {
            services.AddSingleton(typeof(IRepository<>), typeof(Repository<>));

            return services;
        }
    }
}
