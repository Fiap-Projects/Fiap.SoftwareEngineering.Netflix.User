using Fiap.SoftwareEngineering.Netflix.Repository.EntityFramework.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace Fiap.SoftwareEngineering.Netflix.Repository.EntityFramework.Contexts
{
    public class EntityFrameworkContextReader : EntityFrameworkContext, IContextReader
    {
        protected EntityFrameworkContextReader(DbContextOptions<EntityFrameworkContextReader> options) : base(options)
        {
        }
    }
}
