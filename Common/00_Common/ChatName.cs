using System.Text.Json.Serialization;

namespace Common
{
    public record struct ChatName
    {
        private readonly string _value;

        public ChatName(string role) =>
            _value = role ?? throw new ArgumentNullException(nameof(role));

        [JsonConstructor]
        public ChatName(object _) =>
            _value = null;

        public string GetContent(string content) =>
            $"{_value}: {content}";

        public static implicit operator ChatName(string value) => 
            new ChatName(value);

        public override string ToString() => 
            _value;
    }
}
