using System.Security.Claims;
using Authorization.Domain;
using Authorization.Services.Interfaces;
using Common.DataAccess.SharedEntities.Users;
using Common.DataAccess.SharedEntities.Users.Mappers;
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
        private readonly IUserCreationService _userCreationService;
        private readonly UserManager<UserEntity> _userManager;

        public UserController
        (
            ITokenService tokenService,
            IUserCreationService userCreationService,
            UserManager<UserEntity> userManager
        )
        {
            _tokenService = tokenService;
            _userCreationService = userCreationService;
            _userManager = userManager;
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public IActionResult CheckLogin()
        {
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromBody] UserCreateRequest createRequest)
        {
            Console.WriteLine("Register request");

            if (ModelState.IsValid == false)
                return BadRequest(ModelState);

            Console.WriteLine("ValidModel");

            try
            {
                UserEntity user = await _userCreationService.Create(createRequest);

                Console.WriteLine("User created");
                
                string token = await _tokenService.CreateToken(_userManager, user);
                
                Console.WriteLine("User token created");

                return Ok(user.ToViewModel(token));
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
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

            string token = await _tokenService.CreateToken(_userManager, user);

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
