using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Fiap.SoftwareEngineering.Netflix.Domain.Abstractions.Services
{

    public interface IDomainReaderService<TEntity> : IDomainReaderService<TEntity, int>
        where TEntity : class
    { }

    public interface IDomainReaderService<TEntity, in TKey> 
        where TEntity : class
        where TKey : IEquatable<TKey>
    {
        Task<TEntity> GetAsync(TKey key, CancellationToken cancellationToken = default);
        Task<TEntity> GetAsync(CancellationToken cancellationToken = default, params object[] keys);
        Task<IEnumerable<TEntity>> GetAsync(CancellationToken cancellationToken = default);        
        Task<bool> Exists(TKey key, CancellationToken cancellationToken = default);
        Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);
    }
}
