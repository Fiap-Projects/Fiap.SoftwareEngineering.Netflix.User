using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Fiap.SoftwareEngineering.Netflix.Repository.EntityFramework.Abstractions
{
    public interface IContext
    {
        DbSet<TEntity> GetDbSet<TEntity>() where TEntity : class;
        IQueryable<TEntity> GetDbSetAsQueryable<TEntity>() where TEntity : class;
    }
}
