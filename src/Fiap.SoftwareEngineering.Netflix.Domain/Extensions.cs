using Fiap.SoftwareEngineering.Netflix.Domain.Abstractions.Services;
using Fiap.SoftwareEngineering.Netflix.Domain.Abstractions.Validations;
using Fiap.SoftwareEngineering.Netflix.Domain.Services;
using Fiap.SoftwareEngineering.Netflix.Domain.Validations;
using Fiap.SoftwareEngineering.Netflix.Validation;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Fiap.SoftwareEngineering.Netflix.Domain
{
    public static class Extensions
    {
        public static IServiceCollection AddDomain(this IServiceCollection services)
        {
            services.AddDomainServices().AddDomainValidations().AddNotifications();

            return services;
        }

        public static IServiceCollection AddDomainServices(this IServiceCollection services)
        {
            services.AddTransient(typeof(IDomainService<>), typeof(DomainService<>));
            services.AddTransient(typeof(IDomainReaderService<>), typeof(DomainReaderService<>));
            services.AddTransient(typeof(IDomainWriterService<>), typeof(DomainWriterService<>));

            return services;
        }

        public static IServiceCollection AddDomainValidations(this IServiceCollection services)
        {
            services.AddScoped(typeof(IDomainValidator<>), typeof(DomainValidator<>));
            services.AddTransient(typeof(IValidator<>), typeof(Validator<>));

            return services;
        }
    }
}
