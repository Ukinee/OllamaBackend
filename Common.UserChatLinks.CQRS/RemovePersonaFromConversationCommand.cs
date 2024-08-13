using Chat.Services.Interfaces;
using Common.DataAccess.SharedEntities.Chats;
using Common.DataAccess.SharedEntities.Users;
using Common.UserChatLinks.Models;
using Personas.Services.Interfaces;
using Users.FakeUsers.Services.Interfaces;

namespace Common.UserChatLinks.CQRS
{
    public class RemovePersonaFromConversationCommand
    {
        private readonly IPersonaRepository _personaRepository;
        private readonly IConversationRepository _conversationRepository;
        private readonly IFakeUserService _fakeUserService;

        public RemovePersonaFromConversationCommand
        (
            IPersonaRepository personaRepository,
            IConversationRepository conversationRepository,
            IFakeUserService fakeUserService
        )
        {
            _personaRepository = personaRepository;
            _conversationRepository = conversationRepository;
            _fakeUserService = fakeUserService;
        }

        public async Task Execute(RemovePersonaFromConversationRequest request)
        {
            PersonaEntity? persona = await _personaRepository.Get(request.PersonaId);
            ConversationEntity? conversation = await _conversationRepository.GetConcreteConversation(request.ConversationId);

            ArgumentNullException.ThrowIfNull(persona);
            ArgumentNullException.ThrowIfNull(conversation);

            if (conversation.Personas.Remove(persona) == false)
            {
                throw new Exception($"Persona {persona.Name} ({persona.Id}) not found in conversation {conversation.Id}");
            }

            await _conversationRepository.Update(conversation);

            string content = $"Persona {persona.Name} ({persona.Id}) left the conversation"; //todo: hardcoded message
            await _fakeUserService.SendSystemMessage(content, conversation.Id);
        }
    }
}
