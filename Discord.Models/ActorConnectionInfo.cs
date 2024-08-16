using Discord.Models.Base;

namespace Discord.Models
{
    public record ActorConnectionInfo
    {
        public required DateTime ConnectionTime { get; init; }
        
        public required DiscordServer Server { get; init; }
        public required DiscordChannel Channel { get; init; }
        public required DiscordUser Actor { get; init; }

        public required DiscordUser[] ConnectedUsers { get; init; }
    }
}
