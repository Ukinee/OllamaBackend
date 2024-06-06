using Authorization.Domain;
using Microsoft.EntityFrameworkCore;

namespace Authorization.DataAccess
{
    public class UserDbContext(DbContextOptions<UserDbContext> options) : DbContext(options)
    {
        public DbSet<UserEntity> Users { get; set; }
    }
}
