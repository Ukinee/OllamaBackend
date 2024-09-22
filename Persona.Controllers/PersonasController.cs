using System.Diagnostics;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Persona.Models.Personas;
using Personas.Services.Implementations;

namespace Persona.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]/")]
    public class PersonasController : ControllerBase
    {
        private readonly PersonaService _personaService;

        public PersonasController(PersonaService personaService)
        {
            _personaService = personaService;
        }

        [HttpGet("{userId:guid}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> GetByUserId([FromRoute] Guid userId)
        {
            IList<PersonaViewModel> personas = await _personaService.GetByUserId(userId);

            return Ok(personas);
        }

        [HttpGet("{username}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> GetByUsername([FromRoute] string username)
        {
            IList<PersonaViewModel> personas;

            try
            {
                personas = await _personaService.GetByUsername(username);
            }
            catch (ArgumentNullException)
            {
                personas = [];
                Trace.WriteLine("User not found");
            }

            return Ok(personas);
        }

        [HttpGet("{conversationId:guid}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> GetByConversationId([FromRoute] Guid conversationId, CancellationToken token)
        {
            IList<PersonaViewModel> personas = await _personaService.GetByConversationId(conversationId, token);

            return Ok(personas);
        }
    }
}
