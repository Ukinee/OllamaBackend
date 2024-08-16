using Discord.Models.Base;

namespace Discord.Models
{
    /// <summary>
    /// Agent - Any discord user
    /// Channel - Any discord channel
    /// Server - Any discord server
    /// </summary>
    public class AgentConnectionInfo
    {
        public required DiscordUser Agent { get; init; }
        public required DiscordChannel Channel { get; init; }
        public required DiscordServer Server { get; init; }
    }
}
