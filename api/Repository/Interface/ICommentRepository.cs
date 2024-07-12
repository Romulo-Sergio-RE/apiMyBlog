using api.Dtos.Comment;
using api.Models;

namespace api.Interface;

public interface ICommentRepository
{
    Task<List<Comment>> GetCommentsAsync();

    Task<Comment?> GetCommentByIdAsync(int id);

    Task<Comment?> CreateCommentAsync(Comment comment);

    Task<Comment?> UpdateCommentAsync(string userName, int commentId, UpdateCommentRequestDto updateComment);

    Task<Comment?> DeleteCommentAsync(int id);

}
