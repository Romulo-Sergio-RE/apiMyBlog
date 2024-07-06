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
        public async Task<IActionResult> Create([FromBody] UserAllMappers user)
        {
            var userCreate = user.ToUserAllDto();
            await _UserRepository.CreateUserAsync(userCreate);        

            return CreatedAtAction(nameof(GetByIdUser),new {id = userCreate.Id} ,userCreate.ToUserDto());
        }
    }
}