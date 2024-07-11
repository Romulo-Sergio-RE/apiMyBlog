using api.Dtos.Login;
using api.Helpers;
using api.Models;

namespace api.utils.Interfaces;

public interface ITokenUtil
{
    Task<string> GenerateToken(QueryUser user);

   // string GenerateRefreshToken();

    //string GenerateAccessTokenFromRefreshToken(string refreshToken, string secret);
}