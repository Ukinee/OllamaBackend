using Chat.Domain.Conversations;
using Chat.Domain.Messages;
using Common.DataAccess.SharedEntities;
using Microsoft.EntityFrameworkCore;

namespace Chat.DataAccess
{
    public class ChatDbContext(DbContextOptions<ChatDbContext> options) : DbContext(options)
    {
        public DbSet<ConversationEntity> Conversations { get; init; }
        public DbSet<MessageEntity> Messages { get; init; }
    }
}
