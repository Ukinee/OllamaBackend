using Authorization.Domain;
using Authorization.Services.Interfaces;
using Common.DataAccess.SharedEntities;
using Common.DataAccess.SharedEntities.Mappers;
using Common.DataAccess.SharedEntities.Objects;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Users.Authorization.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")] //todo : use cases
    public class UserController
    (
        ILogger<UserController> logger,
        ITokenService tokenService,
        UserManager<UserEntity> userManager
    ) : ControllerBase
    {
        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public IActionResult CheckLogin()
        {
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromBody] UserCreateRequest createRequest)
        {
            logger.Log(LogLevel.Information, $"Registering user {createRequest.UserName}");

            try
            {
                if (ModelState.IsValid == false)
                    return BadRequest(ModelState);

                UserEntity user = createRequest.ToEntity();

                // IdentityResult result = await userManager.CreateAsync(user, createRequest.Password);
                //
                // if (result.Succeeded == false)
                //     return BadRequest(result.Errors);
                //
                // IdentityResult roleResult = await userManager.AddToRoleAsync(user, "User"); //todo : hardcode
                //
                // if (roleResult.Succeeded == false)
                //     return BadRequest(roleResult.Errors);

                string token = await tokenService.CreateToken(userManager, user);

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

            logger.Log
                (LogLevel.Information, $"Authorizing user {userRequest.UserName} with password {userRequest.Password}");

            UserEntity? user = await userManager
                .Users
                .FirstOrDefaultAsync(x => x.UserName == userRequest.UserName);

            if (user == null)
            {
                logger.Log(LogLevel.Error, $"User {userRequest.UserName} not found");

                return Unauthorized("Wrong username or password"); //todo : hardcode
            }

            if (await userManager.CheckPasswordAsync(user, userRequest.Password) == false)
            {
                logger.Log(LogLevel.Error, $"Wrong password for user {userRequest.UserName}");

                return Unauthorized("Wrong username or password"); //todo : hardcode
            }

            string token = await tokenService.CreateToken(userManager, user);
            
            return Ok(user.ToViewModel(token));
        }

        [HttpGet]
        public IActionResult TestAuthorization()
        {
            if (User.Identity?.IsAuthenticated == false)
                return Unauthorized();
            
            
            return Ok(); //todo : hardcode
        }
    }
}
