using Identities.Models;

namespace Common.DataAccess.SharedEntities.Users
{
    public class IdentityEntity : IdentityBase
    {
        public required Guid Id { get; set; }
        
        public Guid PersonaId { get; set; }
        public PersonaEntity? Persona { get; set; }
    }
}
