namespace CesiZenBackEnd.Core.DTO;

public class UpdateUserDto
{
    public string Email { get; set; } = null!;
    public string Nom { get; set; } = null!;
    public string Prenom { get; set; } = null!;

    // Gestion du changement de mot de passe
    public string? CurrentPassword { get; set; }
    public string? NewPassword { get; set; }
    public string? ConfirmPassword { get; set; }
}