using Common.DataAccess.SharedEntities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Persona.Models;
using Persona.Models.Personas;

namespace Common.DataAccess
{
    public class UserDbContext : IdentityDbContext<UserEntity, IdentityRole<Guid>, Guid>
    {
        public UserDbContext(DbContextOptions<UserDbContext> options) : base(options)
        {
        }
        
        public DbSet<ConversationEntity> Conversations { get; init; }
        public DbSet<MessageEntity> Messages { get; init; }
        
        public DbSet<PersonaEntity> Personas { get; init; }
        public DbSet<PersonaLinkEntity> PersonaLinks { get; init; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            
            List<IdentityRole<Guid>> roles =
            [
                new IdentityRole<Guid>
                {
                    Id = Guid.NewGuid(),
                    Name = "Admin", //todo : hardcode
                    NormalizedName = "ADMIN", //todo : hardcode
                },

                new IdentityRole<Guid>
                {
                    Id = Guid.NewGuid(),
                    Name = "User", //todo : hardcode
                    NormalizedName = "USER", //todo : hardcode
                },
            ];

            builder.Entity<IdentityRole<Guid>>().HasData(roles);
        }
    }
}
