using Fiap.SoftwareEngineering.Netflix.Domain.Abstractions.Services;
using System;
using Fiap.SoftwareEngineering.Netflix.Repository.Abstractions.Entities;

namespace Fiap.SoftwareEngineering.Netflix.Domain.Services
{
    public class DomainService<TEntity> : DomainService<TEntity, int>
        where TEntity : class, IEntity<int>
    {
        public DomainService(IDomainReaderService<TEntity> reader, IDomainWriterService<TEntity> writer) : base(reader, writer)
        {
        }
    }

    public class DomainService<TEntity, TKey> : IDomainService<TEntity, TKey>
        where TEntity : class, IEntity<TKey>
        where TKey : IEquatable<TKey>
    {
        public IDomainReaderService<TEntity, TKey> Reader { get; }
        public IDomainWriterService<TEntity, TKey> Writer { get; }

        public DomainService(IDomainReaderService<TEntity, TKey> reader, IDomainWriterService<TEntity, TKey> writer)
        {
            Reader = reader ?? throw new ArgumentNullException(nameof(reader));
            Writer = writer ?? throw new ArgumentNullException(nameof(writer));
        }
    }
}
