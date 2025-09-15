using System;
using CesiZenBackEnd.Core.Entities;    
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("utilisateur")]
public partial class Utilisateur : Entity<int>
{
    [Column("prenom")]
    public string? Prenom { get; set; } = string.Empty; // anciennement Prenom
    
    [Column("nom")]
    public string? Nom { get; set; } = string.Empty;  // anciennement Nom
    
    
    [Required(ErrorMessage = "Email est obligatoire")]
    [EmailAddress]
    [Column("email")]
    [MaxLength(180)]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "MDP est obligatoire")]
    [DataType(DataType.Password)]
    [Column("mot_de_passe", TypeName = "text")]
    public string PasswordHash { get; set; } = string.Empty; // anciennement MotDePasse
    
    [Column("est_actif_util")]
    public bool Active { get; set; } = true;

    [Column("date_creation")]
    public DateTime DateCreation { get; set; } = DateTime.UtcNow;

    [Column("id_role")]
    public int RoleId { get; set; } // anciennement IdRole
    
    
    public Role Role { get; set; }  // navigation property
    public IEnumerable<Diagnostic>? Diagnostics { get; set; }
    public string? Adresse { get; set; }
}
