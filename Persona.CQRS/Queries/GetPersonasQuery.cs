using Authorization.Services.Interfaces;
using Chat.Services.Interfaces;
using Common.DataAccess.SharedEntities.Chats;
using Common.DataAccess.SharedEntities.Users;
using Persona.Domain.Services;
using Persona.Models.Personas;
using Personas.Services.Interfaces;

namespace Persona.CQRS.Queries
{
    public class GetPersonasQuery
    {
        private readonly IPersonaRepository _personaRepository;
        private readonly IUserRepository _userRepository;
        private readonly IConversationRepository _conversationRepository;
        private readonly PersonaMapper _personaMapper;

        public GetPersonasQuery
        (
            IPersonaRepository personaRepository,
            IUserRepository userRepository,
            IConversationRepository conversationRepository,
            PersonaMapper personaMapper
        )
        {
            _personaRepository = personaRepository;
            _userRepository = userRepository;
            _personaMapper = personaMapper;
            _conversationRepository = conversationRepository;
        }

        public async Task<PersonasViewModel> ExecuteByUserId(Guid userId)
        {
            PersonaEntity[] personas = await _personaRepository.GetAll(userId);

            PersonasViewModel personasViewModel = new PersonasViewModel
            {
                Personas = personas.Select(x => _personaMapper.ToViewModel(x)).ToArray(),
            };

            Console.WriteLine(personasViewModel.Personas.Count);

            return personasViewModel;
        }

        public async Task<PersonasViewModel> ExecuteByUsername(string username)
        {
            UserEntity? userEntity = await _userRepository.Get(username);

            if (userEntity == null)
                return new PersonasViewModel { Personas = [] };

            return await ExecuteByUserId(userEntity.Id);
        }

        public async Task<PersonasViewModel> ExecuteByConversationId(Guid conversationId)
        {
            ConversationEntity? conversationEntity = await _conversationRepository.GetConcreteConversation(conversationId);

            ArgumentNullException.ThrowIfNull(conversationEntity);

            return new PersonasViewModel()
            {
                Personas = conversationEntity
                    .Personas
                    .Select(x => _personaMapper.ToViewModel(x))
                    .ToList(),
            };
        }
    }
}
