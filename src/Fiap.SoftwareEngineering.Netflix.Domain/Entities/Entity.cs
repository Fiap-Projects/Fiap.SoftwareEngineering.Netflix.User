using Fiap.SoftwareEngineering.Netflix.Domain.Abstractions.Entities;
using System;
using Fiap.SoftwareEngineering.Netflix.Repository.Abstractions.Entities;

namespace Fiap.SoftwareEngineering.Netflix.Domain.Entities
{
    public class Entity : Entity<int>, IEntity<int>
    {
    }

    public class Entity<TKey> : IEntity<TKey>
        where TKey : IEquatable<TKey>
    {
        public TKey Key { get; }
    }
}
