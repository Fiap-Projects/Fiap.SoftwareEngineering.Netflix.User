using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Fiap.SoftwareEngineering.Netflix.Repository.Abstractions;
using Fiap.SoftwareEngineering.Netflix.Repository.EntityFramework.Abstractions;

namespace Fiap.SoftwareEngineering.Netflix.Repository.EntityFramework
{
    public class RepositoryWriter<TEntity> : IRepositoryWriter<TEntity>
        where TEntity : class
    {
        public IContext Context { get; }
        public IUnitOfWork UnitOfWork { get; }

        public RepositoryWriter(IUnitOfWork unitOfWork, IContextWriter context)
        {
            UnitOfWork = unitOfWork ?? throw new ArgumentException(nameof(unitOfWork));
            Context = context ?? throw new ArgumentException(nameof(context));
        }

        public virtual async Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            var entityEntry = await Context.GetDbSet<TEntity>().AddAsync(entity, cancellationToken);
            return entityEntry.Entity;
        }


        public virtual async Task AddRangeAsync(IEnumerable<TEntity> entities,
            CancellationToken cancellationToken = default) =>
            await Context.GetDbSet<TEntity>().AddRangeAsync(entities, cancellationToken);

        public virtual async Task UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            await Task.Run(() => Context.GetDbSet<TEntity>().Update(entity));
            await Task.CompletedTask;
        }

        public virtual async Task UpdateRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
        {
            await Task.Run(() => Context.GetDbSet<TEntity>().UpdateRange(entities));
            await Task.CompletedTask;
        }

        public virtual async Task RemoveAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            await Task.Run(() => Context.GetDbSet<TEntity>().Remove(entity));
            await Task.CompletedTask;
        }

        public virtual async Task RemoveRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
        {
            await Task.Run(() => Context.GetDbSet<TEntity>().RemoveRange(entities));
            await Task.CompletedTask;
        }

        protected IQueryable<TEntity> Get() => Context.GetDbSet<TEntity>();
    }
}
