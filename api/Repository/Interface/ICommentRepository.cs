using api.Models;
using api.Dtos.Comment;

namespace api.Repository.Interface;

public interface ICommentRepository
{
    Task<List<Comment>> GetCommentsAsync();

    Task<Comment?> GetCommentByIdAsync(int id);

    Task<Comment?> CreateCommentAsync(Comment comment);

    Task<Comment?> UpdateCommentAsync(string userName, int commentId, UpdateCommentRequestDto updateComment);

    Task<Comment?> DeleteCommentAsync(int id);

}
