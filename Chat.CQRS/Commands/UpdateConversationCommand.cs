using Chat.DataAccess.Interfaces;
using Chat.Domain.Conversations;
using Core.Common.DataAccess.SharedEntities.Chats;

namespace Chat.CQRS.Commands
{
    public class UpdateConversationCommand
    {
        private readonly IConversationRepository _conversationRepository;

        public UpdateConversationCommand(IConversationRepository conversationRepository)
        {
            _conversationRepository = conversationRepository;
        }

        public async Task Execute
        (
            ConversationEntity conversationEntity,
            PutConversationRequest request
        )
        {
            conversationEntity.Name = request.Name;
            conversationEntity.Information = request.Information;
            conversationEntity.Context = request.Context;
            await _conversationRepository.Save();
        }
    }
}
