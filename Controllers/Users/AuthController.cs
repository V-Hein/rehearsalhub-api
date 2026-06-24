

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/auth")]
[AllowAnonymous]
public class AuthController : ControllerBase
{
    private readonly IAuthService _auth;
    public AuthController(IAuthService auth) => _auth = auth;
    
    [HttpPost("login")]
    public async Task<ActionResult<AuthResponseDto?>> Login(LoginDto dto)
    {
        var result = await _auth.Login(dto);
        return result == null ? Unauthorized() : Ok(result);
    }
}