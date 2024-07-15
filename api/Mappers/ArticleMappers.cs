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
                ArtilceImageName = article.ArtilceImageName,
                UserId = article.UserId
                
            };
        }
         public static Article ToCreateArticleDto(this CreateArticleRequestDto  article, int userId, string Image )
        {
            return new Article
            {
                Title = article.Title,
                Content = article.Content,
                TimeRead = article.TimeRead,
                ArtilceImageName = Image,
                UserId = userId
            };
        }
    }
}