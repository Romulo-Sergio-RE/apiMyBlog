namespace api.Dtos.Comment;

public class CommentDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public int? UserId { get; set; }
    public int? ArticleId { get; set; }
}
