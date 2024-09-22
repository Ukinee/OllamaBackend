using Chat.DataAccess.Interfaces;
using Core.Common.DataAccess.SharedEntities.Chats;
using Core.Common.DataAccess.SharedEntities.Users;
using Users.FakeUsers.Services.Interfaces;

namespace Common.UserChatLinks.CQRS
{
    public class RemovePersonaFromConversationCommand
    {
        private readonly IConversationRepository _conversationRepository;
        private readonly ISystemMessageService _systemMessageService;

        public RemovePersonaFromConversationCommand
        (
            IConversationRepository conversationRepository,
            ISystemMessageService systemMessageService
        )
        {
            _conversationRepository = conversationRepository;
            _systemMessageService = systemMessageService;
        }

        public async Task Execute(PersonaEntity persona, ConversationEntity conversation)
        {
            if (conversation.Personas.Remove(persona) == false)
            {
                throw new InvalidOperationException($"Persona {persona.Name} ({persona.Id}) not found in conversation {conversation.Id}");
            }

            await _conversationRepository.Save();

            string content = $"Persona {persona.Name} ({persona.Id}) left the conversation"; //todo: hardcoded message
            await _systemMessageService.SendSystemMessage(content, conversation.Id);
        }
    }
}
