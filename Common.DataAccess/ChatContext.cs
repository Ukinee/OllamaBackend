using Common.DataAccess.SharedEntities.Chats;
using Microsoft.EntityFrameworkCore;

namespace Common.DataAccess
{
    public class ChatContext : DbContext
    {
        public ChatContext(DbContextOptions<ChatContext> options) : base(options)
        {
        }
        
        public DbSet<MessageEntity> Messages { get; set; }
        public DbSet<ConversationEntity> Conversations { get; set; }
    }
}
