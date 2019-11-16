namespace Fiap.SoftwareEngineering.Netflix.Domain.Abstractions
{
    public interface IDomain<TEntity> : IDomainReader<TEntity>, IDomainWriter<TEntity>
        where TEntity : class
    {
    }
}
