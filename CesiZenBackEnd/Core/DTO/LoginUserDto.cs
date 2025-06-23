namespace CesiZenBackEnd.Core.DTO;

public class LoginUserDto
{
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;

}


public class LoginResultDto
{
    public int Id { get; set; }
    public string Email { get; set; } = null!;
    public string? Prenom { get; set; } = null!;
    public string? Nom { get; set; } = null!;
    public int? RoleId { get; set; }
    public string? LibelRole { get; set; }

    public string? Token { get; set; }
    public bool Success { get; set; }
    public string? ErrorCode { get; set; }
}