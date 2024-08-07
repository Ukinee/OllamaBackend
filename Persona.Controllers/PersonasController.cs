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
        
        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> Get([FromRoute] Guid userId)
        {
            PersonasViewModel personas = await _getPersonasQuery.Execute(userId);
            
            return Ok(personas);
        }
    }
}
