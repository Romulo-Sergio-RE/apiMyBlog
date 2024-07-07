using api.Context;
using api.Dtos.User;
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

        public async Task<User?> DeleteUserAsync(int id)
        {
            var userId = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
            if (userId == null)
            {
                return null;
            }
            _context.Users.Remove(userId);
            await _context.SaveChangesAsync();
            return userId;
        }

        public async Task<List<User>> GetAllUsersAsync()
        {
            var user = await _context.Users.ToListAsync();
            return user;
        }

        public async Task<User?> GetUserByIdAsync(int id)
        {
            var userId = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
            if (userId == null)
            {
                return null;
            }
            return userId;
        }

        public async Task<User?> UpdateUserAsync(int id, UserAllDto updateUser)
        {
            var userModel = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
            if(userModel == null)
            {
                return null;
            }
            userModel.Name = updateUser.Name;
            userModel.Email = updateUser.Email;
            userModel.Password = updateUser.Password;
            userModel.Genre = updateUser.Genre;
            await _context.SaveChangesAsync();
            return userModel;
        }
    }
}