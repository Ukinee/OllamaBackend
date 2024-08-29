using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Persona.Models.Personas;
using Personas.Services.Implementations;

namespace Persona.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PersonaController : ControllerBase
    {
        private readonly PersonaService _personaService;

        public PersonaController(PersonaService personaService)
        {
            _personaService = personaService;
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> Post([FromBody] PostPersonaRequest personaRequest, CancellationToken token)
        {
            if (ModelState.IsValid == false)
                return BadRequest(ModelState);

            PersonaViewModel viewModel = await _personaService.Create(personaRequest, token);

            return Ok(viewModel);
        }

        [HttpPut("{id:guid}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> Put([FromRoute] Guid id, [FromBody] PutPersonaRequest personaRequest, CancellationToken cancellationToken)
        {
            if (ModelState.IsValid == false)
                return BadRequest(ModelState);

            PersonaViewModel viewModel = await _personaService.Update(personaRequest, id, cancellationToken);

            return Ok(viewModel);
        }
    }
}
