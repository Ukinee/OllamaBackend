using Microsoft.AspNetCore.Identity;

namespace Common.DataAccess.SharedEntities.Users
{
    public class UserEntity : IdentityUser<Guid>
    {
        public required  DateTime CreatedAt { get; set; }
        public required string Role { get; set; } //Identity role
        
        public required ICollection<PersonaEntity> Personas { get; set; }
    }
}
