using api.Interface;
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
    }
}