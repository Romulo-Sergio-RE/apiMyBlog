namespace api.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Genre { get; set; } = string.Empty;
        public bool IsAdmin { get; set; } 
        public List<Article> Articles { get; set; } = new List<Article>();
        public List<Comment> Comments { get; set;} = new List<Comment>();
    }
}