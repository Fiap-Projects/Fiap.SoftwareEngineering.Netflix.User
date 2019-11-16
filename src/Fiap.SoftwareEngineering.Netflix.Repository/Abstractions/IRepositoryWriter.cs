using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Fiap.SoftwareEngineering.Netflix.Repository.Abstractions
{
    public interface IRepositoryWriter<TEntity> where TEntity : class
    {
        IUnitOfWork UnitOfWork { get; }

        Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default);
        Task AddRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default);
        Task Update(TEntity entity);
        Task UpdateRange(IEnumerable<TEntity> entities);
        Task Remove(TEntity entity);
        Task RemoveRange(IEnumerable<TEntity> entities);
    }
}