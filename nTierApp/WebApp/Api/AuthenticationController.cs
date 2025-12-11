using Entities.DataTransferObjects;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;

namespace WebApp.Api;

[ApiController]
[Route("api/auth")]
public class AuthenticationController : ControllerBase
{
    private readonly IServiceManager _manager;

    public AuthenticationController(IServiceManager manager)
    {
        _manager = manager;
    }

    [HttpPost("login")] // api/auth/login
    public async Task<IActionResult> Login([FromBody] UserForAuthenticationDto userForAuthDto)
    {
        var user = await _manager.AuthService.ValidateUserAsync(userForAuthDto);
        if (user is null)
            return Unauthorized(); // 401
        var token = await _manager.AuthService.CreateTokenAsync(user);
        return Ok(new
        {
            UserName = user.UserName,
            Token = token,
        });
    }

    [HttpPost("register")]
    public async Task<IActionResult> RegisterUser([FromBody] UserForRegistrationDto userDto)
    {
       var result = await _manager
            .AuthService
            .RegisterUserAsync(userDto);

        if(!result.Succeeded)
            return BadRequest(); // 400 
        
        return NoContent(); // 201
    }
}