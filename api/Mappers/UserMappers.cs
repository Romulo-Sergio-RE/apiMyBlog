using api.Dtos.User;
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
                Genre = user.Genre,
                IsAdmin = user.IsAdmin,
                Articles = user.Articles.Select(a => a.ToArticleDto()).ToList(),
            };
        }
        public static User ToUserAllDto(this CreateUserRequestDto user)
        {
            return new User
            {
                Name = user.Name,
                Email = user.Email,
                Password = user.Password,
                Genre = user.Genre,
                IsAdmin = user.IsAdmin,
            };
        }
    }
}