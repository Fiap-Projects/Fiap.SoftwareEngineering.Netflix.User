using Fiap.SoftwareEngineering.Netflix.Repository.EntityFramework.Abstractions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Fiap.SoftwareEngineering.Netflix.Repository.EntityFramework.Contexts
{
    public abstract class EntityFrameworkContextReaderWriter: EntityFrameworkContext, IContextWriter
    {
        protected EntityFrameworkContextReaderWriter(DbContextOptions options) : base(options)
        {
        }

        public async Task ExecuteInTransactionAsync(Func<CancellationToken, Task> operation, CancellationToken cancellationToken = default)
        {
            var strategy = Database.CreateExecutionStrategy();
            await strategy.ExecuteAsync(async () =>
            {
                using (var transaction = await Database.BeginTransactionAsync(cancellationToken))
                {
                    await operation(cancellationToken);
                    transaction.Commit();
                }
            });
        }
    }
}
