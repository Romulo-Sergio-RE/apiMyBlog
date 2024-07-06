using api.Context;
using api.Interface;
using api.Models;
using Microsoft.EntityFrameworkCore;

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

        public async Task<User?> CreateUserAsync(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<List<User>> GetAllUsersAsync()
        {
            var user = await _context.Users.ToListAsync();
            return user;
        }

        public async Task<User?> GetUserByIdAsync(int id)
        {
            var userId = await _context.Users.FindAsync(id);
            if (userId == null)
            {
                return null;
            }
            return userId;
        }
        
    }
}