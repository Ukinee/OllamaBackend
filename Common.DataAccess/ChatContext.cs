using Common.DataAccess.SharedEntities.Chats;
using Common.DataAccess.SharedEntities.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Common.DataAccess
{
    public class ChatContext : DbContext
    {
        public ChatContext(IConfiguration configuration, DbContextOptions<ChatContext> options) : base(options)
        {
        }

        public DbSet<MessageEntity> Messages { get; set; }
        public DbSet<ConversationEntity> Conversations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            modelBuilder
                .Entity<PersonaEntity>()
                .HasMany(persona => persona.Conversations)
                .WithMany(conversation => conversation.Personas)
                .UsingEntity(builder => builder.ToTable("PersonaConversations"));
        }
    }
}
