using Authorization.Services.Interfaces;
using Chat.Domain.Conversations;
using Chat.Services.Interfaces;
using Common.DataAccess;
using Common.DataAccess.SharedEntities;
using Common.DataAccess.SharedEntities.Chats;
using Microsoft.AspNetCore.Mvc;

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
