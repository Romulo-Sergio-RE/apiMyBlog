using api.Dtos.Login;
using api.utils.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace api.Controller;
[Route("api/login")]
[ApiController]
public class AuthenticationController : ControllerBase
{
    private readonly ITokenUtil _tokenUtil;

    public AuthenticationController(ITokenUtil tokenUtil)
    {
        _tokenUtil = tokenUtil;
    }
    [HttpPost]
    public async Task<IActionResult> Login([FromQuery] LoginDto loginDto)
    {
        var token = await _tokenUtil.GenerateToken(loginDto);
        if(token == "")
        {
            return Unauthorized();
        }
        return Ok(token);
    }
}