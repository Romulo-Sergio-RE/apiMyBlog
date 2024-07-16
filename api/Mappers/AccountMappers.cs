using api.Models;
using api.Dtos.Account;

namespace api.Mappers;

public static class AccountMappers
{
    public static User ToRegisterUserDto(this RegisterDto user, string passwordCripto)
    {
        return new User
        {
            Name = user.Name,
            Email = user.Email,
            Password = passwordCripto,
            Genre = user.Genre,
            Roles = user.Roles,
        };
    }
}