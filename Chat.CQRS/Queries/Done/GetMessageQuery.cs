using Chat.DataAccess.Interfaces;
using Core.Common.DataAccess;
using Core.Common.DataAccess.SharedEntities.Chats;

namespace Chat.CQRS.Queries.Done
{
    [Obsolete("")]
    public class GetMessageQuery
    {
        private readonly IMessageRepository _messageRepository;

        public GetMessageQuery(IMessageRepository messageRepository)
        {
            _messageRepository = messageRepository;
        }

        public async Task<MessageEntity> Execute(Guid id)
        {
            MessageEntity? message = await _messageRepository.Get(id);

            if (message == null)
                throw new NotFoundException(nameof(message));

            return message;
        }
    }
}
