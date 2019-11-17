using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Fiap.SoftwareEngineering.Netflix.Repository.Abstractions.Entities;

namespace Fiap.SoftwareEngineering.Netflix.Repository.Abstractions
{
    public interface IRepositoryReader<TEntity> : IRepositoryReader<TEntity, int>
        where TEntity : class, IEntity<int>
    { }

    public interface IRepositoryReader<TEntity, in TKey>
        where TEntity : class, IEntity<TKey>
        where TKey : IEquatable<TKey>
    {
        Task<TEntity> GetAsync(TKey key, CancellationToken cancellationToken = default);
        Task<TEntity> GetAsync(CancellationToken cancellationToken = default, params object[] keys);
        Task<IEnumerable<TEntity>> GetAsync(CancellationToken cancellationToken = default);
        Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);
    }
}
