using api.Context;
using api.Dtos.User;
using api.Interface;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class ArticleRepository : IArticleRepository
    {
        private readonly ApplicationDbContext _context;

        public ArticleRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Article> CreateArticlesAsync(Article article)
        {
            await _context.Articles.AddAsync(article);
            await _context.SaveChangesAsync();
            return article;
        }

        public async Task<List<Article>> GetAllArticlesAsync()
        {
            var article =  await _context.Articles.ToListAsync();
            return article;
        }

        public async Task<Article?> GetByIdArticlesAsync(int id)
        {
            var articleById = await _context.Articles.FirstOrDefaultAsync(a => a.Id == id);
            if(articleById == null)
            {
                return null;
            }
            return articleById;
        }

    }
}