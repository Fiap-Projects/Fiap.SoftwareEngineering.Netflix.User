using Fiap.SoftwareEngineering.Netflix.Repository.Abstractions;

namespace Fiap.SoftwareEngineering.Netflix.Repository.EntityFramework.Abstractions
{
    public interface IContextWriter : IContext, IUnitOfWork
    {
    }
}
