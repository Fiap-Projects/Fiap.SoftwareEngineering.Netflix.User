using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Fiap.SoftwareEngineering.Netflix.Repository.Abstractions
{
    public interface IRepositoryReader<TEntity> where TEntity : class
    {
        Task<TEntity> GetAsync(int key);
        Task<TEntity> GetAsync(params object[] keys);
        Task<IEnumerable<TEntity>> GetAsync(CancellationToken cancellationToken = default);        
        Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);
    }
}
