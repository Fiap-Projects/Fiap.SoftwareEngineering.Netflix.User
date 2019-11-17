using Fiap.SoftwareEngineering.Netflix.Domain.Abstractions.Services;
using Fiap.SoftwareEngineering.Netflix.Repository.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Fiap.SoftwareEngineering.Netflix.Repository.Abstractions.Entities;

namespace Fiap.SoftwareEngineering.Netflix.Domain.Services
{
    public class DomainReaderService<TEntity> : DomainReaderService<TEntity, int>, IDomainReaderService<TEntity>
        where TEntity : class, IEntity<int>
    {
        public DomainReaderService(IRepositoryReader<TEntity> repository) : base(repository)
        {
        }
    }

    public class DomainReaderService<TEntity, TKey> : IDomainReaderService<TEntity, TKey>
        where TEntity : class, IEntity<TKey>
        where TKey : IEquatable<TKey>
    {
        protected IRepositoryReader<TEntity, TKey> Repository { get; }

        public DomainReaderService(IRepositoryReader<TEntity, TKey> repository)
        {
            Repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public virtual async Task<TEntity> GetAsync(TKey key, CancellationToken cancellationToken = default) =>
            await Repository.GetAsync(key, cancellationToken);

        public virtual async Task<TEntity> GetAsync(CancellationToken cancellationToken = default, params object[] keys) => 
            await Repository.GetAsync(cancellationToken, keys);

        public virtual async Task<IEnumerable<TEntity>> GetAsync(CancellationToken cancellationToken = default) =>
            await Repository.GetAsync(cancellationToken);

        public async Task<bool> Exists(TKey key, CancellationToken cancellationToken = default)
        {
            var result = await GetAsync(key, cancellationToken);
            return result != null;
        }

        public virtual async Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate,
            CancellationToken cancellationToken = default) => 
            await Repository.FindAsync(predicate, cancellationToken);
    }
}
