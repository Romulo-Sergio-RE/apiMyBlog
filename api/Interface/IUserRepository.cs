using api.Models;

namespace api.Interface
{
    public interface IUserRepository
    {
        Task<List<User>> GetAllUsersAsync();
    }
}