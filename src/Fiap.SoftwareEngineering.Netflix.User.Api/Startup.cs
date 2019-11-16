using Fiap.SoftwareEngineering.Netflix.AspNetCore.Builder;
using Fiap.SoftwareEngineering.Netflix.AspNetCore.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Fiap.SoftwareEngineering.Netflix.User.Api
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services) => services.AddServices(@"Fiap Software Engineering Netflix User Api");

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IApiVersionDescriptionProvider provider)
        {
            env.SetUpEnvironment(app);
            app.BuildApiApplication(provider, @"/health");
        }
    }
}
