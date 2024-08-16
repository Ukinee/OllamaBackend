using Discord.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Discord.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class DiscordVoiceChannelController : ControllerBase
    {
        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> ConnectAgent([FromBody] AgentConnectionInfo signal)
        {
            return Ok();
        }
        
        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> DisconnectAgent([FromBody] AgentConnectionInfo signal)
        {
            return Ok();
        }
        
        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> ConnectActor([FromBody] ActorConnectionInfo signal)
        {
            return Ok();
        }
        
        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> DisconnectActor([FromBody] ActorConnectionInfo signal)
        {
            return Ok();
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> AddUserSpeech([FromBody] UserSpeechInfo signal)
        {
            return Ok();
        }
    }
}
