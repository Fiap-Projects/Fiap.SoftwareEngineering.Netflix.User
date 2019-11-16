using Fiap.SoftwareEngineering.Netflix.AspNetCore.Builder.Middleware;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace Fiap.SoftwareEngineering.Netflix.AspNetCore.Builder
{
    public static class Extensions
    {
        public static IApplicationBuilder BuildApiApplication(this IApplicationBuilder app, IHostingEnvironment env, IApiVersionDescriptionProvider provider)
        {
            app.UseMvcWithDefaultRoute();
            app.UseVersioning();
            app.UseMiddlewares();
            app.AllowOrigins();
            app.UseHealthChecks();
            app.UseApiDocumentation(provider);

            return app;
        }

        public static IHostingEnvironment SetUpEnvironment(this IHostingEnvironment env, IApplicationBuilder applicationBuilder)
        {
            if (env.IsDevelopment())
                applicationBuilder.UseDeveloperExceptionPage();
            else
                applicationBuilder.UseHsts();

            applicationBuilder.UseHttpsRedirection();

            return env;
        }

        public static IApplicationBuilder UseVersioning(this IApplicationBuilder app)
        {
            app.UseApiVersioning();
            
            return app;
        }

        public static IApplicationBuilder UseMiddlewares(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionMiddleware>();

            return app;
        }

        public static IApplicationBuilder AllowOrigins(this IApplicationBuilder app)
        {
            app.UseCors(_ => {
                _.AllowAnyOrigin();
                _.AllowAnyHeader();
                _.AllowAnyMethod();
            });

            return app;
        }

        public static IApplicationBuilder UseHealthChecks(this IApplicationBuilder app)
        {
            app.UseHealthChecks("/health", new HealthCheckOptions
            {
                Predicate = _ => true,
                ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
            });

            return app;
        }
        
        public static IApplicationBuilder UseApiDocumentation(this IApplicationBuilder app, IApiVersionDescriptionProvider provider)
        {
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                foreach (var description in provider.ApiVersionDescriptions)
                {
                    options.SwaggerEndpoint(
                        $"/swagger/{description.GroupName}/swagger.json",
                        description.GroupName.ToUpperInvariant());
                }

                options.DocExpansion(DocExpansion.List);
            });

            return app;
        }
    }
}
