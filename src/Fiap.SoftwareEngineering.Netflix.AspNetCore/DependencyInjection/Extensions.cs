﻿using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;

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
            services.AddApiVersioning(p =>
            {
                p.ReportApiVersions = true;
                p.AssumeDefaultVersionWhenUnspecified = true;
            });

            services.AddVersionedApiExplorer(p =>
            {
                p.GroupNameFormat = @"'v'VVV";
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

                options.SwaggerDoc("v1", new Info { Title = title, Version = "v1" });
            });

            return services;
        }
    }
}
