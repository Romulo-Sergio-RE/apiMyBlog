using api.Dtos.User;
using api.Helpers;
using api.Models;

namespace api.Mappers
{
    public static class UserMappers
    {
        public static UserDto ToUserDto(this User user)
        {
            return new UserDto
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                Password = user.Password,
                //UserImageName = user.UserImageName,
                Genre = user.Genre,
                Roles = user.Roles,
                Articles = user.Articles.Select(a => a.ToArticleDto()).ToList(),
            };
        }
        public static User ToCreateUserDto(this CreateUserRequestDto user, string passwordCripto)
        {
            return new User
            {
                Name = user.Name,
                Email = user.Email,
                Password = passwordCripto,
                //UserImageName = user.UserImageName,
                Genre = user.Genre,
                Roles = user.Roles,
            };
        }
    }
}