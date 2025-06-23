using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;


namespace CesiZenBackEnd.Core.Entities;

[Table("role")]
public partial class Role : Entity<int>
{
    
    [Column("libel_role")]
    public string libelRole { get; set; }
    
    // Collection de navigation : un rôle peut avoir plusieurs utilisateurs
    public ICollection<Utilisateur> Utilisateurs { get; set; } = new List<Utilisateur>();
}
