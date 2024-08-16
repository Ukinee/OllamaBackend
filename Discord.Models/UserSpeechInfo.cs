using Discord.Models.Base;

namespace Discord.Models
{
    public record UserSpeechInfo
    {
        public required DiscordUser Speaker { get; init; }
        
        public required DateTime StartTime { get; init; }
        public required DateTime EndTime { get; init; }
        
        public required string Content { get; init; }
    }
}
