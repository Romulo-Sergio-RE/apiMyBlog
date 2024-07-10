using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using api.Dtos.Login;
using api.Interface;
using api.utils.Interfaces;
using Microsoft.IdentityModel.Tokens;

namespace api.utils;

public class TokenUtil : ITokenUtil
{
    private readonly IConfiguration _configuration;
    private readonly IUserRepository _userRepository;

    public TokenUtil(IConfiguration configuration, IUserRepository userRepository)
    {
        _configuration = configuration;
        _userRepository = userRepository;
    }

    public async Task<string> GenerateToken(LoginDto user)
    {
        var cripto = new UserPasswordCripto();

        var userDB = await _userRepository.GetAllUsersAsync();

        var dataUser = userDB.FirstOrDefault(u => u.Email == user.Email);

        var passwordCripto = cripto.CompareHash(user.Password, dataUser?.Password);

        if (dataUser?.Email != user.Email || !passwordCripto)
            return String.Empty;

        var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"] ?? string.Empty));
        var issuer = _configuration["Jwt:issuer"];
        var audience = _configuration["Jwt:Audience"];
        var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

        var tokenOptions = new JwtSecurityToken(
            issuer: issuer,
            audience: audience,
            claims: new[]
            {
                new Claim(type: ClaimTypes.Email, value: dataUser.Email),
                new Claim(type: ClaimTypes.Role, value: dataUser.Roles),
            },
            expires: DateTime.Now.AddHours(2),
            signingCredentials: signinCredentials
        );

        var token = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

        return token;
    }
}
