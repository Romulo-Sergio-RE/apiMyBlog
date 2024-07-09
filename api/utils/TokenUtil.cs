using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using api.Interface;
using api.Models;
using api.utils.Interfaces;
using Microsoft.IdentityModel.Tokens;

namespace api.utils;

public class TokenUtil : ITokenUtil
{
    private readonly IConfiguration _configuration;
    private readonly IUserRepository _userRepository;

    public TokenUtil(IConfiguration configuration,IUserRepository userRepository)
    {
        _configuration = configuration;
        _userRepository = userRepository;
    }

    public async Task<string> GenerateToken(User user)
    {
        var userDB = await _userRepository.GetUserByIdAsync(user.Id);
        if (userDB?.Email != user.Email || userDB?.Password != user.Password)
            return String.Empty;

        var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"] ?? string.Empty));
        var issuer = _configuration["Jwt:issuer"];
        var audience = _configuration["Jwt:Audience"];
        var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

        var tokenOptions = new JwtSecurityToken(
            issuer : issuer,
            audience: audience,
            claims: new []
            {
                new Claim(type: ClaimTypes.Email, value: user.Email),
                //new Claim(type: ClaimTypes.Role, value: user.IsAdmin),
            },
            expires: DateTime.Now.AddHours(2),
            signingCredentials: signinCredentials
        );
        return "";
    }
}
