using Common.DataAccess.SharedEntities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Common.DataAccess
{
    public class UserDbContext : IdentityDbContext<UserEntity, IdentityRole<Guid>, Guid>
    {
        public UserDbContext(DbContextOptions<UserDbContext> options) : base(options)
        {
        }
        
        public DbSet<ConversationEntity> Conversations { get; init; }
        public DbSet<MessageEntity> Messages { get; init; }
        
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<ConversationEntity>()
                .HasMany(conversationEntity => conversationEntity.Messages)
                .WithOne(messageEntity => messageEntity.Conversation)
                .HasForeignKey(messageEntity => messageEntity.ConversationId);

            builder.Entity<ConversationEntity>()
                .HasOne(conversationEntity => conversationEntity.Owner)
                .WithMany(userEntity => userEntity.Conversations)
                .HasForeignKey(conversationEntity => conversationEntity.OwnerId);
            
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
