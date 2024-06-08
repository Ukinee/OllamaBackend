using System.Diagnostics;
using Common;

namespace Domain.Models.Messages
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
