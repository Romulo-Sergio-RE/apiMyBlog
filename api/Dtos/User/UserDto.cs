using api.Dtos.Article;

namespace api.Dtos.User
{
    public class UserDto
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;

       // public string UserImageName { get; set; } = string.Empty;

        public string Genre { get; set; } = string.Empty;

        public string Roles { get; set; } = string.Empty;
        
        public List<ArticleDto> Articles { get; set; }
        //public List<Comment> Comments { get; set;} 
    }
}