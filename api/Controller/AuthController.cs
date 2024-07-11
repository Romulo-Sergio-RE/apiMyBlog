using api.Dtos.Login;
using api.Helpers;
using api.Interface;
using api.utils.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace api.Controller;
[Route("api")]
[ApiController]
public class AuthenticationController : ControllerBase
{
    private readonly ITokenUtil _tokenUtil;
    private readonly IUserRepository _userRepository;
    public AuthenticationController(ITokenUtil tokenUtil, IUserRepository userRepository)
    {
        _tokenUtil = tokenUtil;
        _userRepository = userRepository;
    }
    [HttpPost("/login")]
    public async Task<IActionResult> Login([FromQuery] QueryUser loginDto)
    {
        var user = await _userRepository.GetAllUsersAsync(loginDto);
    
        if (user == null)
        {
            return NotFound(new {message = "usuario nao encontrado"});
        }
        var token = await _tokenUtil.GenerateToken(loginDto);
        //var refreshToken = _tokenUtil.GenerateRefreshToken();
        if(token == "")
        {
            return Unauthorized();
        }
        
        return Ok(new {
            user = user,
            token = token,
            //refreshToken = refreshToken,
        });
    }
    [HttpPost("/register")]
    public async Task<IActionResult> Register()
    {
        return Ok();
    }
}