using api.Models;
using api.Dtos.Account;

namespace api.Mappers;

public static class AccountMappers
{
    public static User ToRegisterUserImageDto(this RegisterDto user, string passwordCripto, string imageName)
    {
        return new User
        {
            Name = user.Name,
            Email = user.Email,
            Password = passwordCripto,
            UserImageName = imageName,
            Genre = user.Genre,
            Roles = "usuario",
        };
    }
     public static User ToRegisterUserDto(this RegisterDto user, string passwordCripto)
    {
        return new User
        {
            Name = user.Name,
            Email = user.Email,
            Password = passwordCripto,
            UserImageName = "",
            Genre = user.Genre,
            Roles = "usuario",
        };
    }
}