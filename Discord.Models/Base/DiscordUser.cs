namespace Discord.Models.Base
{
    public record DiscordUser
    {
        public required string Id { get; init; }
        public required string Username { get; init; }
    }
}
