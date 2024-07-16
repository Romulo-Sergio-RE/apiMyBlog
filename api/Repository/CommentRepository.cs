using api.Models;
using api.Context;
using api.Dtos.Comment;
using api.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace api.Repository;

public class CommentRepository : ICommentRepository
{
    private readonly ApplicationDbContext _context;
    public CommentRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<Comment>> GetCommentsAsync()
    {
        return await _context.Comments.ToListAsync();
    }
    public async Task<Comment?> GetCommentByIdAsync(int id)
    {
        var comment = await _context.Comments.FirstOrDefaultAsync(c => c.Id == id);
        if (comment == null)
        {
            return null;
        }
        return comment;
    }

    public async Task<Comment?> CreateCommentAsync(Comment comment)
    {
        await _context.Comments.AddAsync(comment);
        await _context.SaveChangesAsync();
        return comment;
    }

    public async Task<Comment?> UpdateCommentAsync(string userName, int commentId, UpdateCommentRequestDto updateComment)
    {
        var comment = await _context.Comments.FirstOrDefaultAsync(c => c.Id == commentId);
        if (comment == null)
        {
            return null;
        }
        comment.Name = userName;
        comment.Content = updateComment.Content;
        await _context.SaveChangesAsync();
        return comment;
    }

    public async Task<Comment?> DeleteCommentAsync(int id)
    {
        var comment = await _context.Comments.FirstOrDefaultAsync(c => c.Id == id);
        if (comment == null)
        {
            return null;
        }
        _context.Comments.Remove(comment);
        await _context.SaveChangesAsync();
        return comment;
    }
}
