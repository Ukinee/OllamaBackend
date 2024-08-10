using Chat.Domain.Conversations;
using Chat.Services.Interfaces;
using Common.DataAccess.SharedEntities;
using Common.DataAccess.SharedEntities.Chats;
using Common.DataAccess.SharedEntities.Chats.Mappers;
using Common.DataAccess.SharedEntities.Users;
using Personas.Services.Interfaces;

namespace Chat.CQRS.Queries
{
    public class GetGeneralConversationsWithUserIdQuery
    {
        private readonly IConversationRepository _conversationRepository;
        private readonly IPersonasRepository _personasRepository;

        public GetGeneralConversationsWithUserIdQuery
        (
            IConversationRepository conversationRepository,
            IPersonasRepository personasRepository
        )
        {
            _conversationRepository = conversationRepository;
            _personasRepository = personasRepository;
        }

        public async Task<IList<GeneralConversationViewModel>> Execute(Guid personaId)
        {
            PersonaEntity? persona = await _personasRepository.GetWithConversations(personaId);
            
            ArgumentNullException.ThrowIfNull(persona);
            
            return persona.Conversations.Select(conversation => conversation.ToGeneralConversationViewModel()).ToList();
        }
    }
}
