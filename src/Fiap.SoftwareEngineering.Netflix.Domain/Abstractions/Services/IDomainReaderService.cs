using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Fiap.SoftwareEngineering.Netflix.Domain.Abstractions.Services
{
    public interface IDomainReaderService<TEntity> where TEntity : class
    {
        Task<TEntity> GetAsync(int key);
        Task<TEntity> GetAsync(params object[] keys);
        Task<IEnumerable<TEntity>> GetAsync(CancellationToken cancellationToken = default);        
        Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);
    }
}
