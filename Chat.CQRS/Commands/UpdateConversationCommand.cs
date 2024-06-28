using Chat.Domain.Conversations;
using Chat.Services.Interfaces;

namespace Chat.CQRS.Commands
{
    public class UpdateConversationCommand
    {
        private readonly IConversationRepository _conversationRepository;

        public UpdateConversationCommand(IConversationRepository conversationRepository)
        {
            _conversationRepository = conversationRepository;
        }
        
        public async Task Execute(PutConversationRequest request)
        {
            await _conversationRepository.Update(request);
        }
    }
}
