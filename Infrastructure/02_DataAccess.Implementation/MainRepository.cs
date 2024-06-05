using Domain.Dto.DataBaseDtos;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Implementation
{
    public class MainRepository(DbContextOptions options) : DbContext(options)
    {
        public DbSet<DatabaseConversationDto> Conversations { get; init; }
        public DbSet<DatabaseMessageDto> Messages { get; init; }
    }
}
