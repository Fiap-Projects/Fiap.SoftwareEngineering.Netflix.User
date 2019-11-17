using System;
using Fiap.SoftwareEngineering.Netflix.AspNetCore.Builder;
using Fiap.SoftwareEngineering.Netflix.AspNetCore.DependencyInjection;
using Fiap.SoftwareEngineering.Netflix.AspNetCore.Hosting;
using Fiap.SoftwareEngineering.Netflix.Configuration;
using Fiap.SoftwareEngineering.Netflix.Domain;
using Fiap.SoftwareEngineering.Netflix.Repository;
using Fiap.SoftwareEngineering.Netflix.Repository.EntityFramework;
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

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddConfiguration<AppSettingsBase>(Configuration);
            services.AddServices(AppSettingsManager<AppSettingsBase>.Settings?.SwaggerTitle);
            services.AddDomain();
            services.AddRepository();

            var connectionStringReader = Environment.GetEnvironmentVariable(EnvironmentVariable.ConnectionStringReader);
            var connectionStringWriter = Environment.GetEnvironmentVariable(EnvironmentVariable.ConnectionStringWriter);
            services.AddEntityFrameworkRepository(connectionStringReader, connectionStringWriter);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IApiVersionDescriptionProvider provider)
        {
            env.SetUpEnvironment(app);
            app.BuildApiApplication(provider, AppSettingsManager<AppSettingsBase>.Settings?.HealthCheckUrl);
        }
    }
}
