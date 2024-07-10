using api.Dtos.Login;
using api.Models;

namespace api.utils.Interfaces;

public interface ITokenUtil
{
    Task<string> GenerateToken(LoginDto user);
}