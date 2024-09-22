using Chat.DataAccess.Interfaces;
using Core.Common.DataAccess.SharedEntities.Chats;
using Core.Common.DataAccess.SharedEntities.Users;
using Users.FakeUsers.Services.Interfaces;

namespace Common.UserChatLinks.CQRS
{
    public class AddPersonaToConversationCommand
    {
        private readonly IConversationRepository _conversationRepository;
        private readonly ISystemMessageService _systemMessageService;

        public AddPersonaToConversationCommand
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
            if (conversation.Personas.Any(x => x.Id == persona.Id))
            {
                throw new Exception("Persona is already in the conversation");
            }
            
            conversation.Personas.Add(persona);
            await _conversationRepository.Save();

            string content = $"Persona {persona.Name} ({persona.Id}) joined the conversation"; //todo: hardcoded message

            await _systemMessageService.SendSystemMessage(content, conversation.Id);
        }
    }
}
