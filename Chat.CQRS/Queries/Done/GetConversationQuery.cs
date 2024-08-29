using Chat.DataAccess.Interfaces;
using Core.Common.DataAccess;
using Core.Common.DataAccess.SharedEntities.Chats;

namespace Chat.CQRS.Queries.Done
{
    public class GetConversationQuery
    {
        private readonly IConversationRepository _conversationRepository;

        public GetConversationQuery(IConversationRepository conversationRepository)
        {
            _conversationRepository = conversationRepository;
        }

        public async Task<ConversationEntity> Execute(Guid guid, CancellationToken cancellationToken)
        {
            ConversationEntity? conversation = await _conversationRepository
                .Find(x => x.Id == guid, cancellationToken);

            if (conversation == null)
                throw new NotFoundException(nameof(conversation));

            return conversation;
        }
    }
}
