using api.Context;
using api.Interface;
using api.Models;

namespace api.Repository
{
    public class UserRepository : IUserRepository
    {
        //private readonly IUserRepository _userRepository;
        private readonly ApplicationDbContext _context;
        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public Task<List<User>> GetAllUsersAsync()
        {
            throw new NotImplementedException();
        }

    }
}