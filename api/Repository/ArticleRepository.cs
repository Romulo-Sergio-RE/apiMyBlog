using api.Context;
using api.Dtos.Article;
using api.Interface;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repository;

public class ArticleRepository : IArticleRepository
{
    private readonly ApplicationDbContext _context;

    public ArticleRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<bool> ArticleExist(int id)
    {
        var userAdmin = await GetByIdArticlesAsync(id);

        if (await _context.Articles.AnyAsync(u => u.Id == id))
        {
            return true;
        }
        return false;
    }

    public async Task<Article?> CreateArticlesAsync(Article article)
    {
        await _context.Articles.AddAsync(article);
        await _context.SaveChangesAsync();
        return article;
    }

    public async Task<Article?> DeleteArticlesAsync(int id)
    {
        var articleById = await _context.Articles.FirstOrDefaultAsync(a => a.Id == id);
        if (articleById == null)
        {
            return null;
        }
        _context.Articles.Remove(articleById);
        await _context.SaveChangesAsync();
        return articleById;

    }

    public async Task<List<Article>> GetAllArticlesAsync()
    {
        var article = await _context.Articles.Include(c => c.Comments).ToListAsync();
        return article;
    }

    public async Task<Article?> GetByIdArticlesAsync(int id)
    {
        var articleById = await _context.Articles
            .Include(c => c.Comments)
            .FirstOrDefaultAsync(a => a.Id == id);
        if (articleById == null)
        {
            return null;
        }
        return articleById;
    }

    public async Task<Article?> UpdateArticlesAsync(int id, UpdateArticleRequestDto updateArticleDto)
    {
        var article = await _context.Articles.FirstOrDefaultAsync(a => a.Id == id);
        if (article == null)
        {
            return null;
        }
        article.Title = updateArticleDto.Title;
        article.Content = updateArticleDto.Content;
        article.TimeRead = updateArticleDto.TimeRead;
        await _context.SaveChangesAsync();
        return article;
    }
}
