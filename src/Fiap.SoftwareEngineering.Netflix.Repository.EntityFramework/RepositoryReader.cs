using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Fiap.SoftwareEngineering.Netflix.Repository.Abstractions;
using Fiap.SoftwareEngineering.Netflix.Repository.EntityFramework.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace Fiap.SoftwareEngineering.Netflix.Repository.EntityFramework
{
    public class RepositoryReader<TEntity> : IRepositoryReader<TEntity>
        where TEntity : class
    {
        public IContext Context { get; }

        public RepositoryReader(IContextReader context)
        {
            Context = context ?? throw new ArgumentException(nameof(context));
        }

        public virtual async Task<TEntity> GetAsync(int key) => await Context.GetDbSet<TEntity>().FindAsync(key);

        public virtual async Task<TEntity> GetAsync(params object[] keys) => await Context.GetDbSet<TEntity>().FindAsync(keys);

        public virtual async Task<IEnumerable<TEntity>> GetAsync(CancellationToken cancellationToken = default) =>
            await Context.GetDbSet<TEntity>().ToListAsync(cancellationToken);

        public virtual async Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate,
            CancellationToken cancellationToken = default) =>
            await Context.GetDbSetAsQueryable<TEntity>().Where(predicate).ToListAsync(cancellationToken);
    }
}
