using api.Models;
using api.Helpers;
using api.Dtos.Article;

namespace api.Repository.Interface;

public interface IArticleRepository
{
    Task<List<Article>> GetAllArticlesAsync(QueryArticles queryArticles);

    Task<Article?> GetByIdArticlesAsync(int id);

    Task<Article?> CreateArticlesAsync(Article article);

    Task<Article?> DeleteArticlesAsync(int id);

    Task<Article?> UpdateArticlesAsync(int id, UpdateArticleRequestDto updateArticleDto, string ImagePath);

    Task<bool> ArticleExist(int id);
    
}
