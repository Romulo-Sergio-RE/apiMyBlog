using api.Dtos.User;
using api.Interface;
using api.Mappers;
using api.utils;
using Microsoft.AspNetCore.Mvc;

namespace api.Controller;

[Route("api/user")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IUserRepository _UserRepository;
    public UserController(IUserRepository userRepository)
    {
        _UserRepository = userRepository;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllUsers()
    {
        var users = await _UserRepository.GetAllUsersAsync();
        return Ok(users);
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> GetByIdUser([FromRoute] int id)
    {
        var userId = await _UserRepository.GetUserByIdAsync(id);
        if (userId == null)
        {
            return NotFound();
        }
        return Ok(userId.ToUserDto());
    }
    [HttpPost]
    public async Task<IActionResult> CreateUser([FromQuery] CreateUserRequestDto userDto)
    {
        var cripto = new UserPasswordCripto();
        var passwordCripto = cripto.ReturnMD5(userDto.Password);

        var userCreate = userDto.ToUserAllDto(passwordCripto);
        await _UserRepository.CreateUserAsync(userCreate);

        return CreatedAtAction(nameof(GetByIdUser), new { id = userCreate.Id }, userCreate.ToUserDto());
    }
    [HttpPut]
    [Route("{id}")]
    public async Task<IActionResult> UpdateUser([FromRoute] int id, [FromBody] UpdateUserRequestDto userDto)
    {
        var user = await _UserRepository.UpdateUserAsync(id, userDto);
        if (user == null)
        {
            return NotFound();
        }
        return Ok(user.ToUserDto());
    }

    [HttpDelete]
    [Route("{id}")]
    public async Task<IActionResult> DeleteUser([FromRoute] int id)
    {
        var deleteUser = await _UserRepository.DeleteUserAsync(id);

        if (deleteUser == null)
        {
            return NotFound();
        }
        return NoContent();
    }
}
