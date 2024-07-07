using api.Context;
using api.Dtos.Article;
using api.Interface;
using api.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace api.Controller
{
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
        public async Task<IActionResult> GetAllArticle()
        {
            var articles = await  _articleRepository.GetAllArticlesAsync();
            return Ok(articles);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdArticle([FromRoute] int id)
        {
            var articles = await  _articleRepository.GetByIdArticlesAsync(id);
            if(articles == null)
            {
                return NotFound();
            }
            return Ok(articles.ToArticleDto());
        }
        [HttpPost("{userId}")]
        public async Task<IActionResult> CreateArticle([FromRoute] int userId, CreateArticleRequestDto createArticleDto)
        {
            
            if(await _userRepository.UserIsAdmin(userId) == false)
            {
               return BadRequest("usuario nao existe");
            }

            
            var articleModel = createArticleDto.ToArticleAllDto(userId);
            await _articleRepository.CreateArticlesAsync(articleModel);

            return CreatedAtAction(nameof(GetByIdArticle),new {id = articleModel}, articleModel.ToArticleDto());
        }
    }
}