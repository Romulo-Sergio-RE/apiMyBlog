using api.Dtos.Article;
using api.Models;

namespace api.Interface;

public interface IArticleRepository
{
    Task<List<Article>> GetAllArticlesAsync();

    Task<Article?> GetByIdArticlesAsync(int id);

    Task<Article?> CreateArticlesAsync(Article article);

    Task<Article?> DeleteArticlesAsync(int id);

    Task<Article?> UpdateArticlesAsync(int id, UpdateArticleRequestDto updateArticleDto);

    Task<bool> ArticleExist(int id);
    
}
