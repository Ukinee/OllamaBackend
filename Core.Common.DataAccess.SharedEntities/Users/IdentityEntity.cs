using Identities.Models;

namespace Core.Common.DataAccess.SharedEntities.Users
{
    public class IdentityEntity : IdentityBase
    {
        public required Guid Id { get; set; }
        
        public PersonaEntity? Persona { get; set; }
    }
}
