using Common.DataAccess.SharedEntities;
using Common.DataAccess.SharedEntities.Links;
using Common.DataAccess.SharedEntities.Objects;
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
        
        public DbSet<UserConversationEntity> UserConversations { get; init; }
        public DbSet<ConversationMessageEntity> ConversationMessages { get; init; }
        public DbSet<UserMessageEntity> UserMessages { get; init; }
    }
}
