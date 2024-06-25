using Chat.Services.Interfaces;
using ChatUserLink.Services.Interfaces;
using Common.DataAccess.SharedEntities.Links;
using Common.DataAccess.SharedEntities.Objects;

namespace ChatUserLink.CQRS
{
    public class GetMessagesQuery
    {
        private readonly IMessageRepository _messageRepository;
        private readonly IUserAssociationRepository _userAssociationRepository;

        public GetMessagesQuery(IMessageRepository messageRepository, IUserAssociationRepository userAssociationRepository)
        {
            _messageRepository = messageRepository;
            _userAssociationRepository = userAssociationRepository;
        }

        public async Task<IList<MessageEntity>> Execute(Guid id)
        {
            IList<ConversationMessageEntity> messagesLinks = await _userAssociationRepository.GetMessagesByConversationId(id);
            Guid[] messageIds = messagesLinks.Select(x => x.MessageId).ToArray();

            return await _messageRepository.FindMessagesByConversationAsync(messageIds);
        }
    }
}
