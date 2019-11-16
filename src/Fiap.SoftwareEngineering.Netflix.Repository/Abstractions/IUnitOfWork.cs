using System;
using System.Threading;
using System.Threading.Tasks;

namespace Fiap.SoftwareEngineering.Netflix.Repository.Abstractions
{
    public interface IUnitOfWork : IDisposable
    {
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
        Task ExecuteInTransactionAsync(Func<CancellationToken, Task> operation,
            CancellationToken cancellationToken = default);
    }
}
