using System.Diagnostics;
using Chat.Common;

namespace Chat.Domain.Messages
{
    [DebuggerDisplay("[{Timestamp.ToShortDateString()}] {Role} as {ChatName}: {Content}")]
    public readonly record struct Message
    (
        ChatName ChatName,
        ChatRole Role,
        string Content,
        DateTime Timestamp,
        Guid Id,
        string[]? Images = null
    );
}
