using api.Mappers;
using api.Dtos.Comment;
using Microsoft.AspNetCore.Mvc;
using api.Repository.Interface;
using Microsoft.AspNetCore.Authorization;

namespace api.Controller;

[Route("api/[controller]")]
[ApiController]
public class CommentController : ControllerBase
{
    private readonly ICommentRepository _commentRepository;
    private readonly IArticleRepository _articleRepository;
    private readonly IUserRepository _userRepository;
    public CommentController(ICommentRepository commentRepository, IArticleRepository articleRepository, IUserRepository userRepository)
    {
        _commentRepository = commentRepository;
        _articleRepository = articleRepository;
        _userRepository = userRepository;
    }

    [HttpGet]
    [Authorize(Roles = "admin")]
    public async Task<IActionResult> GetAllComment()
    {
        var allComment = await _commentRepository.GetCommentsAsync();
        return Ok(allComment);
    }

    [HttpGet("{id}")]
    //[Authorize(Roles = "admin")]

    // erro No route matches the supplied values.
    // [HttpGet("{id:int}")] nao esta aceitando
    public async Task<IActionResult> GetCommentById([FromRoute] int id)
    {
        var comment = await _commentRepository.GetCommentByIdAsync(id);
        if (comment == null)
        {
            return NotFound();
        }
        return Ok(comment.ToCommentDto());
    }

    [HttpPost("{userId:int}/{articleId:int}")]
    //[Authorize(Roles = "admin,usuario")]
    public async Task<IActionResult> CreateComment([FromRoute] int userId, int articleId, CreateCommentResquestDto createComment)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }
        if (!await _articleRepository.ArticleExist(articleId))
        {
            return BadRequest("Article nao encontrado");
        }
        else if (!await _userRepository.UserExist(userId))
        {
            return BadRequest("usurario nao encontrado");
        }
        var userName = await _userRepository.GetUserByIdAsync(userId);
        var commentModel = createComment.ToCreateCommentDto(userId, articleId, userName.Name);
        await _commentRepository.CreateCommentAsync(commentModel);
        return CreatedAtAction(nameof(GetCommentById), new { id = commentModel }, commentModel.ToCommentDto());
    }

    [HttpPut("{userId}/{articleId}/{commentId}")]
    //[Authorize(Roles = "admin,usuario")]
    public async Task<IActionResult> UpdateComment([FromRoute] int userId, int articleId, int commentId, UpdateCommentRequestDto updateComment)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }
        if (!await _articleRepository.ArticleExist(articleId))
        {
            return BadRequest("Article nao encontrado");
        }
        else if (!await _userRepository.UserExist(userId))
        {
            return BadRequest("usurario nao encontrado");
        }
        var userName = await _userRepository.GetUserByIdAsync(userId);
        var comment = await _commentRepository.UpdateCommentAsync(userName.Name, commentId, updateComment);
        if (comment == null)
        {
            return NotFound();
        }
        return Ok(comment.ToCommentDto());
    }

    [HttpDelete("{id:int}")]
    //[Route("{id:int}")]
    //[Authorize(Roles = "admin,usuario")]
    public async Task<IActionResult> DeleteComment([FromRoute] int id)
    {
        var comment = await _commentRepository.DeleteCommentAsync(id);
        if (comment == null)
        {
            return NotFound();
        }
        return NoContent();
    }
}
