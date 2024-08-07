using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Persona.CQRS.Queries;
using Persona.Models.Personas;

namespace Persona.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PersonaController : ControllerBase
    {
        private readonly CreatePersonaQuery _createPersonaQuery;
        private readonly GetUserOwnsPersonaQuery _getUserOwnsPersonaQuery;
        private readonly GetPersonaQuery _getPersonaQuery;
        private readonly UpdatePersonaQuery _updatePersonaQuery;
       

        public PersonaController
        (
            CreatePersonaQuery createPersonaQuery,
            GetUserOwnsPersonaQuery getUserOwnsPersonaQuery,
            GetPersonaQuery getPersonaQuery,
            UpdatePersonaQuery updatePersonaQuery
        )
        {
            _createPersonaQuery = createPersonaQuery;
            _getUserOwnsPersonaQuery = getUserOwnsPersonaQuery;
            _getPersonaQuery = getPersonaQuery;
            _updatePersonaQuery = updatePersonaQuery;
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
            
            PersonaViewModel viewModel = await _createPersonaQuery.Execute(personaRequest);

            return Ok(viewModel);
        }

        [HttpPut("{id:guid}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> Put([FromRoute] Guid id, [FromBody] PutPersonaRequest personaRequest)
        {
            if (ModelState.IsValid == false)
                return BadRequest(ModelState);
            
            PersonaViewModel viewModel = await _updatePersonaQuery.Execute(personaRequest, id);

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
