namespace CesiZenBackEnd.Core.DTO;

public class RegisterUserDto
{
    public string Email { get; set; } = null!;
    public string? Password { get; set; }
    
    public string? ConfirmPassword { get; set; }
    public string? Nom { get; set; }
    public string? Prenom { get; set; }
    
    public int? RoleId { get; set; }
}