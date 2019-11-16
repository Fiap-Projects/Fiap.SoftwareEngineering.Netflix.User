using System.Threading;
using System.Threading.Tasks;
using Fiap.SoftwareEngineering.Netflix.Validation.Abstractions;
using FluentValidation;

namespace Fiap.SoftwareEngineering.Netflix.Domain.Abstractions.Validations
{
    public interface IDomainValidator<TEntity> where TEntity : class
    {
        INotificationContext NotificationContext { get; }
        IValidator<TEntity> Validator { get; }

        Task<bool> ValidateDomainAsync(TEntity entity, CancellationToken cancellationToken = default);
    }
}
