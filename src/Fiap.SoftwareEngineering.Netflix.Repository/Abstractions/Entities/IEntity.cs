using System;

namespace Fiap.SoftwareEngineering.Netflix.Repository.Abstractions.Entities
{
    public interface IEntity : IEntity<int>
    { }

    public interface IEntity<out TKey> where TKey : IEquatable<TKey>
    {
        TKey Key { get; }
    }
}
