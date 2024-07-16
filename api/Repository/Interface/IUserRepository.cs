using api.Models;
using api.Dtos.User;

namespace api.Repository.Interface;

public interface IUserRepository
{
    Task<List<User>> GetAllUsersAsync();

    Task<User?> GetUserByIdAsync(int id);
    
    Task<User?> CreateUserAsync(User user);

    Task<User?> UpdateUserAsync(int id, UpdateUserRequestDto updateUser);

    Task<User?> DeleteUserAsync(int id);

    Task<bool> UserIsAdmin(int id);

    Task<bool> UserExist(int id);

}
