using api.Models;
using api.Context;
using api.Mappers;
using api.Dtos.Account;
using api.Services.Interfaces;
using api.Repository.Interface;
using Microsoft.EntityFrameworkCore;


namespace api.Repository;

public class AccountRepository : IAccountRepository
{
    private readonly ApplicationDbContext _context;

    private readonly ITokenService _tokenService;

    private readonly IUserPasswordCriptoService _userPasswordCripto;

     private readonly IUploadImageService _uploadImage;

    public AccountRepository(ApplicationDbContext context, ITokenService tokenService, IUserPasswordCriptoService userPasswordCripto, IUploadImageService uploadImage)
    {
        _context = context;
        _tokenService = tokenService;
        _userPasswordCripto = userPasswordCripto;
        _uploadImage = uploadImage;
    }

    public async Task<UserAccountDto?> LoginUser(LoginDto loginUser)
    {
        var checkUser = await _context.Users.FirstOrDefaultAsync(u => u.Email == loginUser.Email);
        if (checkUser == null)
        {
            return null;
        }

        var passwordCripto = _userPasswordCripto.CompareHash(loginUser.Password, checkUser.Password);

        if (loginUser.Email != checkUser.Email)
        {
            return null;
        }
        else if (!passwordCripto)
        {
            return null;
        }

        var getUser = new User
        {
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

        var passwordCripto = _userPasswordCripto.ReturnMD5(user.Password);

        if (user?.UserImageName?.fileName?.Length > 0)
        {
            var upload = await _uploadImage.UploadImage(user.UserImageName, "articles");
            if (upload == "Failed.")
            {
                return null;
            }
            var userCreate = user.ToRegisterUserImageDto(passwordCripto, upload);
            await _context.Users.AddAsync(userCreate);
            await _context.SaveChangesAsync();
            return userCreate;
        }

        var registerUser = user.ToRegisterUserDto(passwordCripto);
        await _context.Users.AddAsync(registerUser);
        await _context.SaveChangesAsync();
        return registerUser;
    }
}
