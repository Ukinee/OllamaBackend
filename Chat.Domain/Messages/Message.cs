using System.Diagnostics;
using Chat.Common;

namespace Chat.Domain.Messages
{
    [DebuggerDisplay("[{Timestamp.ToShortDateString()}] {Role} as {ChatName}: {Content}")]
    public readonly record struct Message
    {
        public Message(ChatName ChatName,
            ChatRole Role,
            string Content,
            DateTime Timestamp,
            Guid Id,
            string[]? Images = null)
        {
            this.ChatName = ChatName;
            this.Role = Role;
            this.Content = Content;
            this.Timestamp = Timestamp;
            this.Id = Id;
            this.Images = Images;
        }

        public ChatName ChatName { get; init; }
        public ChatRole Role { get; init; }
        public string Content { get; init; }
        public DateTime Timestamp { get; init; }
        public Guid Id { get; init; }
        public string[]? Images { get; init; }

        public void Deconstruct(
            out ChatName ChatName,
            out ChatRole Role,
            out string Content,
            out DateTime Timestamp,
            out Guid Id,
            out string[]? Images
        )
        {
            ChatName = this.ChatName;
            Role = this.Role;
            Content = this.Content;
            Timestamp = this.Timestamp;
            Id = this.Id;
            Images = this.Images;
        }
    }
}
