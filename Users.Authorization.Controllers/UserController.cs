using System.Security.Claims;
using Authorization.Domain;
using Authorization.Services.Implementations;
using Authorization.Services.Interfaces;
using Core.Common.DataAccess.SharedEntities.Users;
using Core.Common.DataAccess.SharedEntities.Users.Mappers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Users.Authorization.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")] //todo : use cases
    public class UserController : ControllerBase
    {
        private readonly ITokenService _tokenService;
        private readonly UserService _userService;
        private readonly UserManager<UserEntity> _userManager;

        public UserController
        (
            ITokenService tokenService,
            UserService userService,
            UserManager<UserEntity> userManager
        )
        {
            _tokenService = tokenService;
            _userService = userService;
            _userManager = userManager;
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public IActionResult CheckLogin()
        {
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromBody] UserCreateRequest createRequest, CancellationToken cancellationToken)
        {
            if (ModelState.IsValid == false)
                return BadRequest(ModelState);

            UserEntity user = await _userService.Create(createRequest, cancellationToken);

            string token = _tokenService.CreateToken(_userManager, user);

            return Ok(user.ToViewModel(token));
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] UserLoginRequest userRequest)
        {
            if (ModelState.IsValid == false)
                return BadRequest(ModelState);

            UserEntity? user = await _userManager
                .Users
                .Include(user => user.Personas)
                .FirstOrDefaultAsync(x => x.UserName == userRequest.UserName);

            if (user == null)
                return Unauthorized("Wrong username or password"); //todo : hardcode

            if (await _userManager.CheckPasswordAsync(user, userRequest.Password) == false)
                return Unauthorized("Wrong username or password"); //todo : hardcode

            string token = _tokenService.CreateToken(_userManager, user);

            return Ok(user.ToViewModel(token));
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public IActionResult TestAuthorization()
        {
            List<string> roles = User
                .Claims
                .Where(claim => claim.Type == ClaimTypes.Role)
                .Select(claim => claim.Value)
                .ToList();

            return Ok("You're Authorized, your roles are: " + string.Join(", ", roles)); //todo : hardcode
        }
    }
}
