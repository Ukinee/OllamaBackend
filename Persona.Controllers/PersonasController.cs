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
    public class PersonasController : ControllerBase
    {
        private readonly GetPersonasQuery _getPersonasQuery;

        public PersonasController(GetPersonasQuery getPersonasQuery)
        {
            _getPersonasQuery = getPersonasQuery;
        }
        
        [HttpGet("{id:guid}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> Get([FromRoute] Guid id)
        {
            PersonasViewModel personas = await _getPersonasQuery.Execute(id);
            
            return Ok(personas);
        }
    }
}
