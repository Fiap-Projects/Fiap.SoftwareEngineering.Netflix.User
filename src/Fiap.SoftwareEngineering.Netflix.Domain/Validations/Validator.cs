using FluentValidation;

namespace Fiap.SoftwareEngineering.Netflix.Domain.Validations
{
    public class Validator<TEntity> : AbstractValidator<TEntity>
        where TEntity : class
    {
    }
}
