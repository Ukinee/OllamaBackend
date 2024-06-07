using System.Security.Claims;
using Authorization.Domain;
using Authorization.Domain.Extensions;
using Authorization.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Authorization.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class UserController
    (
        ITokenService tokenService,
        UserManager<UserEntity> userManager
    ) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Register([FromBody] UserCreateRequest createRequest)
        {
            try
            {
                if (ModelState.IsValid == false)
                    return BadRequest(ModelState);

                UserEntity user = createRequest.ToEntity();

                IdentityResult result = await userManager.CreateAsync(user, createRequest.Password);

                if (result.Succeeded == false)
                    return BadRequest(result.Errors);

                IdentityResult roleResult = await userManager.AddToRoleAsync(user, "User"); //todo : hardcode

                if (roleResult.Succeeded == false)
                    return BadRequest(roleResult.Errors);

                string token = await tokenService.CreateToken(userManager, user);

                return Ok(user.ToViewModel(token)); //todo : hardcode
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Authorize([FromBody] UserLoginRequest userRequest)
        {
            if (ModelState.IsValid == false)
                return BadRequest(ModelState);

            UserEntity? user = await userManager
                .Users
                .FirstOrDefaultAsync(x => x.UserName == userRequest.UserName);

            if (user == null)
                return Unauthorized("Wrong username or password"); //todo : hardcode

            if (await userManager.CheckPasswordAsync(user, userRequest.Password) == false)
                return Unauthorized("Wrong username or password"); //todo : hardcode

            string token = await tokenService.CreateToken(userManager, user);

            return Ok(user.ToViewModel(token));
        }
        
        [HttpGet]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public IActionResult TestAuthorization()
        {
            List<string> roles = User.Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value).ToList();
            
            return Ok("You're Authorized, your roles are: " + string.Join(", ", roles));
        }
    
    
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin")]
        [HttpGet]
        public IActionResult IsAdmin()
        {
            return Ok("You're Admin");
        }

        // [HttpPut]
        // public async Task<IActionResult> Update([FromBody] UserRequest userRequest)
        // {
        //     return Ok();
        // }
        //
        // [HttpDelete]
        // public async Task<IActionResult> Delete([FromBody] UserRequest createRequest)
        // {
        //     return Ok();
        // }
    }
}
