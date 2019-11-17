using Fiap.SoftwareEngineering.Netflix.Domain.Abstractions.Services;
using Fiap.SoftwareEngineering.Netflix.Domain.Abstractions.Validations;
using Fiap.SoftwareEngineering.Netflix.Domain.Validations;
using Fiap.SoftwareEngineering.Netflix.Repository.Abstractions;
using Fiap.SoftwareEngineering.Netflix.Validation;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Fiap.SoftwareEngineering.Netflix.Domain.Abstractions.Entities;
using Fiap.SoftwareEngineering.Netflix.Repository.Abstractions.Entities;

namespace Fiap.SoftwareEngineering.Netflix.Domain.Services
{
    public class DomainWriterService<TEntity> : DomainWriterService<TEntity, int>, IDomainWriterService<TEntity>
        where TEntity : class, IEntity<int>
    {
        public DomainWriterService(IRepositoryWriter<TEntity> repository, IDomainValidator<TEntity> validator, IDomainReaderService<TEntity> domainReaderService)
            : base(repository, validator, domainReaderService)
        {
        }
    }

    public class DomainWriterService<TEntity, TKey> : IDomainWriterService<TEntity, TKey>
        where TEntity : class, IEntity<TKey>
        where TKey : IEquatable<TKey>
    {
        protected readonly IRepositoryWriter<TEntity> Repository;
        protected readonly IDomainValidator<TEntity> Validator;
        protected readonly IDomainReaderService<TEntity, TKey> DomainReaderService;

        public DomainWriterService(IRepositoryWriter<TEntity> repository, IDomainValidator<TEntity> validator, IDomainReaderService<TEntity, TKey> domainReaderService)
        {
            Repository = repository ?? throw new ArgumentNullException(nameof(repository));
            Validator = validator ?? throw new ArgumentNullException(nameof(validator));
            DomainReaderService = domainReaderService ?? throw new ArgumentNullException(nameof(domainReaderService));
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
            
            AddNotiticationMessage(nameof(StaticMessages.InvalidDatabaseOperation),
                StaticMessages.InvalidDatabaseOperation);
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

            if (!await Exists(entity.Key, cancellationToken))
                return;

            await Repository.UpdateAsync(entity, cancellationToken);
            var operationResult = await Repository.UnitOfWork.SaveChangesAsync(cancellationToken);
            if (operationResult > 0) return;
            
            AddNotiticationMessage(nameof(StaticMessages.InvalidDatabaseOperation),
                StaticMessages.InvalidDatabaseOperation);
        }

        public async Task UpdateRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
        {
            if (entities == null)
                throw new ArgumentNullException(nameof(entities));

            if (!await IsValid(entities, cancellationToken))
                return;

            await Repository.UpdateRangeAsync(entities, cancellationToken);
            var operationResult = await Repository.UnitOfWork.SaveChangesAsync(cancellationToken);
            if (operationResult > 0) return;
            
            AddNotiticationMessage(nameof(StaticMessages.InvalidDatabaseOperation),
                StaticMessages.InvalidDatabaseOperation);
        }

        public async Task RemoveAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            if (!await Exists(entity.Key, cancellationToken))
                return;

            await Repository.RemoveAsync(entity, cancellationToken);
            var operationResult = await Repository.UnitOfWork.SaveChangesAsync(cancellationToken);
            if (operationResult > 0) return;
            
            AddNotiticationMessage(nameof(StaticMessages.InvalidDatabaseOperation),
                StaticMessages.InvalidDatabaseOperation);
        }

        public async Task RemoveRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
        {
            if (entities == null)
                throw new ArgumentNullException(nameof(entities));

            if (!await IsValid(entities, cancellationToken))
                return;

            await Repository.RemoveRangeAsync(entities, cancellationToken);
            var operationResult = await Repository.UnitOfWork.SaveChangesAsync(cancellationToken);
            if (operationResult > 0) return;
            
            AddNotiticationMessage(nameof(StaticMessages.InvalidDatabaseOperation),
                StaticMessages.InvalidDatabaseOperation);
        }

        private async Task<bool> IsValid(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
        {
            var isValid = false;

            foreach (var entity in entities)
                isValid = await Validator.ValidateDomainAsync(entity, cancellationToken);

            return isValid;
        }

        private void AddNotiticationMessage(string key, string message)
        {
            var notification = new Notification(key, message);
            Validator.NotificationContext.AddNotification(notification);
        }

        private async Task<bool> Exists(TKey key, CancellationToken cancellationToken = default)
        {
            var exists = await DomainReaderService.Exists(key, cancellationToken);
            if (!exists)
                AddNotiticationMessage(nameof(StaticMessages.EntityNotFound), StaticMessages.EntityNotFound);

            return exists;
        }
    }
}
