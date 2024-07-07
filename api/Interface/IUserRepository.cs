using api.Dtos.User;
using api.Models;

namespace api.Interface
{
    public interface IUserRepository
    {
        Task<List<User>> GetAllUsersAsync();
        Task<User?> GetUserByIdAsync(int id);
        Task<User?> CreateUserAsync(User user);
        Task<User?> UpdateUserAsync(int id, UserAllDto updateUser);
        Task<User?> DeleteUserAsync(int id);
    }
}