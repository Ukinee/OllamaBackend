using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Persona.CQRS.Queries;
using Persona.Models.Personas;

namespace Persona.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]/")]
    public class PersonasController : ControllerBase
    {
        private readonly GetPersonasQuery _getPersonasQuery;

        public PersonasController(GetPersonasQuery getPersonasQuery)
        {
            _getPersonasQuery = getPersonasQuery;
        }

        [HttpGet("{userId:guid}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> GetByUserId([FromRoute] Guid userId)
        {
            PersonasViewModel personas = await _getPersonasQuery.ExecuteByUserId(userId);

            return Ok(personas);
        }

        [HttpGet("{username}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> GetByUsername([FromRoute] string username)
        {
            PersonasViewModel personas = await _getPersonasQuery.ExecuteByUsername(username);

            return Ok(personas);
        }

        [HttpGet("{conversationId:guid}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> GetByConversationId([FromRoute] Guid conversationId)
        {
            PersonasViewModel personas = await _getPersonasQuery.ExecuteByConversationId(conversationId);

            return Ok(personas);
        }
    }
}
