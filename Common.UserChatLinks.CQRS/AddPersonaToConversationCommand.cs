using Chat.CQRS.Queries;
using Chat.Services.Interfaces;
using Common.DataAccess.SharedEntities.Chats;
using Common.DataAccess.SharedEntities.Users;
using Common.UserChatLinks.Models;
using Personas.Services.Interfaces;
using Users.FakeUsers.Services.Interfaces;

namespace Common.UserChatLinks.CQRS
{
    public class AddPersonaToConversationCommand
    {
        private readonly IPersonaRepository _personaRepository;
        private readonly IConversationRepository _conversationRepository;
        private readonly IFakeUserService _fakeUserService;

        public AddPersonaToConversationCommand
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

        public async Task Execute(AddPersonaToConversationRequest request)
        {
            PersonaEntity? persona = await _personaRepository.Get(request.PersonaId);
            ConversationEntity? conversation = await _conversationRepository.GetConcreteConversation(request.ConversationId);

            ArgumentNullException.ThrowIfNull(persona);
            ArgumentNullException.ThrowIfNull(conversation);

            if (conversation.Personas.Any(x => x.Id == persona.Id))
            {
                throw new Exception("Persona is already in the conversation");
            }
            
            conversation.Personas.Add(persona);
            await _conversationRepository.Update(conversation);

            string content = $"Persona {persona.Name} ({persona.Id}) joined the conversation"; //todo: hardcoded message

            await _fakeUserService.SendSystemMessage(content, conversation.Id);
        }
    }
}
