using Domain.Dto.DataBaseDtos;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Implementation
{
    public class ChatDbContext(DbContextOptions<ChatDbContext> options) : DbContext(options)
    {
        public DbSet<ConversationEntity> Conversations { get; init; }
        public DbSet<MessageEntity> Messages { get; init; }
    }
}
