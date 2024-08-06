using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Persona.CQRS.Queries;
using Persona.Models.Personas;
using Users.Authorization.Common;

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
            
            Guid userId = User.GetGuid();

            PersonaViewModel viewModel = await _createPersonaQuery.Execute(personaRequest, userId);

            return Ok(viewModel);
        }

        [HttpPut("{id:guid}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> Put([FromRoute] Guid id, [FromBody] PutPersonaRequest personaRequest)
        {
            IActionResult? validateResult = await Validate(id);

            if (validateResult != null)
                return validateResult;

            PersonaViewModel viewModel = await _updatePersonaQuery.Execute(personaRequest);

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

        private async Task<IActionResult?> Validate(Guid personaId)
        {
            if (ModelState.IsValid == false)
                return BadRequest(ModelState);

            Guid userId = User.GetGuid();

            if (await _getUserOwnsPersonaQuery.Execute(userId, personaId) == false)
                return Unauthorized();

            return null;
        }
    }
}
