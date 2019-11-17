using System.Linq;
using Fiap.SoftwareEngineering.Netflix.Repository.EntityFramework.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace Fiap.SoftwareEngineering.Netflix.Repository.EntityFramework.Contexts
{
    public class EntityFrameworkContext : DbContext, IContext
    {
        protected EntityFrameworkContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<TEntity> GetDbSet<TEntity>() where TEntity : class => 
            Set<TEntity>();

        public IQueryable<TEntity> GetDbSetAsQueryable<TEntity>() where TEntity : class => 
            GetDbSet<TEntity>();
    }
}
