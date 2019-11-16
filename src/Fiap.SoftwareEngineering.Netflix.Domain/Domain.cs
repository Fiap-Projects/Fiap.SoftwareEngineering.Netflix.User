using Fiap.SoftwareEngineering.Netflix.Domain.Abstractions;
using System;

namespace Fiap.SoftwareEngineering.Netflix.Domain
{
    public class Domain<TEntity>
        where TEntity : class
    {
        protected IDomainReader<TEntity> Reader { get; }
        protected IDomainWriter<TEntity> Writer { get; }

        public Domain(IDomainReader<TEntity> reader, IDomainWriter<TEntity> writer)
        {
            Reader = reader ?? throw new ArgumentNullException(nameof(reader));
            Writer = writer ?? throw new ArgumentNullException(nameof(writer));
        }
    }
}
