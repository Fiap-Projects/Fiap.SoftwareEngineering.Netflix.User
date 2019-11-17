using Fiap.SoftwareEngineering.Netflix.Domain.Entities;
using Fiap.SoftwareEngineering.Netflix.Repository.Abstractions.Entities;

namespace Fiap.SoftwareEngineering.Netflix.User.Domain.Entities
{
    public class User : ValidableEntity<User>, IEntity
    {
        public int Key { get; }
    }
}
