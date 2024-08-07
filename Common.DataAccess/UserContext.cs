﻿using Common.DataAccess.SharedEntities.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Common.DataAccess
{
    public class UserContext : IdentityDbContext<UserEntity, IdentityRole<Guid>, Guid>
    {
        public UserContext(DbContextOptions<UserContext> options) : base(options)
        {
        }

        public DbSet<PersonaEntity> Personas { get; set; }
        public DbSet<IdentityEntity> Identities { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            BuildRoles(modelBuilder);
            
            modelBuilder
                .Entity<PersonaEntity>()
                .HasOne(persona => persona.Identity)
                .WithOne(identity => identity.Persona)
                .HasForeignKey<PersonaEntity>(persona => persona.IdentityId)
                .OnDelete(DeleteBehavior.Cascade); ;

            modelBuilder
                .Entity<PersonaEntity>()
                .HasOne(persona => persona.User)
                .WithMany(user => user.Personas)
                .HasForeignKey(persona => persona.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }

        private static void BuildRoles(ModelBuilder modelBuilder)
        {
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

            modelBuilder.Entity<IdentityRole<Guid>>().HasData(roles);
        }
    }
}