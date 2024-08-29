using System.ComponentModel;
using System.Text.Json.Serialization;

namespace Chat.Common
{
    public readonly record struct ChatRole
    {
        private const string SystemValue = "system";
        private const string AssistantValue = "assistant";
        private const string UserValue = "user";
        private readonly string _value;

        public ChatRole(string role)
        {
            _value = role ?? throw new ArgumentNullException(nameof(role));
        }

        [JsonConstructor]
        public ChatRole(object _)
        {
            _value = string.Empty;
        }

        /// <summary> The role that instructs or sets the behavior of the assistant. </summary>
        public static ChatRole System { get; } = new ChatRole(SystemValue);

        /// <summary> The role that provides responses to system-instructed, user-prompted input. </summary>
        public static ChatRole Assistant { get; } = new ChatRole(AssistantValue);

        /// <summary> The role that provides input for chat completions. </summary>
        public static ChatRole User { get; } = new ChatRole(UserValue);

        public static implicit operator ChatRole(string value)
        {
            return new ChatRole(value);
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public override int GetHashCode()
        {
            return _value?.GetHashCode() ?? 0;
        }

        public override string ToString()
        {
            return _value;
        }
    }
}
