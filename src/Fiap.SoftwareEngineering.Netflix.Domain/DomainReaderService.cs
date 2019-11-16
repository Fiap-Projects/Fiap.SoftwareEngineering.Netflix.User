using Fiap.SoftwareEngineering.Netflix.Repository.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Fiap.SoftwareEngineering.Netflix.Domain.Abstractions;

namespace Fiap.SoftwareEngineering.Netflix.Domain
{
    public class DomainReaderService<TEntity> : IDomainReader<TEntity>
        where TEntity : class
    {
        protected IRepositoryReader<TEntity> Repository { get; }

        public DomainReaderService(IRepositoryReader<TEntity> repository)
        {
            Repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public virtual async Task<TEntity> GetAsync(int key) => await Repository.GetAsync(key);

        public virtual async Task<TEntity> GetAsync(params object[] keys) => await Repository.GetAsync(keys);

        public virtual async Task<IEnumerable<TEntity>> GetAsync(CancellationToken cancellationToken = default) =>
            await Repository.GetAsync(cancellationToken);

        public virtual async Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate,
            CancellationToken cancellationToken = default) => 
            await Repository.FindAsync(predicate, cancellationToken);
    }
}
