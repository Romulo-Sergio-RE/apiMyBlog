namespace api.Dtos.User;

public class CreateUserRequestDto
{
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string Genre { get; set; } = string.Empty;
    public string Roles { get; set; } = string.Empty;
}
