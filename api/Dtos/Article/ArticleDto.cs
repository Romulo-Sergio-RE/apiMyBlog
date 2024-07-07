namespace api.Dtos.Article
{
    public class ArticleDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public string TimeRead { get; set; } = string.Empty;
        public int? UserId { get; set; }
    }
}