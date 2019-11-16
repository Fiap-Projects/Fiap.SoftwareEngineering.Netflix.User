using Fiap.SoftwareEngineering.Netflix.Repository.EntityFramework.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace Fiap.SoftwareEngineering.Netflix.Repository.EntityFramework.Contexts
{
    public abstract class EntityFrameworkContextReader : EntityFrameworkContext, IContextReader
    {
        protected EntityFrameworkContextReader(DbContextOptions options) : base(options)
        {
        }
    }
}
