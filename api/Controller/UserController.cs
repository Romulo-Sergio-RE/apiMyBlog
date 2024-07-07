using api.Dtos.User;
using api.Interface;
using api.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace api.Controller
{
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
        public async Task<IActionResult> CreateUser([FromBody] UserAllDto userDto)
        {
            var userCreate = userDto.ToUserAllDto();
            await _UserRepository.CreateUserAsync(userCreate);        

            return CreatedAtAction(nameof(GetByIdUser),new {id = userCreate.Id} ,userCreate.ToUserDto());
        }
        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateUser([FromRoute] int id , [FromBody] UserAllDto userDto)
        {
            await _UserRepository.UpdateUserAsync(id, userDto);        

            return Ok(userDto.ToUserAllDto());
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteUser([FromRoute] int id)
        {
            var deleteUser = await _UserRepository.DeleteUserAsync(id);

            if(deleteUser == null)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}