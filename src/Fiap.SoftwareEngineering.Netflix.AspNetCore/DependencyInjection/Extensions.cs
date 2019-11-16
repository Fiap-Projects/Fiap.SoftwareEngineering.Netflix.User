using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using System.Reflection;
using Fiap.SoftwareEngineering.Netflix.Api.Versioning;
using Fiap.SoftwareEngineering.Netflix.Validation.Abstractions;

namespace Fiap.SoftwareEngineering.Netflix.AspNetCore.DependencyInjection
{
    public static class Extensions
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            return AddServices(services, string.Empty);
        }

        public static IServiceCollection AddServices(this IServiceCollection services, string swaggerTitle)
        {
            services.AddApi();
            services.AddApiVersioning();
            services.AddSwagger(swaggerTitle);
            services.AddScoped<INotificationContext>();

            return services;
        }

        public static IServiceCollection AddApi(this IServiceCollection services)
        {
            services.AddCors();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddHealthChecks();
            services.AddHttpContextAccessor();

            return services;
        }

        public static IServiceCollection AddApiVersioning(this IServiceCollection services)
        {
            services.AddApiVersioning(options =>
            {
                options.ReportApiVersions = true;
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.ApiVersionReader = new HeaderApiVersionReader(Headers.Version);
            });

            services.AddVersionedApiExplorer(p =>
            {
                p.GroupNameFormat = Headers.VersionPattern;
                p.SubstituteApiVersionInUrl = true;
            });

            return services;
        }

        public static IServiceCollection AddSwagger(this IServiceCollection services)
        {
            return AddSwagger(services, string.Empty);
        }

        public static IServiceCollection AddSwagger(this IServiceCollection services, string swaggerTitle)
        {
            services.AddSwaggerGen(options =>
            {
                var title = swaggerTitle;
                if (string.IsNullOrEmpty(title))
                    title = Assembly.GetEntryAssembly()?.GetName().Name;

                var provider = services.BuildServiceProvider()
                    .GetRequiredService<IApiVersionDescriptionProvider>();
                
                foreach ( var description in provider.ApiVersionDescriptions )
                {
                    var apiVersion = description.GroupName;
                    options.SwaggerDoc(apiVersion, new Info { Title = title, Version = apiVersion });
                }

            });

            return services;
        }
    }
}
