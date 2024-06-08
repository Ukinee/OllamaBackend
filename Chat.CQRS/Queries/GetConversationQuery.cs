using Chat.Domain.Conversations;
using Chat.Services.Interfaces;
using Common.DataAccess;

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
            ConversationEntity? conversation = await _conversationRepository.FindConversationById(id);

            if (conversation == null)
                throw new NotFoundException(nameof(conversation));

            return conversation;
        }
    }
}
