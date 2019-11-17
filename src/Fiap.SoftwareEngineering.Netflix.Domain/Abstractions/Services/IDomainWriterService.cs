using Fiap.SoftwareEngineering.Netflix.Repository.Abstractions.Entities;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Fiap.SoftwareEngineering.Netflix.Domain.Abstractions.Services
{
    public interface IDomainWriterService<TEntity> : IDomainWriterService<TEntity, int>
        where TEntity : class, IEntity<int>
    { }

    public interface IDomainWriterService<TEntity, in TKey> 
        where TEntity : class, IEntity<TKey>
        where TKey : IEquatable<TKey>
    {
        Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default);
        Task AddRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default);
        Task UpdateAsync(TEntity entity, CancellationToken cancellationToken = default);
        Task UpdateRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default);
        Task RemoveAsync(TEntity entity, CancellationToken cancellationToken = default);
        Task RemoveRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default);
    }
}
