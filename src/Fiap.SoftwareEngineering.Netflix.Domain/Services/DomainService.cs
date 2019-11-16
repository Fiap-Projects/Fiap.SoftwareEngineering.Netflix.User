using System;
using Fiap.SoftwareEngineering.Netflix.Domain.Abstractions;
using Fiap.SoftwareEngineering.Netflix.Domain.Abstractions.Services;

namespace Fiap.SoftwareEngineering.Netflix.Domain.Services
{
    public class DomainService<TEntity>
        where TEntity : class
    {
        protected IDomainReaderService<TEntity> Reader { get; }
        protected IDomainWriterService<TEntity> Writer { get; }

        public DomainService(IDomainReaderService<TEntity> reader, IDomainWriterService<TEntity> writer)
        {
            Reader = reader ?? throw new ArgumentNullException(nameof(reader));
            Writer = writer ?? throw new ArgumentNullException(nameof(writer));
        }
    }
}
