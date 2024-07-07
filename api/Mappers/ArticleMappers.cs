using api.Dtos.Article;
using api.Models;

namespace api.Mappers
{
    public static class ArticleMappers
    {
        public static ArticleDto ToArticleDto(this Article  article)
        {
            return new ArticleDto
            {
                Id = article.Id,
                Title = article.Title,
                Content = article.Content,
                TimeRead = article.TimeRead,
                UserId = article.UserId
                
            };
        }
         public static Article ToArticleAllDto(this CreateArticleRequestDto  article, int userId)
        {
            return new Article
            {
                Title = article.Title,
                Content = article.Content,
                TimeRead = article.TimeRead,
                UserId = userId
            };
        }
    }
}