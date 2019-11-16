using Fiap.SoftwareEngineering.Netflix.Repository.Abstractions;
using System;

namespace Fiap.SoftwareEngineering.Netflix.Repository
{
    public class Repository<TEntity> : IRepository<TEntity>
        where TEntity : class
    {
        public IRepositoryReader<TEntity> Reader { get; }
        public IRepositoryReader<TEntity> Writer { get; }

        public Repository(IRepositoryReader<TEntity> reader, IRepositoryReader<TEntity> writer)
        {
            Reader = reader ?? throw new ArgumentException(nameof(reader));
            Writer = writer ?? throw new ArgumentException(nameof(writer));
        }
    }
}
