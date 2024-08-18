using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Persona.CQRS.Queries;
using Persona.CQRS.Queries.Done;
using Persona.Models.Personas;
using Personas.Services.Implementations;

namespace Persona.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PersonaController : ControllerBase
    {
        private readonly CreatePersonaQuery _createPersonaQuery;
        private readonly GetPersonaQuery _getPersonaQuery;
        private readonly UpdatePersonaQuery _updatePersonaQuery;
        private readonly PersonaService _personaService;

        public PersonaController
        (
            CreatePersonaQuery createPersonaQuery,
            GetPersonaQuery getPersonaQuery,
            UpdatePersonaQuery updatePersonaQuery,
            PersonaService personaService
        )
        {
            _createPersonaQuery = createPersonaQuery;
            _getPersonaQuery = getPersonaQuery;
            _updatePersonaQuery = updatePersonaQuery;
            _personaService = personaService;
        }

        // [HttpGet("{id:guid}")]
        // [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        // public async Task<IActionResult> Get([FromRoute] Guid id)
        // {
        //     IActionResult? validateResult = await Validate(id);
        //
        //     if (validateResult != null)
        //         return validateResult;
        //
        //     PersonaEntity personaEntity = await _getPersonaQuery.Execute(id);
        //
        //     return Ok(personaEntity.ToViewModel());
        // }

        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> Post([FromBody] PostPersonaRequest personaRequest)
        {
            if (ModelState.IsValid == false)
                return BadRequest(ModelState);
            
            PersonaViewModel viewModel = await _personaService.Create(personaRequest);

            return Ok(viewModel);
        }

        [HttpPut("{id:guid}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> Put([FromRoute] Guid id, [FromBody] PutPersonaRequest personaRequest)
        {
            if (ModelState.IsValid == false)
                return BadRequest(ModelState);
            
            PersonaViewModel viewModel = await _personaService.Update(personaRequest, id);

            return Ok(viewModel);
        }

        // [HttpDelete("{id:guid}")]
        // [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        // public async Task<IActionResult> Delete([FromRoute] Guid id)
        // {
        //     IActionResult? validateResult = await Validate(id);
        //
        //     if (validateResult != null)
        //         return validateResult;
        //
        //     throw new NotImplementedException();
        //
        //     return NoContent();
        // }
    }
}
