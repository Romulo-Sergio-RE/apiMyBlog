using api.Dtos.Image;
using System.ComponentModel.DataAnnotations;

namespace api.Dtos.Article;

public class CreateArticleRequestDto
{
    [Required]
    [StringLength(20, ErrorMessage = "O nome deve ter no minimo 3 letrar e no maximo 20 letras.", MinimumLength = 3)]
    public string Title { get; set; } = string.Empty;

    [Required]
    [StringLength(280, ErrorMessage = "O texto deve ter no minimo 5 letrar e no maximo 280 letras.", MinimumLength = 5)]
    public string Content { get; set; } = string.Empty;

    [Required]
    [StringLength(3, ErrorMessage = "O TimeRead deve ter no minimo 1 numero  e no maximo 3 numeros.", MinimumLength = 1)]
    public string TimeRead { get; set; } = string.Empty;

    public ImageDto ArtilceImageName { get; set; }
}
