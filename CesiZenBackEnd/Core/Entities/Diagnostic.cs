using System;
using CesiZenBackEnd.Core.Entities;    
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace CesiZenBackEnd.Core.Entities;

[Table("Diagnostic")]
public partial class Diagnostic : Entity<int>
{
    [Column("date_diagnostic")]
    public DateTime DateDiagnostic { get; set; }
    
    [Column("score_total")]
    public int ScoreTotal { get; set; }
    
    [Column("id_utilisateur")]
    public int IdUtilisateur { get; set; }
    public Utilisateur Utilisateur { get; set; }  
    
    public ICollection<Posseder> Possessions { get; set; } = new List<Posseder>();
}