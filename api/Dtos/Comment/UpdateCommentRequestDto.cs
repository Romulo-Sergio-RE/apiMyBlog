using System.ComponentModel.DataAnnotations;

namespace api.Dtos.Comment;

public class UpdateCommentRequestDto
{
    [Required]
    [StringLength(280,ErrorMessage = "O texto deve ter no minimo 5 letrar e no maximo 280 letras", MinimumLength = 5)]
    public string Content { get; set; } = string.Empty;
}
