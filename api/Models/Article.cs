namespace api.Models
{
    public class Article
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public string TimeRead { get; set; } = string.Empty;
        public string Genre { get; set; } = string.Empty;
        public int? UserIdAdmin { get; set; }
        public User? UserAdimin { get; set; }
        public List<Comment> Comments { get; set;} = new List<Comment>();
    }
}