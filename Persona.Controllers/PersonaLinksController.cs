using System.Collections.Concurrent;
using Chat.CQRS.Queries;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Persona.CQRS.Commands;
using Persona.CQRS.Queries;
using Persona.Models;
using Users.Authorization.Common;

namespace Persona.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PersonaLinksController : ControllerBase
    {
        private readonly CheckUserParticipatesInConversationQuery _checkUserParticipatesInConversationQuery;
        private readonly AssociatePersonaCommand _associatePersonaCommand;
        private readonly UpdatePersonaAssociationCommand _updatePersonaAssociationCommand;
        private readonly GetUserOwnsPersonaQuery _getUserOwnsPersonaQuery;
        private readonly GetPersonaLinkQuery _getPersonaLinkQuery;

        public PersonaLinksController
        (
            CheckUserParticipatesInConversationQuery checkUserParticipatesInConversationQuery,
            AssociatePersonaCommand associatePersonaCommand,
            UpdatePersonaAssociationCommand updatePersonaAssociationCommand,
            GetUserOwnsPersonaQuery getUserOwnsPersonaQuery,
            GetPersonaLinkQuery getPersonaLinkQuery
        )
        {
            _checkUserParticipatesInConversationQuery = checkUserParticipatesInConversationQuery;
            _associatePersonaCommand = associatePersonaCommand;
            _updatePersonaAssociationCommand = updatePersonaAssociationCommand;
            _getUserOwnsPersonaQuery = getUserOwnsPersonaQuery;
            _getPersonaLinkQuery = getPersonaLinkQuery;
        }

        [HttpGet("{conversationId:guid}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> Get(Guid conversationId)
        {
            Guid userId = User.GetGuid();
            
            PersonaLinkViewModel personaLink = await _getPersonaLinkQuery.Execute(conversationId, userId);
            
            // IActionResult? validateResult = await Validate(personaLink.PersonaId, personaLink.ConversationId);
            //
            // if (validateResult != null)
            //     return validateResult;
            
            return Ok(personaLink);
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> Associate(PostPersonaLinkRequest request)
        {
            IActionResult? validateResult = await Validate(request.PersonaId, request.ConversationId);

            if (validateResult != null)
                return validateResult;

            Guid userId = User.GetGuid();
            await _associatePersonaCommand.Execute(request, userId);

            return NoContent();
        }

        [HttpPut]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> UpdateAssociation(PutPersonaLinkRequest request)
        {
            IActionResult? validateResult = await Validate(request.PersonaId, request.ConversationId);

            if (validateResult != null)
                return validateResult;

            Guid userId = User.GetGuid();
            await _updatePersonaAssociationCommand.Execute(request, userId);

            return NoContent();
        }

        private async Task<IActionResult?> Validate(Guid personaId, Guid conversationId)
        {
            if (ModelState.IsValid == false)
                return BadRequest(ModelState);

            Guid userId = User.GetGuid();

            if (await _getUserOwnsPersonaQuery.Execute(userId, personaId) == false)
                return Unauthorized();

            if (await _checkUserParticipatesInConversationQuery.Execute(conversationId, userId) == false)
                return Unauthorized();

            return null;
        }
    }
}
