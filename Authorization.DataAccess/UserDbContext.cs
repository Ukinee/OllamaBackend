using Authorization.Domain;
using Common.DataAccess.SharedEntities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Authorization.DataAccess
{
    public class UserDbContext : IdentityDbContext<UserEntity, IdentityRole<Guid>, Guid>
    {
        public UserDbContext(DbContextOptions<UserDbContext> options) : base(options)
        {
        }

        public DbSet<UserEntity> UsersEntities { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            List<IdentityRole<Guid>> roles = new List<IdentityRole<Guid>>
            {
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
            };

            builder.Entity<IdentityRole<Guid>>().HasData(roles);
        }
    }
}
