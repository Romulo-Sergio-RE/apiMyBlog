using api.Models;
using api.Dtos.Comment;

namespace api.Mappers;

public static class CommentMappers
{
    public static CommentDto ToCommentDto(this Comment comment)
    {
        return new CommentDto
        {
            Id = comment.Id,
            Name = comment.Name,
            Content = comment.Content,
            UserId = comment.UserId,
            ArticleId = comment.ArticleId,
        };
    }
    public static Comment ToCreateCommentDto(this CreateCommentResquestDto comment, int userId, int articleId, string userName)
    {
        return new Comment
        {
            Name = userName,
            Content = comment.Content,
            UserId = userId,
            ArticleId = articleId,
        };
    }
}
