namespace Discord.Models.Base
{
    public record DiscordChannel
    {
        public required string Id { get; init; }
        public required string Name { get; init; }
    }
}
