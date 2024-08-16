using Chat.DataAccess.Interfaces;

namespace Chat.CQRS.Commands
{
    public class DeleteConversationCommand
    {
        private readonly IConversationRepository _conversationRepository;

        public DeleteConversationCommand(IConversationRepository conversationRepository)
        {
            _conversationRepository = conversationRepository;
        }

        public async Task Execute(Guid id)
        {
            await _conversationRepository.Delete(id);
        }
    }
}
