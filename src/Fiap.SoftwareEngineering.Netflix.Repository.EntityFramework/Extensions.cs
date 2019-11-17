using System;
using System.Collections.Generic;
using Fiap.SoftwareEngineering.Netflix.Repository.Abstractions;
using Fiap.SoftwareEngineering.Netflix.Repository.EntityFramework.Abstractions;
using Fiap.SoftwareEngineering.Netflix.Repository.EntityFramework.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Npgsql.EntityFrameworkCore.PostgreSQL.Infrastructure;

namespace Fiap.SoftwareEngineering.Netflix.Repository.EntityFramework
{
    public static class Extensions
    {
        public static IServiceCollection AddEntityFrameworkRepository(this IServiceCollection services,
            string connectionStringReader, string connectionStringWriter)
        {
            services.AddEntityFrameworkNpgsql()
                .AddDbContext<IContextReader, EntityFrameworkContextReader>(options =>
                    options.UseNpgsql(connectionStringReader, EnableRetryOnFailure));
            services.AddEntityFrameworkNpgsql()
                .AddDbContext<IContextWriter, EntityFrameworkContextWriter>(options =>
                    options.UseNpgsql(connectionStringWriter, EnableRetryOnFailure));

            //services.AddScoped<IDbContextOptions, DbContextOptions>();
            //services.AddSingleton<IContextReader, EntityFrameworkContextReader>();
            //services.AddSingleton<IContextWriter, EntityFrameworkContextWriter>();

            services.AddSingleton(typeof(IRepositoryReader<>), typeof(RepositoryReader<>));
            services.AddSingleton(typeof(IRepositoryWriter<>), typeof(RepositoryWriter<>));

            return services;
        }

        private static void EnableRetryOnFailure(NpgsqlDbContextOptionsBuilder optionsBuilder) =>
            optionsBuilder.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), new List<string>());

    }
}
