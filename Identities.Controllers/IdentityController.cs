using Identities.Models;
using Identities.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Identities.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class IdentityController : ControllerBase
    {
        private readonly IIdentityRepository _identityRepository;

        public IdentityController(IIdentityRepository identityRepository)
        {
            _identityRepository = identityRepository;
        }
        
        [HttpPut("{id:guid}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> Put([FromRoute] Guid id, [FromBody] PutIdentityRequest request)
        {
            await _identityRepository.Update(request, id);
            
            return Ok();
        }
    }
}
