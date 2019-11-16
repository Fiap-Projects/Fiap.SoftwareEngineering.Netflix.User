namespace Fiap.SoftwareEngineering.Netflix.Domain.Abstractions.Services
{
    public interface IDomainService<TEntity> : IDomainReaderService<TEntity>, IDomainWriterService<TEntity>
        where TEntity : class
    {
    }
}
