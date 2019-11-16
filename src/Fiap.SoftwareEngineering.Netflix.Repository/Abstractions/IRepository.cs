namespace Fiap.SoftwareEngineering.Netflix.Repository.Abstractions
{
    public interface IRepository<TEntity> where TEntity : class
    {
        IRepositoryReader<TEntity> Reader { get; }
        IRepositoryReader<TEntity> Writer { get; }
    }
}