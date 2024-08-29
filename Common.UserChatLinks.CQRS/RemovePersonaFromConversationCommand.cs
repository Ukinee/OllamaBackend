using Chat.DataAccess.Interfaces;
using Common.UserChatLinks.Models;
using Core.Common.DataAccess.SharedEntities.Chats;
using Core.Common.DataAccess.SharedEntities.Users;
using Personas.Services.Interfaces;
using Users.FakeUsers.Services.Interfaces;

namespace Common.UserChatLinks.CQRS
{
    public class RemovePersonaFromConversationCommand
    {
        private readonly IConversationRepository _conversationRepository;
        private readonly IFakeUserService _fakeUserService;

        public RemovePersonaFromConversationCommand
        (
            IConversationRepository conversationRepository,
            IFakeUserService fakeUserService
        )
        {
            _conversationRepository = conversationRepository;
            _fakeUserService = fakeUserService;
        }

        public async Task Execute(PersonaEntity persona, ConversationEntity conversation)
        {
            if (conversation.Personas.Remove(persona) == false)
            {
                throw new Exception($"Persona {persona.Name} ({persona.Id}) not found in conversation {conversation.Id}");
            }

            await _conversationRepository.Save();

            string content = $"Persona {persona.Name} ({persona.Id}) left the conversation"; //todo: hardcoded message
            await _fakeUserService.SendSystemMessage(content, conversation.Id);
        }
    }
}
