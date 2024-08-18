using Chat.DataAccess.Interfaces;
using Core.Common.DataAccess.SharedEntities.Chats;

namespace Chat.CQRS.Commands
{
    public class AddConversationCommand
    {
        private readonly IConversationRepository _conversationRepository;

        public AddConversationCommand(IConversationRepository conversationRepository)
        {
            _conversationRepository = conversationRepository;
        }

        public async Task Execute(ConversationEntity entity, CancellationToken token)
        {
            await _conversationRepository.Add(entity, token);
        }
    }
}
