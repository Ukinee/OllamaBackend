using Chat.Services.Interfaces;
using ChatUserLink.Services.Interfaces;
using Common.DataAccess;
using Common.DataAccess.SharedEntities.Objects;

namespace Chat.CQRS.Queries
{
    public class GetMessageQuery
    {
        private readonly IUserAssociationRepository _userAssociationRepository;
        private readonly IMessageRepository _messageRepository;

        public GetMessageQuery
        (
            IUserAssociationRepository userAssociationRepository,
            IMessageRepository messageRepository
        )
        {
            _userAssociationRepository = userAssociationRepository;
            _messageRepository = messageRepository;
        }

        public async Task<MessageEntity> Execute(Guid messageId, Guid userId)
        {
            MessageEntity? message = await _messageRepository.Get(messageId);

            if (message == null)
                throw new NotFoundException("Message not found");

            return message;
        }
    }
}
