using System;
using System.Threading;
using System.Threading.Tasks;
using Fiap.SoftwareEngineering.Netflix.Domain.Abstractions;
using Fiap.SoftwareEngineering.Netflix.Domain.Abstractions.Entities;
using Fiap.SoftwareEngineering.Netflix.Domain.Abstractions.Validations;
using Fiap.SoftwareEngineering.Netflix.Validation.Abstractions;
using FluentValidation;

namespace Fiap.SoftwareEngineering.Netflix.Domain.Validations
{
    public class DomainValidator<TEntity> : IDomainValidator<TEntity>
        where TEntity : class
    {
        public INotificationContext NotificationContext { get; }
        public IValidator<TEntity> Validator { get; }

        public DomainValidator(INotificationContext notificationContext, IValidator<TEntity> validator)
        {
            NotificationContext = notificationContext ?? throw new ArgumentNullException(nameof(notificationContext));
            Validator = validator ?? throw new ArgumentNullException(nameof(validator));
        }

        public virtual async Task<bool> ValidateDomainAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            if (!(entity is IValidableEntity<TEntity> entityValidable)) return true;
            
            var isValid = entityValidable.Validate(entity, Validator);
            if (!isValid)
                NotificationContext.AddNotifications(entityValidable.ValidationResult);

            return await Task.FromResult(isValid);
        }
    }
}
