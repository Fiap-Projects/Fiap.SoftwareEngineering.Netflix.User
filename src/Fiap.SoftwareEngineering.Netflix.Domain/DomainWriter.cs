using Fiap.SoftwareEngineering.Netflix.Domain.Abstractions;
using Fiap.SoftwareEngineering.Netflix.Repository.Abstractions;
using Fiap.SoftwareEngineering.Netflix.Validation;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Fiap.SoftwareEngineering.Netflix.Domain
{
    public class DomainWriter<TEntity> : IDomainWriter<TEntity>
        where TEntity : class
    {
        protected IRepositoryWriter<TEntity> Repository;
        protected IDomainValidator<TEntity> Validator;

        public DomainWriter(IRepositoryWriter<TEntity> repository, IDomainValidator<TEntity> validator)
        {
            Repository = repository ?? throw new ArgumentNullException(nameof(repository));
            Validator = validator ?? throw new ArgumentNullException(nameof(validator));
        }

        public async Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            var isValid = await Validator.ValidateDomainAsync(entity, cancellationToken);
            if (!isValid)
                return null;

            var entityInserted = await Repository.AddAsync(entity, cancellationToken);
            var operationResult = await Repository.UnitOfWork.SaveChangesAsync(cancellationToken);
            if (operationResult > 0) return entityInserted;
            
            var notification = new Notification("InvalidDatabaseOperation", "Invalid Database Operation.");
            Validator.NotificationContext.AddNotification(notification);
            return entityInserted;
        }

        public async Task AddRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
        {
            if (entities == null)
                throw new ArgumentNullException(nameof(entities));

            await Repository.UnitOfWork.ExecuteInTransactionAsync(async transaction =>
            {
                foreach (var entity in entities)
                    await AddAsync(entity, cancellationToken);
            }, cancellationToken);
        }

        public async Task UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            var isValid = await Validator.ValidateDomainAsync(entity, cancellationToken);
            if (!isValid)
                return;

            await Repository.UpdateAsync(entity, cancellationToken);
            var operationResult = await Repository.UnitOfWork.SaveChangesAsync(cancellationToken);
            if (operationResult > 0) return;
            
            var notification = new Notification("InvalidDatabaseOperation", "Invalid Database Operation.");
            Validator.NotificationContext.AddNotification(notification);
        }

        public async Task UpdateRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
        {
            if (entities == null)
                throw new ArgumentNullException(nameof(entities));

            await Repository.UnitOfWork.ExecuteInTransactionAsync(async transaction =>
            {
                foreach (var entity in entities)
                    await UpdateAsync(entity, cancellationToken);
            }, cancellationToken);
        }

        public async Task RemoveAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            await Repository.RemoveAsync(entity, cancellationToken);
            var operationResult = await Repository.UnitOfWork.SaveChangesAsync(cancellationToken);
            if (operationResult > 0) return;
            
            var notification = new Notification("InvalidDatabaseOperation", "Invalid Database Operation.");
            Validator.NotificationContext.AddNotification(notification);
        }

        public async Task RemoveRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
        {
            if (entities == null)
                throw new ArgumentNullException(nameof(entities));

            await Repository.UnitOfWork.ExecuteInTransactionAsync(async transaction =>
            {
                foreach (var entity in entities)
                    await RemoveAsync(entity, cancellationToken);
            }, cancellationToken);
        }
    }
}
