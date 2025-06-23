using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CesiZenBackEnd.Core.Entities;

[Table("evenementstress")]
public class EvenementStress : Entity<int>
{
    public IEnumerable<Posseder>? idDiagnostic;
    

    [Column("libel_evenement")]
    [Required]
    [MaxLength(255)]
    public string LibelEvenement { get; set; }

    [Column("score")]
    [Required]
    public int Score { get; set; }

    // Navigation : un événement peut appartenir à plusieurs diagnostics
    public ICollection<Posseder> Possessions { get; set; } = new List<Posseder>();
}
