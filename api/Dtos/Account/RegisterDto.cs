using System.ComponentModel.DataAnnotations;
using api.Dtos.Image;

namespace api.Dtos.Account;

public class RegisterDto
{
    [Required]
    [StringLength(20,ErrorMessage = "O nome deve ter no minimo 3 letrar e no maximo 20 letras.", MinimumLength = 3)]
    public string Name { get; set; } = string.Empty;

    [Required]
    [StringLength(100,ErrorMessage = "O email deve ter no minimo 13 letrar e no maximo 100 letras.", MinimumLength = 13)]
    public string Email { get; set; } = string.Empty;

    [Required]
    [StringLength(20,ErrorMessage = "O senha deve ter no minimo 12 letrar e no maximo 20 letras.", MinimumLength = 12)]
    public string Password { get; set; } = string.Empty;

    [Required]
    [AllowedValues("masculino", "feminino", "outros", ErrorMessage = "os valores do compo so podem ser (masculino), (feminino), (outros).")]
    public string Genre { get; set; } = string.Empty;

    public ImageDto? UserImageName { get; set; }
}
