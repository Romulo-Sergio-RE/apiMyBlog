using api.Models;
using api.Dtos.Account;

namespace api.Repository.Interface;

public interface IAccountRepository
{
    Task<UserAccountDto?> LoginUser(LoginDto loginUser);
    Task<User?> RegisterUser(RegisterDto user);
}