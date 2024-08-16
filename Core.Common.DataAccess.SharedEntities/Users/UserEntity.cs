using Microsoft.AspNetCore.Identity;

namespace Core.Common.DataAccess.SharedEntities.Users
{
    public class UserEntity : IdentityUser<Guid>
    {
        public required  DateTime CreatedAt { get; set; }
        public required string Role { get; set; } //Identity role
        
        public string? DiscordId { get; set; }
        public string? TelegramId { get; set; }
        
        public required ICollection<PersonaEntity> Personas { get; set; } //Как минимум 1, задается при создании
    }
}
