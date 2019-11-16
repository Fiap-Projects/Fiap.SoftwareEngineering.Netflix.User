using Fiap.SoftwareEngineering.Netflix.AspNetCore.Builder;
using Fiap.SoftwareEngineering.Netflix.AspNetCore.Builder.Middleware;
using Fiap.SoftwareEngineering.Netflix.AspNetCore.DependencyInjection;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace Fiap.SoftwareEngineering.Netflix.User.Api
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services) => services.AddServices();

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IApiVersionDescriptionProvider provider)
        {
            env.SetUpEnvironment(app);
            app.BuildApiApplication(env, provider);
        }
    }
}
