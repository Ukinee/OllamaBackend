using Authorization.Domain;
using Authorization.Domain.Extensions;
using Authorization.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Authorization.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController
    (
        IUserRepository userRepository,
        IValidationService validationService,
        IPasswordService passwordService
    ) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> Authorize([FromBody] UserRequest userRequest)
        {
            UserEntity? user = await userRepository.Get(userRequest.Id);
    
            if (user == null)
                return NotFound();
    
            if (validationService.Validate(user, userRequest) == false)
                return Unauthorized();
    
            return Ok(user.ToViewModel());
        }
    
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] UserCreateRequest createRequest)
        {
            bool exists = await userRepository.Exists(createRequest.Name);
            
            if(exists)
                return Conflict();
    
            passwordService.HashPassword(createRequest.Password, out string hashedPassword, out string salt);
    
            UserEntity user = createRequest.ToEntity(hashedPassword, salt);
    
            await userRepository.Add(user);
    
            return CreatedAtAction(nameof(Authorize), new { id = user.Id }, user.ToViewModel());
        }
    
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] UserRequest userRequest)
        {
            UserEntity? user = await userRepository.Get(userRequest.Id);
    
            if (user == null)
                return NotFound();
    
            if (validationService.Validate(user, userRequest) == false)
                return Unauthorized();
    
            passwordService.HashPassword(userRequest.Password, out string hashedPassword, out string salt);
    
            UserEntity updatedUser = await userRepository.Update(userRequest, hashedPassword, salt);
    
            return Ok(updatedUser.ToViewModel());
        }
    
        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] UserRequest createRequest)
        {
            UserEntity? user = await userRepository.Get(createRequest.Id);
    
            if (user == null)
                return NotFound();
    
            await userRepository.Delete(user);
    
            return NoContent();
        }
    }
}
