using Fiap.SoftwareEngineering.Netflix.Domain.Abstractions;
using Fiap.SoftwareEngineering.Netflix.Domain.Abstractions.Entities;
using FluentValidation;
using FluentValidation.Results;

namespace Fiap.SoftwareEngineering.Netflix.Domain.Entities
{
    public abstract class ValidableEntity<TEntity> : IValidableEntity<TEntity>
        where TEntity : class
    {
        public ValidationResult ValidationResult { get; private set; }
        public bool Validate(TEntity entity, IValidator<TEntity> validator)
        {
            ValidationResult = validator.Validate(entity);
            return ValidationResult.IsValid;
        }
    }
}
