using System.ComponentModel.DataAnnotations;

namespace CesiZenBackEnd.Core.DTO;

public class RegisterUserDto
{
    [Required]
    [EmailAddress(ErrorMessage = "Email invalide.")]
    [MaxLength(256)]
    public string Email { get; set; } = null!;

    [Required]
    [MinLength(6, ErrorMessage = "Le mot de passe doit faire au moins 6 caractères.")]
    public string Password { get; set; } = null!;

    [Required]
    [Compare(nameof(Password), ErrorMessage = "La confirmation ne correspond pas.")]
    public string ConfirmPassword { get; set; } = null!;

    [Required, MaxLength(100)]
    public string Nom { get; set; } = null!;

    [Required, MaxLength(100)]
    public string Prenom { get; set; } = null!;

    [Required]
    public int? RoleId { get; set; }
}