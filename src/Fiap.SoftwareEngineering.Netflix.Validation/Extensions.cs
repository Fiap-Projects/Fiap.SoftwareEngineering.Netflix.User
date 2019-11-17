using Fiap.SoftwareEngineering.Netflix.Validation.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace Fiap.SoftwareEngineering.Netflix.Validation
{
    public static class Extensions
    {
        public static IServiceCollection AddNotifications(this IServiceCollection services)
        {
            services.AddTransient<INotificationContext, NotificationContext>();

            return services;
        }
    }
}
