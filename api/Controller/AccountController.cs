using api.Dtos.Account;
using api.Dtos.Login;
using api.Mappers;
using api.Repository.Interface;
using Microsoft.AspNetCore.Mvc;

namespace api.Controller;

[Route("api/[controller]")]
[ApiController]
public class AccountController : ControllerBase
{
    private readonly IAccountRepository _accountRepository;
    public AccountController(IAccountRepository accountRepository)
    {
        _accountRepository = accountRepository;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromQuery] LoginDto loginUser)
    {
        var login = await _accountRepository.LoginUser(loginUser);
        if (login == null)
        {
            return NotFound("usuario esta com o email errado ou senha errada");
        }

        return Ok(login);
    }
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromQuery] RegisterDto registerUser)
    {
        var register = await _accountRepository.RegisterUser(registerUser);
        if (register == null)
        {
            return NotFound("usuario esta com o email errado ou senha errada");
        }
        return CreatedAtAction("GetByIdUser", new { id = register.Id }, register.ToUserDto());
    }
}
