namespace api.Dtos.Account;

public class UserAccountDto
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public string Genre { get; set; } = string.Empty;

    public string Roles { get; set; } = string.Empty;

    public string Token { get; set; } = string.Empty;
}
