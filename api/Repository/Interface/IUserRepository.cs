using api.Dtos.User;
using api.Helpers;
using api.Models;

namespace api.Interface;

public interface IUserRepository
{
    Task<List<User>> GetAllUsersAsync();

    Task<User?> GetUserByIdAsync(int id);
    
    Task<User?> CreateUserAsync(User user);

    Task<User?> UpdateUserAsync(int id, UpdateUserRequestDto updateUser, string ImagePath);

    Task<User?> DeleteUserAsync(int id);

    Task<bool> UserIsAdmin(int id);

    Task<bool> UserExist(int id);

}
