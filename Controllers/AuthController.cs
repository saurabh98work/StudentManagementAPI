using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly JwtTokenService _jwtTokenService;

    public AuthController(JwtTokenService jwtTokenService)
    {
        _jwtTokenService = jwtTokenService;
    }

    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginRequest request)
    {

        if (request.Username == "admin" && request.Password == "password") // for testing only
        {
            var token = _jwtTokenService.GenerateToken(request.Username);
            return Ok(new { Token = token });
        }

        return Unauthorized();
    }
}

public class LoginRequest
{
    public string? Username { get; set; }
    public string? Password { get; set; }
}
