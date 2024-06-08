using Domain.Models.Conversations;
using Domain.Models.Messages;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Implementation
{
    public class ChatDbContext(DbContextOptions<ChatDbContext> options) : DbContext(options)
    {
        public DbSet<ConversationEntity> Conversations { get; init; }
        public DbSet<MessageEntity> Messages { get; init; }
    }
}
