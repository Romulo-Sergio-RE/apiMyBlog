using api.Dtos.Article;
using api.Helpers;
using api.Interface;
using api.Mappers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace api.Controller
{
    //[Authorize(Roles = "admin")]
    [Route("api/article")]
    [ApiController]
    public class ArticleController : ControllerBase
    {
        private readonly IArticleRepository _articleRepository;
        private readonly IUserRepository _userRepository;
        public ArticleController(IArticleRepository articleRepository, IUserRepository userRepository)
        {
            _articleRepository = articleRepository;
            _userRepository = userRepository;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllArticle([FromQuery] QueryArticles queryArticles)
        {
            var articles = await _articleRepository.GetAllArticlesAsync(queryArticles);
            return Ok(articles);
        }
        [HttpGet("{id}")]
        // erro No route matches the supplied values.
        // [HttpGet("{id:int}")] nao esta aceitando
        public async Task<IActionResult> GetByIdArticle([FromRoute] int id)
        {
            var articles = await _articleRepository.GetByIdArticlesAsync(id);
            if (articles == null)
            {
                return NotFound();
            }
            return Ok(articles.ToArticleDto());
        }
        [HttpPost("{userId:int}")]
        public async Task<IActionResult> CreateArticle([FromRoute] int userId, CreateArticleRequestDto createArticleDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            if (await _userRepository.UserIsAdmin(userId) == false)
            {
                return BadRequest("usuario nao existe");
            }
            var articleModel = createArticleDto.ToArticleAllDto(userId);
            await _articleRepository.CreateArticlesAsync(articleModel);

            return CreatedAtAction(nameof(GetByIdArticle), new { id = articleModel }, articleModel.ToArticleDto());
        }
        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> UpdateArticle([FromRoute] int id, [FromBody] UpdateArticleRequestDto updateArticle)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var article = await _articleRepository.UpdateArticlesAsync(id, updateArticle);
            if (article == null)
            {
                return NotFound();
            }
            return Ok(article.ToArticleDto());
        }
        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> DeleteArticle([FromRoute] int id)
        {
            var deleteArticle = await _articleRepository.DeleteArticlesAsync(id);
            if (deleteArticle == null)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}