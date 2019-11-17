using System;
using Fiap.SoftwareEngineering.Netflix.Repository.Abstractions.Entities;

namespace Fiap.SoftwareEngineering.Netflix.Repository.Abstractions
{
    public interface IRepository<TEntity> : IRepository<TEntity, int>
        where TEntity : class, IEntity<int>
    { }

    public interface IRepository<TEntity, in TKey>
        where TEntity : class, IEntity<TKey>
        where TKey : IEquatable<TKey>
    {
        IRepositoryReader<TEntity, TKey> Reader { get; }
        IRepositoryWriter<TEntity> Writer { get; }
    }
}