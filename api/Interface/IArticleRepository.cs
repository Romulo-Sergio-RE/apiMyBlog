using api.Dtos.User;
using api.Models;

namespace api.Interface
{
    public interface IArticleRepository
    {
        Task<List<Article>> GetAllArticlesAsync();
        Task<Article?> GetByIdArticlesAsync(int id);
        Task<Article?> CreateArticlesAsync(Article article);
    }
}