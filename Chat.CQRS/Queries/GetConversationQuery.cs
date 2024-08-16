using Chat.DataAccess.Interfaces;
using Core.Common.DataAccess;
using Core.Common.DataAccess.SharedEntities.Chats;

namespace Chat.CQRS.Queries
{
    public class GetConversationQuery
    {
        private readonly IConversationRepository _conversationRepository;

        public GetConversationQuery(IConversationRepository conversationRepository)
        {
            _conversationRepository = conversationRepository;
        }

        public async Task<ConversationEntity> Execute(Guid id)
        {
            ConversationEntity? conversation = await _conversationRepository.GetConcreteConversation(id);

            if (conversation == null)
                throw new NotFoundException(nameof(conversation));

            return conversation;
        }
    }
}
