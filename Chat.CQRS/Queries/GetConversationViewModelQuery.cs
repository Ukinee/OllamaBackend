using Chat.DataAccess.Interfaces;
using Chat.Domain.Conversations;
using Core.Common.DataAccess;
using Core.Common.DataAccess.SharedEntities.Chats;
using Core.Common.DataAccess.SharedEntities.Chats.Mappers;

namespace Chat.CQRS.Queries
{
    public class GetConversationViewModelQuery
    {
        private readonly IConversationRepository _conversationRepository;

        public GetConversationViewModelQuery(IConversationRepository conversationRepository)
        {
            _conversationRepository = conversationRepository;
        }
        public async Task<ConcreteConversationViewModel> Execute(Guid conversationId, int page, int pageSize)
        {
            ConversationEntity? conversation = await _conversationRepository.GetConcreteConversation(conversationId);

            if (conversation == null)
                throw new NotFoundException(nameof(conversation));

            IList<MessageEntity> messages = conversation
                .Messages
                .OrderByDescending(x => x.Timestamp)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();
            
            return conversation.ToConcreteConversation(messages);
        }
    }
}
