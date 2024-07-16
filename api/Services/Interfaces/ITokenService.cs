using api.Models;

namespace api.Services.Interfaces;

public interface ITokenService
{
    string GenerateToken(User user);
}
