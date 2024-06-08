using Domain.Models.Messages;

namespace Domain.Models.Conversations
{
    public record Conversation
    {
        private readonly List<Message> _messages;
        private string _globalContext = string.Empty;

        public Conversation(List<Message> messages, string globalContext, Guid id)
        {
            _messages = messages;
            GlobalContext = globalContext;
            Id = id;
        }

        public Conversation(string globalContext, Guid id) : this([], globalContext, id)
        {
        }

        public Conversation(List<Message> messages, Guid id) : this(messages, string.Empty, id)
        {
        }

        public Conversation(Guid id) : this([], string.Empty, id)
        {
        }

        public IReadOnlyList<Message> Messages => _messages.AsReadOnly();

        public string GlobalContext
        {
            get => _globalContext;
            set => _globalContext = value ?? string.Empty;
        }

        public Guid Id { get; }

        public void AddMessage(Message message) =>
            _messages.Add(message);

        public void RemoveMessage(Guid messageId)
        {
            Message message = _messages.First(m => m.Id == messageId);
            _messages.Remove(message);
        }

        public void Reorder(IList<Guid> messageIds)
        {
            if (messageIds.Count != _messages.Count)
                throw new ArgumentException("Message Id list must have same length as message list");

            IEnumerable<Message> newMessages = messageIds.Select(id => _messages.First(m => m.Id == id));

            _messages.Clear();
            _messages.AddRange(newMessages);
        }
    }
}
