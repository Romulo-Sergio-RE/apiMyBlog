using api.Models;

namespace api.utils.Interfaces;

public interface ITokenUtil
{
    Task<string> GenerateToken(User user);
}