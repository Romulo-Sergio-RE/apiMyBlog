using api.Context;
using api.Dtos.Account;
using api.Dtos.Login;
using api.Interface;
using api.Mappers;
using api.Models;
using api.Repository.Interface;
using api.Services.Interfaces;
using api.utils;
using Microsoft.EntityFrameworkCore;

namespace api.Repository;

public class AccountRepository : IAccountRepository
{
    private readonly ApplicationDbContext _context;

    private readonly ITokenService _tokenService;

    private readonly IUserRepository _userRepository;

    public AccountRepository(ApplicationDbContext context, ITokenService tokenService, IUserRepository userRepository )
    {
        _context = context;
        _tokenService = tokenService;
        _userRepository = userRepository;
    }

    public async Task<UserAccountDto?> LoginUser(LoginDto loginUser)
    {
        var cripto = new UserPasswordCriptoService();
        var checkUser = await _context.Users.FirstOrDefaultAsync(u => u.Email == loginUser.Email);
        if (checkUser == null)
        {
            return null;
        }
        
        var passwordCripto = cripto.CompareHash(loginUser.Password, checkUser.Password);

        if (loginUser.Email != checkUser.Email)
        {
            return null;
        }
        else if(!passwordCripto)
        {
            return null;
        }
        
        var getUser = new User{
            Email = checkUser.Email,
            Roles = checkUser.Roles,
        };

        var token = _tokenService.GenerateToken(getUser);

        return new UserAccountDto
        {
            Id = checkUser.Id,
            Name = checkUser.Name,
            Email = checkUser.Email,
            Genre = checkUser.Genre,
            Roles = checkUser.Roles,
            Token = token,
        };
    }

    public async Task<User?> RegisterUser(RegisterDto user)
    {
        var checkUser = await _context.Users.FirstOrDefaultAsync(u => u.Email == user.Email);
        if (checkUser != null)
        {
            return null;
        }
        var cripto = new UserPasswordCriptoService();
        var passwordCripto = cripto.ReturnMD5(user.Password);
        var registerUser = user.ToRegisterUserDto(passwordCripto);

        await _context.Users.AddAsync(registerUser);
        await _context.SaveChangesAsync();
        return registerUser;
    }
}
