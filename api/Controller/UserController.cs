using api.Mappers;
using api.Dtos.User;
using api.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using api.Repository.Interface;
using Microsoft.AspNetCore.Authorization;

namespace api.Controller;

//[Authorize(Roles  = "admin")]
[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IUserRepository _UserRepository;

    private readonly IUploadImageService _uploadImage;

    private readonly IUserPasswordCriptoService _userPasswordCripto;

    public UserController(IUserRepository userRepository, IUploadImageService uploadImage, IUserPasswordCriptoService userPasswordCripto)
    {
        _UserRepository = userRepository;
        _uploadImage = uploadImage;
        _userPasswordCripto = userPasswordCripto;
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
        var passwordCripto = _userPasswordCripto.ReturnMD5(userDto.Password);

        if (userDto?.UserImageName?.fileName?.Length > 0)
        {
            var upload = await _uploadImage.UploadImage(userDto.UserImageName, "articles");
            if (upload == "Failed.")
            {
                return BadRequest("erro ao add artigo");
            }
            var userCreate = userDto.ToCreateUserImageDto(passwordCripto, upload);
            await _UserRepository.CreateUserAsync(userCreate);
            return CreatedAtAction(nameof(GetByIdUser), new { id = userCreate.Id }, userCreate.ToUserDto());

        }
        var userCreateImage = userDto.ToCreateUserDto(passwordCripto);
        await _UserRepository.CreateUserAsync(userCreateImage);
        return CreatedAtAction(nameof(GetByIdUser), new { id = userCreateImage.Id }, userCreateImage.ToUserDto());
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateUser([FromRoute] int id, [FromQuery] UpdateUserRequestDto userDto)
    {
        var user = await _UserRepository.UpdateUserAsync(id, userDto);
        if (user == null)
        {
            return NotFound();
        }
        return Ok(user.ToUserDto());
    }

    [HttpDelete("{id}")]
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
