using Fiap.SoftwareEngineering.Netflix.Domain.Entities;

namespace Fiap.SoftwareEngineering.Netflix.User.Domain.Entities
{
    public class User : ValidableEntity<User>
    {
        public int IdUser { get; set; }
    }
}
