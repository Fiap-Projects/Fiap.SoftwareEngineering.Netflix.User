using FluentValidation;
using FluentValidation.Results;

namespace Fiap.SoftwareEngineering.Netflix.Domain.Abstractions
{
    public interface IValidableDomain<TEntity> where TEntity : class
    {
        ValidationResult ValidationResult { get; }
        bool Validate(TEntity entity, IValidator<TEntity> validator);
    }
}
