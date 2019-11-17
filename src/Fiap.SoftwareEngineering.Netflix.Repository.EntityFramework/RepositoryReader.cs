using Fiap.SoftwareEngineering.Netflix.Repository.Abstractions;
using Fiap.SoftwareEngineering.Netflix.Repository.Abstractions.Entities;
using Fiap.SoftwareEngineering.Netflix.Repository.EntityFramework.Abstractions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Fiap.SoftwareEngineering.Netflix.Repository.EntityFramework
{
    public class RepositoryReader<TEntity> : RepositoryReader<TEntity, int>, IRepositoryReader<TEntity>
        where TEntity : class, IEntity<int>
    {
        public RepositoryReader(IContextReader context) : base(context)
        {
        }
    }

    public class RepositoryReader<TEntity, TKey> : IRepositoryReader<TEntity, TKey>
        where TEntity : class, IEntity<TKey>
        where TKey : IEquatable<TKey>
    {
        public IContext Context { get; }

        public RepositoryReader(IContextReader context)
        {
            Context = context ?? throw new ArgumentException(nameof(context));
        }

        public virtual async Task<TEntity> GetAsync(TKey key, CancellationToken cancellationToken = default) => 
            await Context.GetDbSet<TEntity>().FindAsync(key, cancellationToken);

        public virtual async Task<TEntity> GetAsync(CancellationToken cancellationToken = default, params object[] keys) =>
            await Context.GetDbSet<TEntity>().FindAsync(keys, cancellationToken);

        public virtual async Task<IEnumerable<TEntity>> GetAsync(CancellationToken cancellationToken = default) =>
            await Context.GetDbSet<TEntity>().ToListAsync(cancellationToken);

        public virtual async Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate,
            CancellationToken cancellationToken = default) =>
            await Context.GetDbSetAsQueryable<TEntity>().Where(predicate).ToListAsync(cancellationToken);
    }
}
