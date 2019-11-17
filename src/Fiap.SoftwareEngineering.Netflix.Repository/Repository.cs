using Fiap.SoftwareEngineering.Netflix.Repository.Abstractions;
using Fiap.SoftwareEngineering.Netflix.Repository.Abstractions.Entities;
using System;

namespace Fiap.SoftwareEngineering.Netflix.Repository
{
    public class Repository<TEntity> : Repository<TEntity, int>, IRepository<TEntity>
        where TEntity : class, IEntity<int>
    {
        public Repository(IRepositoryReader<TEntity> reader, IRepositoryWriter<TEntity> writer)
            : base(reader, writer)
        {
        }
    }

    public class Repository<TEntity, TKey> : IRepository<TEntity, TKey>
        where TEntity : class, IEntity<TKey>
        where TKey : IEquatable<TKey>
    {
        public IRepositoryReader<TEntity, TKey> Reader { get; }
        public IRepositoryWriter<TEntity> Writer { get; }

        public Repository(IRepositoryReader<TEntity, TKey> reader, IRepositoryWriter<TEntity> writer)
        {
            Reader = reader ?? throw new ArgumentException(nameof(reader));
            Writer = writer ?? throw new ArgumentException(nameof(writer));
        }
    }
}
