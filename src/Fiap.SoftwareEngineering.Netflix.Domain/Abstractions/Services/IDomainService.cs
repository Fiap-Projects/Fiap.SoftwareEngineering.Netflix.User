using System;
using Fiap.SoftwareEngineering.Netflix.Repository.Abstractions.Entities;

namespace Fiap.SoftwareEngineering.Netflix.Domain.Abstractions.Services
{
    public interface IDomainService<TEntity> : IDomainService<TEntity, int>
        where TEntity : class, IEntity<int>
    { }

    public interface IDomainService<TEntity, in TKey>
        where TEntity : class, IEntity<TKey>
        where TKey : IEquatable<TKey>
    {
        IDomainReaderService<TEntity, TKey> Reader { get; }
        IDomainWriterService<TEntity, TKey> Writer { get; }
    }
}
