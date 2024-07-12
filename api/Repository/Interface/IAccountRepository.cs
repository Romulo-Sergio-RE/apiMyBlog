using api.Dtos.Account;
using api.Dtos.Login;
using api.Models;

namespace api.Repository.Interface;

public interface IAccountRepository
{
    Task<UserAccountDto?> LoginUser(LoginDto loginUser);
    Task<User?> RegisterUser(RegisterDto user);
}