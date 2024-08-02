using Common.DataAccess.SharedEntities.Users;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Common.DataAccess
{
    public class UserContext : IdentityUserContext<UserEntity, Guid>
    {
        public UserContext(DbContextOptions<UserContext> options) : base(options)
        {
        }
        
        public DbSet<PersonaEntity> Personas { get; set; }
        public DbSet<IdentityEntity> Identities { get; set; }
    }
}
